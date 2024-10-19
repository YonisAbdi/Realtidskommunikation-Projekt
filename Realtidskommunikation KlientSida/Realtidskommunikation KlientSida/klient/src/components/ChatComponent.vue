<template>
    <div>
        <h3>Chatta med Admin</h3>
        <div class="chat-window">
            <div v-for="(msg, index) in messages"
                 :key="index"
                 :class="{'message-left': msg.role !== currentRole, 'message-right': msg.role === currentRole}"
                 class="message">
                <strong>{{ msg.user }} ({{ msg.role }}):</strong> {{ msg.text }}
                <small>{{ msg.timestamp }}</small>
            </div>
        </div>

        <div v-if="!isIdentified" class="identification-area">
            <input v-model="username" placeholder="Enter your name" />
            <input v-model="role" placeholder="Enter your role (admin/klient)" />
            <button @click="identifyUser">Identify</button>
        </div>

        <div v-else class="input-area">
            <input v-model="message" @keyup.enter="sendMessage" placeholder="Enter your message" />
            <button @click="sendMessage">Send Message</button>
        </div>

        <!-- Display real-time clock -->
        <div class="clock">
            <p>Current time: {{ currentTime }}</p>
        </div>
    </div>
</template>

<script>
    import * as signalR from '@microsoft/signalr';

    export default {
        name: 'ChatComponent',
        data() {
            return {
                connection: null,
                messages: [],
                message: '',
                username: '',
                role: '',
                currentRole: '',
                isIdentified: false,
                currentTime: '' // New property to store the current time
            };
        },
        mounted() {
            this.connection = new signalR.HubConnectionBuilder()
                .withUrl("http://localhost:5027/chatHub")
                .withAutomaticReconnect()
                .build();

            this.connection.start()
                .then(() => {
                    console.log("Connected to SignalR hub!");

                    this.connection.on("ReceiveMessage", (user, role, message, timestamp) => {
                        this.messages.push({ user, role, text: message, timestamp });
                    });
                })
                .catch(err => console.error("Connection error: ", err));

            // Set interval to update the current time every second
            this.updateTime();
            setInterval(this.updateTime, 1000);
        },
        methods: {
            // Function to update the current time
            updateTime() {
                const now = new Date();
                this.currentTime = now.toLocaleTimeString();
            },
            identifyUser() {
                if (this.username && this.role) {
                    this.currentRole = this.role;
                    this.isIdentified = true;
                } else {
                    alert('Please enter both your name and role.');
                }
            },
            sendMessage() {
                if (this.message) {
                    const timestamp = new Date().toLocaleTimeString();

                    this.connection.invoke("SendMessage", this.username, this.role, this.message, timestamp)
                        .then(() => {
                            this.message = ''; // Clear the message after sending
                        })
                        .catch(err => console.error("Error sending message: ", err));
                }
            }
        }
    };
</script>

<style scoped>
    .chat-window {
        width: 100%;
        height: 300px;
        border: 1px solid #ccc;
        padding: 10px;
        overflow-y: scroll;
        background-color: #f8f8f8;
    }

    .message {
        max-width: 70%;
        padding: 10px;
        margin: 10px 0;
        border-radius: 10px;
        background-color: #fff;
    }

    .message-left {
        text-align: left;
        background-color: lightseagreen;
        margin-left: 0;
    }

    .message-right {
        text-align: right;
        background-color: lightgreen;
        margin-left: auto;
        margin-right: 0;
    }

    input {
        display: block;
        margin: 5px 0;
        padding: 8px;
        width: 200px;
    }

    button {
        padding: 8px 15px;
        margin-top: 5px;
    }

    small {
        color: #666;
        display: block;
        margin-top: 3px;
    }

    .clock {
        margin-top: 10px;
        font-size: 16px;
        font-weight: bold;
    }
</style>
