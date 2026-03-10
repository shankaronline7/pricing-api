using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Controllers.Filters
{
    public class PermissionAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // ✅ STEP 1: Skip if AllowAnonymous exists
            if (context.ActionDescriptor.EndpointMetadata
                .OfType<IAllowAnonymous>()
                .Any())
            {
                return;
            }

            var user = context.HttpContext.User;

            // ✅ STEP 2: Check Authentication
            if (!user.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // ✅ STEP 3: Get RoleId
            var roleClaim = user.FindFirst("RoleId");
            if (roleClaim == null)
            {
                context.Result = new ForbidResult();
                return;
            }

            int roleId = int.Parse(roleClaim.Value);

            // ✅ STEP 4: Generate Permission Code
            var controller = context.RouteData.Values["controller"]?.ToString() ?? "";
            var action = context.RouteData.Values["action"]?.ToString() ?? "";

            string permissionCode = $"{controller}_{action}".ToUpper();

            // ✅ STEP 5: Check DB Permission
            var authService = context.HttpContext.RequestServices
                .GetRequiredService<Application.Interfaces.IAuthorizationService>();

            bool hasPermission = await authService
                .HasPermissionAsync(roleId, permissionCode);

            if (!hasPermission)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}