using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
    /// Логика взаимодействия для CreateChat.xaml
    /// </summary>
    public partial class CreateChat : Page
    {
        int LenghtSymbol = 16;
        Random random = new Random();
        string RoomID = null;

        public CreateChat()
        {
            InitializeComponent();
        }

        private void OpenCreateChat_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OpenChat());
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close(); // Закрыть текущее окно
        }

        private void CreateChatButton_Click(object sender, RoutedEventArgs e)
        {
            bool succsess = true; // Сбрасываем флаг успешности перед каждой попыткой

            if (IsChatNameTaken(CreateChatName.Text))
            {
                CreateChatName.Text = null;
                Chatname.Content = "CHAT NAME - this chat name already exists";
                Chatname.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");
                succsess = false;
            }

            if (CreateChatPassword.Password.Length < 8 || CreateChatPassword.Password.Length > 24)
            {
                CreateChatPassword.Password = null;
                Password.Content = "PASSWORD - less than 8 or more than 24";
                Password.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");
                succsess = false;
            }

            if (CreateChatPasswaordProof.Password.Length < 8 || CreateChatPasswaordProof.Password.Length > 24)
            {
                CreateChatPasswaordProof.Password = null;
                Passwordconfirm.Content = "PASSWORD CONFIRM - less than 8 or more than 24";
                Passwordconfirm.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");
                succsess = false;
            }

            if (CreateChatPassword.Password != CreateChatPasswaordProof.Password)
            {
                Passwordconfirm.Content = "PASSWORDCONFIRM - Passwords do not match";
                Passwordconfirm.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");

                Password.Content = "PASSWORD - Passwords do not match";
                Password.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");

                CreateChatPassword.Password = null;
                CreateChatPasswaordProof.Password = null;

                succsess = false;
            }

            if (succsess)
            {
                CreateNewChatRoom();
                ResetForm();
                Window.GetWindow(this).Close();
            }
        }

        private bool IsChatNameTaken(string chatName)
        {
            using (var db = new ApplicationContext())
            {
                return db.ChatRooms.Any(c => c.Login == chatName);
            }
        }

        private void CreateNewChatRoom()
        {
            using (var db = new ApplicationContext())
            {
                RoomID = GenerateRoomID();

                ChatRooms chatRoom = new ChatRooms
                {
                    RoomID = RoomID,
                    Login = CreateChatName.Text,
                    Password = CreateChatPassword.Password
                };

                db.ChatRooms.Add(chatRoom);
                db.SaveChanges();

                // Обновляем список чатов в главном меню
                var mainMenu = Application.Current.Windows.OfType<MainMenu>().FirstOrDefault();
                if (mainMenu != null)
                {
                    mainMenu.UpdateChatList(chatRoom.RoomID);
                }
            }
        }

        private string GenerateRoomID()
        {
            return string.Concat(Enumerable.Range(0, 8).Select(_ => random.Next(0, 10).ToString()));
        }

        private void ResetForm()
        {
            RoomID = null;
            CreateChatPassword.Password = null;
            CreateChatPasswaordProof.Password = null;
            CreateChatName.Text = null;
        }
    }
}
