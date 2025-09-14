import React from "react";
import { useAuth0 } from "@auth0/auth0-react";
import logo from "../assets/Weatherlylogo.png";
import "./Navbar.css";

const Navbar = () => {
  const { logout } = useAuth0();

  return (
    <nav className="navbar">
      <div className="navbar-left">
        <img src={logo} alt="Weatherly Logo" className="navbar-logo" />
        <h2 className="navbar-title">Weatherly</h2>
      </div>

      <button
        className="logout-button"
        onClick={() =>
          logout({ logoutParams: { returnTo: window.location.origin } })
        }
      >
        <span className="button-icon">↩</span>
        Logout
      </button>
    </nav>
  );
};

export default Navbar;
