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


        private void OnUpdatePostCodesClicked()
        {

        }


        private void OnImportClicked()
        {
            var models = ImportUtil<Client>.ImportModelsFromFileDialog();

            // file dialog was cancelled, do nothing
            if (models == null)
                return;

            if(models.Code == ResultCode.ERROR)
            {
                var message = models.Error ?? "Nežinoma klaida";
                var buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, "", buttons);
            }
            else
            {
                viewModel.SetData(models.Data);
            }
        }
    }



}
