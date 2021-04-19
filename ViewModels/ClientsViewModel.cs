using ClientAddressManager.Models;
using ClientAddressManager.Utils;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ClientAddressManager
{
    public class ClientsViewModel
    {
        public BindingList<Client> ClientsDataSource { get; set; }

        private readonly IClientsRepository clientsRepo;
        private readonly IPostCodeService postService;

        public ClientsViewModel(IClientsRepository repo, IPostCodeService service)
        {
            clientsRepo = repo;
            postService = service;
            InitData();
        }


        private async void InitData()
        {
            var clientsList = await clientsRepo.GetClientsAsync();
            ClientsDataSource = new BindingList<Client>(clientsList);
        }


        public async void SetDataAsync(List<Client> clients)
        {
            var success = clientsRepo.AddClients(clients);

            if (success)
            {
                var newData = await clientsRepo.GetClientsAsync();
                LoadViewData(newData);     
            }
        }


        public void LoadViewData(List<Client> clients)
        {
            ClientsDataSource.Clear();

            foreach (var client in clients)
            {
                ClientsDataSource.Add(client);
            }
        }


        public async Task<Result<int>> UpdatePostIndexes()
        {
            if (clientsRepo.GetClientCount() <= 0)
                return new Result<int>(ResultCode.ERROR, default, Strings.ERROR_NO_CLIENTS);

            // todo BAD, this block should be in a bussiness layer class
            var clients = await clientsRepo.GetClientsWithoutPostCodesAsync();

            foreach (var client in clients)
            {
                var result = await postService.GetPostCodeAsync(client.Address);

                if (result.Code == ResultCode.ERROR)
                    return new Result<int>(ResultCode.ERROR, default, result.Error);

                client.PostCode = result.Data;
            }

            var updateSuccessful = clientsRepo.UpdateClients(clients);

            if (!updateSuccessful)
            {
                return new Result<int>(ResultCode.ERROR, default, Strings.ERROR_DATABASE);
            }
            else
            {
                var updatedData = await clientsRepo.GetClientsAsync();
                LoadViewData(updatedData);

                return new Result<int>(ResultCode.SUCCESS, clients.Count, null);
            }
        }
    }
}
