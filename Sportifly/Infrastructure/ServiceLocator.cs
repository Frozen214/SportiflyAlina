using Sportifly.Interface;

namespace Sportifly.Infrastructure
{
    internal class ServiceLocator
    {
        public static IUserService UserService { get; set; }
    }
}
