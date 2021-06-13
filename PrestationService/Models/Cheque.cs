using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrestationService.Models
{
    public class Cheque : Paiement
    {

        [Display(Name = "Numero cheque")]
        [MaxLength(50, ErrorMessage = "taille maximale 50")]
        public string numeroChk { get; set; }

        [Display(Name = "A l'ordre De")]
        [MaxLength(50, ErrorMessage = "taille maximale 50")]
        public string lordre { get; set; }

    }
}