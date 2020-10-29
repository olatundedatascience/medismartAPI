using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedismartsAPI.Utilities
{
    public class Response
    {
        public string statusCode { get; set; }
        public string description { get; set; }
        public string message { get; set; }
        public dynamic responseData { get; set; }
    }
}
