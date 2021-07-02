using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace NEVAR_AQC.Core.Hubs
{
    public class SystemHub : Hub
    {
        public async Task UserLogin()
        {
            await Clients.All.SendAsync("userLogin");
        }
    }
}