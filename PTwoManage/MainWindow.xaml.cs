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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PTwoManage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AddUserWindow addUserWindow;
        AddShiftTemplateWindow addShiftTemplateWindow;
        UserFreeTimeRequestWindow userFreeTimeRequestWindow;
        public MainWindow()
        {
            InitializeComponent();
            InitializeLoginPanel();
            //Core.Instance.Run();

            /*shiftDataBindingMonday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Monday, 2);
            shiftDataBindingTuesday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Tuesday, 2);
            shiftDataBindingWednesday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Wednesday, 2);
            shiftDataBindingThursday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Thursday, 2);
            shiftDataBindingFriday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Friday, 2);
            shiftDataBindingSaturday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Saturday, 2);
            shiftDataBindingSunday.ItemsSource = Core.Instance.GetAllShifts(DayOfWeek.Sunday, 2);
           */

        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            if(Authentication.Instance.Prompt())
            {
                addUserWindow = new AddUserWindow();
                addUserWindow.Show();
                addUserWindow.EditUser_Load();
            }
        }

        private void AddTemplate_Click(object sender, RoutedEventArgs e)
        {
            addShiftTemplateWindow = new AddShiftTemplateWindow();
            addShiftTemplateWindow.Show();
            addShiftTemplateWindow.LoadShift();
        }

        private void LoadSchedule_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }
		
        private void UserFreeRequest_Click(object sender, RoutedEventArgs e)
        {
            userFreeTimeRequestWindow = new UserFreeTimeRequestWindow();
            userFreeTimeRequestWindow.Show();
        }

        private void loginSubmit_Click(object sender, RoutedEventArgs e)
        {
            loginFeedbackText.Text = "";
            string loginString = loginUsernameBox.Text + "," + loginPasswordBox.Password;
            System.Net.WebClient wc = new System.Net.WebClient();
            string webData = wc.DownloadString("http://everflows.com/companies.txt");
            string[] split = webData.Split(new Char[] { ';' });

            foreach (string s in split)
            {
                if(s == loginString)
                    loginPanel.Visibility = System.Windows.Visibility.Collapsed;
            }
            loginFeedbackText.Foreground = Brushes.Red;
            loginFeedbackText.Text = "Invalid Company name or password, please retry";
        }

        private void InitializeLoginPanel()
        {
            loginPanel.Width = (System.Windows.SystemParameters.PrimaryScreenWidth - (loginPanel.Margin.Left + loginPanel.Margin.Right));
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            loginPanel.Width = this.Width - (loginPanel.Margin.Left + loginPanel.Margin.Right);
            Console.WriteLine(this.Width.ToString());
        }
    }
}
