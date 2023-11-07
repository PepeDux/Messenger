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
        string UserID = null;

        Random random = new Random();

        int LevelID1 = 16;//
        int LevelID2 = 8; //Уровни ID
        int LevelID3 = 8; //
        int LevelID4 = 8; //

        public static string host = Dns.GetHostName();
        public static IPAddress[] address = Dns.GetHostAddresses(host);
       

        bool succsess = true;

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
            if (PasswordSignUp.Password != PasswordProofSignUp.Password)
            {
                Passwordconfirm.Content = "PASSWORDCONFIRM - Passwords do not match";
                Passwordconfirm.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");

                Password.Content = "PASSWORD - Passwords do not match";
                Password.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");

                PasswordSignUp.Password = null;         //Очищаем поля паролей
                PasswordProofSignUp.Password = null;    //

            
            }//Проверка совпадения пароля в 2-ух полях

            else
            {
                

                if (NicknameSignUp.Text.Length < 1)
                {
                    NicknameSignUp.Text = null;
                    Nickname.Content = "NICKNAME - field cannot be empty";
                    Nickname.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");
                    succsess = false;
                }

                /////////////////////////////////////////////////////////////////////////////////////////

                if (LoginSignUp.Text.Length < 8 || LoginSignUp.Text.Length > 24)
                {
                    Login.Content = "LOGIN - less than 8 more or than 24";
                    Login.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");
                    succsess = false;
                }

                if (NicknameSignUp.Text.Length > 16)
                {
                    Nickname.Content = "NICKNAME - no more than 12";
                    Nickname.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");
                    succsess = false;
                }

                using (var db = new ApplicationContext())
                {
                    var users = db.Users.ToList();

                    foreach (Users u in users)
                    {
                        if (LoginSignUp.Text == u.Login)
                        {
                            LoginSignUp.Text = null;
                            Login.Content = "LOGIN - this login already exists";
                            Login.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");
                            succsess = false;
                            break;
                        }

                        if (NicknameSignUp.Text == u.Nickname)
                        {
                            NicknameSignUp.Text = null;
                            Nickname.Content = "NICKNAME - this nickname already exists";
                            Nickname.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");
                            succsess = false;
                            break;
                        }
                    }
                }

                if (PasswordSignUp.Password.Length < 8 || PasswordSignUp.Password.Length > 24)
                {
                    PasswordSignUp.Password = null;
                    Password.Content = "PASSWORD - less than 8 or more than 24";
                    Password.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");
                    succsess = false;
                }

                if (PasswordProofSignUp.Password.Length < 8 || PasswordSignUp.Password.Length > 24)
                {
                    PasswordProofSignUp.Password = null;
                    Passwordconfirm.Content = "PASSWORD CONFIRM - less than 8 or more than 24";
                    Passwordconfirm.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");
                    succsess = false;
                }


                if (succsess == true)
                {
                    using (var db = new ApplicationContext())
                    {
                        var users = db.Users.ToList();

                        for (int i = 0; i < LevelID1; i++)
                        {
                            UserID = UserID + random.Next(0, 10); // 1 уровень ID
                        }

                        UserID = UserID + "-";

                        for (int i = 0; i < LevelID2; i++)
                        {
                            UserID = UserID + random.Next(0, 10); // 2 уровень ID
                        }

                        UserID = UserID + "-";

                        for (int i = 0; i < LevelID3; i++)
                        {
                            UserID = UserID + random.Next(0, 10); // 3 уровень ID
                        }

                        UserID = UserID + "-";

                        for (int i = 0; i < LevelID4; i++)
                        {
                            UserID = UserID + random.Next(0, 10); // 4 уровень ID
                        }

                        Users user = new Users { ID = UserID, Nickname = NicknameSignUp.Text, Login = LoginSignUp.Text, Password = PasswordSignUp.Password, IP = address[4].ToString()}; // Присваиваем значения к новому пользователю

                        db.Users.Add(user); //Добавляем нового пользователя
                        db.SaveChanges(); //Сохраняем измения в базе данных

                        PasswordSignUp.Password = null;      //
                        PasswordProofSignUp.Password = null; //Очищаем поля регистрации
                        LoginSignUp.Text = null;             //
                        NicknameSignUp.Text = null;          //                 

                        MainMenu mainMenu = new MainMenu();
                        mainMenu.Show();
                        Application.Current.MainWindow.Close();

                    }
                }

                succsess = true;

            }
        }
    }
}
