using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrestationService.Models
{
    public class ChatRoom
    {
        [Key]
        [ScaffoldColumn(false)]
        public int idRoom { get; set; }

        [Required(ErrorMessage = "*"), Display(Name = "Nom")]
        public string libelleRoom { get; set; }

    }
}