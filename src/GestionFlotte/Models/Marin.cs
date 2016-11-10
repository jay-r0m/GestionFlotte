using System;
using System.Collections.Generic;

namespace GestionFlotte.Models
{
    public class Marin
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int BateauID { get; set; }
        // public DateTime EnrollmentDate { get; set; }

        public ICollection<Maitrise> Maitrises { get; set; }
        public Bateau Bateau { get; set; }
    }
}
