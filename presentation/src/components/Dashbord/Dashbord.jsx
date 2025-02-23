import { useNavigate } from "react-router-dom";

const Dashboard = () => {
  const navigate = useNavigate();

  const handleLogout = () => {
    localStorage.removeItem("token"); // Kullanıcıyı çıkış yaptır
    navigate("/");
  };

  return (
    <div className="container mt-5">
      <h2>Dashboard</h2>
      <p>Hoş geldin! 🎉</p>
      <button className="btn btn-danger" onClick={handleLogout}>
        Logout
      </button>
    </div>
  );
};

export default Dashboard;
