using Bogus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tache.Entities.Departement;
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
        protected override void OnModelCreating(ModelBuilder Model)
        {

   /*         int idDep= 6;

                        var departement = new Faker<Departements>()
                            .RuleFor(m => m.IdDepartement, f => ++idDep)
                            .RuleFor(m => m.DepartementName, f => f.Lorem.Word());

            int idPers = 3;

            var personnel = new Faker<Personnels>()
                            .RuleFor(m => m.IdPersonnel, f => ++idPers)
                            .RuleFor(m => m.Name, f => f.Person.FullName)
                            .RuleFor(m => m.Email, f => f.Person.Email)
                            .RuleFor(m => m.Adresse, f => f.Address.FullAddress())
                            .RuleFor(m => m.ImagePersonnel, f => f.Person.FirstName)
                            .RuleFor(m => m.Fonction, f => f.Name.JobArea())

                            .RuleFor(m => m.DateWork, f => f.Date.Between(new DateTime(2015, 12, 12), new DateTime(2017, 04, 08)));





            int idTache = 10;

            Faker<Taches> tache = new Faker<Taches>()
                            .RuleFor(m => m.IdTache , f => ++idTache)
                            .RuleFor(m => m.TacheName, f => f.Name.JobDescriptor())
                            .RuleFor(m => m.TimeStart, f => f.Date.Between(new DateTime(2020, 01, 08), new DateTime(2020, 01, 31)))
                            .RuleFor(m => m.Description, f => f.Lorem.Sentences(f.Random.Int(2, 7)))
                            .RuleFor(m => m.Deadline, f => f.Date.Between(new DateTime(2020, 03, 08), new DateTime(2020, 12, 31)));
                   



                        Model.Entity<Taches>().HasData(tache.Generate(100));
                        Model.Entity<Personnels>().HasData(personnel.Generate(100));
                        Model.Entity<Departements>().HasData(departement.Generate(100));*/


        }

        #region
        public DbSet<Users> User { get; set; }

        public DbSet<Departements> Departement { get; set; }

        public DbSet<Personnels> Personnel { get; set; }

        public DbSet<Taches> Tache { get; set; }


        #endregion
    }
}
