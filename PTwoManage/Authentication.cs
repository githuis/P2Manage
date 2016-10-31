namespace PTwoManage
{
    public sealed class Authentication
    {
        //Empty ctor
        private Authentication()
        {
        }

        private AuthenticationWindow AuthWindow;

        //Public refrence to the single instance of this Class
        public static Authentication Instance { get; } = new Authentication();

        //Prompts the user with a login window
        //If the login is succesful return true othewise return false
        public bool Prompt(string companyName = "")
        {
            AuthWindow = new AuthenticationWindow();
            AuthWindow.UseCompanyName(companyName);
            var dialogResult = AuthWindow.ShowDialog();
            AuthWindow.Topmost = true;

            return (bool) dialogResult;
        }
    }
}