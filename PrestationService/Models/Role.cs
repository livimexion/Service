using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrestationService.Models
{
    public class Role
    {
        [Key]
        [ScaffoldColumn(false)]
        public int idRole { get; set; }

        [Required(ErrorMessage = "*"), Display(Name = "Libelle")]
        [StringLength(100)]
        public string libRole { get; set; }
    }
}