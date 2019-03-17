using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagement.Api.Config
{
    public class SecuritySettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }
}
