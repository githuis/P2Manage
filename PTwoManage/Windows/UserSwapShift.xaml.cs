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

namespace PTwoManage.Windows
{
    /// <summary>
    /// Interaction logic for UserSwapShift.xaml
    /// </summary>
    public partial class UserSwapShift : Window
    {
        public UserSwapShift()
        {
            InitializeComponent();
            PopulateSwapeeUserComboBox();
            PopulateSwapperUserComboBox();
        }

        private void PopulateSwapperUserComboBox()
        {
            SwapperUserComboBox.ItemsSource = Core.Instance.GetAllUsers();
        }

        private void PopulateSwapeeUserComboBox()
        {
            User test = SwapperUserComboBox.SelectedItem as User;
            if(SwapperUserComboBox.SelectedItem == null)
                SwapeeUserComboBox.ItemsSource = Core.Instance.GetAllUsers();
            else
                SwapeeUserComboBox.ItemsSource = Core.Instance.GetAllUsers().Where(user => user.UserName != test.UserName);
        }

        private void PopulateSwapperShiftCombobox()
        {
            User test = SwapperUserComboBox.SelectedItem as User;
            SwapperShiftCombobox.ItemsSource = Core.Instance.GetAllShifts().Where(shift => shift.EmployeeName == test.UserName).Where(shift => shift._startTime >= DateTime.Now);
        }

        private void SwapperShiftCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateSwapperShiftCombobox();
        }

        private void SwapperUserComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateSwapperShiftCombobox();
            PopulateSwapeeUserComboBox();
        }

        private void SwapShifts_Click(object sender, RoutedEventArgs e)
        {
            if (SwapperUserComboBox.SelectedItem != null && SwapeeUserComboBox.SelectedItem != null && SwapperShiftCombobox.SelectedItem != null)
            {
                User swapper = SwapperUserComboBox.SelectedItem as User;
                User swapee = SwapeeUserComboBox.SelectedItem as User;
                Shift shift = SwapperShiftCombobox.SelectedItem as Shift;

                if (Core.Instance.CompareTags(swapee.UserCategories, shift.Tag))
                {
                    swapee.UpdateUserPointBalance(4);
                    swapper.UpdateUserPointBalance(-4);
                    shift.UpdateShift(swapee.UserName);
                    PopulateSwapperShiftCombobox();
                }
                else
                    System.Media.SystemSounds.Beep.Play();
            }
            else
                System.Media.SystemSounds.Beep.Play();
        }
    }
}
