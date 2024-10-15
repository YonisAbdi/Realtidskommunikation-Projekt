<script>
export default {
  name: 'WeatherComponent',
  data() {
    return {
      temperature: '',
      description: '',
    };
  },
  mounted() {
    this.fetchWeather();
  },
  methods: {
    async fetchWeather() {
      const apiKey = '30031b6f1253a6b7605a8cae1abbae79';
      const city = 'Orebro';
      // Corrected the API URL to use city name and the API key
      const url = `https://api.openweathermap.org/data/2.5/weather?q=${city}&appid=${apiKey}&units=metric`;

      try {
        const response = await fetch(url);
        const data = await response.json();
        this.temperature = data.main.temp;
        this.description = data.weather[0].description;
      } catch (error) {
        console.error('Error fetching weather data:', error);
      }
    },
  },
};
</script>
