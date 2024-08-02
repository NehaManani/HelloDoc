using HelloDoc_Common.Constants;
using HelloDoc_Common.Exceptions;
using HelloDoc_Entities.DTOs.Common;
using Microsoft.AspNetCore.Authorization;

namespace HelloDoc_Api.ExtAuthorization
{
    public class ExtAuthorizeHandler : AuthorizationHandler<ExtAuthorizeRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public ExtAuthorizeHandler(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ExtAuthorizeRequirement requirement)
        {
            HttpContext? httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            new AuthHelper(httpContext, _configuration).AuthorizeRequest();

            if (CheckUserType(httpContext, requirement))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }

        private static bool CheckUserType(HttpContext context, ExtAuthorizeRequirement requirement)
        {
            LoggedUser? loggedUser = context.Items[SystemConstants.LOGGED_USER] as LoggedUser;

            if (loggedUser == null) return false;

            // Handle the Policy requirement
            if (requirement.PolicyName == SystemConstants.ADMIN_POLICY)
            {
                if (loggedUser.Role == UserRoleConstants.Admin) return true;
            }
            else if (requirement.PolicyName == SystemConstants.PATIENT_POLICY)
            {
                if (loggedUser.Role == UserRoleConstants.Patient) return true;
            }
            else if (requirement.PolicyName == SystemConstants.PROVIDER_POLICY)
            {
                if (loggedUser.Role == UserRoleConstants.Provider) return true;
            }
            else if (requirement.PolicyName == SystemConstants.ALL_USER_POLICY)
            {
                if (loggedUser.Role == UserRoleConstants.Admin || loggedUser.Role == UserRoleConstants.Patient || loggedUser.Role == UserRoleConstants.Provider) return true;
            }

            throw new CustomException(StatusCodes.Status401Unauthorized, MessageConstants.ErrorMessage.UNAUTHORIZE);
        }
    }
}
