using System;
using System.Net;
using System.Windows;
using CryptSharp;

namespace PTwoManage
{
    /// <summary>
    ///     Interaction logic for AuthenticationWindow.xaml
    ///     Hashing lib from http://www.zer7.com/software/cryptsharp
    /// </summary>
    public partial class AuthenticationWindow : Window
    {
        public AuthenticationWindow()
        {
            InitializeComponent();
        }

        private string _companyName = "";
        //The unique salt for hashing
        private readonly string salt = "$6$rounds=1938$W2Wyyaa59gSmgv0K";

        private void AuthLogin_Click(object sender, RoutedEventArgs e)
        {
            Authenticate(AuthUsernameBox.Text, AuthPasswordBox.Password);
        }

        //Checks if the entered username and password matches one of those on the thirdparty site. 
        //if they do, set the companyname variable in Database
        private void Authenticate(string AuthUsername, string AuthPassword)
        {
            var loginString = AuthUsername + "," + GenPassHash(AuthPassword);

            var wc = new WebClient();
            //We use a temporary personal website to store usernames and hashed passowrds for test users.
            var webData = wc.DownloadString("http://everflows.com/com.txt");
            var split = webData.Split(';');

            foreach (var s in split)
                if ((loginString == s) && loginString.Contains(_companyName))
                {
                    DialogResult = true;
                    Database.Instance.CompanyName = AuthUsername;
                    Title = "Success";
                }
            Title = "Error - Username/password is incorrect";
        }

        //Sets the private companyname variable, which means it also gets used.
        public void UseCompanyName(string companyName)
        {
            _companyName = companyName;
        }

        //Generates a hash from the unique salt in the top of this class
        private string GenPassHash(string pass)
        {
            var hash = Crypter.Sha512.Crypt(pass, salt);
            var split = hash.Split('$');

            return split[4];
        }

        private void AuthCancel_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}