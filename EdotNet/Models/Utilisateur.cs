using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Copie0Web.Models
{
    public class Utilisateur
    {
        [Key]
        public int Id { get; set; }
     
        [Required(ErrorMessage = "Votre Nom est requis !")]
        public string Nom { get; set; }
        [Required(ErrorMessage = "Votre prénom est requis !")]
        public string Prenom { get; set; }
        //[Required]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateNaissance { get; set; }
        //[Required(ErrorMessage = "Votre lieu de naissance est requis !")]
        public string LieuNaissance { get; set; }
        public string Nationalité { get; set; }
        //[Required(ErrorMessage ="Indiquer votre Niveau d'etudes !")]
        public Niveau NiveauEtude { get; set; }
        //[Required(ErrorMessage = "Indiquer votre situation actuelle !")]
        public Statut StatutActuel  { get; set; }
        //[Required(ErrorMessage = "Indiquer l'etat de votre diplome !")]
        public  StatutDiplome Diplome  { get; set; }
        //[Required]
        public StatutCategorie Catégorie { get; set; }
        //[Required(ErrorMessage = "JOindre votre CV!;")]
        public string CV { get; set; }
        //[Required(ErrorMessage = "Votre Adresse est requise !")]
        public string Adresse { get; set; }
        public string ComplementAdresse { get; set; }
        //[Required(ErrorMessage = "Le Code Postal est requis !")]
        public int CodePostal { get; set; }
        //[Required(ErrorMessage = "La Ville est requise !")]
        public string Ville { get; set; }

        //[Required(ErrorMessage = "Merci d'indiquer votre numéro de téléphone")]
        public int TelephoneMobile { get; set; }
        public string IdIdentityFramework { get; internal set; }
        
    }

    public enum Statut
    {
        Selectionner ,
        DemandeurEmploi,
        Salarier,
        Entrepreneur,
        Particulier

    };
    public enum StatutDiplome
    {
        Selectionner,
        Obtenu,
        Encours,
        NonObtenu


    };
    public enum StatutCategorie
    {
        Selectionner,
        sansAllocations,
        RSA,
        AllocationAssédic,
        Autre

    };
    public enum Niveau
    {
        Selectionner,
        CAP ,
        BEP,
        Baccalauréat ,
        DEUG ,
        BTS,
        DUT,
        DEUST,

        Licence, 
        LicenceLMD, 
        licenceprofessionnelle,
        Maîtrise ,
        Master, 
        DEA, 
        DESS,
        diplômeIngénieur
        
                        
    };
}
