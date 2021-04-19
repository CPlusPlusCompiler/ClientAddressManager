using ClientAddressManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAddressManager
{
    public class ClientsViewModel
    {
        public BindingList<Client> ClientsDataSource { get; set; }
        private readonly ClientsRepository clientsRepo;

        public ClientsViewModel(ClientsRepository repo)
        {
            clientsRepo = repo;
            InitData();
        }


        private async void InitData()
        {
            var clientsList = await clientsRepo.GetClients();
            ClientsDataSource = new BindingList<Client>(clientsList);
        }


        public async void SetData(List<Client> clients)
        {
            var success = clientsRepo.AddClients(clients);

            if(success)
            {
                var newData = await clientsRepo.GetClients();
                ClientsDataSource.Clear();
                
                foreach(var client in newData)
                {
                    ClientsDataSource.Add(client);
                }
            }
        }
    }
}
