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
using System.Windows.Shapes;

namespace Messenger
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new ApplicationContext())
            {
                var users = db.Users.ToList();

                foreach (Users u in users)
                {
                    if (DataBank.UserLog == u.Nickname)
                    {
                        DataBank.UserLog = null;      //Указываем программе авторизированного пользователя

                        Users u1 = db.Users.FirstOrDefault();

                        u1.IP = null;
                        db.SaveChanges();   // сохраняем изменения

                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        App.Current.Shutdown();
                    }
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
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            Application.Current.MainWindow.Close();
        }
    }
}
