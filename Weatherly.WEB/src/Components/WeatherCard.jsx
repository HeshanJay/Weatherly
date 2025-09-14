import "./WeatherCard.css";
import {
  WiDaySunny,
  WiCloudy,
  WiRain,
  WiSnow,
  WiDayShowers,
} from "react-icons/wi";

function WeatherCard({ cityName, temp, status }) {
  const getWeatherIcon = (status) => {
    switch (status) {
      case "Clear":
        return <WiDaySunny className="weather-icon" />;
      case "Clouds":
        return <WiCloudy className="weather-icon" />;
      case "Rain":
        return <WiRain className="weather-icon" />;
      case "Drizzle":
        return <WiDayShowers className="weather-icon" />;
      case "Snow":
        return <WiSnow className="weather-icon" />;
      default:
        return <WiCloudy className="weather-icon" />;
    }
  };

  const getCardClass = (status) => {
    switch (status) {
      case "Clear":
        return "weather-card sunny";
      case "Clouds":
        return "weather-card cloudy";
      case "Rain":
        return "weather-card rain";
      case "Drizzle":
        return "weather-card drizzle";
      case "Snow":
        return "weather-card snow";
      default:
        return "weather-card cloudy";
    }
  };

  return (
    <div className={getCardClass(status)}>
      {getWeatherIcon(status)}
      <h3>{cityName}</h3>
      <p className="temperature">{temp}°C</p>
      <p className="status">{status}</p>
    </div>
  );
}

export default WeatherCard;
