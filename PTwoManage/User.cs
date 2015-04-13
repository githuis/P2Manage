using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTwoManage
{
    class User
    {
        List<string> userCategories;
        private string _userName;
        private string _password;

        public string UserName
        {
            get { return _userName; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public User(string userName, string password)
        {
            _userName = userName;
            // Todo: Skal laves til hash
            _password = password;
        }

        static Dictionary<int, string> categories = new Dictionary<int, string>()
        {
            {0, "Default"}, 
            {1, "Lukker"},
            {2, "Åbner"}
        };

        void AddCategory(string newCategory)
        {
            categories.Add(categories.Count, newCategory);
        }

        void RemoveCategory(int categoryKey)
        {
            categories.Remove(categoryKey);
        }
    }
}
