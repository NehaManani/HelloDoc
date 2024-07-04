using HelloDoc_Entities.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace HelloDoc_BusinessAccessLayer.IServices
{
    public interface IAuthenticationService
    {
        Task<string> Login(LoginRequest loginRequest);
        Task ForgotPassword(string email);
        Task SendOtp(string email);
        Task SubmitRegisterPatientRequest(SubmitRegisterPatientRequest submitRegisterPatientRequest);
    }
}