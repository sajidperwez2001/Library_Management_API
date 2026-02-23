import React, { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";

  const Register = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [role, setRole] = useState("User");
  const [message, setMessage] = useState("");
  const navigate = useNavigate();
  
  // Replace with your API URL
  const API_URL = "https://localhost:50200/api/auth/register";

  // Replace this with the Admin JWT token you already have
  const adminToken = localStorage.getItem("token");

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!adminToken) {
      setMessage("Admin token missing. Login first!");
      return;
    }

    try {
      const response = await axios.post(
        API_URL,
        { username, password, role },
        {
          headers: {
            Authorization: `Bearer ${adminToken}`,
            "Content-Type": "application/json",
          },
        }
      );

      //setMessage(`User created: ${response.data.username} (ID: ${response.data.id})`);
	  setMessage(`User created successfully.`);
      setUsername("");
      setPassword("");
      setRole("User");
    } catch (error) {
      if (error.response) {
        setMessage(`Error: ${error.response.status} - ${error.response.data?.error}`);
      } else {
        setMessage("Error: Could not connect to server");
      }
    }
  };

  return (
    <div>
      <h2>Register New User</h2>
      {message && <p>{message}</p>}
      <form onSubmit={handleSubmit}>
        <div>
          <label>Username:</label>
          <input
            type="text"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            required
          />
        </div>

        <div>
          <label>Password:</label>
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>

        <div>
          <label>Role:</label>
          <select value={role} onChange={(e) => setRole(e.target.value)}>
            <option value="User">User</option>
            <option value="Admin">Admin</option>
          </select>
        </div>

        <button type="submit">Register</button>
		<button onClick={() => navigate("/dashboard")} style={{ marginBottom: "20px" }}>
		  Back to Dashboard
		</button>
      </form>
    </div>
  );
};

export default Register;