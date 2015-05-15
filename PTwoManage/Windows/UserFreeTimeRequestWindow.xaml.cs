﻿using System;
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
        }

        private void SendRequest_Click(object sender, RoutedEventArgs e)
        {
            if (Start_Date.SelectedDate.HasValue && End_Date.SelectedDate.HasValue)
            {
                if (RequestValitation(Start_Date.SelectedDate.Value, End_Date.SelectedDate.Value, UsernameBox.Text))
                {
                    ErrorMessage.Content = "";
                    UserFreeRequest ResultRequest = new UserFreeRequest(Start_Date.SelectedDate.Value, End_Date.SelectedDate.Value, Message_Box.Text, UsernameBox.Text);
                    ResultRequest.SaveUserRequest();
                    Message_Box.Clear();
                    UsernameBox.Clear();
                }
                else
                    ErrorMessage.Content = "The selected start date is after the end date or user is not found";
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
    }
}
