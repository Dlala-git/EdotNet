namespace Copie0Web.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class EFCopie0 /*: DbContext*/
    {
        // Votre contexte a été configuré pour utiliser une chaîne de connexion « EFCopie0 » du fichier 
        // de configuration de votre application (App.config ou Web.config). Par défaut, cette chaîne de connexion cible 
        // la base de données « Copie0Web.Models.EFCopie0 » sur votre instance LocalDb. 
        // 
        // Pour cibler une autre base de données et/ou un autre fournisseur de base de données, modifiez 
        // la chaîne de connexion « EFCopie0 » dans le fichier de configuration de l'application.
        public EFCopie0()
            //: base("name=EFCopie0")
        {
        }

        // Ajoutez un DbSet pour chaque type d'entité à inclure dans votre modèle. Pour plus d'informations 
        // sur la configuration et l'utilisation du modèle Code First, consultez http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}