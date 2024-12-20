using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomObjectImport.WorkClas
{
    public class StartAddFileToDigitalObjectRequest
    {
        /// <summary>
        /// Digitālā objekta ID
        /// </summary>
        public int DigitalObjectId { get; set; }

        /// <summary>
        /// Oriģinālais datnes nosaukums ar paplašinājumu
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Datnes tips
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Datnes nosaukums
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Datnes apraksts
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Datnes izmērs baitos
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// Datnes čeksumma
        /// </summary>
        public string Hash { get; set; }
    }

}
