using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;
using Messenger.DB;

namespace Messenger
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        private AuthService authService = new AuthService();

        public Settings()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new ApplicationContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Nickname == DataBank.UserLog);

                if (user != null)
                {
                    DataBank.UserLog = null;

                    user.IP = null;
                    db.SaveChanges();

                    // Получаем путь к текущему исполняемому файлу
                    string applicationPath = Process.GetCurrentProcess().MainModule.FileName;

                    // Запускаем новое экземпляр текущего приложения
                    Process.Start(applicationPath);

                    // Завершаем текущее приложение
                    Application.Current.Shutdown();

                    // Очищаем токен при выходе из аккаунта
                    authService.ClearToken();
                }
            }
        }

        private void ChangeNickname_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChangeNickname());
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChangePassword());
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
           Window.GetWindow(this).Close(); // Закрыть текущее окно
        }
    }
}
