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
        }

        private void Send_Request_Click(object sender, RoutedEventArgs e)
        {
            if (Start_Date.SelectedDate.HasValue && End_Date.SelectedDate.HasValue)
            {
                if (Request_Valitation(Start_Date.SelectedDate.Value, End_Date.SelectedDate.Value, User_Name.Text))
                {
                    Error_message.Content = "";
                    UserFreeRequest ResultRequest = new UserFreeRequest(Start_Date.SelectedDate.Value, End_Date.SelectedDate.Value, Message_Box.Text, User_Name.Text);
                    ResultRequest.SaveUserRequest();
                    Message_Box.Clear();
                }
                else
                    Error_message.Content = "The selected start date is after the end date";
            }
        }

        private bool Request_Valitation(DateTime start, DateTime end, string userName)
        {
            if (start <= end && (User.GetUserByName(userName).UserName == userName))
                return true;
            else
                return false;
        }

    }
}
