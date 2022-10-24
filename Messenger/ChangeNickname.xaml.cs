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
            if(NewNickname.Text == DataBank.UserLog)
            {
                NewNickname.Text = null;
                Name.Content = "NEW NICKNAME - nicknames are the same";
                Name.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");
                succsess = false;
            }
         
            using (var db = new ApplicationContext())
            {
                var users = db.Users.ToList();

                foreach (Users u in users)
                {
                    if(succsess == true)
                    {
                        if (NewNickname.Text != u.Nickname && DataBank.UserLog == u.Nickname)
                        {
                            Users u1 = db.Users.FirstOrDefault();

                            u1.Nickname = NewNickname.Text;
                            DataBank.UserLog = NewNickname.Text;
                            db.SaveChanges();   // сохраняем изменения

                            NavigationService.Navigate(new Settings());
                        }
                        else
                        {
                            NewNickname.Text = null;
                            Name.Content = "NEW NICKNAME - nickname already taken";
                            Name.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");
                            succsess = false;
                        }
                    }
                    
                }
            }
        }
    }
}
