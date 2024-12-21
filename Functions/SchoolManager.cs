using DatabasUppgiftEntity.Models;
using Microsoft.IdentityModel.Tokens;
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
        public void ChangeStudentInfo()
        {
            using var context = new SkolDatabasContext();
            Console.WriteLine("Skrive ID numret av eleven som du vill ändra");
            if (int.TryParse(Console.ReadLine(), out var idStudent))
            {

                var studentToChange = context.Students
                    .Where(x => x.Id == idStudent)
                    .ToList();

                if (studentToChange.Count == 0)
                {
                    Console.WriteLine("Studentet hittades inte");
                }
                else
                {
                    var selectedStudent = studentToChange[0];
                    Console.WriteLine("Vilket information vill du ändra?(Namn/Efternamn/Personnummer/Klass)");

                    string answer = Console.ReadLine();

                    switch (answer)
                    {
                        case "Namn":
                            Console.WriteLine("Skriv den nya namnet:");
                            try
                            {
                                string firstName = Console.ReadLine();
                                if (firstName.IsNullOrEmpty())
                                {
                                    Console.WriteLine("Ange ett namn");
                                    break;
                                }
                                selectedStudent.FirstName = firstName;
                                context.SaveChanges();
                                Console.WriteLine($"Namnet blev ändrat till {firstName}");

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }

                            break;
                        case "Efternamn":
                            Console.WriteLine("Skriv den nya efternamnet:");
                            try
                            {
                                string lastName = Console.ReadLine();
                                if (lastName.IsNullOrEmpty())
                                {
                                    Console.WriteLine("Ange ett efternamn");
                                    break;
                                }
                                selectedStudent.LastName = lastName;
                                context.SaveChanges();
                                Console.WriteLine($"Namnet blev ändrat till {lastName}");

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        case "Personnummer":
                            Console.WriteLine("Skriv den nya personnummer (ÅÅÅÅMMDDXXXX):");
                            try
                            {
                                string personalNumber = Console.ReadLine();

                                if (personalNumber.Length != 12) //personnummer måste vara 12 siffror
                                {
                                    Console.WriteLine("Personnummer måste vara 12 siffror");
                                    return;
                                }
                                if (context.Students.Any(x => x.PersonalNumber == personalNumber))//personnummer måste vara unik
                                {
                                    Console.WriteLine("Personnummer finns redan");
                                    return;
                                }
                                selectedStudent.PersonalNumber = personalNumber;
                                context.SaveChanges();
                                Console.WriteLine($"Personnummer blev ändrat till {personalNumber}");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;

                        case "Klass":
                            Console.WriteLine("Skriv den nya klassen i t.ex följande format: \"3A\"");
                            string className = Console.ReadLine().ToUpper();

                            try
                            {
                                if (className.Length != 2)
                                {
                                    Console.WriteLine("Klassen måste vara exakt 2 tecken lång.");
                                    break;
                                }

                                if (!char.IsDigit(className[0]) || !char.IsLetter(className[1])) //dubbelkolla att klassen är i rätt formatet siffra + bokstav
                                {
                                    Console.WriteLine("Klassen måste vara i formatet 3A (en siffra följt av en stor bokstav).");
                                    break;
                                }

                                selectedStudent.ClassName = className;
                                context.SaveChanges();
                                Console.WriteLine($"Klassen blev ändrat till {className}");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"{e.Message}");
                            }
                            break;

                        default:
                            Console.WriteLine("Fel kategori angiven");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Ogiltigt ID nummer");
            }
        }

    }
}
