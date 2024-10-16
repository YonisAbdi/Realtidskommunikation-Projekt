import { createRouter, createWebHistory } from 'vue-router';
import ChatComponent from './components/ChatComponent.vue';
import WeatherComponent from './components/WeatherComponent.vue';

const routes = [
    { path: '/chat', component: ChatComponent },
    { path: '/weather', component: WeatherComponent }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;
