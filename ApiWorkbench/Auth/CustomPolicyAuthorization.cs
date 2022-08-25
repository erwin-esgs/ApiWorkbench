using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;  

namespace ApiWorkbench.Auth
{
    public class CustomUserRequire : IAuthorizationRequirement
    {
        public string Role { get; }  
        public CustomUserRequire(string _Role)
        {
            Role = _Role;
        }
    }
    public class PoliciesAuthorizationHandler : AuthorizationHandler<CustomUserRequire>  
    {  
        protected override Task HandleRequirementAsync(  
            AuthorizationHandlerContext context,  
            CustomUserRequire requirement)  
        {  
            if (context.User == null || !context.User.Identity.IsAuthenticated)  
            {  
                context.Fail();  
                return Task.CompletedTask;  
            }  
  
            var hasClaim = context.User.Claims.Any(x => x.Type == requirement.Role);  
  
            if (hasClaim)  
            {  
                context.Succeed(requirement);  
                return Task.CompletedTask;  
            }  
  
            context.Fail();  
            return Task.CompletedTask;  
        }  
    } 
}