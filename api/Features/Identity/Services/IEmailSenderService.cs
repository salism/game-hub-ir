using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Features.Identity.Services
{
    public interface IEmailSenderService
    {
        Task SendAsync(
        string to,
        string subject,
        string body,
        CancellationToken cancellationToken);
    }
}