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
            Regex regex = new Regex("[^0-9:]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Save_ShiftTemplate(object sender, RoutedEventArgs e)
        {
            bool isValidated;
            if (!Check_If_Valid_Time(Start_Time.Text))
            {
                isValidated = false;
                Error_message.Content = "Start time is not a valid hour format";
            }
            else if (!Check_If_Valid_Time(End_Time.Text))
            {
                isValidated = false;
                Error_message.Content = "End time is not a valid minute format";
            }
            else if (Day_List.SelectedItem == null)
            {
                isValidated = false;
                Error_message.Content = "A day haven't been selected";
            }
            else
            {
                isValidated = true;
            }

            DateTime test6;
            DateTime test7;
            DateTime Start = new DateTime();
            DateTime End = new DateTime();
            string test3 = Tag_List.SelectedItems.ToString();
            string s = "01/01/1995 ";
            string t = s;
            s += Start_Time.Text;
            t += End_Time.Text;
            s += ":00";
            t += ":00";

            if (DateTime.TryParse(s, out test6) && DateTime.TryParse(t, out test7))
            {
                Start = DateTime.Parse(s);
                End = DateTime.Parse(t);
            }
            else
            {
                Console.WriteLine("Damn");
            }

            List<string> TemplateTags = new List<string>();
            foreach (object item in Tag_List.SelectedItems)
            {
                string tag = item as string;
                TemplateTags.Add(tag);
            }

            if (isValidated == true)
            {
                ListBoxItem SelectedDay = Day_List.SelectedItem as ListBoxItem;
                ShiftTemplate test2 = new ShiftTemplate(SelectedDay.Content.ToString(), Start, End, Database.Instance.listToString(TemplateTags));
                test2.SaveInfoShiftTemplate();
                Error_message.Content = "";
            }
        }

        private bool Check_If_Valid_Time(string s)
        {
            string[] split;

            if (s == "")
                return false;
            else if (s.Contains(':'))
                split = s.Split(new Char[] { ':' });
            else return false;
            
            if (int.Parse(split[0].ToString()) <= 23 && int.Parse(split[1].ToString()) <= 59)
                return true;
            else
                return false;

        }

        private void Tag_Add_To_ListBox_Click(object sender, RoutedEventArgs e)
        {
            Tag_List.Items.Add(Tag_Add_TextBox.Text);
            Tag_Add_TextBox.Clear();
        }

        private void Populate_TagList()
        {
            Tag_List.Items.Clear();
            foreach (string tag in Core.Instance.GetAllTags())
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = tag as string;
                Tag_List.Items.Add(item);
            }
        }

        private void Add_Tag_Click(object sender, RoutedEventArgs e)
        {

            if (Tag_Add_TextBox.Text != "")
            {
                string newTag = Tag_Add_TextBox.Text;
                Core.Instance.AddTagToList(newTag);
                SaveTagToDatabase(newTag);
                Populate_TagList();
                Tag_Add_TextBox.Clear();
            }   
        }

        public void SaveTagToDatabase(string s)
        {
            string tag = s;
            string sql = "INSERT INTO TagTable (tag) values ( '" + tag + "')";
            Database.Instance.Execute(sql);
        }

        public void LoadShift()
        {
            Populate_TagList();
        }

        public void DeleteTag(string s)
        {
            string tag = s;
            foreach (ListBoxItem item in Tag_List.Items)
            {
                string itemTag = item.Name;
                if (itemTag == tag)
                {
                    string sql;
                    sql = "DELETE FROM TagTable WHERE tag IN (SELECT tag FROM TagTable WHERE tag ='" + item + "')";
                    Database.Instance.Execute(sql);
                }
            }
        }

        private void Tag_Delete_From_Listbox_Click(object sender, RoutedEventArgs e)
        {
            object tag = Tag_List.SelectedItem;
            string s = tag as string;
            Console.WriteLine(s);
            DeleteTag(s);
            Core.Instance.DeleteTagFromList(s);
            Populate_TagList();
        }

        // skal smides ind rigtige sted, når det er klar
        public void CompareTags(ShiftTemplate shift, User user)
        {
            foreach (string tag in user.UserCategories)
            {
                if (shift.Tag.Contains(tag))
                {
                    // add user to shift
                }
            }
        }
    }
}
