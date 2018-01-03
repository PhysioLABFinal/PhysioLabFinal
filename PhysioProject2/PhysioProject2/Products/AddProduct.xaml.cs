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
using System.Data.OleDb;
using System.Configuration;
using System.Configuration.Assemblies;

namespace PhysioProject2
{
    
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {

        OleDbConnection con;
        public AddProduct()
        {
            InitializeComponent();
            con = new OleDbConnection();
            con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=.\\PhysioDatabase.accdb"; //" + AppDomain.CurrentDomain.BaseDirectory + "




        }

        private void productSaveButton_Click(object sender, RoutedEventArgs e)
        {





        }

        private void productSaveButton_Click_1(object sender, RoutedEventArgs e)
        {

            OleDbCommand cmd = new OleDbCommand();

            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;


            cmd.CommandText = "insert into Products(Name,Company,PricePerUnit) Values('" + productNameTxt.Text + "','" + productCompanyTxt.Text + "','" + productPriceTxt.Text + "')";
            cmd.ExecuteNonQuery();
            Products x = new Products();
            x.ProductsDataGrid.Items.Refresh();
            this.Close();
            

        }
    }
    }

