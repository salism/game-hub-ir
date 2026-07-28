using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Features.Identity.DTOs.Requests;

namespace api.Features.Identity.Services
{
    public interface IResetPasswordService
    {
        Task SendResetPasswordEmailAsync(
            string email,
            CancellationToken cancellationToken);

        Task ResetPasswordAsync(
            ResetPasswordRequest request,
            CancellationToken cancellationToken);
    }
}