import { useAuth0 } from "@auth0/auth0-react";

const BASE_URL = "https://localhost:7052/api/Weather";

export function useWeatherService() {
  const { getAccessTokenSilently } = useAuth0();

  async function fetchAllWeather() {
    const token = await getAccessTokenSilently();
    const response = await fetch(`${BASE_URL}/all`, {
      headers: { Authorization: `Bearer ${token}` },
    });
    if (!response.ok) throw new Error("Failed to fetch weather data");
    return await response.json();
  }

  async function fetchWeatherByCity(cityId) {
    const token = await getAccessTokenSilently();
    const response = await fetch(`${BASE_URL}/${cityId}`, {
      headers: { Authorization: `Bearer ${token}` },
    });
    if (!response.ok) throw new Error("City not found");
    return await response.json();
  }

  return { fetchAllWeather, fetchWeatherByCity };
}
