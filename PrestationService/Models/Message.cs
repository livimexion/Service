using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrestationService.Models
{
    public class Message
    {
        [Key]
        [ScaffoldColumn(false)]
        public int idMsg { get; set; }

        [Required(ErrorMessage = "*"), Display(Name = "Emettre besoin")]
        public string texte { get; set; }

        [Display(Name ="Date et Heure")]
        public DateTime date { get; }

        [Required(ErrorMessage = "*"), Display(Name = "Type de service")]
        public string serviceType { get; set; }

        [Display(Name = "Utilisateur")]
        public int id { get; set; }
        [ForeignKey("id")]
        public virtual Utilisateur Utilisateur { get; set; }

        [Display(Name = "Utilisateur")]
        public int idTo { get; set; }
        [ForeignKey("id")]
        public virtual Utilisateur UtilisateurTo { get; set; }

        [Display(Name = "Chat")]
        public int idRoom { get; set; }
        [ForeignKey("idRoom")]
        public virtual ChatRoom ChatRoom { get; set; }
    }
}