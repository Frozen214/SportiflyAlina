using Sportifly.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sportifly.Interface
{
    public interface IUserService
    {
        Task<List<UserModel>> GetUsersAsyc();
    }
}
