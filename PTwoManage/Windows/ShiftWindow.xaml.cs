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

        private void Populate_UserList()
        {
            UserList.Items.Clear();
            foreach (User u in Core.Instance.GetAllUsers())
            {
                if(TagList.SelectedItems.Count > 0 /*&& u.UserCategories.Contains( ((ListBoxItem) TagList.SelectedItem).Content)*/)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Content = u.UserName;
                    UserList.Items.Add(item);
                }
                else
                {
                    //ListBoxItem item = new ListBoxItem();
                    //item.Content = u.UserName;
                    //UserList.Items.Add(item);
                    u.UserCategories.ForEach(Console.WriteLine);
                }
            }
        }

        private void SaveShift_Click(object sender, RoutedEventArgs e)
        {
            if( Startime_Shift_Box.Text != null && Endtime_Shift_Box.Text != null)
            {
                DateTime start = DateTime.Parse(Startime_Shift_Box.Text);
                DateTime end = DateTime.Parse(Endtime_Shift_Box.Text);
            }
            
            string tag = TagList.SelectedItems.ToString();
            

            if(TagList.SelectedItems.Count > 0)
            {
                Console.WriteLine( ((ListBoxItem) TagList.SelectedItem).Content);
            }
        }

        private void TagList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Populate_UserList();
            Console.WriteLine("IT WERKS");
        }
    }
}