using Microsoft.AspNetCore.SignalR;

public class WindowHub : Hub
{
    private static bool IsWindowOpen = false;

    public async Task GetWindowStatus()
    {
        await Clients.Caller.SendAsync("ReceiveWindowStatus", IsWindowOpen);
    }

    public async Task ToggleWindow(bool newStatus)
    {
        IsWindowOpen = newStatus;
        await Clients.All.SendAsync("ReceiveWindowStatus", IsWindowOpen);
    }
}
