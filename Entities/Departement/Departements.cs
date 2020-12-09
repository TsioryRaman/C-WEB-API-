using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Tache.Entities.Personnel;

namespace Tache.Entities.Departement
{
    [Table("Departements")]
    public class Departements
    {
        [Key]
        public int IdDepartement { get; set; }

        [Required]
       
        public string DepartementName { get; set; }

        public ICollection<Personnels> Personnel { get; set; }
    }
}
