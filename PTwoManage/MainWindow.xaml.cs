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
        GenerateWindow generatorWindow;
        Windows.UserSwapShift userSwapWindow;

        private int _year = 2015;
        private int _week = 1;
        public int SelectedWeek
        {
            get { return _week; }
            set
            {
                _week = value;
                if(_week > Core.Instance.GetWeeksInYear(_year))
                {
                    _week = 1;
                    _year++;
                }
                else if (_week < 1)
                {
                    _year--;
                    _week = Core.Instance.GetWeeksInYear(_year);
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
            shiftDataBindingMonday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Monday, week, _year);
            shiftDataBindingTuesday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Tuesday, week, _year);
            shiftDataBindingWednesday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Wednesday, week, _year);
            shiftDataBindingThursday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Thursday, week, _year);
            shiftDataBindingFriday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Friday, week, _year);
            shiftDataBindingSaturday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Saturday, week, _year);
            shiftDataBindingSunday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Sunday, week, _year);
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            if(Authentication.Instance.Prompt(Database.Instance.CompanyName))
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

        //Opens GeneratoWindow
        private void ScheduleGenerator_Click(object sender, RoutedEventArgs e)
        {
            generatorWindow = new GenerateWindow();
            generatorWindow.ShowDialog();
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

        private void UpdateWeekNumberDisplay()
        {
            WeekDisplayNumTextBlock.Text = "Week: " + SelectedWeek;
        }

        private void UpdateShiftsDisplay()
        {
            LoadDaysToView(SelectedWeek);
        }

        private void ShiftSwap_Click(object sender, RoutedEventArgs e)
        {
            userSwapWindow = new Windows.UserSwapShift();
            userSwapWindow.ShowDialog();
        }
        
    }
}
