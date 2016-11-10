using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestionFlotte.Models
{
    public class Role
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoleID { get; set; }
        public string Nom { get; set; }
        // public int Credits { get; set; }

        public ICollection<Maitrise> Maitrises { get; set; }
    }
}
