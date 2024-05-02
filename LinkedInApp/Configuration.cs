using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedInApp;
using Microsoft.Extensions.Configuration;

namespace LinkedInApp
{
    class Configuration
    {

        public string DriverPath { get; }

        public Configuration()
        {
            DriverPath = LoadDriverPathFromConfiguration();
        }

        private string LoadDriverPathFromConfiguration()
        {
            var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("AppSettings.json");

            IConfigurationRoot config = builder.Build();
            return config["DriverPath"];
        }
    }
}
