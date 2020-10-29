using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedismartsAPI.Utilities
{
    public class JwtSettings
    {
        public string JwtKey { get; set; }
        public string TokenLifeTime { get; set; }
    }
    public class DbSettings
    {
        public string url { get; set; }
    }
}
