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
using Xceed.Wpf.Toolkit;
using System.Globalization;

namespace PTwoManage
{
    /// <summary>
    /// Interaction logic for Window1.xaml
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
            foreach (string tag in Core.Instance.GetAllTags())
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = tag as string;
                TagList.Items.Add(item);
            }
        }

        //Fills the list of users acording to which tags are selected
        private void Populate_UserList()
        {
            UserList.Items.Clear();
            foreach (User u in Core.Instance.GetAllUsers())
            {
                if (TagList.SelectedItems.Count > 0 && IsUserCompatibleWithTags(u)) /*&& u.UserCategories.Contains( ((ListBoxItem) TagList.SelectedItem).Content)*/
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Content = u.UserName;
                    UserList.Items.Add(item);
                }
                else if (TagList.SelectedItems.Count == 0)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Content = u.UserName;
                    UserList.Items.Add(item);
                }
            }
        }

        //Checks if user is compatible with selected tags
        private bool IsUserCompatibleWithTags(User u)
        {
            foreach (object obj in TagList.SelectedItems)
            {
                if(!u.Tags.Contains(((ListBoxItem) obj).Content))
                    return false;
            }

            return true;
        }


        private void SaveShift_Click(object sender, RoutedEventArgs e)
        {
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;
            Shift toAdd = null;
            User us = null;

            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            var cal =  dfi.Calendar;


            if( Startime_Shift_Box.Text != null && Endtime_Shift_Box.Text != null)
            {
                start = DateTime.Parse(Startime_Shift_Box.Text);
                end = DateTime.Parse(Endtime_Shift_Box.Text);
            }
            
            string tag = "";
            if(TagList.SelectedItems != null)
                tag = TagList.SelectedItems.ToString();

            if(UserList.SelectedItems.Count == 1)
                us = User.GetUserByName( (string)((ListBoxItem) UserList.SelectedItem).Content);

            if( us != null && start != null && end != null)
            {
                Console.WriteLine( ((ListBoxItem) TagList.SelectedItem).Content);

                toAdd = new Shift(start, end, tag, us.UserName, cal.GetWeekOfYear(start, dfi.CalendarWeekRule, dfi.FirstDayOfWeek));
            }

            if (toAdd != null)
                Core.Instance.GetAllShifts().Add(toAdd);
        }

        private void TagList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Populate_UserList();
            Console.WriteLine("IT WERKS");
        }
    }
}