using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PTwoManage.Windows
{
    /// <summary>
    ///     Interaction logic for DeleteShiftsWindow.xaml
    /// </summary>
    public partial class DeleteShiftsWindow : Window
    {
        public DeleteShiftsWindow()
        {
            InitializeComponent();
            PopulateUserComboBox();
        }

        //Repopulates Shift list when a selection in users has changed, so that it is diaplayed to the user
        private void UserCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateShiftListview();
        }

        //Fills the Combobox (dropdown menu) with all users
        private void PopulateUserComboBox()
        {
            UserCombobox.ItemsSource = Core.Instance.GetAllUsers();
        }

        //Fills the list of shifts with data from the selected user.
        private void PopulateShiftListview()
        {
            var selectedUser = UserCombobox.SelectedItem as User;
            ShiftListview.ItemsSource =
                Core.Instance.GetAllShifts()
                    .Where(shift => shift.UserName == selectedUser.UserName)
                    .Where(shift => shift.StartTime >= DateTime.Now);
        }

        //Removes the selected shift from all shifts and repopulates the list
        private void DeleShift_Click(object sender, RoutedEventArgs e)
        {
            var items = ShiftListview.SelectedItems;
            var collection = items.Cast<Shift>();
            foreach (var ShiftToDelete in collection)
                ShiftToDelete.DeleteShift();
            PopulateShiftListview();
        }
    }
}