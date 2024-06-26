﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Authentication
{
    public class ApiKeyAuthFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var extractedApiKey))
            {
                context.Result = new UnauthorizedObjectResult("API Key missing");
                return;
            }

            KeyValuePair<bool, string> validation = ApiKeyValidation.IsValidKey(extractedApiKey!);
            if (!validation.Key)
            {
                context.Result = new UnauthorizedObjectResult(validation.Value);
                return;
            }

            string name = $"{context.HttpContext.Request.Method}{context.HttpContext.Request.Path!}";
            KeyValuePair<bool, string> rightsValidation = ApiKeyValidation.AreRightsValid(extractedApiKey!, name);
            if (!rightsValidation.Key)
            {
                context.Result = new UnauthorizedObjectResult(rightsValidation.Value);
                return;
            }
        }
    }
}
