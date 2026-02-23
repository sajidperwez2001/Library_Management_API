import React, { useState, useEffect } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";

const MembersPage = () => {
  const [members, setMembers] = useState([]);
  const [form, setForm] = useState({
    fullName: "",
    email: "",
    phone: "",
    address: "",
    isActive: true
  });
  const [editId, setEditId] = useState(null);
  const [message, setMessage] = useState("");
  const navigate = useNavigate();
  const API_URL = "https://localhost:50200/api/members"; // update with your Members API
  const token = localStorage.getItem("adminToken"); // JWT token

  const config = {
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json"
    }
  };

  // Fetch all members
  const fetchMembers = async () => {
    try {
      const res = await axios.get(API_URL, config);
      setMembers(res.data);
    } catch (err) {
      console.error(err);
      setMessage("Error fetching members");
    }
  };

  useEffect(() => {
    fetchMembers();
  }, []);

  // Handle input change
  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setForm({ ...form, [name]: type === "checkbox" ? checked : value });
  };

  // Add / Update member
  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (editId) {
        await axios.put(`${API_URL}/${editId}`, form, config);
        setMessage("Member updated successfully");
      } else {
        await axios.post(API_URL, form, config);
        setMessage("Member added successfully");
      }
      setForm({ fullName: "", email: "", phone: "", address: "", isActive: true });
      setEditId(null);
      fetchMembers();
    } catch (err) {
      console.error(err);
      setMessage("Error adding/updating member");
    }
  };

  // Delete member
  const handleDelete = async (id) => {
    if (!window.confirm("Are you sure you want to delete this member?")) return;

    try {
      await axios.delete(`${API_URL}/${id}`, config);
      setMessage("Member deleted successfully");
      fetchMembers();
    } catch (err) {
      console.error(err);
      setMessage("Error deleting member");
    }
  };

  // Edit member
  const handleEdit = (member) => {
    setForm({
      fullName: member.fullName,
      email: member.email || "",
      phone: member.phone || "",
      address: member.address || "",
      isActive: member.isActive
    });
    setEditId(member.id);
  };

  return (
    <div>
      <h2>Members</h2>
      {message && <p>{message}</p>}

      {/* Member Form */}
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          name="fullName"
          placeholder="Full Name"
          value={form.fullName}
          onChange={handleChange}
          required
        />
        <input
          type="email"
          name="email"
          placeholder="Email"
          value={form.email}
          onChange={handleChange}
        />
        <input
          type="text"
          name="phone"
          placeholder="Phone"
          value={form.phone}
          onChange={handleChange}
        />
        <input
          type="text"
          name="address"
          placeholder="Address"
          value={form.address}
          onChange={handleChange}
        />
        <label>
          Active:
          <input
            type="checkbox"
            name="isActive"
            checked={form.isActive}
            onChange={handleChange}
          />
        </label>
        <button type="submit">{editId ? "Update Member" : "Add Member"}</button>
        {editId && (
          <button
            type="button"
            onClick={() => {
              setEditId(null);
              setForm({ fullName: "", email: "", phone: "", address: "", isActive: true });
            }}
          >
            Cancel
          </button>
        )}
		<button onClick={() => navigate("/dashboard")} style={{ marginBottom: "20px" }}>
		  Back to Dashboard
		</button>
      </form>

      {/* Members Table */}
      <table border="1" cellPadding="5" style={{ marginTop: "20px" }}>
        <thead>
          <tr>
            <th>ID</th>
            <th>Full Name</th>
            <th>Email</th>
            <th>Phone</th>
            <th>Address</th>
            <th>Membership Date</th>
            <th>Active</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {members.map((member) => (
            <tr key={member.id}>
              <td>{member.id}</td>
              <td>{member.fullName}</td>
              <td>{member.email}</td>
              <td>{member.phone}</td>
              <td>{member.address}</td>
              <td>{new Date(member.membershipDate).toLocaleDateString()}</td>
              <td>{member.isActive ? "Yes" : "No"}</td>
              <td>
                <button onClick={() => handleEdit(member)}>Edit</button>
                <button onClick={() => handleDelete(member.id)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default MembersPage;