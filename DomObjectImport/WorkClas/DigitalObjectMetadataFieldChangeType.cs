using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomObjectImport.WorkClas
{
    public enum DigitalObjectMetadataFieldChangeType : int
    {
        InsertField = 0,
        InsertSubfield = 1,
        UpdateField = 2,
        UpdateSubfield = 3,
        DeleteField = 4,
        DeleteSubfield = 5
    }


    
}
