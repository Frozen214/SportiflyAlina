using Sportifly.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sportifly.Infrastructure
{
    internal class ServiceLocator
    {
        public static IUserService UserService { get; set; }
        
    }
}
