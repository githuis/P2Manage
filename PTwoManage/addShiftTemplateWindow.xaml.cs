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
using System.Text.RegularExpressions;

namespace PTwoManage
{
    /// <summary>
    /// Interaction logic for addShiftTemplateWindow.xaml
    /// </summary>
    public partial class AddShiftTemplateWindow : Window
    {
        public AddShiftTemplateWindow()
        {
            InitializeComponent();
        }

        private void EditTime_NumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Save_ShiftTemplate(object sender, RoutedEventArgs e)
        {
            DateTime test;
            DateTime Start = new DateTime();
            DateTime End = new DateTime();
            string test3 = Tag_List.SelectedItems.ToString();

            if(DateTime.TryParse(Start_Time.Text, out test) && DateTime.TryParse(End_Time.Text, out test))
            {
                Start = DateTime.Parse(Start_Time.Text);
                End = DateTime.Parse(End_Time.Text);
            }
            else
            {
                Console.WriteLine("Damn");
            }

            ShiftTemplate test2 = new ShiftTemplate("test", Start, End);

            

            Database.Instance.SaveInfoShiftTemplate(test2);
        }

        private void Tag_Add_To_ListBox_Click(object sender, RoutedEventArgs e)
        {
            Tag_List.Items.Add(Tag_Add_TextBox.Text);
            Tag_Add_TextBox.Clear();
        }
    }
}
