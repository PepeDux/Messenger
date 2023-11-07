using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для ChatPage.xaml
    /// </summary>
    public partial class ChatPage : Page
    {
        string mess = ""; //Сообщение
        public ChatPage()
        {
            InitializeComponent();

            

            ChatUpdate();
        }

        private void Message_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                mess = DataBank.UserLog + ": " + Message.Text + "\r\n";

                using (var db = new ApplicationContext())
                {
                    var chatRooms = db.ChatRooms.ToList();

                    foreach (ChatRooms cr in chatRooms)
                    {
                        if (cr.RoomID == DataBank.RoomID)
                        {
                            cr.ChatHistory += mess;         //Добавляет сообщение к истории чата в БД
                            Chat.ScrollToEnd();             //Опускает скролл в конец чата
                            db.SaveChanges();               //Сохраняет изменения в БД
                            mess = null;                    //Стирает данные с сообщения
                            Chat.Text = cr.ChatHistory;     // Обновляет чат(БЕТА)
                        }
                    }
                }

                Message.Text = null;
            }
        }

        public void ChatUpdate()
        {
            Thread.Sleep(200);

            if (DataBank.RoomID != null && DataBank.UserLog != null)
            {
                using (var db = new ApplicationContext())
                {
                    var chatRooms = db.ChatRooms.ToList();

                    foreach (ChatRooms cr in chatRooms)
                    {
                        if (cr.RoomID == DataBank.RoomID && DataBank.UserLog != null)
                        {
                            Chat.Text = cr.ChatHistory;
                        }
                    }//Вытаскиваем историю чата и цепляем к чату
                }

            }
        }
    }
}
