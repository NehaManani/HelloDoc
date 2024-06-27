using HelloDoc_Entities.DTOs.Common;

namespace HelloDoc_BusinessAccessLayer.IServices
{
    public interface IMailService
    {
        Task SendMailAsync(MailDTO mailData, CancellationToken cancellationToken = default);
    }
}