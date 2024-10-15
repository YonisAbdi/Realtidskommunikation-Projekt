<template>
    <div id="app">
        <nav>
            <router-link to="/chat">Chat Room</router-link>
            <router-link to="/weather">Weather Forecast</router-link>
        </nav>
        <router-view />
    </div>
</template>

<script>
    export default {
        name: 'App',
    };
</script>

<style>
    #app {
        font-family: Avenir, Helvetica, Arial, sans-serif;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: center;
        color: #2c3e50;
        margin-top: 60px;
    }

    nav {
        margin-bottom: 20px;
    }

        nav a {
            margin-right: 15px;
            text-decoration: none;
            color: #42b983;
        }

            nav a.router-link-exact-active {
                font-weight: bold;
            }
</style>
