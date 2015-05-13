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
        AddHolidayWindow addHolidayWindow;
        ShiftWindow shiftWindow;

        private int _year = 2015;
        private int _week = 1;
        public int SelectedWeek
        {
            get { return _week; }
            set
            {
                _week = value;
                Console.WriteLine(GetWeeksInYear(_year));
                if(_week > GetWeeksInYear(_year))
                {
                    _week = 1;
                    _year++;
                }
                else if (_week < 1)
                {
                    _year--;
                    _week = GetWeeksInYear(_year);
                }

                this.Title = "P2Manage  -  Week " + _week + "  " + _year;
                    
            }
        }

        public MainWindow()
        {
            if (!Authentication.Instance.Prompt())
                Application.Current.Shutdown();
            InitializeComponent();          
            UpdateWeekNumberDisplay();

            //Temp Shifts
            Core.Instance.Run();
            //Should be called with current week
            LoadDaysToView(1);

        }

        private void LoadDaysToView(int week)
        {
            shiftDataBindingMonday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Monday, week);
            shiftDataBindingTuesday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Tuesday, week);
            shiftDataBindingWednesday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Wednesday, week);
            shiftDataBindingThursday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Thursday, week);
            shiftDataBindingFriday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Friday, week);
            shiftDataBindingSaturday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Saturday, week);
            shiftDataBindingSunday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Sunday, week);
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            if(Authentication.Instance.Prompt())
            {
                addUserWindow = new AddUserWindow();
                addUserWindow.EditUser_Load();
                addUserWindow.ShowDialog();
                
            }
        }

        private void AddTemplate_Click(object sender, RoutedEventArgs e)
        {
            addShiftTemplateWindow = new AddShiftTemplateWindow();
            addShiftTemplateWindow.ShowDialog();
            addShiftTemplateWindow.LoadShift();
        }

        private void LoadSchedule_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void UserFreeRequest_Click(object sender, RoutedEventArgs e)
        {
            userFreeTimeRequestWindow = new UserFreeTimeRequestWindow();
            userFreeTimeRequestWindow.ShowDialog();
        }

        private void DecrementWeekButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedWeek--;
            UpdateWeekNumberDisplay();
            UpdateShiftsDisplay();
        }

        private void IncrementWeekButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedWeek++;
            UpdateWeekNumberDisplay();
            UpdateShiftsDisplay();
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
            LoadDaysToView(SelectedWeek);
        }

        private void MakeHoliday_Click(object sender, RoutedEventArgs e)
        {
            addHolidayWindow = new AddHolidayWindow();
            addHolidayWindow.Load_Holidays();
            addHolidayWindow.ShowDialog();
        }

        private void CreateShift_Click(object sender, RoutedEventArgs e)
        {
            shiftWindow = new ShiftWindow();
            shiftWindow.ShowDialog();
        }
    }
}
