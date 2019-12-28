using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace CustomizableWallClock
{
    public class ClockMaker : Hub
    {
        public Task SendMessage(string settingsJson){
            return Clients.All.SendAsync("ReceiveMessage", settingsJson);
        }
    }
}