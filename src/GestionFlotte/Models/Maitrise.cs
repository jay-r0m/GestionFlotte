using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionFlotte.Models
{
    /* usage pour les rôles minimum par type de bateau */
    //public enum Grade
    //{
    //    A, B, C, D, F
    //}

    public class Maitrise
    {
        public int MaitriseID { get; set; }
        public int RoleID { get; set; }
        public int MarinID { get; set; }
        //public Grade? Grade { get; set; }

        public Role Role { get; set; }
        public Marin Marin { get; set; }
    }
}
