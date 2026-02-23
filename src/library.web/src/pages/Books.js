import React, { useState, useEffect } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";

const BooksPage = () => {
  const [books, setBooks] = useState([]);
  const [form, setForm] = useState({
  isbn: "",
  title: "",
  author: "",
  publisher: "",
  year: "",
  totalCopies: ""
});
  const [editId, setEditId] = useState(null);
  const [message, setMessage] = useState("");
  const navigate = useNavigate();
  const API_URL = "https://localhost:50200/api/books";
  const token = localStorage.getItem("token"); // JWT token for admin actions

  // Axios headers with JWT if needed
  const config = {
    headers: {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json"
    }
  };

  // Fetch all books
  const fetchBooks = async () => {
    try {
      const res = await axios.get(API_URL, config);
      setBooks(res.data);
    } catch (err) {
      console.error(err);
      setMessage("Error fetching books");
    }
  };

  useEffect(() => {
    fetchBooks();
  }, []);

  // Handle input change
  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  // Handle add or update
  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      if (editId) {
        // Update book
        await axios.put(`${API_URL}/${editId}`, form, config);
        setMessage("Book updated successfully");
      } else {
        // Add new book
        await axios.post(API_URL, form, config);
        setMessage("Book added successfully");
      }
      setForm({ ISBN: "", Title: "", Author: "", Publisher: "", Year: "", TotalCopies: "", AvailableCopies: "" });
      setEditId(null);
      fetchBooks();
    } catch (err) {
      console.error(err);
      setMessage("Error adding/updating book");
    }
  };

  // Handle delete
  const handleDelete = async (id) => {
    if (!window.confirm("Are you sure you want to delete this book?")) return;

    try {
      await axios.delete(`${API_URL}/${id}`, config);
      setMessage("Book deleted successfully");
      fetchBooks();
    } catch (err) {
      console.error(err);
      setMessage("Error deleting book");
    }
  };

  // Handle edit
  const handleEdit = (book) => {
  setForm({
    isbn: book.isbn,
    title: book.title,
    author: book.author || "",
    publisher: book.publisher || "",
    year: book.year || "",
    totalCopies: book.totalCopies
  });
  setEditId(book.id);
};

  return (
    <div>
      <h2>Books</h2>
      {message && <p>{message}</p>}

      {/* Book form */}
      <form onSubmit={handleSubmit}>
        <input type="text" name="isbn" placeholder="ISBN" value={form.isbn} onChange={handleChange} required />
		<input type="text" name="title" placeholder="Title" value={form.title} onChange={handleChange} required />
		<input type="text" name="author" placeholder="Author" value={form.author} onChange={handleChange} />
		<input type="text" name="publisher" placeholder="Publisher" value={form.publisher} onChange={handleChange} />
		<input type="number" name="year" placeholder="Year" value={form.year} onChange={handleChange} />
		<input type="number" name="totalCopies" placeholder="Total Copies" value={form.totalCopies} onChange={handleChange} required />
        <button type="submit">{editId ? "Update Book" : "Add Book"}</button>
        {editId && <button onClick={() => { setEditId(null); setForm({ ISBN: "", Title: "", Author: "", Publisher: "", Year: "", TotalCopies: "" })}}>Cancel</button>}
		<button onClick={() => navigate("/dashboard")} style={{ marginBottom: "20px" }}>
		  Back to Dashboard
		</button>
      </form>

      {/* Books list */}
      <table border="1" cellPadding="5" style={{ marginTop: "20px" }}>
        <thead>
          <tr>
            <th>ID</th>
            <th>ISBN</th>
            <th>Title</th>
            <th>Author</th>
            <th>Publisher</th>
            <th>Year</th>
            <th>Total Copies</th>
            <th>Available Copies</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {books.map((book) => (
            <tr key={book.id}>
              <td>{book.id}</td>
              <td>{book.isbn}</td>
              <td>{book.title}</td>
              <td>{book.author}</td>
              <td>{book.publisher}</td>
              <td>{book.year}</td>
              <td>{book.totalCopies}</td>
              <td>{book.availableCopies}</td>
              <td>
                <button onClick={() => handleEdit(book)}>Edit</button>
                <button onClick={() => handleDelete(book.id)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default BooksPage;