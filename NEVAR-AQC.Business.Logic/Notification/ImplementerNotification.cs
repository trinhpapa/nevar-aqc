// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions </Copyright>
//     <Url> http://lehoangtrinh.com </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Data.EF </Project>
//     <File>
//         <Name> ImplementerNotification.cs </Name>
//         <Created> 3/6/2019 - 17:44 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//
//     </Summary>
// <License>

using Microsoft.AspNetCore.SignalR;
using NEVAR_AQC.Business.Notification;
using NEVAR_AQC.Core.Hubs;

namespace NEVAR_AQC.Business.Logic.Notification
{
    public class ImplementerNotification : NotificationBase<SystemHub>, IImplementerNotification
    {
        public ImplementerNotification(IHubContext<SystemHub> hubContext) : base(hubContext)
        {
        }
    }
}