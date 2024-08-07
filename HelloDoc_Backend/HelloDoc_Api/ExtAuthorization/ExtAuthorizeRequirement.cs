﻿using Microsoft.AspNetCore.Authorization;

namespace HelloDoc_Api.ExtAuthorization
{
    public class ExtAuthorizeRequirement : IAuthorizationRequirement
    {
        public string PolicyName { get; }

        public ExtAuthorizeRequirement(string policyName)
        {
            PolicyName = policyName;
        }
    }
}
