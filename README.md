# 🌦️ Weatherly – Full Stack Weather Dashboard

## 🚀 Features
- Read **city codes** from a JSON file
- Fetch live weather data (name, description, temperature) via **OpenWeatherMap**
- Cache API responses for **5 minutes**
- Responsive frontend (desktop + mobile)
- Secure access with **Auth0 (JWT)**
- **Multi-Factor Authentication (MFA)** enabled
- **Public signups disabled** – only test account works

---

## 🛠️ Tech Stack
- **Backend**: ASP.NET Core Web API  
- **Frontend**: React + Vite  
- **Auth**: Auth0 (JWT + MFA)  
- **API**: OpenWeatherMap  

---

## ⚙️ Setup Instructions

### Clone Repository
git clone https://github.com/HeshanJay/Weatherly.git


### Backend Setup (ASP.NET Core API)
cd Weatherly.API
dotnet restore
dotnet run

### Frontend Setup (React + Vite)
cd Weatherly.WEB
npm install
npm run dev





