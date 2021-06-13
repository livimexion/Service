using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrestationService.Models
{
    public enum TypePay
    {
        CARTE, CHEQUE, TRANSFERT
    }

    public abstract class Paiement
    {
        [Key]
        [ScaffoldColumn(false)]
        public int idPay { get; set; }

        [Display(Name = "Type de Paiement")]
        public TypePay typPay { get; set; }

        [Display(Name = " Acompte")]
        public bool acompte { get; set; }

    }
}