using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrestationService.Models
{
    public class Categorie
    {
        public Categorie()
            {
            this.sousCategories = new HashSet<SousCategorie>();

            }

        [Key]
        [ScaffoldColumn(false)]
        public int idCat { get; set; }

        [Required(ErrorMessage ="*"), Display(Name ="Nom Categorie")]
        [StringLength(100)]
        public string libCat { get; set; }

        [Required(ErrorMessage ="*"), Display(Name ="Description")]
        [DataType(DataType.MultilineText)]
        public string description { get; set; }

        [Required(ErrorMessage = "*"), Display(Name = "Photo")]
        [DataType(DataType.ImageUrl, ErrorMessage ="Choisir une image")]
        public string photo { get; set; }

        public virtual ICollection<SousCategorie> sousCategories { get; set; }
    }
}