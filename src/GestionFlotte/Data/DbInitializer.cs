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
        public static void Initialize(SchoolContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Students.Any())
            {
                return;   // DB has been seeded
            }

            var students = new Student[]
            {
            new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
            new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
            new Course{CourseID=1050,Title="Chemistry",Credits=3,},
            new Course{CourseID=4022,Title="Microeconomics",Credits=3,},
            new Course{CourseID=4041,Title="Macroeconomics",Credits=3,},
            new Course{CourseID=1045,Title="Calculus",Credits=4,},
            new Course{CourseID=3141,Title="Trigonometry",Credits=4,},
            new Course{CourseID=2021,Title="Composition",Credits=3,},
            new Course{CourseID=2042,Title="Literature",Credits=4,}
            };
            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
            new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
            new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
            new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
            new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
            new Enrollment{StudentID=3,CourseID=1050},
            new Enrollment{StudentID=4,CourseID=1050,},
            new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
            new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
            new Enrollment{StudentID=6,CourseID=1045},
            new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},
            };
            foreach (Enrollment e in enrollments)
            {
                context.Enrollments.Add(e);
            }
            context.SaveChanges();
        }

        public static void InitializeFleet(FlotteContext context)
        {
            context.Database.EnsureCreated();

            if (context.Roles.Any())
            {
                return;   // DB has been seeded
            }
            /*
                        var Marins = new Marin[]
                        {
                            new Marin{Nom="Chemistry",Prenom="Jean-Pierre"},
                            new Marin{Nom="Microeconomics",Prenom="Jean-Pierre"},
                            new Marin{Nom="Macroeconomics",Prenom="Jean-Pierre"},
                            new Marin{Nom="Calculus",Prenom="Jean-Pierre"},
                            new Marin{Nom="Trigonometry",Prenom="Jean-Pierre"},
                            new Marin{Nom="Composition",Prenom="Jean-Pierre"},
                            new Marin{Nom="Literature",Prenom="Jean-Pierre"}
                        };
                        foreach (Marin c in Marins)
                        {
                            context.Marins.Add(c);
                        }
                        context.SaveChanges();
            */
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
            /*
            var Maitrises = new Maitrise[]
            {
                new Maitrise{RoleID=1,MarinID=1},
                new Maitrise{RoleID=1,MarinID=2},
                new Maitrise{RoleID=1,MarinID=3},
                new Maitrise{RoleID=2,MarinID=1},
                new Maitrise{RoleID=2,MarinID=3},
                new Maitrise{RoleID=2,MarinID=4},
                new Maitrise{RoleID=3,MarinID=1},
                new Maitrise{RoleID=4,MarinID=1},
                new Maitrise{RoleID=4,MarinID=4},
                new Maitrise{RoleID=5,MarinID=5},
                new Maitrise{RoleID=5,MarinID=6},
                new Maitrise{RoleID=5,MarinID=7},
            };
            foreach (Maitrise e in Maitrises)
            {
                context.Maitrises.Add(e);
            }
            */
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