using DatabasUppgiftEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasUppgiftEntity.Functions
{
    public class SchoolManager
    {
        public void AddStudent()
        {
            using (var context = new SkolDatabasContext())
            {

                Console.WriteLine("Skriv studentens förnamn");
                var firstName = Console.ReadLine();
                Console.WriteLine("Skriv studentens efternamn");
                var lastName = Console.ReadLine();
                Console.WriteLine("Skriv studentens personnummer");
                var personalNumber = Console.ReadLine();
                Console.WriteLine("Skriv studentens klass");
                var studentClass = Console.ReadLine();
                var student = new Student
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PersonalNumber = personalNumber,
                    ClassName = studentClass
                };
                //Try catch för att fånga eventuella undantag som kan uppstå när en ny student läggs till
                try
                {
                    context.Students.Add(student);
                    context.SaveChanges();
                    Console.WriteLine($"Du har framgångsrikt lagt till en ny student {firstName} {lastName}, {personalNumber}, {studentClass}");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Misslyckades med att lägga till en ny student");
                    Console.WriteLine($"Felinformation: {e.Message}");

                }
            }
        }
        public void AddEmployee()
        {
            using (var context = new SkolDatabasContext())
            {
                Console.WriteLine("Skriv den nya personalens förnamn:");
                string staffFirstName = Console.ReadLine();
                Console.WriteLine("Skriv den nya personalens efternamn:");
                string staffLastName = Console.ReadLine();
                Console.WriteLine("Skriv den nya personalens position:");
                string staffPosition = Console.ReadLine();
                Console.WriteLine("Vilken lön?");
                decimal staffSalary = decimal.Parse(Console.ReadLine());
                var newStaff = new Employee
                {
                    FirstName = staffFirstName,
                    LastName = staffLastName,
                    Position = staffPosition,
                    Salary = staffSalary

                };
                //Try catch för att kontrollera om anställdes lades till 
                try
                {
                    context.Employees.Add(newStaff);
                    context.SaveChanges();
                    Console.WriteLine($"Du har framgångsrikt lagt till en ny personal {staffFirstName} {staffLastName}, {staffPosition}");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Misslyckades med att lägga till personalen");
                    Console.WriteLine($"Felinformation: {e.Message}");
                }


            }
        }

    }
}
