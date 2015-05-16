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

namespace PTwoManage
{
    /// <summary>
    /// Interaction logic for AddHolidayWindow.xaml
    /// </summary>
    public partial class AddHolidayWindow : Window
    {
        public AddHolidayWindow()
        {
            InitializeComponent();
        }

        private void Submit_OffDay_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIfHolidayExsists(FreeDate.SelectedDate.Value))
            {
                ConfirmationMessage.Content = "Holiday: " + FreeDate.SelectedDate.Value.ToLongDateString() + " already exsists";
            }
            else
            {
                Holiday resultHoliday = new Holiday(FreeDate.SelectedDate.Value);
                resultHoliday.SaveHoliday();
                Core.Instance.AddToHolidayList(resultHoliday);
                ConfirmationMessage.Content = "Holiday: " + FreeDate.SelectedDate.Value.ToLongDateString() +" is now a holiday :)";
                Populate_HolidayList();
            }
        }

        private void Populate_HolidayList()
        {
            Current_Holidays.Items.Clear();
            foreach (Holiday h in Core.Instance.GetAllHolidays())
            {
                ListBoxItem item = new ListBoxItem();
                TextBlock text = new TextBlock();
                text.Text = h.Date.ToLongDateString();
                item.Content = text;
                Current_Holidays.Items.Add(item);
            }
        }

        public void Load_Holidays()
        {
            Populate_HolidayList();
        }

        private bool CheckIfHolidayExsists(DateTime SubmittetDate)
        {
            List<Holiday> AllHolidays = Core.Instance.GetAllHolidays();
            foreach(Holiday h in AllHolidays)
            {
                if (h.Date == SubmittetDate)
                    return true;
            }
            return false;
        }

        private void RemoveOffDay_Click(object sender, RoutedEventArgs e)
        {
            if(Current_Holidays.SelectedItems.Count > 0)
            {
                string date = ((TextBlock)((ListBoxItem)Current_Holidays.SelectedItem).Content).Text;

                foreach (Holiday h in Core.Instance.GetAllHolidays())
                {
                    if (date == h.Date.ToLongDateString())
                    {
                        h.RemoveHoliday();
                        break;
                    }
                }
                Populate_HolidayList();
            } 
        }
    }
}
