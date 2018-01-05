﻿using System;
using System.Data;
using System.Data.OleDb;
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

			MoreInfo.Visibility = Visibility.Hidden;
			clientsAppData.Visibility = Visibility.Hidden;
			appointmentsLbl.Visibility = Visibility.Hidden;
			clientIDTxt.IsEnabled = false;

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
			cmd.CommandText = "select * from Clients";
			OleDbDataAdapter da = new OleDbDataAdapter(cmd);
			dt = new DataTable();
			da.Fill(dt);
			ClientdataGrid.ItemsSource = dt.AsDataView();
        }

		private void SearchTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchTxt.Text = "";
        }


		private void addClient_Click(object sender, RoutedEventArgs e)
		{
			ClearAll();
			MoreInfo.Visibility = Visibility.Visible;
			clientsAppData.Visibility = Visibility.Hidden;
			appointmentsLbl.Visibility = Visibility.Hidden;
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
			clientCommentsTxt.Text = "";
			MoreInfo.Visibility = Visibility.Hidden;
			clientsAppData.Visibility = Visibility.Hidden;
			appointmentsLbl.Visibility = Visibility.Hidden;
		}

		private void editClient_Click(object sender, RoutedEventArgs e)
		{
			if (ClientdataGrid.SelectedItems.Count > 0)
			{
				MoreInfo.Visibility = Visibility.Visible;
				clientsAppData.Visibility = Visibility.Visible;
				appointmentsLbl.Visibility = Visibility.Visible;
				DataRowView row = (DataRowView)ClientdataGrid.SelectedItems[0];
				clientIDTxt.Text = row["CID"].ToString();
				clientNameTxt.Text = row["Name"].ToString();
				clientSurnameTxt.Text = row["Surname"].ToString();
				clientBirthdayTxt.Text = row["Birthday"].ToString();
				clientPhoneTxt.Text = row["Telephone"].ToString();
				clientDoctorTxt.Text = row["Doctor"].ToString();
				clientDiagnosisTxt.Text = row["Diagnosis"].ToString();
				clientCommentsTxt.Text = row["Comments"].ToString();

				OleDbCommand cmd = new OleDbCommand();

				if (con.State != ConnectionState.Open)
					con.Open();
				cmd.Connection = con;

				cmd.CommandText = "select * from Appointments where CID=" + clientIDTxt.Text;
				cmd.ExecuteNonQuery();
				OleDbDataAdapter da = new OleDbDataAdapter(cmd);
				dt = new DataTable();
				da.Fill(dt);
				clientsAppData.ItemsSource = dt.AsDataView();
			}
			else
			{
				MessageBox.Show("Παρακαλώ επιλέξτε ένα πελάτη από τη λίστα...");
			}
		}

		private void deleteClient_Click(object sender, RoutedEventArgs e)
		{
			if (ClientdataGrid.SelectedItems.Count > 0)
			{
				DataRowView row = (DataRowView)ClientdataGrid.SelectedItems[0];

				OleDbCommand cmd = new OleDbCommand();
				if (con.State != ConnectionState.Open)
					con.Open();
				cmd.Connection = con;
				cmd.CommandText = "delete from Clients where CID=" + row["CID"].ToString();
				cmd.ExecuteNonQuery();
				BindGrid();
				MessageBox.Show("Ο πελάτης διαγράφηκε με επιτυχία!!");
				ClearAll();
			}
			else
			{
				MessageBox.Show("Παρακαλώ επιλέξτε ένα πελάτη από τη λίστα...");
			}
		}

		private void ClientdataGrid_AutoGeneratedColumns(object sender, EventArgs e)
		{
			ClientdataGrid.Columns[3].Visibility = Visibility.Hidden;
			ClientdataGrid.Columns[4].Visibility = Visibility.Hidden;
			ClientdataGrid.Columns[5].Visibility = Visibility.Hidden;
			ClientdataGrid.Columns[6].Visibility = Visibility.Hidden;
			ClientdataGrid.Columns[7].Visibility = Visibility.Hidden;
		}

		private void save_Click(object sender, RoutedEventArgs e)
		{
			OleDbCommand cmd = new OleDbCommand();

			if (con.State != ConnectionState.Open)
				con.Open();
			cmd.Connection = con;

			if (clientNameTxt.Text != "")
			{
				if (clientIDTxt.Text == "")
				{

                    cmd.CommandText = "insert into Clients(Name,Surname,Birthday,Telephone,Doctor,Diagnosis,Comments) Values('" + clientNameTxt.Text + "','" + clientSurnameTxt.Text + "','" + clientBirthdayTxt.Text + "','" + clientPhoneTxt.Text + "','" + clientDoctorTxt.Text + "','" + clientDiagnosisTxt.Text + "','" + clientCommentsTxt.Text + "')";
					cmd.ExecuteNonQuery();
					BindGrid();
                   

                    MessageBox.Show("Ο νέος πελάτης προστέθηκε με επιτυχία!!");

                    ClearAll();
				}
				else
				{
					cmd.CommandText = "update Clients set Name='" + clientNameTxt.Text + "',Surname='" + clientSurnameTxt.Text + "',Birthday='" + clientBirthdayTxt.Text + "',Telephone='" + clientPhoneTxt.Text + "',Doctor='" + clientDoctorTxt.Text + "',Diagnosis='" + clientDiagnosisTxt.Text + "',Comments='" + clientCommentsTxt.Text + "' where CID=" + clientIDTxt.Text;
					cmd.ExecuteNonQuery();
					BindGrid();
					MessageBox.Show("Οι αλλαγές έγιναν με επιτυχία!!");
					ClearAll();
				}
			}
			else
			{
				MessageBox.Show("Παρακαλώ προσθέστε όνομα πελάτη.......");
			}
		}

		private void SearchTxt_TextChanged(object sender, TextChangedEventArgs e)
		{
			DataView dv = new DataView(dt);
			dv.RowFilter = string.Format("Surname LIKE '{0}*'", SearchTxt.Text);
			ClientdataGrid.ItemsSource = dv;
		}

		private void clientsAppData_AutoGeneratedColumns(object sender, EventArgs e)
		{
			clientsAppData.Columns[1].Visibility = Visibility.Hidden;
			clientsAppData.Columns[1].Visibility = Visibility.Hidden;
		}
	}
}
