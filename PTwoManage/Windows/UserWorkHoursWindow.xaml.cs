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

            for (int i = DateTime.Now.Year; i >= 1990; i--)
            {
                string year = i.ToString();
                Selected_Year.Items.Add(new ComboBoxItem { Content = year });
            }
        }

        private void CalcMonthHours()
        {
            if (Selected_user.SelectedItem != null && Selected_Month.SelectedItem != null &&  Selected_Year.SelectedItem != null)
            {
                DateTime selectedDate = ConvertSelectedMonth(Selected_Month.Text, Selected_Year.Text);
                User target = Selected_user.SelectedItem as User;
                double HoursThisMonth = 0;
                TimeSpan HoursInShift = new TimeSpan(0);

                foreach (Shift s in Core.Instance.GetAllShifts())
                {
                    if (s.EndTime < DateTime.Now && s.StartTime.Year == selectedDate.Year && s.StartTime.Month == selectedDate.Month && s.UserName == target.UserName)
                    {
                        HoursInShift += s.EndTime - s.StartTime;
                    }
                }
                HoursThisMonth = HoursInShift.TotalHours;
                WorkHours.Text = HoursThisMonth.ToString();
            }
            else
                WorkHours.Text = "Waiting for input";
        }

        private void Populate_User_Combobox()
        {
            Selected_user.ItemsSource = Core.Instance.GetAllUsers();
        }

        private DateTime ConvertSelectedMonth(string selectedMonth, string selectedYear)
        {
            int resultYear = int.Parse(selectedYear);
            DateTime resultDate;
            switch (selectedMonth)
            {
                case "January":
                    resultDate = new DateTime(resultYear, 1, 1);
                    break;
                case "February":
                    resultDate = new DateTime(resultYear, 2, 1);
                    break;
                case "March":
                    resultDate = new DateTime(resultYear, 3, 1);
                    break;
                case "April":
                    resultDate = new DateTime(resultYear, 4, 1);
                    break;
                case "May":
                    resultDate = new DateTime(resultYear, 5, 1);
                    break;
                case "June":
                    resultDate = new DateTime(resultYear, 6, 1);
                    break;
                case "July":
                    resultDate = new DateTime(resultYear, 7, 1);
                    break;
                case "August":
                    resultDate = new DateTime(resultYear, 8, 1);
                    break;
                case "September":
                    resultDate = new DateTime(resultYear, 9, 1);
                    break;
                case "October":
                    resultDate = new DateTime(resultYear, 10, 1);
                    break;
                case "November":
                    resultDate = new DateTime(resultYear, 11, 1);
                    break;
                case "December":
                    resultDate = new DateTime(resultYear, 12, 1);
                    break;
                default:
                    resultDate = new DateTime();
                    break;
            }
            return resultDate;
        }

        private void CalcHours_Button_Click(object sender, RoutedEventArgs e)
        {
            CalcMonthHours();
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
