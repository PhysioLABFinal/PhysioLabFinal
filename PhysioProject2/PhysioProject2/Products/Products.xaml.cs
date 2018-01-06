﻿using System;
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

			moreInfo.Visibility = Visibility.Hidden;
			productIDTxt.IsEnabled = false;
			ordersGrid.Visibility = Visibility.Hidden;
			ordersLbl.Visibility = Visibility.Hidden;
			addOrder.Visibility = Visibility.Hidden;
		}

        public void BindGrid()
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

		private void ProductsDataGrid_AutoGeneratedColumns(object sender, EventArgs e)
		{
			ProductsDataGrid.Columns[3].Visibility = Visibility.Hidden;
		}

		private void productAddBtn_Click(object sender, RoutedEventArgs e)
		{
			ClearAll();
			moreInfo.Visibility = Visibility.Visible;
			ordersGrid.Visibility = Visibility.Hidden;
			ordersLbl.Visibility = Visibility.Hidden;
			addOrder.Visibility = Visibility.Hidden;
		}

		private void productSaveButton_Click(object sender, RoutedEventArgs e)
		{
			OleDbCommand cmd = new OleDbCommand();

			if (con.State != ConnectionState.Open)
				con.Open();
			cmd.Connection = con;

			if (productNameTxt.Text != "" && productCompanyTxt.Text != "" && productPriceTxt.Text != "")
			{
				if (productIDTxt.Text == "")
				{

					cmd.CommandText = "insert into Products(Name,Company,PricePerUnit) Values('" + productNameTxt.Text + "','" + productCompanyTxt.Text + "','" + productPriceTxt.Text + "')";
					cmd.ExecuteNonQuery();
					BindGrid();


					MessageBox.Show("Το νέο προιόν προστέθηκε με επιτυχία!!");

					ClearAll();
				}
				else
				{
					cmd.CommandText = "update Products set Name='" + productNameTxt.Text + "',Company='" + productCompanyTxt.Text + "',PricePerUnit='" + productPriceTxt.Text + "' where ProID=" + productIDTxt.Text;
					cmd.ExecuteNonQuery();
					BindGrid();
					MessageBox.Show("Οι αλλαγές έγιναν με επιτυχία!!");
					ClearAll();
				}
			}
			else
			{
				MessageBox.Show("Δεν συμπληρώσατε όλα τα παραπάνω στοιχεία.......");
			}
		}

		private void ClearAll()
		{
			productIDTxt.Text = "";
			productNameTxt.Text = "";
			productCompanyTxt.Text = "";
			productNameTxt.Text = "";
			moreInfo.Visibility = Visibility.Hidden;
			ordersGrid.Visibility = Visibility.Hidden;
			ordersLbl.Visibility = Visibility.Hidden;
			addOrder.Visibility = Visibility.Hidden;
		}

		private void deleteProductBtn_Click(object sender, RoutedEventArgs e)
		{
			if (ProductsDataGrid.SelectedItems.Count > 0)
			{
				DataRowView row = (DataRowView)ProductsDataGrid.SelectedItems[0];

				OleDbCommand cmd = new OleDbCommand();
				if (con.State != ConnectionState.Open)
					con.Open();
				cmd.Connection = con;
				cmd.CommandText = "delete from Products where ProID=" + row["ProID"].ToString();
				cmd.ExecuteNonQuery();
				BindGrid();
				MessageBox.Show("Το προιόν διαγράφηκε με επιτυχία!!");
				ClearAll();
			}
			else
			{
				MessageBox.Show("Παρακαλώ επιλέξτε ένα προιόν από τη λίστα...");
			}
		}

		private void editProduct_Click(object sender, RoutedEventArgs e)
		{
			if (ProductsDataGrid.SelectedItems.Count > 0)
			{
				moreInfo.Visibility = Visibility.Visible;
				ordersGrid.Visibility = Visibility.Visible;
				ordersLbl.Visibility = Visibility.Visible;
				addOrder.Visibility = Visibility.Visible;
				DataRowView row = (DataRowView)ProductsDataGrid.SelectedItems[0];
				productIDTxt.Text = row["ProID"].ToString();
				productNameTxt.Text = row["Name"].ToString();
				productCompanyTxt.Text = row["Company"].ToString();
				productPriceTxt.Text = row["PricePerUnit"].ToString();

				
			}
			else
			{
				MessageBox.Show("Παρακαλώ επιλέξτε ένα προιόν πελάτη από τη λίστα...");
			}
		}

		private void SearchProductsTB_TextChanged(object sender, TextChangedEventArgs e)
		{
			DataView dv = new DataView(dt);
			dv.RowFilter = string.Format("Name LIKE '{0}*'", SearchProductsTB.Text);
			ProductsDataGrid.ItemsSource = dv;
		}

		private void ordersGrid_AutoGeneratedColumns(object sender, EventArgs e)
		{
			OleDbCommand cmd = new OleDbCommand();

			if (con.State != ConnectionState.Open)
				con.Open();
			cmd.Connection = con;

			cmd.CommandText = "select * from Orders where ProID=" + productIDTxt.Text;
			cmd.ExecuteNonQuery();
			OleDbDataAdapter da = new OleDbDataAdapter(cmd);
			dt = new DataTable();
			da.Fill(dt);
			ordersGrid.ItemsSource = dt.AsDataView();

			ordersGrid.Columns[1].Visibility = Visibility.Hidden;
		}
	}
}
