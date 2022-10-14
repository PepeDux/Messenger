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
    /// Логика взаимодействия для SignIn.xaml
    /// </summary>
    public partial class SignIn : Page
    {
        public SignIn()
        {
            InitializeComponent();
        }

        public event EventHandler ButtonClicked;
        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new ApplicationContext())
            {
                var users = db.Users.ToList();

                foreach (Users u in users)
                {
                    if (LoginSignIn.Text == u.Login && PasswordSignIn.Password == u.Password)
                    {
                        DataBank.UserLog = u.Nickname;      //Указываем программе авторизированного пользователя

                        LoginSignIn.Text = null;            //Очищаем поля авторизации
                        PasswordSignIn.Password = null;     //   
                    }
                }
            }
        }

        private void OpenSignUp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignUp());
        }
    }
}
