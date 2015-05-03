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

        public bool Prompt()
        {
            AuthWindow = new AuthenticationWindow();
            Nullable<bool> dialogResult = AuthWindow.ShowDialog();
            AuthWindow.Topmost = true; 

            return (bool) dialogResult;
        }
    }
}
