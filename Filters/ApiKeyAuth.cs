using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProfileAPI.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProfileAPI.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuth : Attribute, IAsyncActionFilter
    {
        private const string ApiKeyHeaderName = "ApiKey";
        private const string UsernameHeader = "Username";
        private XmlSerializer accountSerializer = new XmlSerializer(typeof(UserProfile));

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var potentialApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            if (!context.HttpContext.Request.Headers.TryGetValue(UsernameHeader, out var potentialUsername))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            UserProfile user;

            using (FileStream file = File.Open(ProfileControllerBase.userPath + potentialUsername + ".txt", FileMode.OpenOrCreate))
            {
                user = (UserProfile)accountSerializer.Deserialize(file);
            }
            var config = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var apiKey = user.ApiKey;

            if (!apiKey.Equals(potentialApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
