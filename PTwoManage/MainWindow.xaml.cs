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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PTwoManage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AddUserWindow addUserWindow;
        AddShiftTemplateWindow addShiftTemplateWindow;
        UserFreeTimeRequestWindow userFreeTimeRequestWindow;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            addUserWindow = new AddUserWindow();
            addUserWindow.Show();
            addUserWindow.EditUser_Load();

        }

        private void AddTemplate_Click(object sender, RoutedEventArgs e)
        {
            addShiftTemplateWindow = new AddShiftTemplateWindow();
            addShiftTemplateWindow.Show();
            addShiftTemplateWindow.LoadShift();
        }

        private void UserFreeRequest_Click(object sender, RoutedEventArgs e)
        {
            userFreeTimeRequestWindow = new UserFreeTimeRequestWindow();
            userFreeTimeRequestWindow.Show();
        }

    }
}
