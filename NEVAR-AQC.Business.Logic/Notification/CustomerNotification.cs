// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions </Copyright>
//     <Url> http://lehoangtrinh.com </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Data.EF </Project>
//     <File>
//         <Name> CustomerNotification.cs </Name>
//         <Created> 27/2/2019 - 22:28 </Created>
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
    public class CustomerNotification : NotificationBase<SystemHub>, ICustomerNotification
    {
        public CustomerNotification(IHubContext<SystemHub> hubContext) : base(hubContext)
        {
        }
    }
}