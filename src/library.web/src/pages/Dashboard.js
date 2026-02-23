import React from "react";
import { useNavigate } from "react-router-dom";

const Dashboard = () => {
  const navigate = useNavigate();

  const handleLogout = () => {
    localStorage.removeItem("adminToken");
    navigate("/login");
  };

  const cards = [
    { name: "Books", path: "/books" },
    { name: "Members", path: "/members" },
    { name: "Loans", path: "/loans" },
    { name: "Register", path: "/register" },
  ];

  return (
    <div style={{ padding: "20px" }}>
      
      {/* Top Bar */}
      <div style={{ display: "flex", justifyContent: "space-between", marginBottom: "30px" }}>
        <h2>Library Dashboard</h2>
        <button onClick={handleLogout} style={{ background: "red", color: "white" }}>
          Logout
        </button>
      </div>

      {/* Cards */}
      <div style={{ display: "flex", gap: "20px", flexWrap: "wrap" }}>
        {cards.map((card) => (
          <div
            key={card.name}
            onClick={() => navigate(card.path)}
            style={{
              cursor: "pointer",
              border: "1px solid #ccc",
              padding: "30px",
              borderRadius: "8px",
              width: "150px",
              textAlign: "center",
              boxShadow: "2px 2px 5px rgba(0,0,0,0.1)",
            }}
          >
            <h3>{card.name}</h3>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Dashboard;