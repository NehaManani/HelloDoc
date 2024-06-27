using HelloDoc_Entities.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace HelloDoc_BusinessAccessLayer.IServices
{
    public interface IAuthenticationService
    {
        Task<IActionResult> Login(LoginRequest loginRequest);
    }
}