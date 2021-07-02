// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions </Copyright>
//     <Url> http://lehoangtrinh.com </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Data </Project>
//     <File>
//         <Name> INotificationBase.cs </Name>
//         <Created> 27/2/2019 - 22:28 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//
//     </Summary>
// <License>

using Microsoft.AspNetCore.SignalR;

namespace NEVAR_AQC.Business.Notification
{
    public interface INotificationBase<H> where H : Hub
    {
        void SendNotificaion(string messageHub);
    }
}