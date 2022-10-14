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
        public CreateChat()
        {
            InitializeComponent();
        }

        private void CreateChatButton_Click(object sender, RoutedEventArgs e)
        {
            if (CreateChatPassword.Password != CreateChatPasswaordProof.Password)
            {
                CreateChatPassword = null;
                CreateChatPasswaordProof = null;
            }

            else
            {
                using (var db = new ApplicationContext())
                {
                    for (int i = 0; i < 8; i++)
                    {
                        RoomID = RoomID + random.Next(0, 10);
                    }

                    ChatRooms chatRoom = new ChatRooms { RoomID = RoomID, Login = CreateChatLogin.Text, Password = CreateChatPassword.Password };

                    db.ChatRooms.Add(chatRoom);
                    db.SaveChanges();

                    RoomID = null;
                    Code = null;
                    LibraryID = null;

                    CreateChatPassword.Password = null;
                    CreateChatPasswaordProof.Password = null;
                    CreateChatLogin.Text = null;
                }
            }
        }

        private void OpenCreateChat_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new OpenChat());
        }

       
    }
}
