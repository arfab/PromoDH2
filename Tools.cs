using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace PromoDH
{
    public class Tools
    {
        public static string GetConnectionString(string name = "DefaultConnection")
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            IConfigurationSection sConnString = configuration.GetSection("DB").GetSection("conn");

            return sConnString.Value;

        }

    }
}
