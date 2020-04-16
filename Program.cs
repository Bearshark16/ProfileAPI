using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ProfileAPI
{
    public class Program
    {
        public static string SystemPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.UseUrls("http://*:8080")
                .UseStartup<Startup>();
                //.UseUrls("http://4MF96H2:5001");

        public static string NameToUpper(string name)
        {
            List<string> split = new List<string>() { };

            char first;
            string upper;
            string result;

            name = name.ToLower();

            if (name.Contains(" ") || name.Contains('-'))
            {
                string[] stringSplit = null;

                if (name.Contains(" "))
                {
                    stringSplit = name.Split(' ');
                }
                else if (name.Contains('-'))
                {
                    stringSplit = name.Split('-');
                }

                foreach (string x in stringSplit)
                {
                    first = x[0];
                    upper = first.ToString().ToUpper();
                    string nameCaps = upper + x.Remove(0, 1);
                    split.Add(nameCaps);
                }

                result = string.Join(" ", split.ToArray());
            }
            else
            {
                first = name[0];
                upper = first.ToString().ToUpper();
                result = upper + name.Remove(0, 1);
            }

            return result;
        }
    }
}
