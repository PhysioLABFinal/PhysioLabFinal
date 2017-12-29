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
            this.Close();
            
        }

        private void productSaveButton_Click_1(object sender, RoutedEventArgs e)
        {

            con.Open();

            // DbCommand also implements IDisposable
            using (OleDbCommand cmd = con.CreateCommand())
            {
                // create command with placeholders
                cmd.CommandText =
                   "INSERT INTO Products " +
                   "([Name], [Company],  [PricePerUnit]) " +
                   "VALUES(@Name, @Company, @PricePerUnit)";

                // add named parameters
                String cmp = productCompanyTxt.Text;
                String nm = productNameTxt.Text;
                String ppr = productPriceTxt.Text;
                cmd.Parameters.AddRange(new OleDbParameter[]
                {

               new OleDbParameter("@Name", nm),
               new OleDbParameter("@Company", cmp),
               new OleDbParameter("@PricePerUnit",ppr)
               
           });

                // execute
                cmd.ExecuteNonQuery();


            }



            this.Close();


        }
    }
}
