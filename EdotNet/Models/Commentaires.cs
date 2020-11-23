using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Copie0Web.Models
{
    public class Commentaires
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; }
        //public string Email { get; set; }
        public string Comments { get; set; }
        
    }
}