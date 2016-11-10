using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionFlotte.Models
{
    public class Poste
    {
        public int PosteID { get; set; }
        public int RoleID { get; set; }
        public int TypeBateauID { get; set; }
        public int? Minimum { get; set; }

        public Role Role { get; set; }
        public TypeBateau TypeBateau { get; set; }
    }
}
