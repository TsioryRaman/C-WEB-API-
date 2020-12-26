using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Tache.Entities.Contexte;

namespace Tache.Entities.User
{
    [Table("Users")]
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUser { get; set; }

        [Required()]
        [MaxLength(255)]
        public string username { get; set; }

        [Required]
        [MaxLength(255)]
        public string password { get; set; }
       
        public bool isAdmin { get; set; }

        public static Users IsUser(Users user)
        {
            using(var context = new TacheContext())
            {
            
                try
                {
                   return context.Set<Users>().Where(s => s.username == user.username && s.password == user.password).FirstOrDefault();
                }
                catch (Exception e)
                {
                    return null;
                }

            }
        }

        public static Users Find(int id)
        {
            using (var context = new TacheContext())
            {
                return context.Set<Users>().Find(id);
            }
        }

        public static bool verfify(Users user)
        {
            using(var context = new TacheContext())
            {
                var _user = context.Set<Users>().Where(u => u.username == user.username);
                if(user != null)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
