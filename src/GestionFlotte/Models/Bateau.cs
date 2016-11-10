using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GestionFlotte.Models
{
    public class Bateau
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public int TypeBateauID { get; set; }
                
        public TypeBateau TypeBateau { get; set; }
        public ICollection<Marin> Marins { get; set; }
    }
}
