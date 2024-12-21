using DatabasUppgiftEntity.Models;
using System;

namespace DatabasUppgiftEntity.Functions
{
    internal class Menu
    {
        private FindMethods findMethods = new FindMethods();
        private SchoolManager schoolManager = new SchoolManager();
        string logo = @"
   ▄▄▄▄▄   ▄█▄     ▄  █ ████▄ ████▄ █     
  █     ▀▄ █▀ ▀▄  █   █ █   █ █   █ █     
▄  ▀▀▀▀▄   █   ▀  ██▀▀█ █   █ █   █ █     
 ▀▄▄▄▄▀    █▄  ▄▀ █   █ ▀████ ▀████ ███▄  
           ▀███▀     █                  ▀ 
                    ▀                     
                                          
";

        public void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(logo);
                Console.WriteLine("Välj mellan olika alternativ:");
                Console.WriteLine("[1] Information relaterad till eleverna");
                Console.WriteLine("[2] Information relaterad till personalen");
                Console.WriteLine("[3] Information relaterad till betyg och kurser");
                Console.WriteLine("[4] Personal och student hanterare");
                Console.WriteLine("[0] Avsluta programmet");

                Console.Write("\nAnge ett nummer (0-4): ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 0:
                            Console.WriteLine("Avslutar programmet...");
                            return;
                        case 1:
                            StudentMenu();
                            break;
                        case 2:
                            StaffMenu();
                            break;
                        case 3:
                            GradesAndCoursesMenu();
                            break;
                        case 4:
                            SchoolPeopleManager();
                            break;
                        default:
                            Console.WriteLine("Ogiltigt val. Tryck på valfri tangent för att fortsätta...");
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltig inmatning. Tryck på valfri tangent för att fortsätta...");
                    Console.ReadKey();
                }
            }
        }

        private void StudentMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Elevmeny:");
                Console.WriteLine("[1] Skriv ut listan över elever");
                Console.WriteLine("[2] Skriv ut listan över elever efter klass [NEW FEATURE]");
                Console.WriteLine("[0] Tillbaka till huvudmenyn");

                Console.Write("\nAnge ett nummer (0-2): ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 0:
                            return;
                        case 1:
                            findMethods.PrintStudentList();
                            break;
                        case 2:
                            findMethods.PrintStudentByClass();
                            break;
                        default:
                            Console.WriteLine("Ogiltigt val.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltig inmatning.");
                }
                Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
        }

        private void StaffMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Personalmeny:");
                Console.WriteLine("[1] Skriv ut listan över personalen");
                Console.WriteLine("[2] Skriv ut information om lärare[NEW FEATURE]");
                Console.WriteLine("[0] Tillbaka till huvudmenyn");

                Console.Write("\nAnge ett nummer (0-2): ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 0:
                            return;
                        case 1:
                            findMethods.PrintStaffList();
                            break;
                        case 2:
                            findMethods.PrintTeachersCount();
                            break;
                        default:
                            Console.WriteLine("Ogiltigt val.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltig inmatning.");
                }
                Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
        }

        private void GradesAndCoursesMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Betyg och kurser meny:");
                Console.WriteLine("[1] Skriv ut betygen som tilldelades förra månaden");
                Console.WriteLine("[2] Skriv ut information om en specifik klass betyg och deras medelbetyg");
                Console.WriteLine("[3] Skriv ut information om aktiva kurser [NEW FEATURE]");
                Console.WriteLine("[0] Tillbaka till huvudmenyn");

                Console.Write("\nAnge ett nummer (0-3): ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 0:
                            return;
                        case 1:
                            findMethods.PrintGradesLastMonth();
                            break;
                        case 2:
                            findMethods.PrintAverageGrades();
                            break;
                        case 3:
                            findMethods.PrintActiveCourses();
                            break;
                        default:
                            Console.WriteLine("Ogiltigt val.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltig inmatning.");
                }
                Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
        }

        private void SchoolPeopleManager()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Hanteringsmeny:");
                Console.WriteLine("[1] Lägg till en ny elev");
                Console.WriteLine("[2] Lägg till en ny personal");
                Console.WriteLine("[3] Ändra students information [NEW FEATURE]");
                Console.WriteLine("[0] Tillbaka till huvudmenyn");

                Console.Write("\nAnge ett nummer (0-3): ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 0:
                            return;
                        case 1:
                            schoolManager.AddStudent();
                            break;
                        case 2:
                            schoolManager.AddEmployee();
                            break;
                        case 3:
                            schoolManager.ChangeStudentInfo();
                            break;
                        default:
                            Console.WriteLine("Ogiltigt val.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltig inmatning.");
                }
                Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
        }
    }
}
