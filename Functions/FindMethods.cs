using DatabasUppgiftEntity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasUppgiftEntity.Functions
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
                    Console.WriteLine("Skriv den position du vill söka: (Lärare/Administratör/Rektor)");
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
                Console.WriteLine("Välj från vilken klass du vill skriva ut eleverna (Skriv 3A/3B/3C)");
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
                        x.Student.FirstName,
                        x.Student.LastName,
                        Class = x.Student.ClassName,
                        x.GradeValue,
                        x.DateAssigned,
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
                    x.Student.FirstName,
                    x.Student.LastName,
                    Class = x.Student.ClassName,
                    x.GradeValue
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
                    string lowestGrade = null;
                    double lowestGradeValue = double.MaxValue;
                    string highestGrade = null;
                    double highestGradeValue = double.MinValue;
                    foreach (var record in grades)
                    {
                        if (gradeValues.TryGetValue(record.GradeValue, out double gradeValue)) //Om betygsvärdet hittas i dictionaryn, lägg till det i summan
                        {
                            sum += gradeValue;
                            if (gradeValue < lowestGradeValue)
                            {
                                lowestGradeValue = gradeValue;
                                lowestGrade = record.GradeValue;
                            }

                            if (gradeValue > highestGradeValue)
                            {
                                highestGradeValue = gradeValue;
                                highestGrade = record.GradeValue;
                            }

                            Console.WriteLine($"Student: {record.FirstName} {record.LastName}, Klass: {record.Class}, Betyg: {record.GradeValue}");
                        }
                    }
                    double average = sum / grades.Count;


                    Console.WriteLine($"\nMedelbetyget för klass {userClass.ToUpper()} är {GetGrade(average)}");//GetGrade-metod för att konvertera medelvärdet till ett betygsvärde i bokstäver


                    if (lowestGrade != null)
                    {
                        Console.WriteLine($"\nLägsta betyget i klassen är {lowestGrade}");
                    }
                    if( highestGrade != null)
                    {
                        Console.WriteLine($"\nHögsta betyget i klassen är {highestGrade}");
                    }
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

            Console.WriteLine($"Här är lista på alla aktiva kurser på våran skolan just nu\n");
            foreach (var activeCourse in activeCourses)
            {
                Console.WriteLine($"Kursnamn: {activeCourse.Name}");
            }

            while (true)
            {
                Console.WriteLine("Vill du se listan över inaktiva kurser? (ja/nej)");
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
                    Console.WriteLine($"Här är lista på alla inaktiva kurser på våran skolan just nu\n");
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