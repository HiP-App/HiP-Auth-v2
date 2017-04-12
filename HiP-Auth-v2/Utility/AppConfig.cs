using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace PaderbornUniversity.SILab.Hip.Auth.Utility
{
    public class AppConfig
    {
        public AppConfig(IConfiguration configuration)
        {
            Port = configuration.GetValue<int>("Port");
            User = configuration.GetValue<string>("User");
            UserPassword = configuration.GetValue<string>("User-Password");
            UserSecret = configuration.GetValue<string>("User-Secret");
        }

        public string UserPassword { get; set; }

        public string User { get; set; }

        public int Port { get; set; }

        public string UserSecret { get; set; }
    }
}
