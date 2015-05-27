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

        // Checks that the character you enter is a number, if not, don't type it.
        private void EditTime_NumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9:]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        // If: Starttime is valid format, endtime is valid format and there is a day selected
        //starttime is before endtime
        //
        private void Save_ShiftTemplate(object sender, RoutedEventArgs e)
        {
            // If the bool isValidated at one of the checks is marked as false the ShiftTemplate will not be created.
            // Instead a message explaning the error will be written
            bool isValidated;
            if (!CheckIfValidTime(Start_Time.Text))
            {
                isValidated = false;
                Error_message.Content = "Start time is not a valid hour format";
            }

            else if (!CheckIfValidTime(End_Time.Text))
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

                // Creates a datetime with the given times.
                DateTime Start;
                DateTime End;
                string s = DayInWeek + "/01/2007 ";
                string t = s;
                s += Start_Time.Text;
                t += End_Time.Text;
                s += ":00";
                t += ":00";

                if (!(DateTime.TryParse(s, out Start) && DateTime.TryParse(t, out End)))
                {
                    throw new ArgumentException("Start or End is not a valid datetime");
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
        }

        // Checks wether a string is a valid HH:MM format.
        private bool CheckIfValidTime(string s)
        {
            string[] split;

            if (s == "")
                return false;
            else if (s.Contains(':'))
                split = s.Split(new Char[] { ':' });
            else return false;

            if (split.Length != 2)
                return false;

            if (int.Parse(split[0]) <= 23 && int.Parse(split[1]) <= 59)
                return true;
            else
                return false;
        }

        //Checks wether starttime is before endtime
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

        //Called when Save Tag is clicked
        //Adds tag to taglist and clears textbox
        private void Tag_Add_To_ListBox_Click(object sender, RoutedEventArgs e)
        {
            Tag_List.Items.Add(Tag_Add_TextBox.Text);
            Tag_Add_TextBox.Clear();
        }

        //Fills the list of tags with all existing tags
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

        //Saves tags to database
        public void SaveTagToDatabase(string s)
        {
            string tag = s;
            string sql = "INSERT INTO TagTable (tag) values ( '" + tag + "')";
            Database.Instance.Execute(sql);
        }

        //Populates lists when called
        public void LoadShift()
        {
            Populate_TagList();
            TemplateList.ItemsSource = null;
            TemplateList.ItemsSource = Core.Instance.GetAllTemplates();
        }
        
        //Called when Delete Template is Clicked
        //Finds a ShiftTemplate, then removes it, then reloads list
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

        //Removes a tag, if the taglist isn't empty
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
        
        //Deletes tag from the database
        private void DeleteTag(string s)
        {
            string sql;
            sql = "DELETE FROM TagTable WHERE tag IN (SELECT tag FROM TagTable WHERE tag ='" + s + "')";
            Database.Instance.Execute(sql);
        }
    }
}
