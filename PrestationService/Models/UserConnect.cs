using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrestationService.Models
{
    public class UserConnect
    {
        [Key]
        [ScaffoldColumn(false)]
        public int idConnect { get; set; }

        [Display(Name = "Utilisateur")]
        public int id { get; set; }
        [ForeignKey("id")]
        public virtual Utilisateur Utilisateur { get; set; }

        [Display(Name = "Chat")]
        public int idRoom { get; set; }
        [ForeignKey("idRoom")]
        public virtual ChatRoom ChatRoom { get; set; }
    }
}