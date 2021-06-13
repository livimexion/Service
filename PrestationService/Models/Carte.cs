using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrestationService.Models
{
    public enum TypeCarte
    {
        VISA, MASTER, PAYPAL
    }

    public class Carte : Paiement
    {
        
        [Display(Name = "Type de Carte")]
        public TypeCarte typeCarte { get; set; }

        [Display(Name = "Numero carte")]
        [MaxLength(50, ErrorMessage = "taille maximale 50")]
        public string numeroCarte { get; set; }

        [Display(Name = "Numero CSV")]
        [MaxLength(50, ErrorMessage = "taille maximale 50")]
        public string numeroCsv { get; set; }

        [Required(ErrorMessage = "*"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? expiration { get; set; }
    }
}