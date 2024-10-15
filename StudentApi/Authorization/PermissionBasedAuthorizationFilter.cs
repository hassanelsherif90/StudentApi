using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StudentApi.Data;
using StudentApi.Model.User;
using System.Security.Claims;

namespace StudentApi.Authorization
{

    public class PermissionBasedAuthorizationFilter(ApplicationDbcontext dbcontext) : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            CheckPermissionAttribute? attribute = (CheckPermissionAttribute)context.ActionDescriptor.EndpointMetadata
                .FirstOrDefault(x => x is CheckPermissionAttribute);

            if (attribute != null)
            {
                if (context.HttpContext.User.Identity is not ClaimsIdentity claimIdentity || !claimIdentity.IsAuthenticated)
                {
                    context.Result = new ForbidResult();
                }
                else
                {
                    int userId = int.Parse(claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

                    bool hasPermissions = dbcontext.Set<UserPermission>().Any(x => x.UserId == userId &&
                            x.PermissionId == attribute.Permission);
                    if (!hasPermissions)
                    {
                        context.Result = new ForbidResult();
                    }
                }
            }
        }
    }
}
