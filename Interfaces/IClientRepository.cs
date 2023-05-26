using System.Threading.Tasks;

namespace LegacyApp.Interfaces
{
    public interface IClientRepository
    {
        public IClient GetById(int clientid);
        public Task<IClient> GetByIdAsync(int clientid);
    }
}