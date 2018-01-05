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
     

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (MailMessage mail = new MailMessage())
            {
                ProgressBar.Value = 0;
                mail.From = new MailAddress(MailTB.Text);
                mail.To.Add(MailTB.Text);
                
                mail.Subject = "BackUp βάσης δεδομένων ";
                mail.Body = "<h1>Σας έχουμε επισυνάψει το αρχείο της βάσης δεδομένων σας</h1>";
                mail.IsBodyHtml = true;
                mail.Attachments.Add(new Attachment(".\\PhysioDatabase.accdb"));
                ProgressBar.Value = 40;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    ProgressBar.Value = 70;
                    

                    string myemail = MailTB.Text;
                    string mypassword = PasswordTB.Password.ToString();
                    smtp.Credentials = new NetworkCredential(myemail, mypassword);
                   
                    smtp.EnableSsl = true;
                    ProgressBar.Value = 100;
                    smtp.Send(mail);
                    MessageBox.Show("Επιτυχής αποστολή, παρακαλώ ελέγξτε το Email σας. ");
                    
                }
            }
        }
    }
        
}
    
