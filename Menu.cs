using DatabasUppgiftEntity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasUppgiftEntity
{
    internal class Menu
    {
        FindMethods findMethods = new FindMethods();

        public void MenuChoice()
        {
            Console.WriteLine("Välj mellan olika alternativ:");
            Console.WriteLine("1. Skriv ut personal listan");
            Console.WriteLine("2. Skriv ut elev listan");
            Console.WriteLine("3. Skriv ut elev listan efter klass");
            Console.WriteLine("4. Skriv ut betygen som tilldelades förra månaden");
            Console.WriteLine("5. Skriv ut information om en specifik klass betyg och deras medelbetyg");
            Console.WriteLine("6. Lägg till en ny elev");
            Console.WriteLine("7. Lägg till en ny personal");
            Console.WriteLine("8. Skriv ut information om lärare");
            Console.WriteLine("9. Skriv ut information om aktiva kurser");

            while (true)
            {
                Console.WriteLine("Ange ett nummer (1-8) eller 0 för att avsluta:");

                var input = int.TryParse(Console.ReadLine(), out var choice);
                if (!input)
                {
                    Console.WriteLine("Ogiltig inmatning. Vänligen ange ett nummer.");
                    continue;
                }

                if (choice == 0)
                {
                    Console.WriteLine("Avslutar programmet...");
                    break;
                }
                switch (choice)
                {
                    case 1:
                        findMethods.PrintStaffList();
                        break;
                    case 2:
                        findMethods.PrintStudentList();
                        break;
                    case 3:
                        findMethods.PrintStudentByClass();
                        break;
                    case 4:
                        findMethods.PrintGradesLastMonth();
                        break;
                    case 5:
                        findMethods.PrintAverageGrades();
                        break;
                    case 6:
                        findMethods.AddStudent();
                        break;
                    case 7:
                        findMethods.AddEmployee();
                        break;
                    case 8:
                        findMethods.PrintTeachersCount();
                        break;
                    case 9:
                        findMethods.PrintActiveCourses();
                        break;

                    default:
                        Console.WriteLine("Ogiltigt val");
                        break;
                }
            }
        }
    }
}
