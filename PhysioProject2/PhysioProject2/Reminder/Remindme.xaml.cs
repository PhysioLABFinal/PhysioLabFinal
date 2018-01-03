using System;
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
            
        }



        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                MailMessage mail = new MailMessage();
                //put your SMTP address and port here.
                SmtpClient SmtpServer = new SmtpClient("");
                //Put the email address
                mail.From = new MailAddress("");
                //Put the email where you want to send.
                mail.To.Add("");


                mail.Subject = "CheckoutPOS Exception Log";

                StringBuilder sbBody = new StringBuilder();

                sbBody.AppendLine("Hi Dev Team,");

                sbBody.AppendLine("Something went wrong with CheckoutPOS");

                sbBody.AppendLine("Here is the error log:");

                sbBody.AppendLine("Exception: Object reference not set to an instance of an object....");

                sbBody.AppendLine("Thanks,");

                mail.Body = sbBody.ToString();

                MessageBox.Show("Test2");

                //Your log file path
                System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(@".\\PhysioDatabase.accdb");

                mail.Attachments.Add(attachment);
                MessageBox.Show("Test1");


                //Your username and password!
                SmtpServer.Credentials = new System.Net.NetworkCredential("", "");
                //Set Smtp Server port
                MessageBox.Show("connected");

                SmtpServer.Port = 25;
                //SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                MessageBox.Show("The exception has been sent! :)");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
