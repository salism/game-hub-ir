using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Features.Identity.Services
{
    public interface ICurrentUserService
    {
         string UserId { get; }

        string Username { get; }

        string Email { get; }
    }
}