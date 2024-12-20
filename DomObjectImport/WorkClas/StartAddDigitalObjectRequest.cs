using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomObjectImport.WorkClas
{
    class StartAddDigitalObjectRequest
    {
        public string Type { get; set; }

        public string TitleLang { get; set; }

        public string Title { get; set; }

        public string Source { get; set; }
        public string ExternalId { get; set; }

        public string subType { get; set; }
        public string Copyright { get; set; }
        public string AccessRight { get; set; }
        public string dateCreated { get; set; }

        public string  formatExtent { get; set; }
        /// <summary>
        /// DO Metadatu lauku sarakts.
        /// </summary>
        public List<DigitalObjectMetadataField> Fields { get; set; }

        /// <summary>
        /// Pazīme, vai veikt tikai parametru validāciju.
        /// </summary>
        /// <remarks>Izmaiņas DB netiek veiktas!</remarks>
        public bool VerifyOnly { get; set; }
        
    }
}
