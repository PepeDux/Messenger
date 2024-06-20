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

namespace Messenger
{
    /// <summary>
    /// Логика взаимодействия для ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Page
    {
        bool succsess = true;
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            // Сброс переменной succsess в начале метода
            succsess = true;

            // Проверка длины нового пароля
            if (NewPassword.Password.Length < 8 || NewPassword.Password.Length > 24)
            {
                NewPassword.Password = null;
                Pass.Content = "PASSWORD - less than 8 or more than 24";
                Pass.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");
                succsess = false;
            }

            // Проверка длины подтверждения нового пароля
            if (NewPasswordProof.Password.Length < 8 || NewPasswordProof.Password.Length > 24)
            {
                NewPasswordProof.Password = null;
                PassConfirm.Content = "PASSWORD CONFIRM - less than 8 or more than 24";
                PassConfirm.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");
                succsess = false;
            }

            // Проверка совпадения нового пароля и его подтверждения
            if (NewPassword.Password != NewPasswordProof.Password)
            {
                PassConfirm.Content = "PASSWORD CONFIRM - Passwords do not match";
                PassConfirm.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");

                Pass.Content = "PASSWORD - Passwords do not match";
                Pass.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");

                NewPassword.Password = null;
                NewPasswordProof.Password = null;

                succsess = false;
            }

            // Проверка старого пароля
            using (var db = new ApplicationContext())
            {
                var user = db.Users.FirstOrDefault(u => DataBank.UserLog == u.Nickname);

                if (user != null)
                {
                    if (OldPassword.Password != user.Password)
                    {
                        OldPass.Content = "OLD PASSWORD - Invalid password";
                        OldPass.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");

                        OldPassword.Password = null;

                        succsess = false;
                    }
                }
                else
                {
                    succsess = false;
                }
            }

            // Если все проверки пройдены успешно, обновляем пароль
            if (succsess)
            {
                using (var db = new ApplicationContext())
                {
                    var user = db.Users.FirstOrDefault(u => DataBank.UserLog == u.Nickname);

                    if (user != null)
                    {
                        user.Password = NewPassword.Password;
                        db.SaveChanges();   // сохраняем изменения

                        NavigationService.Navigate(new Settings());
                    }
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Settings());
        }
    }
}
