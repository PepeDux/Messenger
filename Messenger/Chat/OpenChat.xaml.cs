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
    /// Логика взаимодействия для OpenChat.xaml
    /// </summary>
    public partial class OpenChat : Page
    {
        public OpenChat()
        {
            InitializeComponent();
        }

        private void OpenCreateChat_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateChat());
        }

        private void ChatOpen_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new ApplicationContext())
            {
                var chatrooms = db.ChatRooms.ToList();

                foreach (var cr in chatrooms)
                {
                    if (OpenChatLogin.Text == cr.Login && OpenChatPassword.Password == cr.Password)
                    {
                        DataBank.RoomID = cr.RoomID;

                        OpenChatLogin.Text = null;
                        OpenChatPassword.Password = null;

                        MainMenu mainMenu = Application.Current.Windows.OfType<MainMenu>().FirstOrDefault();
                        if (mainMenu == null)
                        {
                            mainMenu = new MainMenu();
                            mainMenu.Show();
                            Application.Current.MainWindow.Close();
                            Application.Current.MainWindow = mainMenu;
                        }
                        mainMenu.UpdateChatList(cr.RoomID);
                        mainMenu.RefreshChatList(); // Обновляем список чатов
                        Window.GetWindow(this).Close();

                        return;
                    }
                }

                Password.Content = "PASSWORD - wrong chat name or password";
                Password.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");

                Chat.Content = "CHAT NAME - wrong chat name or password";
                Chat.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");

                OpenChatPassword.Password = null;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close(); // Закрыть текущее окно
        }
    }
}
