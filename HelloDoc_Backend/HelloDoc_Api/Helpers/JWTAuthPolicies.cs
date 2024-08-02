using HelloDoc_Common.Constants;
using Microsoft.AspNetCore.Authorization;

namespace HelloDoc_Api.Helpers
{
    public class AdminPolicyAttribute : AuthorizeAttribute
    {
        public AdminPolicyAttribute()
        {
            Policy = SystemConstants.ADMIN_POLICY;
        }
    }

    public class PatientPolicyAttribute : AuthorizeAttribute
    {
        public PatientPolicyAttribute()
        {
            Policy = SystemConstants.PATIENT_POLICY;
        }
    }

    public class ProviderPolicyAttribute : AuthorizeAttribute
    {
        public ProviderPolicyAttribute()
        {
            Policy = SystemConstants.PROVIDER_POLICY;
        }
    }

    public class AllPolicyAttribute : AuthorizeAttribute
    {
        public AllPolicyAttribute()
        {
            Policy = SystemConstants.ALL_USER_POLICY;
        }
    }
}