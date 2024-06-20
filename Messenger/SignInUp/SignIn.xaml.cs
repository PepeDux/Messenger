using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace Messenger
{
    /// <summary>
    /// Логика взаимодействия для SignIn.xaml
    /// </summary>
    public partial class SignIn : Page
    {
        private AuthService authService = new AuthService();

        public static string host = Dns.GetHostName();
        public static IPAddress[] address = Dns.GetHostAddresses(host);

        public SignIn()
        {
            InitializeComponent();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new ApplicationContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Login == LoginSignIn.Text && u.Password == PasswordSignIn.Password);
                if (user != null)
                {
                    DataBank.UserLog = user.Nickname;

                    user.IP = address[4].ToString();
                    db.SaveChanges();

                    // Сохраняем токен в реестре
                    authService.SaveToken(user.AuthToken);

                    LoginSignIn.Text = null;
                    PasswordSignIn.Password = null;

                    MainMenu mainMenu = new MainMenu();
                    mainMenu.Show();
                    mainMenu.RefreshChatList(); // Обновляем список чатов
                    Application.Current.MainWindow.Close();
                }
                else
                {
                    Password.Content = "PASSWORD - wrong username or password";
                    Password.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");
                    Login.Content = "LOGIN - wrong username or password";
                    Login.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");
                    PasswordSignIn.Password = null;
                }
            }
        }

        private void OpenSignUp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignUp());
        }
    }
}
