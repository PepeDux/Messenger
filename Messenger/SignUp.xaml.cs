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

        public SignUp()
        {
            InitializeComponent();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordSignUp == PasswordProofSignUp)
            {
                PasswordSignUp.Password = null;         //Очищаем поля паролей
                PasswordProofSignUp.Password = null;    //
            }//Проверка совпадения пароля в 2-ух полях

            else
            {
                using (var db = new ApplicationContext())
                {
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

                    Users user = new Users { ID = UserID, Nickname = NicknameSignUp.Text, Login = LoginSignUp.Text, Password = PasswordSignUp.Password }; // Присваиваем значения к новому пользователю

                    db.Users.Add(user); //Добавляем нового пользователя
                    db.SaveChanges(); //Сохраняем измения в базе данных

                    PasswordSignUp.Password = null;      //
                    PasswordProofSignUp.Password = null; //Очищаем поля регистрации
                    LoginSignUp.Text = null;             //
                    NicknameSignUp.Text = null;          //
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignIn());
        }
    }
}
