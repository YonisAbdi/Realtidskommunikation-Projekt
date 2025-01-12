﻿<template>
  <div class="weather-container">
    <div class="weather-header">
      <h2>Naturliga Växter & Trädgårdar / Väder Prognos</h2>
    </div>

    <!-- Display weather icons and temperatures at hourly intervals -->
    <div class="weather-grid">
      <div v-for="(hour, index) in forecast" :key="index" class="weather-item">
        <p>{{ formatTime(hour.time) }}</p>
        <img :src="hour.icon" alt="weather icon" />
        <p>{{ Math.round(hour.temp) }}°C</p>
      </div>
    </div>

    <!-- Display current weather information -->
    <div class="current-weather">
      <h2>Väder</h2>
      <img :src="current.icon" alt="current weather icon" />
      <p>{{ Math.round(current.temp) }}°C</p>
      <p>{{ city }}</p>
      <p>{{ currentTime }}</p> <!-- Real-time clock displayed here -->
    </div>

    <!-- Display window status -->
    <div class="window-status">
      <h2>Fönsterstatus: {{ isWindowOpen ? 'Öppet' : 'Stängt' }}</h2>
    </div>

    <!-- Control buttons for window -->
    <div class="buttons">
      <button @click="toggleWindow">{{ isWindowOpen ? 'Stäng Fönstret' : 'Öppna Fönstret' }}</button>
    </div>
  </div>
</template>

<script>
import { HubConnectionBuilder } from '@microsoft/signalr';

export default {
  data() {
    return {
      forecast: [], // Hourly forecast data
      current: {},  // Current weather data
      city: 'Jönköping', // City name
      currentTime: new Date().toLocaleTimeString(), // Local time
      apiKey: 'b0eb5944fbf94961aaf80621241610', // WeatherAPI key
      isWindowOpen: false, // Window status
      windowHubConnection: null // SignalR connection for window control
    };
  },
  mounted() {
    // Fetch weather and set up auto-refresh
    this.fetchWeather();
    
    // Set interval to update the current time every second
    setInterval(() => {
      this.fetchWeather();
      this.currentTime = new Date().toLocaleTimeString();
    }, 1000); // Update the time every second

    // Initialize SignalR connection for window control
    this.windowHubConnection = new HubConnectionBuilder()
      .withUrl('http://localhost:5027/windowHub')
      .build();

    this.windowHubConnection.start().then(() => {
      this.windowHubConnection.on('ReceiveWindowStatus', (status) => {
        this.isWindowOpen = status;
      });
      
      // Request the current window status from the server
      this.windowHubConnection.invoke('GetWindowStatus');
    }).catch(error => console.error('SignalR connection error:', error));
  },
  methods: {
    async fetchWeather() {
      const url = `https://api.weatherapi.com/v1/forecast.json?key=${this.apiKey}&q=${this.city}&days=1`;

      try {
        const response = await fetch(url);
        const data = await response.json();

        const currentHour = new Date().getHours();
        const nextHour = (currentHour + 1) % 24;
        const forecastHours = data.forecast.forecastday[0].hour.filter(hour => {
          const forecastHour = new Date(hour.time).getHours();
          return forecastHour >= nextHour;
        });

        this.forecast = forecastHours.slice(0, 5).map(hour => ({
          time: hour.time,
          temp: hour.temp_c,
          icon: `https:${hour.condition.icon}`,
        }));

        this.current = {
          temp: data.current.temp_c,
          icon: `https:${data.current.condition.icon}`,
        };
      } catch (error) {
        console.error('Error fetching weather data:', error);
      }
    },
    formatTime(time) {
      const date = new Date(time);
      return date.getHours() + ':00';
    },
    toggleWindow() {
      this.isWindowOpen = !this.isWindowOpen;
      this.windowHubConnection.invoke('ToggleWindow', this.isWindowOpen)
        .catch(error => console.error('SignalR invoke error:', error));
    }
  }
};
</script>

<style scoped>
.weather-container {
  background-color: #92D050;
  padding: 50px;
  border-radius: 10px;
  width: 700px;
  margin: 0 auto;
  font-family: Arial, sans-serif;
  color: black;
}

.weather-header {
  text-align: center;
}

.weather-grid {
  display: flex;
  justify-content: space-evenly;
  margin-bottom: 20px;
}

.weather-item {
  text-align: center;
}

.current-weather {
  text-align: right;
}

.window-status {
  text-align: center;
  font-weight: bold;
}

.buttons {
  text-align: center;
}

button {
  background-color: white;
  border: 2px solid black;
  padding: 10px;
  cursor: pointer;
  font-size: 16px;
}

button:hover {
  background-color: #d3d3d3;
}
</style>
