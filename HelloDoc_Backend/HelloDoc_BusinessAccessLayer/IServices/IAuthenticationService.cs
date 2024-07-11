using HelloDoc_Entities.DTOs.Request;
using HelloDoc_Entities.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace HelloDoc_BusinessAccessLayer.IServices
{
    public interface IAuthenticationService
    {
        Task<string> Login(LoginRequest loginRequest);
        Task ForgotPassword(string email);
        Task SendOtp(string email);
        Task<string> VerifyOtp(VerifyOtpRequest verifyOtpRequest);
        Task ResetPassword(ResetPasswordRequest resetPasswordRequest);
        Task RegisterPatientRequest(RegisterPatientRequest registerPatientRequest);
        Task RegisterProviderRequest(RegisterProviderRequest registerProviderRequest);
    }
}