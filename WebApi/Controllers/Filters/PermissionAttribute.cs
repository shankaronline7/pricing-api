using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Controllers.Filters
{
    public class PermissionAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string _permissionCode;

        public PermissionAttribute(string permissionCode)
        {
            _permissionCode = permissionCode;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            // 1️⃣ Check if user authenticated
            if (!user.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // 2️⃣ Extract RoleId from JWT
            var roleClaim = user.FindFirst("RoleId");

            if (roleClaim == null)
            {
                context.Result = new ForbidResult();
                return;
            }

            int roleId = int.Parse(roleClaim.Value);

            // 3️⃣ Get AuthorizationService from DI
            var authService = context.HttpContext.RequestServices
                .GetRequiredService<IAuthorizationService>();

            bool hasPermission = await authService
                .HasPermissionAsync(roleId, _permissionCode);

            if (!hasPermission)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}

