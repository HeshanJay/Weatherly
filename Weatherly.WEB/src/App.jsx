import { useAuth0 } from "@auth0/auth0-react";
import { useEffect } from "react";
import WeatherDashbord from "./Pages/WeatherDashbord/WeatherDashbord";
import Navbar from "./Components/Navbar";
import Footer from "./Components/Footer";

function App() {
  const { loginWithRedirect, isAuthenticated, isLoading } = useAuth0();

  useEffect(() => {
    if (!isLoading && !isAuthenticated) {
      loginWithRedirect();
    }
  }, [isLoading, isAuthenticated, loginWithRedirect]);

  if (isLoading) {
    return <p>Loading...</p>;
  }

  return (
    <div className="app-container">
      {isAuthenticated && (
        <>
          <Navbar />
          <WeatherDashbord />
          <Footer />
        </>
      )}
    </div>
  );
}

export default App;
