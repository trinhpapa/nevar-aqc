#region	License
// <License>
//     <Copyright> 2019 © Le Hoang Trinh </Copyright>
//     <Url> https://github.com/trinhpapa </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC </Project>
//     <File>
//         <Name> SessionFilter.cs </Name>
//         <Created> 14/2/2019 - 11:10:35 </Created>
//     </File>
//     <Summary>
//
//     </Summary>
// </License>
#endregion License

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NEVAR_AQC.Filters
{
    public class SessionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (string.IsNullOrEmpty(context.HttpContext.Session.GetString("user-session")))
            {
                context.Result = new RedirectResult("/login");
            }
            base.OnActionExecuting(context);
        }
    }
}