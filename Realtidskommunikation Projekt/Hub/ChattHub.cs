using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class ChattHub : Hub
{
    // Ensure all parameters (user, role, message, timestamp) are properly received
    public async Task SendMessage(string user, string role, string message, string timestamp)
    {
        // Make sure the parameters are valid
        if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(message))
        {
            throw new HubException("User and message cannot be empty.");
        }

        // Broadcast the message to all connected clients
        await Clients.All.SendAsync("ReceiveMessage", user, role, message, timestamp);
    }
}
