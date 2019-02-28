using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagement.Api.Config
{
    public class ApiSettings
    {
        public string[] CORS { get; set; }
        public bool LogRequests { get; set; }
        public bool LogResponses { get; set; }
        public Dictionary<string, string> ConnectionStrings { get; set; }
    }
}
