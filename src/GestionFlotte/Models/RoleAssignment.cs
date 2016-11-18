using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionFlotte.Models
{
    public class RoleAssignment
    {
        public int MarinID { get; set; }
        public int RoleID { get; set; }
        public Marin Marin { get; set; }
        public Role Role { get; set; }
    }
}
