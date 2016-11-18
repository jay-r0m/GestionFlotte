using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionFlotte.Models
{
    public class Marin
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Le nom est obligatoire")]
        public string Nom { get; set; }
        [Required(ErrorMessage = "Le prénom est obligatoire")]
        public string Prenom { get; set; }
        public int BateauID { get; set; }
        // public DateTime EnrollmentDate { get; set; }
        
        public ICollection<RoleAssignment> Roles { get; set; }
        public Bateau Bateau { get; set; }
    }
}
