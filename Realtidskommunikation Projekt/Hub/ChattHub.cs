using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class ChattHub : Hub
{
    public async Task SendMessage(string user, string role, string message, string timestamp)
    {
        if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(message))
        {
            throw new HubException("User and message cannot be empty.");
        }
        await Clients.All.SendAsync("ReceiveMessage", user, role, message, timestamp);
    }
}
