using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Tache.Entities.Contexte;
using Tache.Entities.Personnel;

namespace Tache.Entities.Tache
{
    [Table("Taches")]
    public class Taches
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTache { get; set; }

        [Required]
        public string TacheName { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime TimeStart { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName ="Date")]
        public DateTime Deadline { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }

        public bool Completed { get; set; }

        public ICollection<Personnels> personnels { get; set; }

        public static Boolean Create(Taches taches)
        {
            using(var context = new TacheContext())
            {
                context.Tache.Add(taches);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public static List<Taches> GetAll()
        {
            using(var context = new TacheContext())
            {
                return context.Tache.ToList();
            }
        }

        public static void addPersonnelToTask(Taches taches,Personnels personnels)
        {
            using(var context = new TacheContext())
            {
                context.Tache.Update(taches);
                context.SaveChanges();
            }
        }
        public static List<dynamic> GetTask(int idTache = 0,int page = 0)
        {
            int count = 5;

            if (page == 0)
            {
                page = 1;
            }
            if (page > GetAll().Count + count)
            {
                page = 1;
            }
            int skip = (page * count) - count;

            using (var context = new TacheContext())
            {
                return context.Tache
                    .Where(e => (e.IdTache == idTache) || idTache == 0)
                    .Select(tache => new
                    {
                        idTache = tache.IdTache,
                        TacheName = tache.TacheName,
                        Description = tache.Description,
                        TimeStart = tache.TimeStart,
                        Deadline = tache.Deadline,
                        Completed = tache.Completed,
                        Personnel = tache.personnels                     
                                    .Select(personnel => new
                                    {
                                        idPersonnel = personnel.IdPersonnel,
                                        Name = personnel.Name,
                                        Email = personnel.Email,
                                        filename = personnel.ImagePersonnel
                                    }).ToList()
                    })
                    .Skip(skip)
                    .OrderByDescending(t => t.TimeStart)
                    .Take(count)
                    .ToList<dynamic>();
            
            }
        } 

    }
}
