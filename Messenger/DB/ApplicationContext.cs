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
            optionsBuilder.UseSqlServer(@"Data Source=ms-sql-10.in-solve.ru;Initial Catalog=1gb_mesenger;User ID=1gb_vudu;Password=z5c357c6uiw;Trust Server Certificate=True");
        }
    }
}
