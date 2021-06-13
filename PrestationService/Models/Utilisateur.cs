using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrestationService.Models
{
    public class Utilisateur
    {
        [Key]
        public int id { get; set; }

        [MaxLength(250)]
        public string iduser { get; set; }

        [Required(ErrorMessage = "*"), Display(Name = "Identifiant")]
        [StringLength(30)]
        public string identifiant { get; set; }

        [Required(ErrorMessage = "*"), Display(Name = "Nom")]
        [StringLength(80)]
        public string nom { get; set; }

        [Required(ErrorMessage = "*"), Display(Name = "Prénom")]
        [StringLength(80)]
        public string prenom { get; set; }

        [Required(ErrorMessage = "*"), Display(Name = "Email"), DataType(DataType.EmailAddress)]
        [StringLength(80)]
        public string email { get; set; }

        [Required(ErrorMessage = "*"), Display(Name = "Telephone")]
        [MaxLength(80, ErrorMessage = "taille maximale 80"), DataType(DataType.PhoneNumber)]
        public string tel { get; set; }

        [Required(ErrorMessage = "*"), Display(Name = "Role")]
        [StringLength(100)]
        public string role { get; set; }
    }
}