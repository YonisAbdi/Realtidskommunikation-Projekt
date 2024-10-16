<template>
  <div class="weather-container">
    <div class="weather-header">
      <h2>Naturliga Växter & Trädgårdar / Väder Prognos</h2>
    </div>

    <!-- Display weather icons and temperatures at hourly intervals starting from the next full hour -->
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
      <p>{{ currentTime }}</p>
    </div>

    <!-- Control buttons -->
    <div class="buttons">
      <button @click="openWindow">Öppna Fönstret</button>
      <button @click="closeWindow">Stäng Fönstret</button>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      forecast: [], // Hourly forecast data
      current: {},  // Current weather data
      city: 'Jönköping', // City name
      currentTime: new Date().toLocaleTimeString(), // Local time
      apiKey: 'b0eb5944fbf94961aaf80621241610', // Your WeatherAPI key
    };
  },
  mounted() {
    this.fetchWeather();
    setInterval(() => {
      this.fetchWeather(); // Auto-refresh the weather data every hour
      this.currentTime = new Date().toLocaleTimeString();
    }, 60000); // Update time and refresh weather data every minute
  },
  methods: {
    async fetchWeather() {
      const url = `https://api.weatherapi.com/v1/forecast.json?key=${this.apiKey}&q=${this.city}&days=1`;

      try {
        const response = await fetch(url);
        const data = await response.json();

        const currentHour = new Date().getHours();
        const nextHour = (currentHour + 1) % 24; // Get the next full hour
        const forecastHours = data.forecast.forecastday[0].hour.filter(hour => {
          const forecastHour = new Date(hour.time).getHours();
          return forecastHour >= nextHour;
        });

        // Process the forecast data to start from the next hour, displaying 5 intervals every hour
        this.forecast = forecastHours.slice(0, 5).map(hour => ({
          time: hour.time,
          temp: hour.temp_c,
          icon: `https:${hour.condition.icon}`, // Fix the icon URL
        }));

        // Set current weather data
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
    openWindow() {
      console.log('Window opened!');
    },
    closeWindow() {
      console.log('Window closed!');
    }
  }
};
</script>

<style scoped>
.weather-container {
  background-color: #92D050;
  padding: 20px;
  border-radius: 10px;
  width: 400px;
  margin: 0 auto;
  font-family: Arial, sans-serif;
  color: black;
}

.weather-header {
  text-align: center;
}

.weather-grid {
  display: flex;
  justify-content: space-around;
  margin-bottom: 20px;
}

.weather-item {
  text-align: center;
}

.current-weather {
  text-align: center;
  margin-bottom: 20px;
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
