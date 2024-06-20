using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
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
using System.Net;
using Messenger.DB;

namespace Messenger
{
    /// <summary>
    /// Логика взаимодействия для SignUp.xaml
    /// </summary>
    public partial class SignUp : Page
    {
        private AuthService authService = new AuthService();

        string UserID = null;

        Random random = new Random();

        int LevelID1 = 16;
        int LevelID2 = 8;
        int LevelID3 = 8;
        int LevelID4 = 8;

        public static string host = Dns.GetHostName();
        public static IPAddress[] address = Dns.GetHostAddresses(host);

        bool success = true;

        public SignUp()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignIn());
        }

        private void Sign_Click(object sender, RoutedEventArgs e)
        {
            success = true;

            if (PasswordSignUp.Password != PasswordProofSignUp.Password)
            {
                Passwordconfirm.Content = "PASSWORDCONFIRM - Passwords do not match";
                Passwordconfirm.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");

                Password.Content = "PASSWORD - Passwords do not match";
                Password.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");

                PasswordSignUp.Password = null;
                PasswordProofSignUp.Password = null;
                success = false;
            }

            if (NicknameSignUp.Text.Length < 1)
            {
                NicknameSignUp.Text = null;
                Nickname.Content = "NICKNAME - field cannot be empty";
                Nickname.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");
                success = false;
            }

            if (LoginSignUp.Text.Length < 6 || LoginSignUp.Text.Length > 24)
            {
                LoginSignUp.Text = null;
                Login.Content = "LOGIN - less than 6 or more than 24";
                Login.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");
                success = false;
            }

            if (NicknameSignUp.Text.Length > 16)
            {
                Nickname.Content = "NICKNAME - no more than 16";
                Nickname.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");
                success = false;
            }

            if (PasswordSignUp.Password.Length < 8 || PasswordSignUp.Password.Length > 24)
            {
                PasswordSignUp.Password = null;
                Password.Content = "PASSWORD - less than 8 or more than 24";
                Password.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");
                success = false;
            }

            if (PasswordProofSignUp.Password.Length < 8 || PasswordSignUp.Password.Length > 24)
            {
                PasswordProofSignUp.Password = null;
                Passwordconfirm.Content = "PASSWORD CONFIRM - less than 8 or more than 24";
                Passwordconfirm.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");
                success = false;
            }

            using (var db = new ApplicationContext())
            {
                // Проверяем уникальность логина
                if (db.Users.Any(u => u.Login == LoginSignUp.Text))
                {
                    LoginSignUp.Text = null;
                    Login.Content = "LOGIN - this login already exists";
                    Login.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");
                    success = false;
                }

                if (success)
                {
                    string UserID = GenerateUserID();

                    Users user = new Users
                    {
                        ID = UserID,
                        Nickname = NicknameSignUp.Text,
                        Login = LoginSignUp.Text,
                        Password = PasswordSignUp.Password,
                        IP = address[4].ToString(),
                        AuthToken = UserID // Сохранение токена в поле AuthToken
                    };

                    db.Users.Add(user);
                    db.SaveChanges();

                    PasswordSignUp.Password = null;
                    PasswordProofSignUp.Password = null;
                    LoginSignUp.Text = null;
                    NicknameSignUp.Text = null;

                    // Сохраняем токен в реестре
                    authService.SaveToken(UserID);

                    DataBank.UserLog = user.Nickname; // Сохраняем никнейм

                    MainMenu mainMenu = new MainMenu();
                    mainMenu.Show();
                    mainMenu.RefreshChatList(); // Обновляем список чатов
                    Application.Current.MainWindow.Close();
                }
            }
        }

        private string GenerateUserID()
        {
            Random random = new Random();
            int LevelID1 = 4, LevelID2 = 4, LevelID3 = 4, LevelID4 = 4;

            string UserID = string.Empty;
            for (int i = 0; i < LevelID1; i++)
            {
                UserID += random.Next(0, 10);
            }
            UserID += "-";
            for (int i = 0; i < LevelID2; i++)
            {
                UserID += random.Next(0, 10);
            }
            UserID += "-";
            for (int i = 0; i < LevelID3; i++)
            {
                UserID += random.Next(0, 10);
            }
            UserID += "-";
            for (int i = 0; i < LevelID4; i++)
            {
                UserID += random.Next(0, 10);
            }

            return UserID;
        }
    }
}
