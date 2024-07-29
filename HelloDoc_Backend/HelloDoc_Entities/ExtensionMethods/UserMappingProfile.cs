using HelloDoc_Common.Constants;
using HelloDoc_Entities.DataModels;
using HelloDoc_Entities.DTOs.Request;

namespace HelloDoc_Entities.ExtensionMethods
{
    public static class UserMappingProfile
    {
        public static void ToGenerateOtp(this User user, string otp, DateTime expiryTime)
        {
            user.OTP = otp;
            user.OtpExpiryTime = expiryTime;
        }

        public static void ToVerifyOtp(this User user)
        {
            user.OTP = null;
            user.OtpExpiryTime = null;
        }

        public static void ToSetPassword(this User user, string hashedPassword)
        {
            user.Password = hashedPassword;
        }

        public static User ToRegisterPatientUserRequest(this RegisterPatientRequest registerPatientRequest) => new()
        {
            FirstName = registerPatientRequest.FirstName,
            LastName = registerPatientRequest.LastName,
            Email = registerPatientRequest.Email,
            Password = registerPatientRequest.Password,
            PhoneNumber = registerPatientRequest.PhoneNumber,
            City = registerPatientRequest.City,
            Zip = registerPatientRequest.Zip,
            Address = registerPatientRequest.Address,
            Gender = registerPatientRequest.Gender,
        };

        public static void ToSetPatientStatusAndRole(this User user, string hashedPassword)
        {
            user.Password = hashedPassword;
            user.Status = UserStatusConstants.New;
            user.Role = UserRoleConstants.Patient;
        }

        public static void ToSetProviderStatusAndRole(this User user, string hashedPassword)
        {
            user.Password = hashedPassword;
            user.Status = UserStatusConstants.New;
            user.Role = UserRoleConstants.Provider;
        }

        public static User ToRegisterProviderUserRequest(this RegisterProviderRequest registerProviderRequest) => new()
        {
            FirstName = registerProviderRequest.FirstName,
            LastName = registerProviderRequest.LastName,
            Email = registerProviderRequest.Email,
            Password = registerProviderRequest.Password,
            PhoneNumber = registerProviderRequest.PhoneNumber,
            City = registerProviderRequest.City,
            Zip = registerProviderRequest.Zip,
            Address = registerProviderRequest.Address,
            Gender = registerProviderRequest.Gender,
        };

        public static List<UserRequest> ToPatientRequestListResponseDTO(this List<User> users)
        {
            return users.Select(patientRequest => patientRequest.ToGetPatientUserRequest()).ToList();
        }

        public static UserRequest ToGetPatientUserRequest(this User user)
        {
            return new UserRequest
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Gender = (int)user.Gender,
                Role = user.UserRoles.Role,
                City = user.City,
                Zip = user.Zip,
                Address = user.Address
            };
        }

        public static StatusCountResponse ToStatusCountResponse(this IEnumerable<User> users)
        {
            return new()
            {
                New = users.Count(user => user.UserStatuses.Status == "New"),
                Pending = users.Count(user => user.UserStatuses.Status == "Pending"),
                Active = users.Count(user => user.UserStatuses.Status == "Active"),
                Conclude = users.Count(user => user.UserStatuses.Status == "Conclude"),
                Close = users.Count(user => user.UserStatuses.Status == "Close"),
                Unpaid = users.Count(user => user.UserStatuses.Status == "Unpaid"),
            };
        }

        public static RegisterPatientRequest ToGetPatientDetails(this User user) => new()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Password = user.Password,
            PhoneNumber = user.PhoneNumber,
            City = user.City,
            Zip = user.Zip,
            Address = user.Address,
            Gender = user.Gender,
        };

        public static void ToSetReasonForBlock(this BlockCaseRequest blockCaseRequest, User user)
        {
            user.Status = UserStatusConstants.Block;
            user.ReasonForBlock = blockCaseRequest.ReasonForBlock;
        }
    };
}

