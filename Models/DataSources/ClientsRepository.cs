using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ClientAddressManager.Models
{
    public class ClientsRepository : IClientsRepository
    {
        private readonly DatabaseContext database;

        public ClientsRepository(DatabaseContext database)
        {
            this.database = database;
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            try
            {
                return await database.Clients.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
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


        public int GetClientCount()
        {
            try
            {
                return database.Clients.Count();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return -1;
        }


        public bool UpdateClients(List<Client> clients)
        {
            try
            {
                database.Clients.UpdateRange(clients);
                database.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return false;
        }


        public async Task<List<Client>> GetClientsWithoutPostCodesAsync()
        {
            try
            {
                return await database.Clients
                    .Where(c => (c.PostCode == null || c.PostCode.Length == 0))
                    .ToListAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
    }
}
