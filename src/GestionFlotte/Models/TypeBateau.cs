using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionFlotte.Models
{
    public class TypeBateau
    {
        public int TypeBateauID { get; set; }
        public string Designation { get; set; }

        public ICollection<Poste> Postes { get; set; }
    }
}
