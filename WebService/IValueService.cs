using Microsoft.ServiceFabric.Services.Remoting;
using System.Threading.Tasks;

namespace WebService
{
    public interface IValueService: IService
    {
        Task<string> GetValueAsync();
    }
}
