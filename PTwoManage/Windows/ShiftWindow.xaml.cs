using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PTwoManage
{
    /// <summary>
    ///     Interaction logic for Window1.xaml
    /// </summary>
    public partial class ShiftWindow : Window
    {
        public ShiftWindow()
        {
            InitializeComponent();
            LoadShift();
        }

        private void LoadShift()
        {
            Populate_TagList();
            Populate_UserList();
        }

        //Fils TagList with all existing tags
        private void Populate_TagList()
        {
            TagList.Items.Clear();
            foreach (var tag in Core.Instance.GetAllTags())
            {
                var item = new ListBoxItem();
                item.Content = tag;
                TagList.Items.Add(item);
            }
        }

        //Fills the list of users acording to which tags are selected
        private void Populate_UserList()
        {
            UserList.Items.Clear();
            foreach (var u in Core.Instance.GetAllUsers())
                if ((TagList.SelectedItems.Count > 0) && IsUserCompatibleWithTags(u))
                    /*&& u.UserCategories.Contains( ((ListBoxItem) TagList.SelectedItem).Content)*/
                {
                    var item = new ListBoxItem();
                    item.Content = u.UserName;
                    UserList.Items.Add(item);
                }
                else if (TagList.SelectedItems.Count == 0)
                {
                    var item = new ListBoxItem();
                    item.Content = u.UserName;
                    UserList.Items.Add(item);
                }
        }

        //Checks if user is compatible with selected tags
        private bool IsUserCompatibleWithTags(User u)
        {
            foreach (var obj in TagList.SelectedItems)
                if (!u.Tags.Contains(((ListBoxItem) obj).Content))
                    return false;

            return true;
        }

        // A method for saving a custom Shift to the database
        private void SaveShift_Click(object sender, RoutedEventArgs e)
        {
            var start = DateTime.Now;
            var end = DateTime.Now;
            Shift toAdd = null;
            User us = null;

            // These objects are used for finding the specified week
            var dfi = DateTimeFormatInfo.CurrentInfo;
            var cal = dfi.Calendar;

            // Checks if the inputboxes are empty. The information is not TryPares since the DatePicker do not allow users
            // To enter and invalid DateTime
            if ((Startime_Shift_Box.Text != null) && (Endtime_Shift_Box.Text != null))
            {
                start = DateTime.Parse(Startime_Shift_Box.Text);
                end = DateTime.Parse(Endtime_Shift_Box.Text);
            }

            var tag = "";
            if (TagList.SelectedItems != null)
                tag = TagList.SelectedItems.ToString();

            if (UserList.SelectedItems.Count == 1)
                us = User.GetUserByName((string) ((ListBoxItem) UserList.SelectedItem).Content);

            if ((us != null) && (start != null) && (end != null))
            {
                Console.WriteLine(((ListBoxItem) TagList.SelectedItem).Content);

                toAdd = new Shift(start, end, tag, us.UserName,
                    cal.GetWeekOfYear(start, dfi.CalendarWeekRule, dfi.FirstDayOfWeek));
            }

            if (toAdd != null)
                Core.Instance.GetAllShifts().Add(toAdd);
        }

        //Repopulates list to reflect changes to selected
        private void TagList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Populate_UserList();
        }
    }
}