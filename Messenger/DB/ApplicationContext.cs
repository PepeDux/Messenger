using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.DB
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Users> Users { get; set; } = null!;
        public DbSet<ChatRooms> ChatRooms { get; set; } = null!;

        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Строка подключения");
        }
    }
}
