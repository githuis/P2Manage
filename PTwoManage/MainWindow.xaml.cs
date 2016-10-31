using System;
using System.ComponentModel;
using System.Windows;
using PTwoManage.Windows;

namespace PTwoManage
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Force the user to log in before accessing the actual application. If log in fails, exit program
        //Then loads window and loads all seven days with the current week.
        public MainWindow()
        {
            try
            {
                if (!Authentication.Instance.Prompt())
                    Application.Current.Shutdown();


                InitializeComponent();
                InstanciateCore();
                SelectedWeek = Core.Instance.GetWeeksInYear(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                UpdateWeekNumberDisplay();
                LoadDaysToView(SelectedWeek);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.InnerException.Message);
            }
        }

        private int _week = 1;

        private int _year = 2015;
        private AddHolidayWindow addHolidayWindow;
        private AddShiftTemplateWindow addShiftTemplateWindow;
        private AddUserWindow addUserWindow;
        private DeleteShiftsWindow deleteShiftWindow;
        private GenerateWindow generatorWindow;
        private ShiftWindow shiftWindow;
        private UserFreeTimeRequestWindow userFreeTimeRequestWindow;
        private UserSwapShift userSwapWindow;
        private UserWorkHoursWindow userWorkHoursWindow;

        //Handles the value for _week so that it remains from 1 to the maximum week in year.
        //If the value goes above max or below min it also changes the year.
        //This is what Main Window uses to determine what week it should display
        public int SelectedWeek
        {
            get { return _week; }
            set
            {
                _week = value;
                if (_week > Core.Instance.GetWeeksInYear(_year))
                {
                    _week = 1;
                    _year++;
                }
                else if (_week < 1)
                {
                    _year--;
                    _week = Core.Instance.GetWeeksInYear(_year);
                }

                Title = "P2Manage  -  Week " + _week + "  " + _year;
            }
        }

        //Loads all the info from the database into core.
        private void InstanciateCore()
        {
            Core.Instance.LoadShiftsFromDatabase();
            Core.Instance.LoadUserFreeRequestsFromDatabase();
            Core.Instance.LoadShiftTemplatesFromDatabase();
            Core.Instance.LoadTagsFromDatabase();
            Core.Instance.LoadUsersFromDatabase();
        }

        //For all seven days in the week:
        //Load all shifts with a given Day, Weeknumber and year.
        private void LoadDaysToView(int week)
        {
            shiftDataBindingMonday.ItemsSource = Core.Instance.GetAllShiftsForDayInWeekInYear(DayOfWeek.Monday, week,
                _year);
            shiftDataBindingTuesday.ItemsSource = Core.Instance.GetAllShiftsForDayInWeekInYear(DayOfWeek.Tuesday, week,
                _year);
            shiftDataBindingWednesday.ItemsSource = Core.Instance.GetAllShiftsForDayInWeekInYear(DayOfWeek.Wednesday,
                week, _year);
            shiftDataBindingThursday.ItemsSource = Core.Instance.GetAllShiftsForDayInWeekInYear(DayOfWeek.Thursday, week,
                _year);
            shiftDataBindingFriday.ItemsSource = Core.Instance.GetAllShiftsForDayInWeekInYear(DayOfWeek.Friday, week,
                _year);
            shiftDataBindingSaturday.ItemsSource = Core.Instance.GetAllShiftsForDayInWeekInYear(DayOfWeek.Saturday, week,
                _year);
            shiftDataBindingSunday.ItemsSource = Core.Instance.GetAllShiftsForDayInWeekInYear(DayOfWeek.Sunday, week,
                _year);
        }

        //Is called when Menu > Manage Users is clicked
        //Opens a window where Users can be edited.
        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            if (Authentication.Instance.Prompt(Database.Instance.CompanyName))
            {
                addUserWindow = new AddUserWindow();
                addUserWindow.EditUser_Load();
                addUserWindow.ShowDialog();
            }
        }

        //Is called when Menu > Schedule > Templates and Tags is clicked.
        //Opens a window for editing ShiftTemplates and Tags
        private void AddTemplate_Click(object sender, RoutedEventArgs e)
        {
            addShiftTemplateWindow = new AddShiftTemplateWindow();
            addShiftTemplateWindow.ShowDialog();
        }

        //Is called when Menu > Utilities > Create holiday is clicked.
        //Opens a window for creating and deleting holidays
        private void MakeHoliday_Click(object sender, RoutedEventArgs e)
        {
            addHolidayWindow = new AddHolidayWindow();
            addHolidayWindow.Load_Holidays();
            addHolidayWindow.ShowDialog();
        }

        //Is called when Menu > Schedule > Create Extra Shifts is clicked.
        //Opens a window for making additional shifts.
        private void CreateShift_Click(object sender, RoutedEventArgs e)
        {
            shiftWindow = new ShiftWindow();
            shiftWindow.ShowDialog();
        }

        //Is called when Menu > Schedule > Generate Schedule is clicked
        //Opens a window for generating the schedule.
        private void ScheduleGenerator_Click(object sender, RoutedEventArgs e)
        {
            generatorWindow = new GenerateWindow();
            generatorWindow.ShowDialog();
        }

        //Is called when Menu > Schedule > Request Day Off is clicked
        //Opens a window for making time off for employees
        private void UserFreeRequest_Click(object sender, RoutedEventArgs e)
        {
            userFreeTimeRequestWindow = new UserFreeTimeRequestWindow();
            userFreeTimeRequestWindow.ShowDialog();
        }

        //Is called when the Previous button is clicked
        //Decrements the week, then updates the display from the new week value
        private void DecrementWeekButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedWeek--;
            UpdateWeekNumberDisplay();
            UpdateShiftsDisplay();
        }

        //Is called when the Next button is clicked
        //Increments the week, then updates the display from the new week value
        private void IncrementWeekButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedWeek++;
            UpdateWeekNumberDisplay();
            UpdateShiftsDisplay();
        }

        //Changes the text between Previous and Next buttons to Week: num
        //Where num is the currently selected week
        private void UpdateWeekNumberDisplay()
        {
            WeekDisplayNumTextBlock.Text = "Week: " + SelectedWeek;
        }

        //Loads Shifts with current week number
        private void UpdateShiftsDisplay()
        {
            LoadDaysToView(SelectedWeek);
        }

        //Is called when Menu > Utilities > Swap Shift is clicked
        //Opens a window for interchanging Shifts between users
        private void ShiftSwap_Click(object sender, RoutedEventArgs e)
        {
            userSwapWindow = new UserSwapShift();
            userSwapWindow.ShowDialog();
        }

        //Closes the connection to the database when the program closes
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            Database.Instance.m_dbConnection.Close();
        }

        //Is called when Menu > Utilities > Lookup Users Hours is clicked
        //Opens a window for checking how many hours a user has worked
        private void LookUpHours_Click(object sender, RoutedEventArgs e)
        {
            userWorkHoursWindow = new UserWorkHoursWindow();
            userWorkHoursWindow.ShowDialog();
        }

        //Is called when Menu > Schedule > Delete Shifts is clicked
        //Opens a window for deleting existing shifts
        private void DeleteShifts_Click(object sender, RoutedEventArgs e)
        {
            deleteShiftWindow = new DeleteShiftsWindow();
            deleteShiftWindow.ShowDialog();
        }
    }
}