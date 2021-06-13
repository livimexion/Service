using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrestationService.Models
{
    public enum TypeTransfert
    {
        ORANGEMONEY, TIGOCASH, WARI, WAVE
    }

    public class Transfert : Paiement
    {
        [Display(Name = " Operateur")]
        public TypeTransfert typeTransfert { get; set; }

        [Display(Name = "Numero Telephone")]
        [MaxLength(50, ErrorMessage = "taille maximale 50")]
        public string numero { get; set; }

    }
}