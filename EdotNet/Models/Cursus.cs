using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Copie0Web.Models
{
    public class Cursus
    {
        [Key]
        public int Id { get; set; }
        public int IdSession { get; set; }
        public string Titre { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateDebut { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateFin { get; set; }
        public string Image { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<Module> Modules { get; set; }
        public virtual ICollection<Inscription> Inscriptions { get; set; }
       
    }

   
}

