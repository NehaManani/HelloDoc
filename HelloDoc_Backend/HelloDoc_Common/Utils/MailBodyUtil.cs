using HelloDoc_Common.Constants;

namespace HelloDoc_Common.Utils
{
    public class MailBodyUtil
    {
        public static string SendOtpForAuthenticationBody(string otp, string mailTemplateLink)
        {
            string filePath = Path.Combine(mailTemplateLink, SystemConstants.MAIL_TEMPLATES, SystemConstants.OTP_MAIL_TEMPLATE_FILE);

            string body = File.ReadAllText(filePath);
            body = body.Replace("{otp}", otp);
            return CreateMessage(body);
        }

        public static string SendResetPasswordLink(string link, string mailTemplateLink)
        {
            string filePath = Path.Combine(mailTemplateLink, SystemConstants.MAIL_TEMPLATES, SystemConstants.RESET_PASSWORD_MAIL_TEMPLATE_FILE);

            string body = File.ReadAllText(filePath);
            body = body.Replace("{link}", link);
            return CreateMessage(body);
        }

        private static string CreateMessage(string body)
        {
            return body;
        }
    }
}