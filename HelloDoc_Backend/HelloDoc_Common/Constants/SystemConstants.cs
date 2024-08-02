namespace HelloDoc_Common.Constants
{
    public class SystemConstants
    {
        public const string CORS_POLICY = "CorsPolicy";

        public const string SUCCESS = "Success";

        public const string DEFAULT_CONNECTION = "DefaultConnection";

        public const string ASCENDING = "ascending";

        public const string DESCENDING = "descending";

        public const string MAIL_TEMPLATES = "MailTemplates";

        public const string RESET_PASSWORD_MAIL_TEMPLATE_FILE = "ResetPasswordMailTemplate.html";

        public const string OTP_MAIL_TEMPLATE_FILE = "OtpMailTemplate.html";

        public static readonly int PASSWORD_ITERATION = 10;

        public static readonly int TOKEN_EXPIRE_MINUTES = 10;

        public const int INITIAL_PAGE_SIZE = 1;

        public const int DEFAULT_PAGE_SIZE = 10;

        public const string DEFAULT_SORTCOLUMN = "Id";

        public const string USER_ID_CLAIM = "UserId";

        public const string ZERO_STRING = "0";

        #region ModelStateConstant

        public static class ModelStateConstant
        {
            public const string SORTORDER_REGEX = $"^({ASCENDING}|{DESCENDING})$";
            public const string VALIDATE_SORTORDER = "Sort Order must be ascending or descending!";
        }

        #endregion ModelStateConstant

        #region Policy Attribute

        public const string ADMIN_POLICY = "Admin";

        public const string PATIENT_POLICY = "Patient";

        public const string PROVIDER_POLICY = "Provider";

        public const string ALL_USER_POLICY = "AllUser";

        #endregion Policy Attribute


        #region Session Constant

        public const string LOGGED_USER = "LoggedUser";

        public const string BEARER = "Bearer ";

        #endregion Session Constant
    }
}