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

namespace Messenger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string host = Dns.GetHostName();
        public static IPAddress[] address = Dns.GetHostAddresses(host);
        public MainWindow()
        {
            
        }
        private void UserFrame_Initialized(object sender, EventArgs e)
        {
            using (var db = new ApplicationContext())
            {
                var users = db.Users.ToList();

                foreach (Users u in users)
                {
                    if (address[4].ToString() == u.IP)
                    {
                        DataBank.UserLog = u.Nickname;      //Указываем программе авторизированного пользователя                       

                        MainMenu mainMenu = new MainMenu();
                        mainMenu.Show();
                        Application.Current.MainWindow.Close();
                    }
                }              
            }

            UserFrame.Navigate(new SignIn());
        }
    }
}
