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
    /// Interaction logic for UserFreeTimeRequest.xaml
    /// </summary>
    public partial class UserFreeTimeRequestWindow : Window
    {
        public UserFreeTimeRequestWindow()
        {
            InitializeComponent();
            PopulateRequestList();
            PopulateSelectUserComboBox();
        }

        private void SendRequest_Click(object sender, RoutedEventArgs e)
        {
            if (Start_Date.SelectedDate.HasValue && End_Date.SelectedDate.HasValue && SelectUserComboBox.SelectedItem != null)
            {
                if (RequestValitation(Start_Date.SelectedDate.Value, End_Date.SelectedDate.Value, SelectUserComboBox.SelectedItem.ToString()))
                {
                    ErrorMessage.Content = "";
                    User user = SelectUserComboBox.SelectedItem as User;
                    string message = Message_Box.Text.Replace(",","");
                    UserFreeRequest ResultRequest = new UserFreeRequest(Start_Date.SelectedDate.Value, End_Date.SelectedDate.Value, message, user.UserName);
                    ResultRequest.SaveUserRequest();
                    Message_Box.Clear();
                }
                else
                    ErrorMessage.Content = "The selected start date is after the end date";
            }
        }

        private bool RequestValitation(DateTime start, DateTime end, string userName)
        {
            if (start <= end && User.CheckUserExists(userName))
                return true;
            else
                return false;
        }

        private void PopulateRequestList()
        {
            RequestList.ItemsSource = Core.Instance.GetAllFreeRequests();
        }

        private void PopulateSelectUserComboBox()
        {
            SelectUserComboBox.ItemsSource = Core.Instance.GetAllUsers();
        }
    }
}
