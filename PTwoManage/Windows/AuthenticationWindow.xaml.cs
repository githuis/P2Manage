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
using CryptSharp;

namespace PTwoManage
{
    /// <summary>
    /// Interaction logic for AuthenticationWindow.xaml
    /// Hashing lib from http://www.zer7.com/software/cryptsharp
    /// </summary>
    public partial class AuthenticationWindow : Window
    {
        private string _companyName = "";
        string salt = "$6$rounds=1938$W2Wyyaa59gSmgv0K";
        public AuthenticationWindow()
        {
            InitializeComponent();
             
        }

        private void AuthLogin_Click(object sender, RoutedEventArgs e)
        {
            Authenticate(AuthUsernameBox.Text, AuthPasswordBox.Password);
        }

        private void Authenticate(string AuthUsername, string AuthPassword)
        {
            string loginString = AuthUsername + "," + GenPassHash(AuthPassword);

            System.Net.WebClient wc = new System.Net.WebClient();
            //Vi bruger en personlig hjemmeside til midlertidigt at holde vores test-brugere
            string webData = wc.DownloadString("http://everflows.com/com.txt");
            string[] split = webData.Split(new Char[] { ';' });

            foreach (string s in split)
            {
                Console.WriteLine("Login: " + loginString + "\nMatch: " + s);
                if (loginString == s && loginString.Contains(_companyName))
                {
                    this.DialogResult = true;
                    Database.Instance.CompanyName = AuthUsername;
                    this.Title = "Success";
                }
            }
            this.Title = "Error - Username/password is incorrect";
        }

        private void AuthCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        public void UseCompanyName(string companyName)
        {
            _companyName = companyName;
        }

        private string GenPassHash(string pass)
        {
            string hash = Crypter.Sha512.Crypt(pass, salt);
            string[] split = hash.Split('$');

            return split[4];
        }
    }
}
