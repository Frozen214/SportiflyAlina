using Sportifly.API.Model;

namespace Sportifly.API.Interface;

public interface IServiceRepository
{
    public Task<IEnumerable<ServiceModel>> GetServiceAll();
}

