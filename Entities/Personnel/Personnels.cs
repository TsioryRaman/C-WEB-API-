using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Tache.Entities.Contexte;
using Tache.Entities.Departement;
using Tache.Entities.Tache;

namespace Tache.Entities.Personnel
{
    [Table("Personnels")]
    public class Personnels
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPersonnel { get; set; }

        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MinLength(5)]
        public string Email { get; set; }

        [Required]
        public string Fonction { get; set; }

        [Required]
        public string Adresse { get; set; }

        [Column(TypeName ="Date")]
        [DataType(DataType.Date)]
        public DateTime DateWork { get; set; }

        [Column(TypeName = "Image")]
        public string ImagePersonnel { get; set; }

        public int DepartementId { get; set; }

        [ForeignKey("DepartementId")]
        public virtual Departements Departement { get; set; }

        [NotMapped]
        public IFormFile file { get; set; }
        
        public ICollection<Taches> taches { get; set; }

        // CREATE PERSONNEL
        public static void create(Personnels Personnel)
        {
            using(var context = new TacheContext())
            {
                
                context.Personnel.Add(Personnel);
                context.SaveChanges();
            }
        }
        // GET BY ID
        public static Personnels GetById(int id)
        {
            using(var context = new TacheContext())
            {
                return context.Set<Personnels>().Find(id);
            }
        }
        public static List<Personnels> GetAll()
        {
            using(var context = new TacheContext())
            {
                return context.Personnel.ToList();
            }
        }

        public static List<Personnels> GetByPage(int page)
        {
            if(page == 0)
            {
                page = 1;
            }
            int count = 5; 
            var skip = (count * page) - count;

            if (GetAll().Count()<skip)
            {
                skip = 0;
            }

            using(var context = new TacheContext())
            {              
                return context.Set<Personnels>().Skip(skip).Take(count).ToList();
            }
        }

        
    }
}
