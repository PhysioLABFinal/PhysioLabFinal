﻿using System;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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


		private void SearchTxt_KeyDown(object sender, KeyEventArgs e)
		{
			DataView dv = dt.DefaultView;
			dv.RowFilter = string.Format("CSurname like '*{0}*'", SearchTxt.Text);
			ClientdataGrid.ItemsSource = dv;
		}

		private void addClient_Click(object sender, RoutedEventArgs e)
		{
			OleDbCommand cmd = new OleDbCommand();
			if (con.State != ConnectionState.Open)
				con.Open();
			cmd.Connection = con;

			if (clientIDTxt.Text != "")
			{
				if (clientIDTxt.IsEnabled == true)
				{
					cmd.CommandText = "insert into ClientTableDB(CID,Cname,CSurname,Birthday,Telephone,Doctor,Diagnosis) Values(" + clientIDTxt.Text + ",'" + clientNameTxt.Text + "','" + clientSurnameTxt.Text + "','" + clientBirthdayTxt.Text + "','" + clientPhoneTxt.Text + "','" + clientDoctorTxt.Text + "','" + clientDiagnosisTxt.Text + "')";
					cmd.ExecuteNonQuery();
					BindGrid();
					MessageBox.Show("Ο νέος πελάτης προστέθηκε με επιτυχία!!");
					ClearAll();
				}
				else
				{
					cmd.CommandText = "update ClientTableDB set Cname='" + clientNameTxt.Text + "',CSurname='" + clientSurnameTxt.Text + "',Birthday='" + clientBirthdayTxt.Text + "',Telephone='" + clientPhoneTxt.Text + "',Doctor='" + clientDoctorTxt.Text + "',Diagnosis='" + clientDiagnosisTxt.Text + "' where CID=" + clientIDTxt.Text;
					cmd.ExecuteNonQuery();
					BindGrid();
					MessageBox.Show("Οι αλλαγές έγιναν με επιτυχία!!");
					ClearAll();
				}
			}
			else
			{
				MessageBox.Show("Παρακαλώ προσθέστε ID πελάτη.......");
			}
		}

		private void ClearAll()
		{
			clientIDTxt.Text = "";
			clientNameTxt.Text = "";
			clientSurnameTxt.Text = "";
			clientBirthdayTxt.Text = "";
			clientPhoneTxt.Text = "";
			clientDoctorTxt.Text = "";
			clientDiagnosisTxt.Text = "";
			addClient.Content = "Add";
			clientIDTxt.IsEnabled = true;
		}

		private void editClient_Click(object sender, RoutedEventArgs e)
		{
			if (ClientdataGrid.SelectedItems.Count > 0)
			{
				DataRowView row = (DataRowView)ClientdataGrid.SelectedItems[0];
				clientIDTxt.Text = row["CID"].ToString();
				clientNameTxt.Text = row["Cname"].ToString();
				clientSurnameTxt.Text = row["CSurname"].ToString();
				clientBirthdayTxt.Text = row["Birthday"].ToString();
				clientPhoneTxt.Text = row["Telephone"].ToString();
				clientDoctorTxt.Text = row["Doctor"].ToString();
				clientDiagnosisTxt.Text = row["Diagnosis"].ToString();
				clientIDTxt.IsEnabled = false;
				addClient.Content = "Update";
			}
			else
			{
				MessageBox.Show("Παρακαλώ επιλέξτε ένα πελάτη από τη λίστα...");
			}
		}


	}
}
