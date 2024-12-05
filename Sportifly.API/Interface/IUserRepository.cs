using Sportifly.API.Model;

namespace Sportifly.API.Interface;

public interface IUserRepository
{
    public Task<IEnumerable<UserModel>> GetUserAll();
}
