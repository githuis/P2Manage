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
using Xceed.Wpf.Toolkit;

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

            Tag_ListBox.ItemsSource = Core.Instance.GetAllTags();
        }
        
        private void Submit_AddUser()
        {

            if (Password_TextBox.Password != "" && Password_TextBox.Password == ConfirmPassword.Password && EditUser_FullName.Text != "" && EditUser_CPR.Text != "" && EditUser_Number.Text != "" && EditUser_Email.Text != ""
                && (Core.Instance.GetAllUsers().Find(x => x.UserName.Contains(CreateUserName(EditUser_FullName.Text, EditUser_CPR.Text))) == null))
            {
                User newUser = new User(1, CreateUserName(EditUser_FullName.Text, EditUser_CPR.Text), Password_TextBox.Password, EditUser_FullName.Text, EditUser_CPR.Text, EditUser_Number.Text, EditUser_Email.Text, GetCheckedTags(), 100);

                Core.Instance.AddUserToList(newUser);
                AddUser_Confirmation.Content = EditUser_FullName.Text + " was added to the system";
                AddUser_Confirmation.Foreground = Brushes.Green;
                newUser.SaveUserInfoToDatabase();
                EmptyForm();
               
            }
            else
            {
                string tempName = EditUser_FullName.Text;
                AddUser_Confirmation.Content = "ERROR: Could not add '" + (tempName == "" ? "<No Name Found>" : tempName) + "' to the system";
                AddUser_Confirmation.Foreground = Brushes.Red;
            }
            Password_TextBox.Password = "";
            ConfirmPassword.Password = "";
            Populate_UserList();
        }

        private void Populate_UserList()
        {
            EditUser_NameList.Items.Clear();
            foreach (User u in Core.Instance.GetAllUsers())
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = u.UserName;
                EditUser_NameList.Items.Add(item);
            }
        }

        private void Populate_TagList()
        {
            Tag_ListBox.ItemsSource = null;
            Tag_ListBox.ItemsSource = Core.Instance.GetAllTags();
        }

        private void Populate_TagList(List<string> tags)
        {
            Tag_ListBox.ItemsSource = null;
            Tag_ListBox.ItemsSource = Core.Instance.GetAllTags();
            foreach (string tag in Core.Instance.GetAllTags())
            {
                Tag_ListBox.SelectedItems.Add(tag);
            }

            foreach (string s in Core.Instance.GetAllTags())
            {
                Console.WriteLine("User has tag: " + s);
                if (!tags.Contains(s))
                    Tag_ListBox.SelectedItems.Remove(s);
            }      
        }

        //TODO Should this be public ?
        public void EditUser_Load()
        {
            Populate_TagList();
            Populate_UserList();
        }

        private void EditUser_Select_Button_Click(object sender, RoutedEventArgs e)
        {
            //TODO check if user exists
            if (EditUser_NameList.SelectedItem != null)
            {
                ListBoxItem item = (ListBoxItem) EditUser_NameList.SelectedItem;
                string userName = item.Content.ToString();
                //TODO Create a user and use it's properties instead of calling getuser 9999 times
                EditUser_UserNameBox.Text = User.GetUserByName(userName).UserName;
                Password_TextBox.Password = User.GetUserByName(userName).Password;
                EditUser_FullName.Text = User.GetUserByName(userName).Name;
                EditUser_CPR.Text = User.GetUserByName(userName).CprNumber;
                EditUser_Number.Text = User.GetUserByName(userName).Phone;
                EditUser_Email.Text = User.GetUserByName(userName).Email;
                ConfirmPassword.Password = User.GetUserByName(userName).Password;
                ConfirmPassword.IsEnabled = false;
                Tag_ListBox.SelectedItemsOverride = User.GetUserByName(item.Content.ToString()).UserCategories;
                Console.WriteLine(User.GetUserByName(item.Content.ToString()).Points);
            }
            Tag_ListBox.Items.Refresh();
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
            u.Password = Password_TextBox.Password;
            u.UserCategories = GetCheckedTags();
            u.SaveUserInfoToDatabase();
            Populate_UserList();
        }

        private void SaveUser_Click(object sender, RoutedEventArgs e)
        {
            string userName = EditUser_UserNameBox.Text;
            if (User.CheckUserExists(userName))
            {
                SaveToCurrentUser(User.GetUserByName(userName));
                EmptyForm();
                Console.WriteLine("Saved");
            }
            else
                Submit_AddUser();
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
            ConfirmPassword.IsEnabled = true;
            Tag_ListBox.SelectedItemsOverride = new List<string>();
        }

        private string CreateUserName(string FullName, string cpr)
        {
            string userName;
            string[] split;
            long n = long.Parse(cpr), sum = 0;

            while (n != 0)
            {
                sum += n % 10;
                n /= 10;
            }
            split = FullName.Split(new Char[] { ' ' });
            userName = split[0] + sum;

            return userName;
        } 
        
        private void Remove_User_Click(object sender, RoutedEventArgs e)
        {
            User u = User.GetUserByName(EditUser_UserNameBox.Text);
            if (EditUser_UserNameBox.Text != "")
                u.DeleteUser();
            EmptyForm();
            Core.Instance.RemoveUserFromList(u);
            Populate_UserList();
        }

        private List<string> GetCheckedTags()
        {
            List<string> UserTags = new List<string>();
            foreach (string s in Tag_ListBox.SelectedItems)
            {
                UserTags.Add(s);
                //string tag = item as string;
                //UserTags.Add(tag);
            }
            return UserTags;
        }


        private void EditUser_NameList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
