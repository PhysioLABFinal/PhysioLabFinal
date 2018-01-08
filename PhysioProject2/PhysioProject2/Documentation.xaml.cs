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
using System.Diagnostics;


namespace PhysioProject2
{
    /// <summary>
    /// Interaction logic for Documentation.xaml
    /// </summary>
    public partial class Documentation : Page
    {
        public Documentation()
        {
            InitializeComponent();
            string pathh = "..\\..\\README_PhysioLAb.docx";
            Process wordProcess = new Process();
            wordProcess.StartInfo.FileName =pathh;
            wordProcess.StartInfo.UseShellExecute = true;
            wordProcess.Start();
        }
    }
}
