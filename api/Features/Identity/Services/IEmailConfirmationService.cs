using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Features.Identity.Services
{
    public interface IEmailConfirmationService
    {
        Task SendConfirmationEmailAsync(
            CancellationToken cancellationToken);

        Task ConfirmEmailAsync(
            string token,
            CancellationToken cancellationToken);
    }
}