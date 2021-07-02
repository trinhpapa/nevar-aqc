#region	License
//------------------------------------------------------------------------------------------------
// <License>
//     <Copyright> 2019 © NEVAR Technology Solutions</Copyright>
//     <Url> https://github.com/trinhpapa </Url>
//     <Author> Le Hoang Trinh </Author>
//     <Project> NEVAR-AQC </Project>
//     <File>
//         <Name> FunctionFilter.cs </Name>
//         <Created> 1/4/2019 - 22:38:34 </Created>
//         <Key></Key>
//     </File>
//     <Summary>
//         FunctionFilter.cs
//     </Summary>
// <License>
//------------------------------------------------------------------------------------------------
#endregion License

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NEVAR_AQC.Filters
{
    public class FunctionFilter : ActionFilterAttribute
    {
        private int _functionKey;

        public FunctionFilter(int functionKey)
        {
            _functionKey = functionKey;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var functions = context.HttpContext.Session.GetString("user-function");
            if (string.IsNullOrEmpty(functions))
            {
                context.Result = new RedirectResult("/home/access-denied");
            }
            else
            {
                if (!functions.Contains(_functionKey.ToString()))
                {
                    context.Result = new RedirectResult("/home/access-denied");
                }
            }
            base.OnActionExecuting(context);
        }
    }
}