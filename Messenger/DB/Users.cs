using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.DB
{
    public class Users
    {
        [Key]
        public string ID { get; set; }
        public string Login { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string? IP { get; set; }
        public string AuthToken { get; set; }
    }
}
