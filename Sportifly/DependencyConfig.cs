using Sportifly.Infrastructure;
using Sportifly.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sportifly
{
    public class DependencyConfig
    {
        public static void ConfigureServices()
        {
            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://127.0.0.1:5239/")
            };

            ServiceLocator.UserService = new UserService(httpClient);
        }
    }
}
