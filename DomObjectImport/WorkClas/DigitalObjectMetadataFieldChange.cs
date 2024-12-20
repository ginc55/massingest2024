using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomObjectImport.WorkClas
{
    public class DigitalObjectMetadataFieldChange
    {
        public DigitalObjectMetadataFieldChangeType Type { get; set; }

        /// <summary>
        /// InsertField, InsertSubfield, UpdateField, UpdateSubfield, DeleteField, DeleteSubfield
        /// </summary>
        public string ElementName { get; set; }

        /// <summary>
        /// InsertField, InsertSubfield, UpdateSubfield, DeleteSubfield
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// UpdateField
        /// </summary>
        public string NewValue { get; set; }

        /// <summary>
        /// InsertField
        /// </summary>
        public string Lang { get; set; }

        /// <summary>
        /// UpdateField
        /// </summary>
        public string NewLang { get; set; }

        /// <summary>
        /// UpdateField, DeleteField
        /// </summary>
        public string ByValue { get; set; }

        /// <summary>
        /// InsertField, InsertSubfield, UpdateSubfield, DeleteSubfield
        /// </summary>
        public string SubfieldElementName { get; set; }

        /// <summary>
        /// InsertField, InsertSubfield
        /// </summary>
        public string SubfieldValue { get; set; }

        /// <summary>
        /// UpdateSubfield
        /// </summary>
        public string NewSubfieldValue { get; set; }

        /// <summary>
        /// UpdateSubfield, DeleteSubfield
        /// </summary>
        public string BySubfieldValue { get; set; }
    }

}
