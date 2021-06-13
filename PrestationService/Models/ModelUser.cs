using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestationService.Models
{
    public class ModelUser
    {

        public string identifiant { get; set; }

        public string nom { get; set; }
        
        public string prenom { get; set; }
        
        public string email { get; set; }

        public string tel { get; set; }

        public string role { get; set; }
    }
}