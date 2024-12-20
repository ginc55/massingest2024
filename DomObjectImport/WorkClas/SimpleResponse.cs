using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomObjectImport.WorkClas
{
    class SimpleResponse
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


    }
}
