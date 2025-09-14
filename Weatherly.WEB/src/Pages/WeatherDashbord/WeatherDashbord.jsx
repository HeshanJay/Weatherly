import React, { useEffect, useState } from "react";
import { useWeatherService } from "../../Services/weatherService";
import WeatherCard from "../../Components/WeatherCard";
import "./WeatherDashbord.css";

export default function WeatherDashbord() {
  const { fetchAllWeather } = useWeatherService();
  const [weatherData, setWeatherData] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const loadWeather = async () => {
      try {
        const data = await fetchAllWeather();
        setWeatherData(data);
      } catch (err) {
        setError("Failed to load weather data");
        console.error(err);
      } finally {
        setLoading(false);
      }
    };
    loadWeather();
  }, [fetchAllWeather]);

  if (loading) return <p>Loading weather data...</p>;
  if (error) return <p style={{ color: "red" }}>{error}</p>;

  return (
    <div className="dashboard-bg">
      <h1 className="dashboard-title">Weather Dashboard</h1>
      <div className="card-container">
        {weatherData.map((city) => (
          <WeatherCard
            key={city.cityCode || city.id}
            cityName={city.cityName || city.name}
            temp={city.temp}
            status={city.status}
          />
        ))}
      </div>
    </div>
  );
}
