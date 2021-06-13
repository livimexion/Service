using PrestationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestationService.ViewModels
{
    public class RelationService
    {
        public IEnumerable<Service> services { get; set; }
        public IEnumerable<Professionnel> professionnels { get;set; }
        public IEnumerable<SousCategorie> SousCategories { get; set; }
        public IEnumerable<Prestation> prestations { get; set; }
    }
}