using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Copie0Web.Models
{
    public class Session
    {
        [Key]
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateDebut { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateFin { get; set; }
    
        public virtual ICollection<Cursus> Parcours { get; set; }
    }
}

