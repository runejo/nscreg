using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nscreg.Data.Entities.ComplexTypes
{
    public class Permission
    {
        public Permission(string propertyName, bool canRead, bool canWrite)
        {
            PropertyName = propertyName;
            CanRead = canRead;
            CanWrite = canWrite;
        }

        public string PropertyName { get; set; }
        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }

    }
}
