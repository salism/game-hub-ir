using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Features.Identity.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly ILogger<EmailSenderService> _logger;

        public EmailSenderService(ILogger<EmailSenderService> logger)
        {
            _logger = logger;
        }

        public Task SendAsync(
        string to,
        string subject,
        string body,
        CancellationToken cancellationToken)
        {
            _logger.LogInformation("""
            ===== EMAIL =====
            To: {To}
            Subject: {Subject}

            {Body}
            =================
            """, to, subject, body);

            return Task.CompletedTask;
        }
    }
}