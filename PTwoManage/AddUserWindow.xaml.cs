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
using System.Text.RegularExpressions;

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

        private void Submit_AddUser()
        {

            if (EditUser_UserNameBox.Text != "" && Password_TextBox.Password != "" && Password_TextBox.Password == ConfirmPassword.Password && EditUser_FullName.Text != "" && EditUser_CPR.Text != "" && EditUser_Number.Text != "" && EditUser_Email.Text != "")
            {
                //Skal fixes
                User newUser = new User(1, EditUser_UserNameBox.Text, Password_TextBox.Password, EditUser_FullName.Text, EditUser_CPR.Text, EditUser_Number.Text, EditUser_Email.Text);
                Core.Instance.AddUserToList(newUser);
                AddUser_Confirmation.Content = EditUser_FullName.Text + " was added to the system";
                AddUser_Confirmation.Foreground = Brushes.Green;
                newUser.SaveUserInfoToDatabase();
                EmptyForm();
               
            }
            else
            {
                AddUser_Confirmation.Content = "ERROR: Could not add " + EditUser_UserNameBox.Text + " to the system";
                AddUser_Confirmation.Foreground = Brushes.Red;
            }
            
            Password_TextBox.Password = "";
            ConfirmPassword.Password = "";
            EditUser_NameList.Items.Clear();
            Populate_UserList();
        }

        private void Populate_UserList()
        {
            foreach (User u in Core.Instance.GetAllUsers())
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = u.Name;
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
                Password_TextBox.Password = User.GetUserByName(item.Content.ToString()).Password;
                EditUser_FullName.Text = User.GetUserByName(item.Content.ToString()).Name;
                EditUser_CPR.Text = User.GetUserByName(item.Content.ToString()).CprNumber;
                EditUser_Number.Text = User.GetUserByName(item.Content.ToString()).Phone;
                EditUser_Email.Text = User.GetUserByName(item.Content.ToString()).Email;
            }
        }

        private void EditUser_NumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SaveToCurrentUser(User user)
        {
            User u = User.GetUserByName(user.UserName);
            u.Name = EditUser_FullName.Text;
            u.CprNumber = EditUser_CPR.Text;
            u.Email = EditUser_Email.Text;
            u.Phone = EditUser_Number.Text;
            u.UserName = EditUser_UserNameBox.Text;
            u.SaveUserInfoToDatabase();
        }

        private void SaveUser_Click(object sender, RoutedEventArgs e)
        {
            User u = User.GetUserByName(EditUser_UserNameBox.Text);

            if (u.UserName != "User not found" && EditUser_UserNameBox.Text == u.UserName)
            {
                SaveToCurrentUser(u);
                EmptyForm();
            }
            else
            {
                Submit_AddUser();
            }
            
        }

        private void EmptyForm()
        {
            EditUser_UserNameBox.Text = "";
            EditUser_FullName.Text = "";
            EditUser_CPR.Text = "";
            EditUser_Number.Text = "";
            EditUser_Email.Text = "";
            Password_TextBox.Password = "";
            ConfirmPassword.Password = "";
        }

        private void EditUser_NameList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
