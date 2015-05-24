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

namespace PTwoManage.Windows
{
    /// <summary>
    /// Interaction logic for DeleteShiftsWindow.xaml
    /// </summary>
    public partial class DeleteShiftsWindow : Window
    {
        public DeleteShiftsWindow()
        {
            InitializeComponent();
            PopulateUserComboBox();
        }

        private void UserCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateShiftListview();
        }

        private void PopulateUserComboBox()
        {
            UserCombobox.ItemsSource = Core.Instance.GetAllUsers();
        }

        private void PopulateShiftListview()
        {
            User selectedUser = UserCombobox.SelectedItem as User;
            ShiftListview.ItemsSource = Core.Instance.GetAllShifts().Where(shift => shift.EmployeeName ==  selectedUser.UserName).Where(shift => shift._startTime >= DateTime.Now);
        }

        private void DeleShift_Click(object sender, RoutedEventArgs e)
        {
            System.Collections.IList items = (System.Collections.IList)ShiftListview.SelectedItems;
            var collection = items.Cast<Shift>();
            foreach (Shift ShiftToDelete in collection)
            {
                ShiftToDelete.DeleteShift();
            }
            PopulateShiftListview();
        }

    }
}
