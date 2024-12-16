using DatabasUppgiftEntity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasUppgiftEntity
{
    internal class FindMethods
    {
        public void PrintStaffList()
        {
            Console.WriteLine("Vill du se listan över alla anställda? (Ja/Nej)");
            string answer = Console.ReadLine();
            if (answer?.ToLower() == "ja")
            {
                using (var context = new SkolDatabasContext())
                {
                    var staff = context.Employees.ToList();
                    foreach (var employee in staff)
                    {
                        Console.WriteLine($"ID: {employee.Id}, Förnamn: {employee.FirstName}, Efternamn: {employee.LastName}, Roll: {employee.Position}");
                    }
                }

            }
            else //om användaren vill se en specifik position, skriver han nej och väljer sedan positionen
            {
                using (var context = new SkolDatabasContext())
                {
                    Console.WriteLine("Skriv den position du vill söka: (Lärare/Admin/Rektor)");
                    var userPosition = Console.ReadLine();
                    var position = context.Employees
                    .Where(x => x.Position == userPosition).ToList();
                    if (position.Count == 0)
                    {
                        Console.WriteLine("Ingen position hittades");
                    }
                    else
                    {
                        foreach (var employee in position)
                        {
                            Console.WriteLine($"ID: {employee.Id}, Förnamn: {employee.FirstName}, Efternamn: {employee.LastName}, Roll: {employee.Position}");
                        }

                    }

                }

            }
        }
        public void PrintStudentList()
        {
            using (var context = new SkolDatabasContext())
            {
                var students = context.Students.ToList();
                Console.WriteLine("Vill du skriva ut listan i stigande eller fallande ordning? (Skriv ASC/DSC)");
                var answer1 = Console.ReadLine();
                if (answer1.ToLower() == "asc")
                {
                    students = students.OrderBy(s => s.LastName).ToList();
                    foreach (var student in students)
                    {
                        Console.WriteLine($"Efternamn: {student.LastName}, Förnamn: {student.FirstName}, Personnummer: {student.PersonalNumber}, Klass: {student.ClassName}");
                    }
                }
                else
                {
                    students = students.OrderByDescending(s => s.LastName).ToList();
                    foreach (var student in students)
                    {
                        Console.WriteLine($"Efternamn: {student.LastName}, Förnamn: {student.FirstName}, Personnummer: {student.PersonalNumber}, Klass: {student.ClassName}");
                    }
                }
            }
        }
        public void PrintStudentByClass()
        {
            using (var context = new SkolDatabasContext())
            {
                Console.WriteLine("Välj från vilken klass du vill skriva ut eleverna (Skriv A/B/C)");
                var userClass = Console.ReadLine();
                if (userClass.ToLower() == "3a" || userClass.ToLower() == "3b" || userClass.ToLower() == "3c")
                {
                    Console.WriteLine("Vill du skriva ut listan i stigande eller fallande ordning? (Skriv ASC/DSC)");
                    var answer1 = Console.ReadLine();
                    var students = context.Students.Where(x => x.ClassName == userClass).ToList();

                    if (answer1.ToLower() == "asc")
                    {
                        students = students.OrderBy(s => s.LastName).ToList();
                        foreach (var student in students)
                        {
                            Console.WriteLine($"Efternamn: {student.LastName}, Förnamn: {student.FirstName}, Klass: {student.ClassName}");
                        }
                    }
                    else
                    {
                        students = [.. students.OrderByDescending(s => s.LastName)]; //kortare sätt att ordna listan föreslagen av visual studdiom
                        foreach (var student in students)
                        {
                            Console.WriteLine($"Efternamn: {student.LastName}, Förnamn: {student.FirstName}, Klass: {student.ClassName}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltig klass");
                }
            }
        }
        public void PrintGradesLastMonth()
        {
            using (var context = new SkolDatabasContext())
            {
                var grades = context.Grades
                    .Where(x => x.DateAssigned.Month == DateTime.Now.Month - 1)
                    .Include(x => x.Student) //Inkluderar studenttabellen för att få studentens namn (vänster join i en råfråga)
                    .Include(x => x.Course) //Inkluderar kurstabellen för att få kursens namn (vänster join i en råfråga)
                    .Select(x => new
                    {
                        FirstName = x.Student.FirstName,
                        LastName = x.Student.LastName,
                        Class = x.Student.ClassName,
                        GradeValue = x.GradeValue,
                        DateAssigned = x.DateAssigned,
                        CourseName = x.Course.Name

                    }).ToList();
                foreach (var record in grades)
                {
                    Console.WriteLine($"Student: {record.FirstName} {record.LastName}, Klass: {record.Class}, Kursnamn: {record.CourseName}, Betyg: {record.GradeValue}, Betyg tilldelat: {record.DateAssigned}");
                }

            }
        }

        public void PrintAverageGrades()
        {
            using (var context = new SkolDatabasContext())
            {
                Console.WriteLine("För vilken klass vill du veta medelbetyget? (3A/3B/3C) ");
                string userClass = Console.ReadLine();
                var grades = context.Grades
                .Include(x => x.Student)
                .Where(x => x.Student.ClassName == userClass)
                .Select(x => new
                {
                    FirstName = x.Student.FirstName,
                    LastName = x.Student.LastName,
                    Class = x.Student.ClassName,
                    GradeValue = x.GradeValue
                }).ToList();

                if (grades.Count == 0)
                {
                    Console.WriteLine("Inga betyg hittades");
                }
                else
                {
                    var gradeValues = new Dictionary<string, double> //Dictionary för att konvertera betygsvärdet till en double för att sedan bearbeta medelvärdet
                                    {
                                        { "A", 5.0 },
                                        { "B", 4.0 },
                                        { "C", 3.0},
                                        { "D", 2.0},
                                        { "E", 1.0},
                                        { "F", 0.0 }
                                    };

                    double sum = 0;
                    foreach (var record in grades)
                    {
                        if (gradeValues.TryGetValue(record.GradeValue, out double gradeValue)) //Om betygsvärdet hittas i dictionaryn, lägg till det i summan
                        {
                            sum += gradeValue;
                            Console.WriteLine($"Student: {record.FirstName} {record.LastName}, Klass: {record.Class}, Betyg: {record.GradeValue}");
                        }
                    }
                    double average = sum / grades.Count;


                    Console.WriteLine($"Medelbetyget för klass {userClass.ToUpper()} är {GetGrade(average)}");//GetGrade-metod för att konvertera medelvärdet till ett betygsvärde i bokstäver


                }
            }
        }
        public string GetGrade(double average) //Konverterar medelbetyget till ett bokstavsbetyg
        {
            if (average == 5.0) return "A";
            if (average >= 4.5) return "A-";
            if (average >= 4.0) return "B+";
            if (average >= 3.5) return "B";
            if (average >= 3.0) return "C+";
            if (average >= 2.5) return "C";
            if (average >= 2.0) return "D+";
            if (average >= 1.5) return "D";
            return "F";
        }
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

        public void PrintTeachersCount()
        {
            using var context = new SkolDatabasContext();
            var teachersCount = context.Employees
                .Where(x => x.Position == "Lärare")
                .Include(x => x.Courses)
                .ToList();

            foreach (var teacher in teachersCount)
            {
                var courses = string.Join(":", teacher.Courses.Select(x => x.Name).ToList());//string join för att komma åt kursnamnet
                Console.WriteLine($"Lärare: {teacher.FirstName} {teacher.LastName}, Kurs: {courses}");

            }
            Console.WriteLine($"Det finns totalt {teachersCount.Count} som jobbar på skolan");
        }

        public void PrintActiveCourses()
        {
            using var context = new SkolDatabasContext();
            var activeCourses = context.Courses
                .Where(x => x.Active == true)
                .ToList();

            Console.WriteLine($"Här är lista på alla aktiva kurser på våran skolan just nu");
            foreach (var activeCourse in activeCourses)
            {
                Console.WriteLine($"Kursnamn: {activeCourse.Name}");
            }

            while (true)
            {
                Console.WriteLine("Vill du se listan över inaktivar kurser? (ja/nej)");
                string answer = Console.ReadLine().ToLower();
                if (answer == "nej")
                {
                    break;
                }
                else if (answer == "ja")
                {
                    var inactiveCourses = context.Courses
                        .Where(x => x.Active == false)
                        .ToList();
                    Console.WriteLine($"Här är lista på alla inaktiva kurser på våran skolan just nu");
                    foreach (var inactiveCourse in inactiveCourses)
                    {
                        Console.WriteLine($"Kursnamn: {inactiveCourse.Name}");
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Inkorrekt svar");
                }

            }
        }
    }
}