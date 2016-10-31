using System;
using System.Windows;

namespace PTwoManage
{
    /// <summary>
    ///     Interaction logic for UserFreeTimeRequest.xaml
    /// </summary>
    public partial class UserFreeTimeRequestWindow : Window
    {
        public UserFreeTimeRequestWindow()
        {
            InitializeComponent();
            PopulateRequestList();
            PopulateSelectUserComboBox();
        }

        //First checks that all required fields are filled
        //Validates Dates and User, see RequestValitation method
        //If these checks pass, clean the error message.
        //Then cast the user from the combobox into a user object and removes all commas (,) from the text
        //Then create a UserFreeRequest with dates, message and username
        //Then saves user and clears messagebox.
        private void SendRequest_Click(object sender, RoutedEventArgs e)
        {
            if (Start_Date.SelectedDate.HasValue && End_Date.SelectedDate.HasValue &&
                (SelectUserComboBox.SelectedItem != null))
                if (RequestValitation(Start_Date.SelectedDate.Value, End_Date.SelectedDate.Value,
                    SelectUserComboBox.SelectedItem.ToString()))
                {
                    ErrorMessage.Content = "";
                    var user = SelectUserComboBox.SelectedItem as User;
                    var message = Message_Box.Text.Replace(",", "");
                    var ResultRequest = new UserFreeRequest(Start_Date.SelectedDate.Value, End_Date.SelectedDate.Value,
                        message, user.UserName);
                    ResultRequest.SaveUserRequest();
                    Message_Box.Clear();
                }
                else
                    ErrorMessage.Content = "The selected start date is after the end date";
        }

        //Checks that User exists
        //Checks that that dates are in correct order.
        private bool RequestValitation(DateTime start, DateTime end, string userName)
        {
            if ((start <= end) && User.CheckUserExists(userName))
                return true;
            return false;
        }

        //Sets itemsource to fill list of requests
        private void PopulateRequestList()
        {
            RequestList.ItemsSource = Core.Instance.GetAllFreeRequests();
        }

        //Fills combobox with all users
        private void PopulateSelectUserComboBox()
        {
            SelectUserComboBox.ItemsSource = Core.Instance.GetAllUsers();
        }
    }
}