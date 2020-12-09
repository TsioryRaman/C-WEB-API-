using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tache.Entities.Personnel;
using Tache.Entities.Tache;
using Tache.Entities.User;

namespace Tache.Entities.Contexte
{
    public class TacheContext:DbContext
    {
        public TacheContext()
        {

        }

        public TacheContext(DbContextOptions<TacheContext> options)
        : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            // . mais pas SQLEXPRESS...
            dbContextOptionsBuilder.UseSqlServer(@"Server=.;Database=TacheFinal;Trusted_Connection=True;");
        }

        #region
        public DbSet<Users> User { get; set; }

        public DbSet<Personnels> Personnel { get; set; }

        public DbSet<Taches> Tache { get; set; }
        #endregion
    }
}
