using EmpAPI.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAPI.Middleware
{
    public class UserKeyValidatorsMiddleware
    {
        private RequestDelegate _next;

        public UserKeyValidatorsMiddleware( RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IEmployee employeeRepo)
        {
            if (!context.Request.Headers.Keys.Contains("user-key"))
            {
                context.Response.StatusCode = 400; // bad Request
                await context.Response.WriteAsync("User key is missing");
                return;
            }
            else
            {
                if (!employeeRepo.CheckValidUserKey(context.Request.Headers["user-key"]))
                {
                    context.Response.StatusCode = 401; // UnAuthorized
                    await context.Response.WriteAsync("Invalid user key");
                    return;
                }
            }

            await _next.Invoke(context);
        }
    }
}
