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

namespace PhysioProject2
{
    /// <summary>
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class Welcome : Page
    {
        public Welcome()
        {
            InitializeComponent();
            string[] arr1 = new string[] { "I am a doctor - it's a profession that may be considered a special mission, a devotion. It calls for involvement, respect and willingness to help all other people.",
                                             "A doctor does not ask about political views and opinions - that is how I understand my role.",
                                              "It's the best time ever to be a doctor because you can heal and treat conditions that were untreatable even a few years ago." };
            Random rnd = new Random();
            int i = rnd.Next(0, 3);
            WCLabel.Content = arr1[i];
        }
    }
}
