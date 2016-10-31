using System;
using System.Windows;
using System.Windows.Input;
using MessageBox = System.Windows.Forms.MessageBox;

namespace PTwoManage
{
    /// <summary>
    ///     Interaction logic for GenerateWindow.xaml
    /// </summary>
    public partial class GenerateWindow : Window
    {
        public GenerateWindow()
        {
            InitializeComponent();
            SetYearBoxText();
            SetWeekComboBoxes();
        }

        private int yr, fromWeek, toWeek;

        //Sets the year textbox's text to the current year
        private void SetYearBoxText()
        {
            YearTextBox.Text = DateTime.Now.Year.ToString();
        }

        //Gets the max week for selected year, and adds weeks from 1 to max to the comboboxes
        private bool SetWeekComboBoxes()
        {
            var max = 0;


            if (int.TryParse(YearTextBox.Text, out max))
            {
                max = Core.Instance.GetWeeksInYear(max);
                FromWeekComboBox.Items.Clear();
                ToWeekComboBox.Items.Clear();

                for (var i = 1; i <= max; i++)
                {
                    FromWeekComboBox.Items.Add(i);
                    ToWeekComboBox.Items.Add(i);
                }
                FromWeekComboBox.SelectedItem = 1;
                ToWeekComboBox.SelectedItem = 1;
                return true;
            }
            return false;
        }

        //Checks if weeks are valid
        //if they are open a window with a progressbar in it
        //Then calls the ScheduleGenerator for every week selected.
        private void GenerateSchedule_Click(object sender, RoutedEventArgs e)
        {
            toWeek = (int) ToWeekComboBox.SelectedItem;
            fromWeek = (int) FromWeekComboBox.SelectedItem;

            if (fromWeek > toWeek)
                return;

            try
            {
                var bar = new GeneratorProgressbar();
                bar.Show();
                bar.pbStatus.IsEnabled = true;

                int.TryParse(YearTextBox.Text, out yr);
                for (var i = fromWeek; i <= toWeek; i++)
                {
                    Core.Instance.ScheduleGenerator(i, yr);
                    Console.WriteLine("Generated days for week: " + i);
                }
                bar.Close();
                Close();
            }
            catch (ArgumentException argE)
            {
                MessageBox.Show(argE.Message, "Error");
            }
        }

        //Is supposed to update the maximum weeks in comboboxes, I don't think it works though
        private void UpdateWeeksInYear(object sender, TextCompositionEventArgs e)
        {
            int a;
            if (int.TryParse(YearTextBox.ToString(), out a))
            {
                SetWeekComboBoxes();
                e.Handled = SetWeekComboBoxes();
            }
        }
    }
}