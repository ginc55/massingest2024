using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomObjectImport.WorkClas
{

    /// <summary>
    /// Viena DO metadatu lauks/apakšlauks.
    /// </summary>

    class DigitalObjectMetadataField
    {
        public string ElementName { get; set; }

        /// <summary>
        /// Ja ir datuma lauks, tad vērtība ir formātā: yyyy-MM-dd
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Obligāts tikai, ja lauka tips ir Tezaurs.
        /// </summary>
        /// <remarks>Persons|Institutions|Events|UnifiedNames|Subjects|Places|GenreFormTerms</remarks>
        public string ThesaurusType { get; set; }

        public string Lang { get; set; }

        public  List<DigitalObjectMetadataSubfield> Subfields { get; set; }
        

    }
}
