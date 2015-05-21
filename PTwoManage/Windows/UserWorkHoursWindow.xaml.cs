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
using System.Globalization;

namespace PTwoManage.Windows
{
    /// <summary>
    /// Interaction logic for UserWorkOursWindow.xaml
    /// </summary>
    public partial class UserWorkHoursWindow : Window
    {
        public UserWorkHoursWindow()
        {
            InitializeComponent();
            Populate_User_Combobox();

            var americanCulture = new CultureInfo("en-US");
            var count = 1;
            foreach (var month in americanCulture.DateTimeFormat.MonthNames.Take(12))
            {
                Selected_Month.Items.Add(new ComboBoxItem { Content = month });
                count++;
            }
        }

        private void Selected_user_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime month = ConvertSelectedMonth(Selected_Month.Text);
            User target = Selected_user.SelectedItem as User;
            double HoursThisMonth = 0;
            TimeSpan HoursInShift = new TimeSpan(0);


            foreach (Shift s in Core.Instance.GetAllShifts())
            {
                if (s._endTime < DateTime.Now && s._startTime.Month == month.Month && s.EmployeeName == target.UserName)
                {
                    HoursInShift += s._endTime - s._startTime;
                }
            }
            HoursThisMonth = HoursInShift.TotalHours;
            WorkHours.Text = HoursThisMonth.ToString();
        }

        private void Populate_User_Combobox()
        {
            Selected_user.ItemsSource = Core.Instance.GetAllUsers();
        }

        private void Selected_Month_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private DateTime ConvertSelectedMonth(string selectedMonth)
        {
            DateTime resultMonth;
            switch (selectedMonth)
            {
                case "January":
                    resultMonth = new DateTime(DateTime.Now.Year, 1, 1);
                    break;
                case "February":
                    resultMonth = new DateTime(DateTime.Now.Year, 2, 1);
                    break;
                case "March":
                    resultMonth = new DateTime(DateTime.Now.Year, 3, 1);
                    break;
                case "April":
                    resultMonth = new DateTime(DateTime.Now.Year, 4, 1);
                    break;
                case "May":
                    resultMonth = new DateTime(DateTime.Now.Year, 5, 1);
                    break;
                case "June":
                    resultMonth = new DateTime(DateTime.Now.Year, 6, 1);
                    break;
                case "July":
                    resultMonth = new DateTime(DateTime.Now.Year, 7, 1);
                    break;
                case "August":
                    resultMonth = new DateTime(DateTime.Now.Year, 8, 1);
                    break;
                case "September":
                    resultMonth = new DateTime(DateTime.Now.Year, 9, 1);
                    break;
                case "October":
                    resultMonth = new DateTime(DateTime.Now.Year, 10, 1);
                    break;
                case "November":
                    resultMonth = new DateTime(DateTime.Now.Year, 11, 1);
                    break;
                case "December":
                    resultMonth = new DateTime(DateTime.Now.Year, 12, 1);
                    break;
                default:
                    resultMonth = new DateTime();
                    break;
            }
            return resultMonth;
        }
        /*
                    <ComboBoxItem>January</ComboBoxItem>
            <ComboBoxItem>February</ComboBoxItem>
            <ComboBoxItem>March</ComboBoxItem>
            <ComboBoxItem>April</ComboBoxItem>
            <ComboBoxItem>May</ComboBoxItem>
            <ComboBoxItem>June</ComboBoxItem>
            <ComboBoxItem>July</ComboBoxItem>
            <ComboBoxItem>August</ComboBoxItem>
            <ComboBoxItem>September</ComboBoxItem>
            <ComboBoxItem>October</ComboBoxItem>
            <ComboBoxItem>November</ComboBoxItem>
            <ComboBoxItem>December</ComboBoxItem>
         */
    }
}
