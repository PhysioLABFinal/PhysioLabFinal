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
using System.Windows.Shapes;
using System.Data;
using System.Data.OleDb;

namespace PhysioProject2
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {   
            string  constring = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=.\\PhysioDatabase.accdb"; //" + AppDomain.CurrentDomain.BaseDirectory + "
            string cmdText = "select Count(*) from Users where Username=? and [Password]=?";
            using (OleDbConnection con = new OleDbConnection(constring))
            using (OleDbCommand cmd = new OleDbCommand(cmdText, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@p1", UsernameTB.Text);
                cmd.Parameters.AddWithValue("@p2", PasswordTB.Text);  // <- is this a variable or a textbox?
                int result = (int)cmd.ExecuteScalar();
                if (result > 0) { 
                    MessageBox.Show("Επιτυχής Σύνδεση, Καλώς Ορισάτε");
                    MainWindow obj = new MainWindow();
                    obj.Show();
                    this.Close();
                     }
                else
                    MessageBox.Show("Λάθος Όνομα Χρήστη/Κωδικός");
            }
        }
    }
}
