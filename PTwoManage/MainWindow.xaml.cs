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
using System.Globalization;

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

        private int _year = 2015;
        private int _week = 1;
        public int SelectedWeek
        {
            get { return _week; }
            set
            {
                if (value < GetWeeksInYear(_year) && value > 0)
                {
                    _week = value;
                }
                else if (value > GetWeeksInYear(_year))
                {
                    _year++;
                    _week = 1;
                }
                else
                {
                    _year--;
                    _week = GetWeeksInYear(_year);
                }
                    
                    
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            Authentication.Instance.Prompt();
            UpdateWeekNumberDisplay();
            //Core.Instance.Run();

            /*shiftDataBindingMonday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Monday, 2);
            shiftDataBindingTuesday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Tuesday, 2);
            shiftDataBindingWednesday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Wednesday, 2);
            shiftDataBindingThursday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Thursday, 2);
            shiftDataBindingFriday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Friday, 2);
            shiftDataBindingSaturday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Saturday, 2);
            shiftDataBindingSunday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Sunday, 2);
           */

        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            if(Authentication.Instance.Prompt())
            {
                addUserWindow = new AddUserWindow();
                addUserWindow.Show();
                addUserWindow.EditUser_Load();
            }
        }

        private void AddTemplate_Click(object sender, RoutedEventArgs e)
        {
            addShiftTemplateWindow = new AddShiftTemplateWindow();
            addShiftTemplateWindow.Show();
            addShiftTemplateWindow.LoadShift();
        }

        private void LoadSchedule_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void UserFreeRequest_Click(object sender, RoutedEventArgs e)
        {
            userFreeTimeRequestWindow = new UserFreeTimeRequestWindow();
            userFreeTimeRequestWindow.Show();
        }

        private void DecrementWeekButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedWeek--;
            UpdateWeekNumberDisplay();
        }

        private void IncrementWeekButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedWeek++;
            UpdateWeekNumberDisplay();
        }

        private int GetWeeksInYear(int year)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            DateTime dt = new DateTime(year, 12, 31);
            System.Globalization.Calendar cal = dfi.Calendar;
            return cal.GetWeekOfYear(dt, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
        }

        private void UpdateWeekNumberDisplay()
        {
            WeekDisplayNumTextBlock.Text = "Week: " + SelectedWeek;
        }

        private void UpdateShiftsDisplay()
        {

        }
    }
}
