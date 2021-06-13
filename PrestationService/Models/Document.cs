using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestationService.Models
{
    public class Document
    {
        public int documentId { get; set; }
        public string documentName { get; set; }
        public int obligatoire { get; set; }
        public List<Document> DocumentList { get; set; }
    }
}