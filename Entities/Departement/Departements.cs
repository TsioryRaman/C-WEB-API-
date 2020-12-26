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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDepartement { get; set; }

        public string DepartementName { get; set; }

        public virtual ICollection<Personnels> Personnel { get; set; }
    }
}
