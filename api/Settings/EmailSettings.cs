using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Settings
{
    public sealed class EmailSettings
    {
        public string ApiKey { get; set; } = null!;

        public string FromEmail { get; set; } = null!;

        public string FromName { get; set; } = null!;
    }
}