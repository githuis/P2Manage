using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTwoManage
{
    public sealed class Authentication
    {
        static readonly Authentication _instance = new Authentication();

        AuthenticationWindow AuthWindow;

        //Public refrence to the single instance of this Class
        public static Authentication Instance
        {
            get {return _instance; }
        }

        //Empty ctor
        Authentication()
        { 
        }

        //Prompts the user with a login window
        //If the login is succesful return true othewise return false
        public bool Prompt(string companyName = "")
        {
            AuthWindow = new AuthenticationWindow();
            AuthWindow.UseCompanyName(companyName);
            Nullable<bool> dialogResult = AuthWindow.ShowDialog();
            AuthWindow.Topmost = true; 

            return (bool) dialogResult;
        }
    }
}
