namespace HelloDoc_Common.Constants
{
    public class MessageConstants
    {
        #region Success_Messages
        public static class SuccessMessage
        {
            public const string Login_SUCCESS = "Login Successful!";

        }
        #endregion

        #region Error_Messages

        public static class ErrorMessage
        {
            public const string TRANSACTION_IS_ALREADY_RUNNING = "A transaction is already in progress.";
            public const string PAGE_SIZE = "Page size must be greater than or equal to 1.";
            public const string PAGE_NUMBER = "Page number must be greater than or equal to 1.";
            public const string INTERNAL_SERVER = "An error occurred while processing the request";
            public const string INVALID_ATTEMPT = "Invalid Attempt!";
            public const string VALID_CREDENTIALS = "Please enter valid user name and password!";
            public const string INVALID_MODELSTATE = "Invalid Entry";
        }
        #endregion
    }
}