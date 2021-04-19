using ClientAddressManager.Utils;
using System;
using System.Windows.Forms;


namespace ClientAddressManager
{
    public partial class MainForm : Form
    {
        private readonly ClientsViewModel viewModel;

        public MainForm(ClientsViewModel viewModel)
        {
            this.viewModel = viewModel;
            InitializeComponent();

            var homeControl = new HomeControl()
            {
                Dock = DockStyle.Fill
            };
            panel.Controls.Add(homeControl);

            // binding menu click events
            menuImport.Click += (s, e) => OnImportClicked();
            menuClients.Click += (s, e) => OnClientListClicked();
            menuUpdateCodes.Click += (s, e) => OnUpdatePostCodesClicked();
        }


        private void OnClientListClicked()
        {
            var clientsControl = new ClientsControl(viewModel)
            {
                Dock = DockStyle.Fill
            };
            panel.Controls.Clear();
            panel.Controls.Add(clientsControl);
        }


        private async void OnUpdatePostCodesClicked()
        {
            var result = await viewModel.UpdatePostIndexes();

            if(result.Code == ResultCode.ERROR)
            {
                MessageBox.Show(result.Error, Strings.ERROR_OCCURED, MessageBoxButtons.OK); 
            }
            else
            {
                var message = "Atnaujinti " + result.Data + " pašto kodai.";
                MessageBox.Show(message, Strings.SUCCESS_OPERATION, MessageBoxButtons.OK); 
            }
        }


        private void OnImportClicked()
        {
            var models = ImportUtil<Client>.ImportModelsFromFileDialog();

            // file dialog was cancelled, do nothing
            if (models == null)
                return;

            if(models.Code == ResultCode.ERROR)
                MessageBox.Show(models.Error, Strings.ERROR_OCCURED, MessageBoxButtons.OK);
            else
                viewModel.SetDataAsync(models.Data);
        }
    }



}
