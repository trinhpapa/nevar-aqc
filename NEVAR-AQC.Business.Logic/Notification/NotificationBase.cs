// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions </Copyright>
//     <Url> http://lehoangtrinh.com </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Data.EF </Project>
//     <File>
//         <Name> NotificationBase.cs </Name>
//         <Created> 27/2/2019 - 22:28 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//
//     </Summary>
// <License>

using Microsoft.AspNetCore.SignalR;
using NEVAR_AQC.Business.Notification;

namespace NEVAR_AQC.Business.Logic.Notification
{
    public class NotificationBase<H> : INotificationBase<H> where H : Hub
    {
        private readonly IHubContext<H> _hubContext;

        public NotificationBase(IHubContext<H> hubContext)
        {
            _hubContext = hubContext;
        }

        public void SendNotificaion(string messageHub)
        {
            _hubContext.Clients.All.SendAsync(messageHub);
        }
    }
}