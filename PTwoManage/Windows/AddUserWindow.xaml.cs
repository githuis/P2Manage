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
            // Fills the ListBox with all tags from the Core instance
            Tag_ListBox.ItemsSource = Core.Instance.GetAllTags();

        }

        //This method is called when a user should be saved to the database but the user information still need to be verified
        private void Submit_AddUser()
        {
            try
            {
                if (ValidateBoxContent() && !CheckIfUsernameExsists())
                {
                    // This method remove SQL tags from the inputboxes and allows a users input to be used.
                    PreventSqlInjection();

                    // At creation the user points is set to 0 and ID is set to 1 but not saved ind the database because the database automaticaly applies a new ID
                    User newUser = new User(1, CreateUserName(EditUser_FullName.Text, EditUser_CPR.Text), EditUser_Password.Password, EditUser_FullName.Text, EditUser_CPR.Text, EditUser_Number.Text, EditUser_Email.Text, GetCheckedTags(), 0);

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
                EditUser_Password.Password = "";
                EditUser_ConfirmPassword.Password = "";
                Populate_UserList();
            }
            catch (ArgumentException e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Error");
            }
        }

        // This method check each user inputbox for content. If the boxes are empty the method returns false
        private bool ValidateBoxContent()
        {
            if (EditUser_Password.Password != "" && EditUser_Password.Password == EditUser_ConfirmPassword.Password
                && EditUser_FullName.Text != "" && EditUser_CPR.Text != "" && EditUser_Number.Text != "")
                return true;
            else
                return false;
        }

        // A method for removing SQL tags from userinputs
        private void PreventSqlInjection()
        {
            string toTrim = ";'\\:";
            EditUser_FullName.Text = EditUser_FullName.Text.Trim(toTrim.ToCharArray());
            EditUser_Password.Password = EditUser_Password.Password.Trim(toTrim.ToCharArray());
            EditUser_ConfirmPassword.Password = EditUser_ConfirmPassword.Password.Trim(toTrim.ToCharArray());
            EditUser_CPR.Text = EditUser_CPR.Text.Trim(toTrim.ToCharArray());
            EditUser_Number.Text = EditUser_Number.Text.Trim(toTrim.ToCharArray());
            EditUser_Email.Text = EditUser_Email.Text.Trim(toTrim.ToCharArray());
        }

        // A method for refreshing the UserList in the GUI
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

        // A method for refreshing the TagList in the GUI
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
                if (!tags.Contains(s))
                    Tag_ListBox.SelectedItems.Remove(s);
            }
        }

        private void EditUser_Load()
        {
            Populate_TagList();
            Populate_UserList();
        }

        // A method called when the Edit User button is clicked
        private void EditUser_Select_Button_Click(object sender, RoutedEventArgs e)
        {
            // If no user is selected the no user i loaded
            if (EditUser_NameList.SelectedItem != null)
            {
                ListBoxItem item = (ListBoxItem)EditUser_NameList.SelectedItem;
                string userName = item.Content.ToString();
                EditUser_UserNameBox.Text = User.GetUserByName(userName).UserName;
                EditUser_Password.Password = User.GetUserByName(userName).Password;
                EditUser_FullName.Text = User.GetUserByName(userName).Name;
                EditUser_CPR.Text = User.GetUserByName(userName).CprNumber;
                EditUser_Number.Text = User.GetUserByName(userName).Phone;
                EditUser_Email.Text = User.GetUserByName(userName).Email;
                EditUser_ConfirmPassword.Password = User.GetUserByName(userName).Password;
                EditUser_ConfirmPassword.IsEnabled = false;
                Tag_ListBox.SelectedItemsOverride = User.GetUserByName(item.Content.ToString()).Tags;
                AddUser_Confirmation.Content = "";
            }
            Tag_ListBox.Items.Refresh();
        }

        // A method called each time a user enters a charecter in some of the inputboxes in the GUI
        private void EditUser_NumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        // Updates an exsisting Users information
        private void SaveToCurrentUser(User user)
        {
            User u = User.GetUserByName(user.UserName);
            u.Name = EditUser_FullName.Text;
            u.CprNumber = EditUser_CPR.Text;
            u.Email = EditUser_Email.Text;
            u.Phone = EditUser_Number.Text;
            u.UserName = EditUser_UserNameBox.Text;
            u.Password = EditUser_Password.Password;
            u.Tags = GetCheckedTags();
            u.SaveUserInfoToDatabase();
            Populate_UserList();
        }

        private void SaveUser_Click(object sender, RoutedEventArgs e)
        {
            string userName = EditUser_UserNameBox.Text;
            if (User.CheckUserExists(userName))
            {
                if (ValidateBoxContent())
                {
                    if (!(CheckIfUsernameExsists()))
                    {
                        SaveToCurrentUser(User.GetUserByName(userName));
                        EmptyForm();
                    }
                }
                else
                    AddUser_Confirmation.Content = "Not all input boxes are filled or valid";
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
            EditUser_Password.Password = "";
            EditUser_ConfirmPassword.Password = "";
            EditUser_ConfirmPassword.IsEnabled = true;
            Tag_ListBox.SelectedItemsOverride = new List<string>();
        }

        private string CreateUserName(string FullName, string cpr)
        {
            string userName;
            string[] split;
            long n = 0, sum = 0;
            if (!long.TryParse(cpr, out n))
            {
                throw new ArgumentException("CPR number must only be numbers");
            }

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
            User u = null;

            if (EditUser_UserNameBox.Text == "")
                System.Windows.Forms.MessageBox.Show("You must load the user you want to delete");
            else if (User.CheckUserExists(EditUser_UserNameBox.Text))
                u = User.GetUserByName(EditUser_UserNameBox.Text);

            if (u != null)
                u.DeleteUser();

            Core.Instance.RemoveUserFromList(u);
            EmptyForm();
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

        private bool CheckIfUsernameExsists()
        {
            if (Core.Instance.GetAllUsers().Find(user => user.UserName.Contains(CreateUserName(EditUser_FullName.Text, EditUser_CPR.Text))) == null)
                return false;
            else
                return true;
        }
    }
}
