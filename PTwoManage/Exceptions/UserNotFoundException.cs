using System;
using System.Windows;

namespace PTwoManage
{
    /// <summary>
    ///     Prompts the user with an errormessage. May be fatal.
    /// </summary>
    internal class UserNotFoundException : Exception
    {
        public UserNotFoundException()
        {
            MessageBox.Show("Error: User not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public UserNotFoundException(string message) : base(message)
        {
            MessageBox.Show("Error: User not found - " + message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public UserNotFoundException(string message, Exception inner) : base(message, inner)
        {
            MessageBox.Show("Error: User not found - " + message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}