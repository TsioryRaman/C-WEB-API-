using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Tache.Entities.Personnel;

namespace Tache.Entities.Tache
{
    [Table("Taches")]
    public class Taches
    {
        [Key]
        public int IdTache { get; set; }

        [Required]
        public string TacheName { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime TimeStart { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName ="Date")]
        public DateTime Deadline { get; set; }

        public virtual ICollection<Personnels> personnels { get; set; }
    }
}
