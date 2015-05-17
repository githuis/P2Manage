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

        public static Authentication Instance
        {
            get {return _instance; }
        }

        Authentication()
        {
            
        }

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
