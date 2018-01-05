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



namespace PhysioProject2
{
    /// <summary>
    /// Interaction logic for Appointments.xaml
    /// </summary>
    public partial class Appointments : Page
    {


        OleDbConnection con;
        DataTable dt;
        OleDbDataReader odd;
        

        public Appointments()
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
            //Clients.Cname, Appointments.AppDate, Appointments.TreatmentOrTherapy, Appointments.AppPrice
            cmd.Connection = con;
            cmd.CommandText = "SELECT Appointments.AID, Clients.CID, Clients.Name, Clients.Surname, Appointments.AppDate, Appointments.AppTime, Appointments.AppEndTime, Clients.Telephone ,Appointments.TreatmentOrTherapy, Appointments.AppPrice FROM (Clients INNER JOIN Appointments on Clients.CID = Appointments.CID) WHERE AppStatus='pending'";
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            DataGrid1.ItemsSource = dt.AsDataView();
            


        }


        private void SearchTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            ApSearch.Text = "";
        }

        /* private void rep_bind()
         {
             String query = "Select * from Clients where Name like'" + ApName.Text + "%'";

             OleDbDataAdapter da = new OleDbDataAdapter(query, con);
             DataSet ds = new DataSet();
             da.Fill(ds);



         }
         */


        private void ClearAll()
        {
            AppID.Text = "";
            ApID.Text = "";
            ApName.Text = "";
            ApLast.Text = "";
            ApTreatment.Text = "";
            ApStart.Text = "";
            ApEnd.Text = "";
            ApDate.Text = "";
            ApCost.Text = "";


        }


        private void ApSearch_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = string.Format("Surname LIKE '{0}*'", ApSearch.Text);
            DataGrid1.ItemsSource = dv;

        }

        private void Add_Click_1(object sender, RoutedEventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;

          
            if (ApTreatment.Text!="" && ApStart.Text != "" && ApEnd.Text != "" && ApDate.Text != "" && ApCost.Text != "")
            

            { 
                    cmd.CommandText = "insert into Appointments(CID,AppDate,AppTime,AppEndTime,AppPrice,AppStatus,TreatmentOrTherapy) Values('" + ApID.Text + "','" + ApDate.Text + "','" + ApStart.Text + "','" + ApEnd.Text + "','" + ApCost.Text + "','pending','" + ApTreatment.Text + "')";
                    cmd.ExecuteNonQuery();
                    BindGrid();
                    MessageBox.Show("Ο πελάτης προστέθηκε με επιτυχία!!");
                    ClearAll();
                    Add.IsEnabled = false;

            }
            else
            {
                
                MessageBox.Show("Δεν συμπληρώσατε όλα τα παραπάνω στοιχεία");

            }
            
            ApID.IsEnabled = true;

        }

        private void ApEdit_Click(object sender, RoutedEventArgs e)
        {

            
            if (DataGrid1.SelectedItems.Count > 0)
            {
                AppEditOK.IsEnabled = true;
                ApTreatment.IsEnabled = true;
                ApStart.IsEnabled = true;
                ApEnd.IsEnabled = true;
                ApCost.IsEnabled = true;
                ApDate.IsEnabled = true;
                ApID.IsEnabled = false;
               
                DataRowView row = (DataRowView)DataGrid1.SelectedItems[0];
                AppID.Text = row["AID"].ToString();
                ApID.Text = row["CID"].ToString();
                ApLast.Text = row["Surname"].ToString();
                ApName.Text = row["Name"].ToString();
                ApDate.Text = row["AppDate"].ToString();
                ApTreatment.Text = row["TreatmentOrTherapy"].ToString();
                ApStart.Text = row["AppTime"].ToString();
                ApEnd.Text = row["AppEndTime"].ToString();
                ApCost.Text = row["AppPrice"].ToString();
                
            }
            else
            {
                MessageBox.Show("Παρακαλώ επιλέξτε ένα πελάτη από τη λίστα...");
            }
            
        }

        private void Completed_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedItems.Count > 0)
            {
                DataRowView row = (DataRowView)DataGrid1.SelectedItems[0];

                OleDbCommand cmd = new OleDbCommand();
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.Connection = con;
                cmd.CommandText = "update Appointments set AppStatus='ended' where AID=" + row["AID"].ToString();
                cmd.ExecuteNonQuery();
                BindGrid();
                MessageBox.Show("Το ραντεβού ολοκληρώθηκε!");
                ClearAll();
            }
            else
            {
                MessageBox.Show("Παρακαλώ επιλέξτε ένα πελάτη από τη λίστα...");
            }
        }

        private void ApName_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = string.Format("Surname LIKE '{0}*'", ApName.Text);
            DataGrid1.ItemsSource = dv;
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand();
            string selectQuery = "SELECT * FROM Clients INNER JOIN Appointments WHERE Clients.CID=" + (ApID.Text);
            if (ApID.Text != "") { 
            cmd.Connection = con;
            cmd = new OleDbCommand(selectQuery, con);
            odd = cmd.ExecuteReader();
           
            if (odd.Read())
            {
                AppID.Text = odd["AID"].ToString();
                ApName.Text = odd["Name"].ToString();
                ApLast.Text = odd["Surname"].ToString();
                ApTreatment.IsEnabled = true;
                ApStart.IsEnabled = true;
                ApEnd.IsEnabled = true;
                ApCost.IsEnabled = true;
                ApDate.IsEnabled = true;



                Add.IsEnabled = true;
            }
            else
            {
                ApID.Text = "";
                ApName.Text = "";
                ApLast.Text = "";
                ApTreatment.Text = "";
                ApStart.Text = "";
                ApEnd.Text = "";
                ApDate.Text = "";
                ApCost.Text = "";

                MessageBox.Show("Δεν Υπάρχει Εγγραφή με αυτό το ID");

            }

            }
            else
            {

                MessageBox.Show("Τοποθέτησε το ID του πελάτη για να προχωρήσεις");


            }

        }

        private void ApSearchID_Click(object sender, RoutedEventArgs e)
        {
			DataGrid1.Visibility = Visibility.Visible;
            OleDbCommand cmd = new OleDbCommand();
            string selectQuery = "SELECT * FROM Clients LEFT JOIN Appointments on Clients.CID = Appointments.CID WHERE Clients.CID=" + ApID.Text;
            if (ApID.Text != "")
            {
                cmd.Connection = con;
                cmd = new OleDbCommand(selectQuery, con);
                odd = cmd.ExecuteReader();

                if (odd.Read())
                {
                    AppID.Text = odd["AID"].ToString();
                    ApName.Text = odd["Name"].ToString();
                    ApLast.Text = odd["Surname"].ToString();
                    ApTreatment.IsEnabled = true;
                    ApStart.IsEnabled = true;
                    ApEnd.IsEnabled = true;
                    ApCost.IsEnabled = true;
                    ApDate.IsEnabled = true;



                    Add.IsEnabled = true;
                }
                else
                {
                    AppID.Text="";
                    ApID.Text = "";
                    ApName.Text = "";
                    ApLast.Text = "";
                    ApTreatment.Text = "";
                    ApStart.Text = "";
                    ApEnd.Text = "";
                    ApDate.Text = "";
                    ApCost.Text = "";

                    MessageBox.Show("Δεν Υπάρχει Εγγραφή με αυτό το ID");

                }

            }
            else
            {

                MessageBox.Show("Τοποθέτησε το ID του πελάτη για να προχωρήσεις");


            }
        }

        private void AppEditOK_Click(object sender, RoutedEventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;

            if (ApName!=null)


            {

                cmd.CommandText = "update Appointments set AppDate='" + ApDate.Text + "',AppTime='" + ApStart.Text + "',AppEndTime='" + ApEnd.Text + "',AppPrice='" + ApCost.Text + "',TreatmentOrTherapy='" + ApTreatment.Text +"' where AID=" + AppID.Text;
                cmd.ExecuteNonQuery();
                BindGrid();
                MessageBox.Show("Οι αλλαγές έγιναν με επιτυχία!!");
                ClearAll();
            }
            else
            {
                MessageBox.Show("Δεν συμπλήρώσατε τα παραπάνω στοιχεία");
            }
          
        }

		private void DataGrid1_AutoGeneratedColumns(object sender, EventArgs e)
		{
			DataGrid1.Columns[1].Visibility = Visibility.Hidden;
			DataGrid1.Columns[7].Visibility = Visibility.Hidden;
			DataGrid1.Columns[8].Visibility = Visibility.Hidden;
		}
	}
}
