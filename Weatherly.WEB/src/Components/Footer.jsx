import "./Footer.css";

export default function Footer() {
  return (
    <footer className="footer">
      <p>© {new Date().getFullYear()} Weatherly. All Rights Reserved.</p>
    </footer>
  );
}
