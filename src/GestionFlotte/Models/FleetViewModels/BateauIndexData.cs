using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionFlotte.Models.FleetViewModels
{
    public class BateauIndexData
    {
        public IEnumerable<Bateau> Bateaux { get; set; }
        public IEnumerable<Poste> Postes { get; set; }
        public IEnumerable<Marin> Marins { get; set; }
    }
}
