using System;
using System.Windows;
using System.Windows.Controls;

namespace PTwoManage
{
    /// <summary>
    ///     Interaction logic for AddHolidayWindow.xaml
    /// </summary>
    public partial class AddHolidayWindow : Window
    {
        public AddHolidayWindow()
        {
            InitializeComponent();
        }

        // The method checks if the holiday already exsists and writes a error message if this is the case.
        // Else the holiday is made
        private void Submit_Holiday_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIfHolidayExsists(FreeDate.SelectedDate.Value))
            {
                ConfirmationMessage.Content = FreeDate.SelectedDate.Value.ToLongDateString() + " \nalready exsists";
            }
            else
            {
                var resultHoliday = new Holiday(FreeDate.SelectedDate.Value);
                resultHoliday.SaveHoliday();
                Core.Instance.AddToHolidayList(resultHoliday);
                ConfirmationMessage.Content = FreeDate.SelectedDate.Value.ToLongDateString() + " \nis now a holiday :)";
                Populate_HolidayList();
            }
        }

        // Fills the list with curent holidays
        private void Populate_HolidayList()
        {
            Current_Holidays.Items.Clear();
            foreach (var h in Core.Instance.GetAllHolidays())
            {
                var item = new ListBoxItem();
                var text = new TextBlock();
                text.Text = h.Date.ToLongDateString();
                item.Content = text;
                Current_Holidays.Items.Add(item);
            }
        }

        // Public variant of Populate_HolidayList()
        public void Load_Holidays()
        {
            Populate_HolidayList();
        }

        // Returns a bool based on wether there already is a holiday for this DateTime (Day and Time)
        private bool CheckIfHolidayExsists(DateTime SubmittetDate)
        {
            var AllHolidays = Core.Instance.GetAllHolidays();
            foreach (var h in AllHolidays)
                if (h.Date == SubmittetDate)
                    return true;
            return false;
        }

        // If there are holidays, check all holidays wether they match the clicked one
        // if it does, remove it and repopulate list.
        private void RemoveHoliday_Click(object sender, RoutedEventArgs e)
        {
            if (Current_Holidays.SelectedItems.Count > 0)
            {
                var date = ((TextBlock) ((ListBoxItem) Current_Holidays.SelectedItem).Content).Text;

                foreach (var h in Core.Instance.GetAllHolidays())
                    if (date == h.Date.ToLongDateString())
                    {
                        h.RemoveHoliday();
                        break;
                    }
                Populate_HolidayList();
            }
        }
    }
}