<template>
    <div>
        <h3>Chat with Admin</h3>

        <!-- Chat window to display incoming messages -->
        <div class="chat-window">
            <div v-for="(msg, index) in messages"
                 :key="index"
                 :class="{'message-left': msg.role !== currentRole, 'message-right': msg.role === currentRole}"
                 class="message">
                <strong>{{ msg.user }} ({{ msg.role }}):</strong> {{ msg.text }}
                <small>{{ msg.timestamp }}</small>
            </div>
        </div>

        <!-- Input fields for sending messages -->
        <div class="input-area">
            <input v-model="username" placeholder="Enter your name" />
            <input v-model="role" placeholder="Enter your role (admin/client)" />
            <input v-model="message" placeholder="Enter your message" />
            <button @click="sendMessage">Send Message</button>
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
                role: '', // Current user's role (admin or client)
                currentRole: '', // This will be the role of the user currently logged in
            };
        },
        mounted() {
    this.connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:5027/chatHub") // Ensure this URL matches the backend
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
},

        methods: {
            sendMessage() {
                if (this.message && this.username && this.role) {
                    const timestamp = new Date().toLocaleTimeString(); // Add a timestamp

                    // Set the current user's role on first message
                    if (!this.currentRole) {
                        this.currentRole = this.role;
                    }

                    // Send the message to the SignalR hub with username, role, and timestamp
                    this.connection.invoke("SendMessage", this.username, this.role, this.message, timestamp)
                        .then(() => {
                            this.message = ''; // Clear the message input after sending
                        })
                        .catch(err => console.error("Error sending message: ", err));
                }
            }
        }
    };
</script>