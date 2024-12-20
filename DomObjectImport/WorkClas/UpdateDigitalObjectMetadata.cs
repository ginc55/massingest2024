using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomObjectImport.WorkClas
{
    public class UpdateDigitalObjectMetadataRequest
    {
        public int DigitalObjectId { get; set; }

        /// <summary>
        /// DO Metadatu veicamo izmaiņu sarakts.
        /// </summary>
        public List <DigitalObjectMetadataFieldChange> Changes { get; set; }

        /// <summary>
        /// Pazīme, vai veikt tikai parametru validāciju.
        /// </summary>
        /// <remarks>Izmaiņas DB netiek veiktas!</remarks>
        public bool VerifyOnly { get; set; }
    }

}
