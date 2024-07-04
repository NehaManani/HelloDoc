using HelloDoc_Common.Constants;

namespace HelloDoc_Common.Enums
{
    public enum UserRoleEnum : byte
    {
        Admin = UserRoleConstants.Admin,
        Patient = UserRoleConstants.Patient,
        Provider = UserRoleConstants.Provider
    }
}