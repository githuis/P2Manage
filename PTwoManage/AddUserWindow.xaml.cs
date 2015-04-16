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
using System.ComponentModel;

namespace PTwoManage
{
    /// <summary>
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public AddUserWindow()
        {
            InitializeComponent();
        }

        private void Submit_AddUser_Click(object sender, RoutedEventArgs e)
        {
            if (EditUser_UserNameBox.Text != "" && Password_TextBox.Password != "" && EditUser_FullName.Text != "")
            {
                User newUser = new User(EditUser_UserNameBox.Text, Password_TextBox.Password);
                Core.AddUserToList(newUser);
                AddUser_Confirmation.Content = EditUser_FullName.Text + " was added to the system";
                AddUser_Confirmation.Foreground = Brushes.Green;
            }
            else
            {
                AddUser_Confirmation.Content = "Could not add user to system - please check username and password";
                AddUser_Confirmation.Foreground = Brushes.Red;
            }
            EditUser_UserNameBox.Text = "";
            Password_TextBox.Password = "";
            EditUser_NameList.Items.Clear();
            Populate_UserList();
        }

        private void Populate_UserList()
        {
            foreach (User u in Core.GetAllUsers())
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = u.UserName;
                EditUser_NameList.Items.Add(item);
            }
        }

        public void EditUser_Load()
        {
            Populate_UserList();
        }

        private void EditUser_Select_Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (EditUser_NameList.SelectedItem != null)
            {
                ListBoxItem item = (ListBoxItem) EditUser_NameList.SelectedItem;
                EditUser_UserNameBox.Text = User.GetUserByName(item.Content.ToString()).UserName; 
            }
        }
    }
}
