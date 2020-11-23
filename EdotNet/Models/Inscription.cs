using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Copie0Web.Models
{
    public class Inscription
    {
        [Key]
        public int Id { get; set; }
        public virtual ICollection<Cursus> Parcours { get; set; }
        public virtual ICollection<Module>Modules { get; set; }
        public virtual Utilisateur Utilisateur { get; set; }
        public DateTime DateInscription { get; set; }

    }
}