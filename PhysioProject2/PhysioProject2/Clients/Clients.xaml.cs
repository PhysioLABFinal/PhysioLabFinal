using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhysioProject2
{
    /// <summary>
    /// Interaction logic for Clients.xaml
    /// </summary>
    public partial class Clients : Page
    {

		OleDbConnection con;
		DataTable dt;

		public Clients()
        {
            InitializeComponent();
            this.DataContext = this;

			con = new OleDbConnection();
			con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\PhysioDatabase.accdb";
			BindGrid();
		}

		private void BindGrid()
		{
			OleDbCommand cmd = new OleDbCommand();
			if (con.State != ConnectionState.Open)
				con.Open();
			cmd.Connection = con;
			cmd.CommandText = "select * from ClientTableDB";
			OleDbDataAdapter da = new OleDbDataAdapter(cmd);
			dt = new DataTable();
			da.Fill(dt);
			ClientdataGrid.ItemsSource = dt.AsDataView();
		}

		private void SearchTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchTxt.Text = "";
        }

       

        private void ClientdataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ClientInfo info = new ClientInfo();
            info.Show();
        }

        private void addclieck(object sender, RoutedEventArgs e)
        {
            Window1 win = new Window1();
            win.Show();
        }
    }
}
