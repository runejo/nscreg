using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nscreg.Data.Entities.ComplexTypes
{
    public class DataAccessPermissions
    {
        public DataAccessPermissions()
        {
            Permissions = new List<Permission>();
        }

        public DataAccessPermissions(IEnumerable<Permission> permissions)
        {
            Permissions = permissions.ToList();
        }

        public List<Permission> Permissions { get; set; }
    }
}
