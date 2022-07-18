using System.Windows;

namespace ATC.Wpf.Services
{
    internal static class MessageBoxManager
    {
        public static void ShowError(string msg)
        {
            MessageBox.Show(msg, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
