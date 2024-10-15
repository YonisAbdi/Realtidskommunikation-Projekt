<template>
    <div id="app">
        <ChatComponent /> 
    </div>
</template>

<script>
    import ChatComponent from './components/ChatComponent.vue'; // Make sure this matches the correct file name

    export default {
        name: 'App',
        components: {
            ChatComponent // Correct name here
        }
    }
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
</style>
