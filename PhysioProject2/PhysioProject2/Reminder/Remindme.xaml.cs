﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

namespace PhysioProject2.Reminder
{
    /// <summary>
    /// Interaction logic for Remindme.xaml
    /// </summary>
    public partial class Remindme : Page
    {

        public Remindme()
        {
            InitializeComponent();
            Combo.Items.Add("Gmail");
            Combo.Items.Add("Yahoo");
            Combo.Items.Add("Hotmail");
            Combo.Items.Add("Outlook");
            WaitLBL.Visibility =Visibility.Hidden;


        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WaitLBL.Visibility = Visibility.Visible;
            string smtpClient = "";
            if (Combo.Text=="Gmail")

            {
                smtpClient = "smtp.gmail.com";
                
            }
            else if (Combo.Text=="Yahoo")
            {
                smtpClient = "smtp.mail.yahoo.com";

            }

            else if (Combo.Text == "Hotmail")
            {

                smtpClient = "smtp.live.com";

            }

            else if (Combo.Text=="Outlook")
            {
                smtpClient = "smtp-mail.outlook.com";

            }

            else
            {
                MessageBox.Show("Η υπηρεσια Email που επιλεξατε δεν υπάρχει");
            }

            using (MailMessage mail = new MailMessage())
            {
               

               

                try
                {

                    mail.From = new MailAddress(MailTB.Text);
                    mail.To.Add(MailTB.Text);

                    mail.Subject = "BackUp βάσης δεδομένων ";
                    mail.Body = "<h1>Σας έχουμε επισυνάψει το αρχείο της βάσης δεδομένων σας</h1>";
                    mail.IsBodyHtml = true;
                    mail.Attachments.Add(new Attachment(".\\PhysioDatabase.accdb"));
                    using (SmtpClient smtp = new SmtpClient(smtpClient, 587))
                    {



                        string myemail = MailTB.Text;
                        string mypassword = PasswordTB.Password.ToString();
                        smtp.Credentials = new NetworkCredential(myemail, mypassword);

                        smtp.EnableSsl = true;

                        smtp.Send(mail);
                        MessageBox.Show("Επιτυχής αποστολή, παρακαλώ ελέγξτε το Email σας. ");
                        WaitLBL.Visibility = Visibility.Hidden;

                    }


                }
                catch {
                    MessageBox.Show("Λαθος Ονομα/Κωδικος");

                }
            }
        }

        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
        
}
    
