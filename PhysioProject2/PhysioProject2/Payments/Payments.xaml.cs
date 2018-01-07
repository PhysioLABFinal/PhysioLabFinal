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
    /// Interaction logic for Payments.xaml
    /// </summary>
    public partial class Payments : Page
    {

        OleDbConnection con;
        DataTable dt;

        public Payments()
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
            
            cmd.CommandText = "select Clients.Name,Clients.Surname,Appointments.AppPrice,Payments.Status,Payments.PayID from ((Payments inner join Appointments on Payments.AID=Appointments.AID) inner join Clients on Payments.CID=Clients.CID) where Status<>'Paid'";
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            datagrid1.ItemsSource = dt.AsDataView();

            cmd.CommandText = "select Clients.Name,Clients.Surname,Appointments.AppPrice,Payments.Status,Payments.PayID from ((Payments inner join Appointments on Payments.AID=Appointments.AID) inner join Clients on Payments.CID=Clients.CID) where Status='Paid'";
            da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            datagrid2.ItemsSource = dt.AsDataView();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            katastatikomina.Visibility = Visibility.Visible;

            String sDate = DateTime.Now.ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));

            int month = datevalue.Month;
            int year = datevalue.Year;
            int totalprice = 0;

            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;

            cmd.CommandText = "select Clients.Name,Clients.Surname,Appointments.AppDate,Appointments.AppPrice from Appointments inner join Clients on Appointments.CID=Clients.CID";
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            DataTable dt2 = new DataTable();
            dt2 = dt.Clone();
            foreach (DataRow row in dt.Rows)
            {
                String date = row["AppDate"].ToString();
                date.Split();
                string[] datearray = date.Split('/');
                int x = Int32.Parse(datearray[1]);

                string[] timestamp_split = datearray[2].Split(' ');
                int y = Int32.Parse(timestamp_split[0]);


                if (x == month & y == year)
                {
                    String price = row["AppPrice"].ToString();
                    dt2.ImportRow(row);
                    totalprice = totalprice + Int32.Parse(price);
                }
            }
            datagrid3.ItemsSource = dt2.AsDataView();    

            cmd.CommandText = "select Products.Name,Products.Company,Orders.OrderDate,Orders.FinalPrice from Orders inner join Products on Orders.ProID=Products.ProID";
            da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            dt2 = new DataTable();
            dt2 = dt.Clone();
            foreach (DataRow row in dt.Rows)
            {
                String date = row["OrderDate"].ToString();
                

                
                date.Split();
                string[] datearray = date.Split('/');
                int x = Int32.Parse(datearray[0]);

                string[] timestamp_split = datearray[2].Split(' ');
                int y = Int32.Parse(timestamp_split[0]);


                if (x == month & y == year)
                {
                    String price = row["FinalPrice"].ToString();
                    dt2.ImportRow(row);
                    totalprice = totalprice + Int32.Parse(price);
                }
                    
            }
            datagrid4.ItemsSource = dt2.AsDataView();
            sunoloText.Content = totalprice.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (datagrid1.SelectedItems.Count > 0)
            {
                DataRowView row = (DataRowView)datagrid1.SelectedItems[0];
                String payid = row["PayID"].ToString();
                String status = row["Status"].ToString();

                    OleDbCommand cmd = new OleDbCommand();
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.Connection = con;

                    cmd.CommandText = "update Payments set Status = 'Paid'  where Payid = payid";
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    datagrid1.ItemsSource = dt.AsDataView();
                    BindGrid();
                
            }
            else
            {
                MessageBox.Show("Παρακαλώ επιλέξτε από τη λίστα...");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.PrintDialog Printdlg = new System.Windows.Controls.PrintDialog();
            if ((bool)Printdlg.ShowDialog().GetValueOrDefault())
            {
                Size pageSize = new Size(Printdlg.PrintableAreaWidth, Printdlg.PrintableAreaHeight);
                // sizing of the element.
                datagrid3.Measure(pageSize);
                datagrid3.Arrange(new Rect(5, 5, pageSize.Width, pageSize.Height));
                Printdlg.PrintVisual(datagrid3, Title);
            }
        }
    }
}
