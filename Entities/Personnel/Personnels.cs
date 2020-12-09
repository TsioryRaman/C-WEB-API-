using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Tache.Entities.Contexte;
using Tache.Entities.Departement;
using Tache.Entities.Tache;

namespace Tache.Entities.Personnel
{
    [Table("Personnels")]
    public class Personnels
    {
        [Key]
        public int IdPersonnel { get; set; }

        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        [MinLength(5)]
        public string Email { get; set; }

        [Required]
        public string Fonction { get; set; }

        [Column(TypeName ="Date")]
        [DataType(DataType.Date)]
        public DateTime DateWork { get; set; }

        [Column(TypeName = "Image")]
        public string ImagePersonnel { get; set; }

        [ForeignKey("Departement")]
        public Departements DepartementRefId { get; set; }

        [NotMapped]
        public IFormFile file { get; set; }

        
        public virtual ICollection<Taches> taches { get; set; }

        // CREATE PERSONNEL
        public static void create(Personnels Personnel)
        {
            using(var context = new TacheContext())
            {
                context.Personnel.Add(Personnel);
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


    }
}
