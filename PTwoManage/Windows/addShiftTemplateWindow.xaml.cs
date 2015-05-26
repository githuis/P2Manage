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
           
            LoadShift();
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
            else if (CompareTime(Start_Time.Text, End_Time.Text))
            {
                isValidated = false;
                Error_message.Content = "Start time must be before end time";
            }
            else
            {
                isValidated = true;
            }

            if (isValidated == true)
            {
                ListBoxItem SelectedDay = Day_List.SelectedItem as ListBoxItem;

                int DayInWeek = 0;
                switch (SelectedDay.Content.ToString())
                {
                    case "Monday":
                        DayInWeek = 1;
                        break;
                    case "Tuesday":
                        DayInWeek = 2;
                        break;
                    case "Wednesday":
                        DayInWeek = 3;
                        break;
                    case "Thursday":
                        DayInWeek = 4;
                        break;
                    case "Friday":
                        DayInWeek = 5;
                        break;
                    case "Saturday":
                        DayInWeek = 6;
                        break;
                    case "Sunday":
                        DayInWeek = 7;
                        break;
                    default:
                        DayInWeek = 0;
                        break;
                }

                DateTime Start = new DateTime();
                DateTime End = new DateTime();
                string s = DayInWeek + "/01/2007 ";
                string t = s;
                s += Start_Time.Text;
                t += End_Time.Text;
                s += ":00";
                t += ":00";

                if (DateTime.TryParse(s, out Start) && DateTime.TryParse(t, out End))
                {
                    Start = DateTime.Parse(s);
                    End = DateTime.Parse(t);
                }
                else
                {
                    Console.WriteLine("Damn");
                }

                if (Start < End)
                {

                }

                List<string> TemplateTags = new List<string>();
                foreach (ListBoxItem item in Tag_List.SelectedItems)
                {
                    string tag = item.Content.ToString();
                    TemplateTags.Add(tag);
                }
                ShiftTemplate shiftTemplate = new ShiftTemplate(Start, End, Database.Instance.ListToString(TemplateTags));

                shiftTemplate.SaveInfoShiftTemplate();
                Core.Instance.AddTemplateToList(shiftTemplate);
                Start_Time.Clear();
                End_Time.Clear();
                Error_message.Content = "";
                Tag_List.UnselectAll();
            }

            TemplateList.Items.Refresh();
        }

        private bool Check_If_Valid_Time(string s)
        {
            string[] split;

            if (s == "")
                return false;
            else if (s.Contains(':'))
                split = s.Split(new Char[] { ':' });
            else return false;

            if (split.Length != 2)
                return false;


            try
            {
                if (int.Parse(split[0]) <= 23 && int.Parse(split[1]) <= 59)
                    return true;
            }
            catch
            {

            }

            return false;
        }

        private bool CompareTime(string start, string end)
        {
            string[] splitS = null, splitE = null;
            int startA, startB, endA, endB;

            if (start.Contains(':') && end.Contains(':'))
            {
                splitS = start.Split(new Char[] { ':' });
                splitE = end.Split(new char[] { ':' });
            }
            if (splitS != null &&
                splitE != null &&
                int.TryParse(splitS[0], out startA) &&
                int.TryParse(splitS[1], out startB) &&
                int.TryParse(splitE[0], out endA) &&
                int.TryParse(splitE[1], out endB))
            {
                if (startA > endA)
                    return true;
                else if (startA == endA)
                    return startB > endB;
                else
                    return false;
            }
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
            string toTrim = ";'\\:";
            string newTag = Tag_Add_TextBox.Text.Trim(toTrim.ToCharArray());

            if (newTag != "")
            {
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
            TemplateList.ItemsSource = null;
            TemplateList.ItemsSource = Core.Instance.GetAllTemplates();
        }

        public static bool CompareTags(List<string> UserTags, List<string> ShiftTags)
        {
            return !ShiftTags.Except(UserTags).Any();
        }
        
        private void DeleteTemplate_Click(object sender, RoutedEventArgs e)
        {
            if(Core.Instance.GetAllTemplates().Count > 0 && TemplateList.SelectedItems.Count > 0)
            {
                ShiftTemplate toRemove = null;
                toRemove = Core.Instance.GetAllTemplates().Find(x => x.ToString() == ((ShiftTemplate)TemplateList.SelectedItem).ToString());
                
                if (toRemove != null)
                    toRemove.DeleteShiftTemplate();
            }
            LoadShift();
        }

        private void DeleteTag_Click(object sender, RoutedEventArgs e)
        {
            if(Tag_List.SelectedItems.Count > 0)
            {
                object tag = ((ListBoxItem) Tag_List.SelectedItem).Content;
                string s = tag as string;
                DeleteTag(s);
                Core.Instance.DeleteTagFromList(s);
                Populate_TagList();
            }            
        }
        
        private void DeleteTag(string s)
        {
            string sql;
            sql = "DELETE FROM TagTable WHERE tag IN (SELECT tag FROM TagTable WHERE tag ='" + s + "')";
            Database.Instance.Execute(sql);
        }
    }
}
