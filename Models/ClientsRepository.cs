using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAddressManager.Models
{
    public class ClientsRepository
    {
        private readonly DatabaseContext database;

        public ClientsRepository(DatabaseContext database)
        {
            this.database = database;
        }

        public async Task<List<Client>> GetClients()
        {
            return await database.Clients.ToListAsync();
        }


        public bool AddClients(List<Client> clients)
        {
            try
            {
                foreach (var client in clients)
                {
                    // checking for repeating data. would use Equals() but Id does not matter here.
                    if (!database.Clients.Any(c => c.Name.Equals(client.Name) && c.Address.Equals(client.Address)))
                    {
                        database.Clients.Add(client);
                    }
                }
                database.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }



    }
}
