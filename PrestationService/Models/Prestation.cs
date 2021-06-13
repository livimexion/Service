using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrestationService.Models
{
    public class Prestation
    {
        [Key]
        [ScaffoldColumn(false)]
        public int idPrestation { get; set; }

        [Required(ErrorMessage = "*"), Display(Name = "Adresse")]
        [StringLength(100)]
        public string adresse { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? date { get; set; }

        //[Display(Name = "Client")]
        //public int IdClient { get; set; }
        //[ForeignKey("IdClient")]
        //public virtual Client Client { get; set; }

         [Display(Name = "Client")]
        public int id { get; set; }
        //[ForeignKey("id")]
        //public virtual Utilisateur UtilisateurClient { get; set; }


        [Display(Name = "Service")]
        public int idService { get; set; }
        [ForeignKey("idService")]
        public virtual Service Service { get; set; }

        //[Display(Name = "Paiement")]
        //public int idPay { get; set; }
        //[ForeignKey("idPay")]
        //public virtual Paiement Paiement { get; set; }

  
    }
}