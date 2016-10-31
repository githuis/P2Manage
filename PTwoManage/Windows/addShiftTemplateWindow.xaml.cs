using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PTwoManage
{
    /// <summary>
    ///     Interaction logic for addShiftTemplateWindow.xaml
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
            var regex = new Regex("[^0-9:]+");
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

            // If all validation checkout then the ShiftTemplate infomration is exstracted and saved to the database
            if (isValidated)
            {
                var SelectedDay = Day_List.SelectedItem as ListBoxItem;

                var DayInWeek = 0;
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
                var s = DayInWeek + "/01/2007 ";
                var t = s;
                s += Start_Time.Text;
                t += End_Time.Text;
                s += ":00";
                t += ":00";

                // If the datatime is not able to be parsed to a DataTime object, the ShiftTemplate is not made
                // As of the validation this should not be able to happen
                if (!(DateTime.TryParse(s, out Start) && DateTime.TryParse(t, out End)))
                    throw new ArgumentException("Start or End is not a valid datetime");

                // Gets the selected tags and adds them to a list of strings
                var TemplateTags = new List<string>();
                foreach (ListBoxItem item in Tag_List.SelectedItems)
                {
                    var tag = item.Content.ToString();
                    TemplateTags.Add(tag);
                }

                // The ShiftTemplate object is made with the gathered information
                var shiftTemplate = new ShiftTemplate(Start, End, Database.Instance.ListToString(TemplateTags));
                shiftTemplate.SaveInfoShiftTemplate();

                // After the object is saved it is saved to the database and the inputboxes are cleared
                Core.Instance.AddTemplateToList(shiftTemplate);
                Start_Time.Clear();
                End_Time.Clear();
                Error_message.Content = "";
                Tag_List.UnselectAll();
                LoadShift();
            }
        }

        // Checks wether a string is a valid HH:MM format.
        private bool CheckIfValidTime(string s)
        {
            string[] split;

            if (s == "")
                return false;
            if (s.Contains(':'))
                split = s.Split(':');
            else return false;

            if (split.Length != 2)
                return false;

            if ((int.Parse(split[0]) <= 23) && (int.Parse(split[1]) <= 59))
                return true;
            return false;
        }

        //Checks wether starttime is before endtime
        private bool CompareTime(string start, string end)
        {
            string[] splitS = null, splitE = null;
            int startA, startB, endA, endB;

            if (start.Contains(':') && end.Contains(':'))
            {
                splitS = start.Split(':');
                splitE = end.Split(':');
            }
            if ((splitS != null) &&
                (splitE != null) &&
                int.TryParse(splitS[0], out startA) &&
                int.TryParse(splitS[1], out startB) &&
                int.TryParse(splitE[0], out endA) &&
                int.TryParse(splitE[1], out endB))
                if (startA > endA)
                    return true;
                else if (startA == endA)
                    return startB > endB;
                else
                    return false;
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
            foreach (var tag in Core.Instance.GetAllTags())
            {
                var item = new ListBoxItem();
                item.Content = tag;
                Tag_List.Items.Add(item);
            }
        }

        // This method is called when a new tag should be added to the list of tags and saved to the database
        private void Add_Tag_Click(object sender, RoutedEventArgs e)
        {
            var toTrim = ";'\\:";
            var newTag = Tag_Add_TextBox.Text.Trim(toTrim.ToCharArray());

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
            var tag = s;
            var sql = "INSERT INTO TagTable (tag) values ( '" + tag + "')";
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
            if ((Core.Instance.GetAllTemplates().Count > 0) && (TemplateList.SelectedItems.Count > 0))
            {
                ShiftTemplate toRemove = null;
                toRemove =
                    Core.Instance.GetAllTemplates()
                        .Find(x => x.ToString() == ((ShiftTemplate) TemplateList.SelectedItem).ToString());

                if (toRemove != null)
                    toRemove.DeleteShiftTemplate();
            }
            LoadShift();
        }

        //Removes a tag, if the taglist isn't empty
        private void DeleteTag_Click(object sender, RoutedEventArgs e)
        {
            if (Tag_List.SelectedItems.Count > 0)
            {
                var tag = ((ListBoxItem) Tag_List.SelectedItem).Content;
                var s = tag as string;
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