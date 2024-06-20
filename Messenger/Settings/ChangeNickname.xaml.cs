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
using Messenger.DB;
using System.Diagnostics;

namespace Messenger
{
    /// <summary>
    /// Логика взаимодействия для ChangeNickname.xaml
    /// </summary>
    public partial class ChangeNickname : Page
    {
        bool succsess = true;

        public ChangeNickname()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Settings());
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            string newNickname = NewNickname.Text;

            // Сбрасываем флаги успеха
            succsess = true;

            if (newNickname == DataBank.UserLog)
            {
                NewNickname.Text = null;
                Name.Content = "NEW NICKNAME - nicknames are the same";
                Name.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");
                succsess = false;
            }

            if (succsess)
            {
                using (var db = new ApplicationContext())
                {
                    var currentUser = db.Users.FirstOrDefault(u => u.Nickname == DataBank.UserLog);

                    if (currentUser != null)
                    {
                        currentUser.Nickname = newNickname;
                        DataBank.UserLog = newNickname;
                        db.SaveChanges(); // сохраняем изменения

                        // Перезапуск приложения
                        RestartApplication();
                    }
                }
            }
        }

        private void RestartApplication()
        {
            // Получаем путь к текущему исполняемому файлу
            string applicationPath = Process.GetCurrentProcess().MainModule.FileName;

            // Запускаем новое экземпляр текущего приложения
            Process.Start(applicationPath);

            // Завершаем текущее приложение
            Application.Current.Shutdown();
        }
    }
}
