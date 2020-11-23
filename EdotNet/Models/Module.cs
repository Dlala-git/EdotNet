using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Copie0Web.Models
{
    public class Module
    {
        [Key]
        public int Id { get; set; }
        public int IdCursus { get; set; }
        public string Nom { get; set; }
        //public string Description { get; set; }
        public int NombreHeures { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateDebut { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateFin { get; set; }
        public string LogoModule { get; set; }
        public string NomFormateur { get; set; }
        public virtual ICollection<Cursus> Parcours { get; set; }
        public virtual ICollection<Inscription> Inscriptions { get; set; }
       //public virtual Utilisateur Utilisateur { get; set; }


    }
}
