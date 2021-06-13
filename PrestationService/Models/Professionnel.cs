using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrestationService.Models
{
    public class Professionnel
    {
        //public Professionnel()
        //{
        //    this.services = new HashSet<Service>();

        //}

        [Key]
        [ScaffoldColumn(false)]
        public int IdProfessionnel { get; set; }

        [Required(ErrorMessage = "*"), Display(Name = "Adresse")]
        [DataType(DataType.MultilineText)]
        [StringLength(200)]
        public string adresse { get; set; }

        [Required(ErrorMessage = "*"), Display(Name = "Diplome")]
        [DataType(DataType.Upload)]
        public string diplome { get; set; }

        [Required(ErrorMessage = "*"), Display(Name = "Specialite")]
        [MaxLength(50, ErrorMessage = "taille maximale 50")]
        public string specialite { get; set; }

        [Required(ErrorMessage = "*"), Display(Name = "Ninea")]
        [DataType(DataType.Upload)]
        public string ninea { get; set; }

        [Required(ErrorMessage = "*"), Display(Name = "Registre de commerce")]
        [DataType(DataType.Upload)]
        public string registre { get; set; }


        //public virtual ICollection<File> Files { get; set; }

        // public virtual ICollection<Service> services { get; set; }

    }
}