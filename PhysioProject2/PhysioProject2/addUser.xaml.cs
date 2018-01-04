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
using System.Data.OleDb;

namespace PhysioProject2
{
    /// <summary>
    /// Interaction logic for addUser.xaml
    /// </summary>
    public partial class addUser : Window
    {
        public addUser()
        {
            InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			string constring = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=.\\PhysioDatabase.accdb"; //" + AppDomain.CurrentDomain.BaseDirectory + "
			string cmdText = "INSERT INTO Users(Username,Password) VALUES('" + newUsernameTB.Text +"','" + newPasswordTB.Password.ToString() + "')";
			using (OleDbConnection con = new OleDbConnection(constring))
			using (OleDbCommand cmd = new OleDbCommand(cmdText, con))
			{
				try
				{ 				
					con.Open();
					if (newPasswordTB.Password.ToString() == PasswordAgainTB.Password.ToString())
					{

                        cmd.ExecuteNonQuery();
						MessageBox.Show("Επιτυχής προσθήκη χρήστη");
						this.Close();
					}
					else
					{
						MessageBox.Show("Η επαλήθευση κωδικού δεν έγινε με επιτυχία");
					}
				}
				catch
				{
					MessageBox.Show("Σφάλμα");
				}
			}
		}
	}
	
}
