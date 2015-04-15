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
using System.IO;

namespace PTwoManage
{
    /// <summary>
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public AddUserWindow()
        {
            InitializeComponent();
        }

        private void Submit_AddUser_Click(object sender, RoutedEventArgs e)
        {
            if (UserName_TextBox.Text != "" && Password_TextBox.Password != "")
            {
                 User newUser = new User(UserName_TextBox.Text, Password_TextBox.Password);
                 Core.AddUserToList(newUser);
            }
           
        }

        private void Populate_UserList()
        {
            foreach (User u in Core.GetAllUsers())
            {
                Console.WriteLine(u.ToString());
            }
        }

        public void EditUser_Load()
        {

        }
    }
}
