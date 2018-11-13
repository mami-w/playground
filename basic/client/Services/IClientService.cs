using System.Threading.Tasks;

namespace client
{
    public interface IClientService
    {
        Task<string[]> GetSomeData();
    }
}