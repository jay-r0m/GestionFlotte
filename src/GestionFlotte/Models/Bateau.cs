using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionFlotte.Models
{
    public class Bateau
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Le nom est obligatoire")]
        public string Nom { get; set; }
        public int TypeBateauID { get; set; }
                
        public TypeBateau TypeBateau { get; set; }
        public ICollection<Marin> Marins { get; set; }

        public bool isStaffed
        {
            get
            {
                if (Marins == null || Marins.Count < TypeBateau.Postes.Count) return false;

                // On génère un dictionnaire poste-minimum
                Dictionary<string, int?> dictionary = new Dictionary<string, int?>();
                foreach ( var poste in TypeBateau.Postes)
                {
                    dictionary.Add(poste.Role.Nom, poste.Minimum == null ? 0 : poste.Minimum );
                }
                // On parcours les marins et leurs roles pour décrémenter les postes du dictionnaire
                foreach( var marin in Marins)
                {
                    foreach (var roleAssignment in marin.Roles)
                    {
                        if (dictionary.ContainsKey(roleAssignment.Role.Nom) && dictionary[roleAssignment.Role.Nom] > 0)
                        {
                            dictionary[roleAssignment.Role.Nom] -= 1;
                        }
                    }
                }
                // S'il reste des entrées > 0 dans le dictionnaire, on manque d'efectifs
                foreach( var poste in dictionary)
                {
                    if( poste.Value > 0)
                    {
                        return false;
                    }
                }
                // ---- WARNING /!\
                // TODO : si des marins ont plusieurs rôles, on peu avoir un déficit apparent car un marin serait compté comme matelot alors que c'est le seul commandant.

                // On renvoi true si les tests sont OK.
                return true;
            }
        }
    }
}
