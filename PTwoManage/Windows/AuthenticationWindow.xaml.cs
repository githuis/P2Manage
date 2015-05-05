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
using System.Windows.Shapes;

namespace PTwoManage
{
    /// <summary>
    /// Interaction logic for AuthenticationWindow.xaml
    /// </summary>
    public partial class AuthenticationWindow : Window
    {
        public AuthenticationWindow()
        {
            InitializeComponent();
        }

        private void AuthLogin_Click(object sender, RoutedEventArgs e)
        {
            string loginString = AuthUsernameBox.Text + "," + AuthPasswordBox.Password;

            System.Net.WebClient wc = new System.Net.WebClient();
            string webData = wc.DownloadString("http://everflows.com/companies.txt");
            string[] split = webData.Split(new Char[] { ';' });

            foreach (string s in split)
            {
                if (s == loginString)
                    this.DialogResult = true;
            }
        }

        private void AuthCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
