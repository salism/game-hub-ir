using api.Settings;
using Microsoft.Extensions.Options;
using Resend;

namespace api.Features.Identity.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IResend _resend;
        private readonly EmailSettings _emailSettings;

        public EmailSenderService(IOptions<EmailSettings> emailSettings, IResend resend)
        {
            _resend = resend;
            _emailSettings = emailSettings.Value;
        }

        public async Task SendAsync(
        string to,
        string subject,
        string body,
        CancellationToken cancellationToken)
        {
            EmailMessage email = new()
            {
              From = new EmailAddress
              {
                  Email = _emailSettings.FromEmail,
                  DisplayName = _emailSettings.FromName
              },

              To = to,

              Subject = subject,

              HtmlBody = body 
            };

            await _resend.EmailSendAsync(email, cancellationToken: cancellationToken);
        }
    }
}