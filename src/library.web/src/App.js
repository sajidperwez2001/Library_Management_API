import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Login from "./pages/Login";
import Dashboard from "./pages/Dashboard";
import Books from "./pages/Books";
import Register from "./pages/Register";
import Members from "./pages/Members";
import Loans from "./pages/Loans";


function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/books" element={<Books />} />
        <Route path="*" element={<Navigate to="/login" />} />
		<Route path="/register" element={<Register />} />
		<Route path="/members" element={<Members />} />		
		<Route path="/loans" element={<Loans />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;