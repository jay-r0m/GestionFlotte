using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionFlotte.Data;
using GestionFlotte.Models;

namespace GestionFlotte.Data
{
    public static class DbInitializer
    {
        public static void InitializeFleet(FlotteContext context)
        {
            context.Database.EnsureCreated();

            if (context.Roles.Any())
            {
                return;   // DB has been seeded
            }
            
            var Roles = new Role[]
            {
                new Role{RoleID=1,Nom="Commandant"},
                new Role{RoleID=2,Nom="Second"},
                new Role{RoleID=3,Nom="Cuisinier"},
                new Role{RoleID=4,Nom="Mécano"},
                new Role{RoleID=5,Nom="Matelot"}
            };
            foreach (Role s in Roles)
            {
                context.Roles.Add(s);
            }
            context.SaveChanges();
            
            var TypesBateaux = new TypeBateau[]
            {
                new TypeBateau { Designation="Barque" },
                new TypeBateau { Designation="Voilier" },
                new TypeBateau { Designation="Remorqueur" },
                new TypeBateau { Designation="Corvette" },
                new TypeBateau { Designation="Pétrolier" }
            };
            foreach (TypeBateau s in TypesBateaux)
            {
                context.TypesBateaux.Add(s);
            }
            context.SaveChanges();

            var Postes = new Poste[]
            {
                new Poste { RoleID=1, TypeBateauID=3, Minimum=1 },
                new Poste { RoleID=2, TypeBateauID=3, Minimum=1 },
                new Poste { RoleID=4, TypeBateauID=3, Minimum=1 },
                new Poste { RoleID=5, TypeBateauID=3, Minimum=2 },
            };
            foreach (Poste s in Postes)
            {
                context.Postes.Add(s);
            }
            context.SaveChanges();
        }
    }
}