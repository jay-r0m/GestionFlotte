using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionFlotte.Models.FleetViewModels
{
    public class MarinIndexData
    {
        public IEnumerable<Marin> Marins { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
}
