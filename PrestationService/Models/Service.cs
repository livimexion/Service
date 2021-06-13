using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrestationService.Models
{
    public class Service
    {
        [Key]
        [ScaffoldColumn(false)]
        public int idService { get; set; }

        [Required(ErrorMessage = "*"), Display(Name = "Lbelle")]
        [StringLength(100)]
        public string libelle { get; set; }

        [Required(ErrorMessage = "*"), Display(Name = "Description")]
        [StringLength(100)]
        [DataType(DataType.MultilineText)]
        public string description { get; set; }

        [Required(ErrorMessage = "*"), Display(Name = "Prix")]
        public int prix { get; set; }

        [Required(ErrorMessage = "*"), Display(Name = "nombre d'heure")]
        public int nbHeure { get; set; }

        //[Display(Name = "Professionnel")]
        //public int IdProfessionnel { get; set; }
        //[ForeignKey("IdProfessionnel")]
        //public virtual Professionnel Professionnel { get; set; }

        [Display(Name = "Professionnel")]
        public int id { get; set; }
        //[ForeignKey("id")]
        //public virtual Utilisateur UtilisateurProf { get; set; }


        [Display(Name = "Sous Categorie")]
        public int idSouCat { get; set; }
        [ForeignKey("idSouCat")]
        public virtual SousCategorie SousCategorie { get; set; }

     
    }
}