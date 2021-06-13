using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrestationService.Models
{
    public class SousCategorie
    {

        public  SousCategorie()
        {
            this.services = new HashSet<Service>();

        }

        [Key]
        [ScaffoldColumn(false)]
        public int idSouCat { get; set; }

        [Required(ErrorMessage = "*"), Display(Name = "Nom  Sous Categorie")]
        [StringLength(100)]
        public string libSouCat { get; set; }

        [Required(ErrorMessage = "*"), Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string description { get; set; }

        [Required(ErrorMessage = "*"), Display(Name = "Photo")]
        [DataType(DataType.ImageUrl, ErrorMessage = "Choisir une image")]
        public string photo { get; set; }

        [Display(Name = "Categorie")]
        public int idCat { get; set; }
        [ForeignKey("idCat")]
        public virtual Categorie Categorie { get; set; }

        public virtual ICollection<Service> services { get; set; }


    }
}