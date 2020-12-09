using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Tache.Entities.Contexte;

namespace Tache.Entities.User
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string username { get; set; }

        [Required]
        [MaxLength(255)]
        public string password { get; set; }

        public static Users IsUser(Users user)
        {
            using(var context = new TacheContext())
            {
            
                try
                {
                   return context.User.Where(s => s.username == user.username && s.password == user.password).FirstOrDefault();
                }
                catch (Exception e)
                {
                    Console.WriteLine("erreur");
                    return null;
                }

            }
        }

        public static Users Find(string id)
        {
            using (var context = new TacheContext())
            {
                return context.Set<Users>().Find(id);
            }
        }
    }
}
