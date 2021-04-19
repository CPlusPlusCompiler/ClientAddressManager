using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClientAddressManager
{
    public partial class ClientsControl : UserControl
    {
        private readonly ClientsViewModel viewModel;

        public ClientsControl(ClientsViewModel viewModel)
        {
            InitializeComponent();

            this.viewModel = viewModel;
            LoadData();
        }


        private void LoadData()
        {
            clientsTable.Columns.Clear();
            clientsTable.Rows.Clear();
            clientsTable.DataSource = viewModel.ClientsDataSource; 
        }


    }
}
