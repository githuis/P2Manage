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
        ShiftWindow shiftWindow;
        public MainWindow()
        {
            InitializeComponent();
            /*Core.Instance.Run();

            shiftDataBindingMonday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Monday, 2);
            shiftDataBindingTuesday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Tuesday, 2);
            shiftDataBindingWednesday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Wednesday, 2);
            shiftDataBindingThursday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Thursday, 2);
            shiftDataBindingFriday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Friday, 2);
            shiftDataBindingSaturday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Saturday, 2);
            shiftDataBindingSunday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Sunday, 2);
             * 
             * */

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
            addShiftTemplateWindow.LoadShiftTemplate();
        }

        private void LoadSchedule_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }
		
        private void UserFreeRequest_Click(object sender, RoutedEventArgs e)
        {
            userFreeTimeRequestWindow = new UserFreeTimeRequestWindow();
            userFreeTimeRequestWindow.Show();
        }

        private void Shift_Click(object sender, RoutedEventArgs e)
        {
            shiftWindow = new ShiftWindow();
            shiftWindow.Show();
            shiftWindow.LoadShift();
        }
    }
}
