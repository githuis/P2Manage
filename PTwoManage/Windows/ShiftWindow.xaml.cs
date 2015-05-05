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
        }

        public void LoadShift()
        {
            Populate_TagList();
            Populate_UserList();
        }

        private void Populate_TagList()
        {
            Shift_TagList.Items.Clear();
            foreach (string tag in Core.Instance.GetAllTags())
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = tag as string;
                Shift_TagList.Items.Add(item);
            }
        }

        private void Populate_UserList()
        {
            Shift_UserList.Items.Clear();
            foreach (User u in Core.Instance.GetAllUsers())
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = u.UserName;
                Shift_UserList.Items.Add(item);
            }
        }

        private void Save_Shift_Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime start = new DateTime();
            DateTime end = new DateTime();
            string tag = Shift_TagList.SelectedItems.ToString();
            start = DateTime.Parse(Startime_Shift_Box.Text);
            end = DateTime.Parse(Endtime_Shift_Box.Text);

        }
    }
}