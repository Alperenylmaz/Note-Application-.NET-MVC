using MyEveryNote.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEveryNote.DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        public DbSet<EveryNoteUser> EveryNoteUser { get; set; }
        public DbSet<Note> Note { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Liked> Liked { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer(new MyInitializer());
        }

    }
}
