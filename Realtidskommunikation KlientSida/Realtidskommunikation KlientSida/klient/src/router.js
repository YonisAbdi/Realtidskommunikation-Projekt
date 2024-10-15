import { createRouter, createWebHistory } from 'vue-router';
import ChatComponent from './components/ChatComponent.vue';
import WeatherComponent from './components/WeatherComponent.vue'; 

const routes = [
    {
        path: '/chat',
        name: 'Chat',
        component: ChatComponent,
    },
    {
        path: '/weather',
        name: 'Weather',
        component: WeatherComponent,
    },
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;
