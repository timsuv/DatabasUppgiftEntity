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
            Console.WriteLine("Choose between different options:");
            Console.WriteLine("1. Print the staff list");
            Console.WriteLine("2. Print the students list");
            Console.WriteLine("3. Print the students list by class");
            Console.WriteLine("4. Print the grades assigned the last month");
            Console.WriteLine("5. Print information about a special class grades and their average grade");
            Console.WriteLine("6. Add a new student");
            Console.WriteLine("7. Add a new staff");

            while (true)
            {

                Console.WriteLine("Enter a number (1-8) or 0 to exit:");

                var input = int.TryParse(Console.ReadLine(), out var choice);
                if (!input)
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                if (choice == 0)
                {
                    Console.WriteLine("Exiting the program...");
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

                       findMethods.PrintStudentList();
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

                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
        
    }
}
