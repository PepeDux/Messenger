using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.DB
{
    public class ChatRooms
    {
        [Key]
        public string RoomID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string? ChatHistory { get; set; }
        public string? LastMessage { get; set; }
    }
}
