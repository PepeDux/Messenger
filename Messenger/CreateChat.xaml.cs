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
    /// Логика взаимодействия для CreateChat.xaml
    /// </summary>
    public partial class CreateChat : Page
    {
        int LenghtSymbol = 16;

        Random random = new Random();

        string LibraryID = null;
        //string LibraryIDKey = null;
        //string LibraryIDStrength = null;        

        string Code = null;

        string Buf = null;
        int LibraryLenghtLevel1 = 8;
        //int LibraryLenghtLevel2 = 8;
        //int LibraryLenghtLevel3 = 8;

        string RoomID = null;
        bool succsess = true;
        public CreateChat()
        {
            InitializeComponent();
        }

        private void CreateChatButton_Click(object sender, RoutedEventArgs e)
        {
            if (CreateChatPassword.Password != CreateChatPasswaordProof.Password)
            {
                Passwordconfirm.Content = "PASSWORDCONFIRM - Passwords do not match";
                Passwordconfirm.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");

                Password.Content = "PASSWORD - Passwords do not match";
                Password.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");

                CreateChatPassword = null;
                CreateChatPasswaordProof = null;
            }

            else
            {
                using (var db = new ApplicationContext())
                {
                    var users = db.Users.ToList();

                    foreach (Users u in users)
                    {
                        if (CreateChatName.Text == u.Login)
                        {
                            CreateChatName.Text = null;
                            Chatname.Content = "CHAT NAME - this chat name already exists";
                            Chatname.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");
                            succsess = false;
                            break;
                        }
                    }

                    

                    if (CreateChatName.Text.Length > 24 || CreateChatName.Text.Length < 8)
                    {
                        Chatname.Content = "CHATNAME - less than 8 or more than 24";
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

                }
                if (succsess == true)
                {
                    using (var db = new ApplicationContext())
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            RoomID = RoomID + random.Next(0, 10);
                        }

                        ChatRooms chatRoom = new ChatRooms { RoomID = RoomID, Login = CreateChatName.Text, Password = CreateChatPassword.Password };

                        db.ChatRooms.Add(chatRoom);
                        db.SaveChanges();

                        RoomID = null;
                        Code = null;
                        LibraryID = null;

                        CreateChatPassword.Password = null;
                        CreateChatPasswaordProof.Password = null;
                        CreateChatName.Text = null;

                        MainMenu mainMenu = new MainMenu();
                        mainMenu.Show();
                        Application.Current.MainWindow.Close();
                    }
                }
                succsess = true;
            }
        }

        private void OpenCreateChat_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new OpenChat());
        }

       
    }
}
