#region	License
//------------------------------------------------------------------------------------------------
// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions</Copyright>
//     <Url> https://github.com/trinhpapa </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC.Data </Project>
//     <File>
//         <Name> ICustomerNotification.cs </Name>
//         <Created> 27/2/2019 - 22:28:23 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//         ICustomerNotification.cs
//     </Summary>
// <License>
//------------------------------------------------------------------------------------------------
#endregion License

using NEVAR_AQC.Core.Hubs;

namespace NEVAR_AQC.Business.Notification
{
    public interface ICustomerNotification : INotificationBase<SystemHub>
    {
    }
}