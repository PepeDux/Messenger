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
using System.IO;
using System.Net;
using Messenger.DB;

namespace Messenger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AuthService authService = new AuthService();
        public static string host = Dns.GetHostName();
        public static IPAddress[] address = Dns.GetHostAddresses(host);

        public MainWindow()
        {
            InitializeComponent();
            string token = authService.LoadToken();
            if (!string.IsNullOrEmpty(token))
            {
                AutoLogin(token);
            }
            else
            {
                CheckIPAddressLogin();
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Перемещает окно при зажатии левой кнопки мыши
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void AutoLogin(string token)
        {
            using (var db = new ApplicationContext())
            {
                var user = db.Users.FirstOrDefault(u => u.AuthToken == token);
                if (user != null)
                {
                    DataBank.UserLog = user.Nickname;
                    MainMenu mainMenu = new MainMenu();
                    mainMenu.Show();
                    Application.Current.MainWindow.Close();
                }
                else
                {
                    UserFrame.Navigate(new SignIn());
                }
            }
        }

        private void CheckIPAddressLogin()
        {
            using (var db = new ApplicationContext())
            {
                var users = db.Users.ToList();
                foreach (Users u in users)
                {
                    if (address[4].ToString() == u.IP)
                    {
                        DataBank.UserLog = u.Nickname; // Указываем программе авторизированного пользователя
                        MainMenu mainMenu = new MainMenu();
                        mainMenu.Show();
                        Application.Current.MainWindow.Close();
                        return;
                    }
                }
            }

            UserFrame.Navigate(new SignIn());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Additional initialization code if needed
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
