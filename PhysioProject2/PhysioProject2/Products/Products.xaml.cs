using System;
using System.Collections.Generic;
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
using System.Data;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace PhysioProject2
{
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Products : Page
    {
        OleDbConnection con;
        DataTable dt;

        public Products()
        {
            InitializeComponent();
            
            this.DataContext = this;


            con = new OleDbConnection();
            con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=.\\PhysioDatabase.accdb"; //" + AppDomain.CurrentDomain.BaseDirectory + "
            BindGrid();

        }

        private void BindGrid()
        {
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from Products";
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            ProductsDataGrid.ItemsSource = dt.AsDataView();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
