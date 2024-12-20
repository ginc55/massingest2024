using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomObjectImport.WorkClas
{
     public class StartAddDigitalObjectResponse
    {
        /// <summary>
        /// Pazīme, vai ir veiksmīga operācija.
        /// </summary>
        /// <remarks>Ja ir kaut viena kļūda, tad jābūt "false"!</remarks>
        public bool OK { get; set; }

        /// <summary>
        /// Validācijas kļūdu saraksts.
        /// </summary>
        /// <remarks>
        /// Sistēmas kļūdas šeit nenonāk! 
        /// (Tās tiek ielogotas servera EventLog-ā.)</remarks>
        public List<string> Errors { get; set; }

        /// <summary>
        /// No jauna izveidotās Digitālā Objekta ID.
        /// </summary>
        /// <remarks>Tukšs, ja ir bijusi kļūda.</remarks>
        public int? DigitalObjectId { get; set; }
        
    }
}
