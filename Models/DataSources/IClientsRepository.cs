using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClientAddressManager.Models
{
    public interface IClientsRepository
    {
        public Task<List<Client>> GetClientsAsync();

        public Task<List<Client>> GetClientsWithoutPostCodesAsync();

        public bool AddClients(List<Client> clients);
        public int GetClientCount();
        public bool UpdateClients(List<Client> clients);
    }
}
