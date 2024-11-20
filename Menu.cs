using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasUppgiftEntity
{
    internal class Menu
    {
        public void MenuChoice()
        {
            Console.WriteLine("Choose between different options:");
            Console.WriteLine("1. Print the staff list");
            Console.WriteLine("2. Print the students list");
            Console.WriteLine("3. Print the students list by class");
            Console.WriteLine("4. Print the grades assigned the last month");
            Console.WriteLine("5. Print information about a special class grades");
            Console.WriteLine("6. Add a new student");
            Console.WriteLine("7. Add a new staff");

            while (true)
            {

                Console.WriteLine("Enter a number (1-10) or 0 to exit:");

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
                        using (var context = new MyDBCOntext())
                        {
                            var staff = context.Employee.ToList();
                            foreach (var employee in staff)
                            {
                                Console.WriteLine($"ID: {employee.Id}, Name: {employee.Name}, Role: {employee.Position}");
                            }
                        } 
                        using (var context = new MyDBCOntext())
                        {
                            var staff = context.Employee.ToList();
                            var position = context.Employee.Where(x => x.Position == "Teacher").ToList();
                            foreach (var employee in position)
                            {
                                Console.WriteLine($"ID: {employee.Id}, Name: {employee.Name}, Role: {employee.Position}");
                            }
                        }

                        break;
                    case 2:
                        Console.WriteLine("You chose 2");
                        break;
                    case 3:
                        Console.WriteLine("You chose 3");
                        break;
                    case 4:
                        Console.WriteLine("You chose 4");
                        break;
                    case 5:
                        Console.WriteLine("You chose 5");
                        break;
                    case 6:
                        Console.WriteLine("You chose 6");
                        break;
                    case 7:
                        Console.WriteLine("You chose 7");
                        break;

                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
    }
}
