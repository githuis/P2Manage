using System;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Controls;

namespace PTwoManage.Windows
{
    /// <summary>
    ///     Interaction logic for UserSwapShift.xaml
    /// </summary>
    public partial class UserSwapShift : Window
    {
        public UserSwapShift()
        {
            //Start the window and populates the comboboxes
            InitializeComponent();
            PopulateSwapeeUserComboBox();
            PopulateSwapperUserComboBox();
        }

        // A method for populating the Swapper combobox
        private void PopulateSwapperUserComboBox()
        {
            SwapperUserComboBox.ItemsSource = Core.Instance.GetAllUsers();
        }

        // A method for loading Users into the Swapee combobox but excluding the choosen Swapper User
        private void PopulateSwapeeUserComboBox()
        {
            var swapee = SwapperUserComboBox.SelectedItem as User;
            if (SwapperUserComboBox.SelectedItem == null)
                SwapeeUserComboBox.ItemsSource = Core.Instance.GetAllUsers();
            else
                SwapeeUserComboBox.ItemsSource =
                    Core.Instance.GetAllUsers().Where(user => user.UserName != swapee.UserName);
        }

        // Populate's the Swapper Shifts combobox with Shifts still active (After the current DateTime)
        private void PopulateSwapperShiftCombobox()
        {
            var selectedUser = SwapperUserComboBox.SelectedItem as User;
            SwapperShiftCombobox.ItemsSource =
                Core.Instance.GetAllShifts()
                    .Where(shift => shift.UserName == selectedUser.UserName)
                    .Where(shift => shift.StartTime >= DateTime.Now);
        }

        private void SwapperShiftCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateSwapperShiftCombobox();
        }

        // If the Swapper is changed the shifts and swapee combobox needs to update
        private void SwapperUserComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateSwapperShiftCombobox();
            PopulateSwapeeUserComboBox();
        }

        // A method for swapping shifts between to users
        private void SwapShifts_Click(object sender, RoutedEventArgs e)
        {
            // First it checks if the comboboxes are empty
            if ((SwapperUserComboBox.SelectedItem != null) && (SwapeeUserComboBox.SelectedItem != null) &&
                (SwapperShiftCombobox.SelectedItem != null))
            {
                var swapper = SwapperUserComboBox.SelectedItem as User;
                var swapee = SwapeeUserComboBox.SelectedItem as User;
                var shift = SwapperShiftCombobox.SelectedItem as Shift;

                // It compares the swapee tags with the shift tags to make sure the swapee mathces the tags
                if (Core.Instance.CompareTags(swapee.Tags, shift.Tag))
                {
                    swapee.UpdateUserPointBalance(4);
                    swapper.UpdateUserPointBalance(-4);
                    shift.UpdateShift(swapee.UserName);
                    PopulateSwapperShiftCombobox();
                }
                else
                    SystemSounds.Beep.Play();
            }
            else
                SystemSounds.Beep.Play();
        }
    }
}