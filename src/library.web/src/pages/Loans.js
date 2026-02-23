import React, { useState, useEffect } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";

const LoansPage = () => {
  const [loans, setLoans] = useState([]);
  const [books, setBooks] = useState([]);
  const [members, setMembers] = useState([]);
  const [form, setForm] = useState({ bookId: "", memberId: "", days: 14 });
  const [message, setMessage] = useState("");
  const navigate = useNavigate();
  const API_URL = "https://localhost:50200/api/loans";
  const BOOKS_API = "https://localhost:50200/api/books";
  const MEMBERS_API = "https://localhost:50200/api/members";
  const token = localStorage.getItem("token"); // use your token
  
  const statusMap = {
  0: "Pending",
  1: "Returned",
  2: "Overdue",
 };

  const config = {
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    },
  };

  // Fetch loans
  const fetchLoans = async () => {
    try {
      const res = await axios.get(API_URL, config);
      setLoans(res.data);
    } catch (err) {
      console.error(err.response?.data || err);
      setMessage("Error fetching loans");
    }
  };

  // Fetch books and members for dropdowns
  const fetchBooksAndMembers = async () => {
    try {
      const [booksRes, membersRes] = await Promise.all([
        axios.get(BOOKS_API, config),
        axios.get(MEMBERS_API, config),
      ]);
      setBooks(booksRes.data);
      setMembers(membersRes.data);
    } catch (err) {
      console.error(err.response?.data || err);
      setMessage("Error fetching books or members");
    }
  };

  useEffect(() => {
    fetchLoans();
    fetchBooksAndMembers();
  }, []);

  // Handle form changes
  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm({ ...form, [name]: value });
  };

  // Borrow a book
  const handleBorrow = async (e) => {
    e.preventDefault();
    try {
      const payload = {
        bookId: parseInt(form.bookId),
        memberId: parseInt(form.memberId),
        days: parseInt(form.days),
      };

      await axios.post(`${API_URL}/borrow`, payload, config);
      setMessage("Loan created successfully");
      setForm({ bookId: "", memberId: "", days: 14 });
      fetchLoans();
    } catch (err) {
      console.error(err.response?.data || err);
      setMessage(
        `Error borrowing loan: ${err.response?.status || ""} ${JSON.stringify(
          err.response?.data
        )}`
      );
    }
  };

  // Return a book
  const handleReturn = async (loanId) => {
    try {
      await axios.post(`${API_URL}/${loanId}/return`, {}, config);
      setMessage("Book returned successfully");
      fetchLoans();
    } catch (err) {
      console.error(err.response?.data || err);
      setMessage(
        `Error returning loan: ${err.response?.status || ""} ${JSON.stringify(
          err.response?.data
        )}`
      );
    }
  };

  return (
    <div>
      <h2>Loans</h2>
      {message && <p>{message}</p>}

      {/* Borrow Form */}
      <form onSubmit={handleBorrow} style={{ marginBottom: "20px" }}>
        <select
          name="bookId"
          value={form.bookId}
          onChange={handleChange}
          required
        >
          <option value="">Select Book</option>
          {books.map((b) => (
            <option key={b.id} value={b.id}>
              {b.title}
            </option>
          ))}
        </select>

        <select
          name="memberId"
          value={form.memberId}
          onChange={handleChange}
          required
        >
          <option value="">Select Member</option>
          {members.map((m) => (
            <option key={m.id} value={m.id}>
              {m.fullName}
            </option>
          ))}
        </select>

        <input
          type="number"
          name="days"
          value={form.days}
          min="1"
          max="60"
          onChange={handleChange}
          required
        />

        <button type="submit">Borrow</button>
		<button onClick={() => navigate("/dashboard")} style={{ marginBottom: "20px" }}>
		  Back to Dashboard
		</button>
      </form>

      {/* Loans Table */}
      <table border="1" cellPadding="5">
        <thead>
          <tr>
            <th>ID</th>
            <th>Book</th>
            <th>Member</th>
            <th>Loan Date</th>
            <th>Due Date</th>
            <th>Return Date</th>
            <th>Status</th>
            <th>Fine</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {loans.map((loan) => (
            <tr key={loan.id}>
              <td>{loan.id}</td>
              <td>{loan.book?.title}</td>
              <td>{loan.member?.fullName}</td>
              <td>{new Date(loan.loanDate).toLocaleDateString()}</td>
              <td>{new Date(loan.dueDate).toLocaleDateString()}</td>
              <td>
                {loan.returnDate ? new Date(loan.returnDate).toLocaleDateString() : ""}
              </td>
              <td>{statusMap[loan.status]}</td>
              <td>{loan.fine.toFixed(2)}</td>
              <td>
                {loan.status === 0 && (
				  <button onClick={() => handleReturn(loan.id)}>Return</button>
				)}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default LoansPage;