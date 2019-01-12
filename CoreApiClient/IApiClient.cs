using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiModels;

namespace CoreApiClient
{
    public interface IApiClient
    {
        Task<IEnumerable<T>> GetApiObjects<T>();

        Task PostApiObject<T>(T model);

        Task EditApiObject<T>(int id, T model);

        Task DeleteApiObject(int id);
    }
}
