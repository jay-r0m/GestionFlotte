using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionFlotte.Models.FleetViewModels
{
    public class AssignedRoleData
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public bool Assigned { get; set; }
    }
}
