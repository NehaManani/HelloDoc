using HelloDoc_BusinessAccessLayer.IServices;
using HelloDoc_Common.Constants;
using HelloDoc_Entities.DTOs.Common;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace HelloDoc_BusinessAccessLayer.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettingsDTO _mailSetting;
        public MailService(IOptions<MailSettingsDTO> mailSetting)
        {
            _mailSetting = mailSetting.Value;
        }

        public async Task SendMailAsync(MailDTO mailData, CancellationToken cancellationToken = default)
        {
            MimeMessage email = new();
            email.Sender = MailboxAddress.Parse(_mailSetting.Mail);
            email.To.Add(MailboxAddress.Parse(mailData.ToEmail));
            email.Subject = !String.IsNullOrEmpty(mailData.Subject) ? mailData.Subject : EmailConstants.GENERIC_SUBJECT;

            BodyBuilder? builder = new BodyBuilder();
            if (mailData.Attachments != null && mailData.Attachments.Count != 0)
            {
                byte[] fileBytes;
                foreach (var file in mailData.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailData.Body;
            email.Body = builder.ToMessageBody();

            using SmtpClient? smtp = new SmtpClient();
            smtp.Connect(_mailSetting.Host, _mailSetting.Port, SecureSocketOptions.StartTls, cancellationToken);
            smtp.Authenticate(_mailSetting.Mail, _mailSetting.Password, cancellationToken);
            await smtp.SendAsync(email, cancellationToken);
            smtp.Disconnect(true, cancellationToken);
        }
    }
}