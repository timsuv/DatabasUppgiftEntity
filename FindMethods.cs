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
            Console.WriteLine("Do you want to see the list over all employees?(Yes/No)");
            string answer = Console.ReadLine();
            if (answer?.ToLower() == "yes")
            {
                using (var context = new MyDBCOntext())
                {
                    var staff = context.Employee.ToList();
                    foreach (var employee in staff)
                    {
                        Console.WriteLine($"ID: {employee.Id}, Name: {employee.Name}, Role: {employee.Position}");
                    }
                }

            }
            else //if the user wants to see a specific position, he writes no and then chooses the position
            {
                using (var context = new MyDBCOntext())
                {
                    Console.WriteLine("Write the position you want to search: (Teacher/Admin/Principle)");
                    var userPosition = Console.ReadLine();
                    var position = context.Employee
                    .Where(x => x.Position == userPosition).ToList();
                    if (position.Count == 0)
                    {
                        Console.WriteLine("No position found");
                    }
                    else
                    {
                        foreach (var employee in position)
                        {
                            Console.WriteLine($"ID: {employee.Id}, Name: {employee.Name}, Role: {employee.Position}");
                        }

                    }

                }

            }
        }
        public void PrintStudentList()
        {
            using (var context = new MyDBCOntext())
            {
                var students = context.Student.ToList();
                Console.WriteLine("Do you want to print the list in an ascending order or descending (Type ASC/DSC)");
                var answer1 = Console.ReadLine();
                if (answer1.ToLower() == "asc")
                {
                    students = students.OrderBy(s => s.Name).ToList();
                    foreach (var student in students)
                    {
                        Console.WriteLine($"Name: {student.Name}, Class: {student.Class}");
                    }
                }
                else
                {
                    students = students.OrderByDescending(s => s.Name).ToList();
                    foreach (var student in students)
                    {
                        Console.WriteLine($"Name: {student.Name}, Class: {student.Class}");
                    }
                }
            }
        }
        public void PrintStudentByClass()
        {
            using (var context = new MyDBCOntext())
            {
                Console.WriteLine("Choose from which class you want to print the students(Type A/B/C)");
                var userClass = Console.ReadLine();
                if (userClass.ToLower() == "3a" || userClass.ToLower() == "3b" || userClass.ToLower() == "3c")
                {
                    Console.WriteLine("Do you want to print the list in an ascending order or descending (Type ASC/DSC)");
                    var answer1 = Console.ReadLine();
                    var students = context.Student.Where(x => x.Class == userClass).ToList();

                    if (answer1.ToLower() == "asc")
                    {
                        students = students.OrderBy(s => s.Name).ToList();
                        foreach (var student in students)
                        {
                            Console.WriteLine($"Name: {student.Name}, Class: {student.Class}");
                        }
                    }
                    else
                    {
                        students = [.. students.OrderByDescending(s => s.Name)]; //shorter way to order the list suggested by the IDE
                        foreach (var student in students)
                        {
                            Console.WriteLine($"Name: {student.Name}, Class: {student.Class}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid class");
                }
            }
        }
        public void PrintGradesLastMonth()
        {
            using (var context = new MyDBCOntext())
            {
                var grades = context.Grade
                    .Where(x => x.DateAssigned.Month == DateTime.Now.Month - 1)
                    .Include(x => x.Student) //Including the student table to get the student name (left join in a raw query)
                    .Select(x => new
                    {
                        Name = x.Student.Name,
                        Class = x.Student.Class,
                        GradeValue = x.GradeValue,
                        DateAssigned = x.DateAssigned
                    }).ToList();
                foreach (var record in grades)
                {
                    Console.WriteLine($"Student: {record.Name}, Course: {record.Class}, Grade: {record.GradeValue}, Grade Assigned: {record.DateAssigned}");
                }

            }
        }

        public void PrintAverageGrades()
        {
            using (var context = new MyDBCOntext())
            {
                Console.WriteLine("For which class do you want to know the average grade? (3A/3B/3C) ");
                string userClass = Console.ReadLine();
                var grades = context.Grade
                .Include(x => x.Student)
                .Where(x => x.Student.Class == userClass)
                .Select(x => new
                {
                    Name = x.Student.Name,
                    Class = x.Student.Class,
                    GradeValue = x.GradeValue
                }).ToList();

                if (grades.Count == 0)
                {
                    Console.WriteLine("No grades found");
                }
                else
                {
                    var gradeValues = new Dictionary<string, double> //Dictionary to convert the grade value to a double to then process the average
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
                        if (gradeValues.TryGetValue(record.GradeValue, out double gradeValue)) //If the grade value is found in the dictionary, add it to the sum
                        {
                            sum += gradeValue;
                            Console.WriteLine($"Student: {record.Name}, Course: {record.Class}, Grade: {record.GradeValue}");
                        }
                    }
                    double average = sum / grades.Count;


                    Console.WriteLine($"The average grade for class {userClass.ToUpper()} is {GetGrade(average)}");//GetGrade method to convert the average to a grade value in letters


                }
            }
        }
        public string GetGrade(double average) //Converts the average grade to a letter grade
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
            using (var context = new MyDBCOntext())
            {
                Console.WriteLine("Write students name");
                var name = Console.ReadLine();
                Console.WriteLine("Write students personal number");
                var personalNumber = Console.ReadLine();
                Console.WriteLine("Write students class");
                var studentClass = Console.ReadLine();
                var student = new Student
                {

                    Name = name,
                    PersonalNumber = personalNumber,
                    Class = studentClass
                };
                //Try catch to catch any exceptions that might occur when adding a new student
                try
                {
                    context.Student.Add(student);
                    context.SaveChanges();
                    Console.WriteLine($"You successfully added a new student {name}, {personalNumber}, {studentClass}");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to add a new student");
                    Console.WriteLine($"Error details: {e.Message}");

                }
            }
        }
        public void AddEmployee()
        {
            using (var context = new MyDBCOntext())
            {
                Console.WriteLine("Write the new staffs name:");
                string staffName = Console.ReadLine();
                Console.WriteLine("Write the new staffs position:");
                string staffPosition = Console.ReadLine();
                var newStaff = new Employee
                {
                    Name = staffName,
                    Position = staffPosition
                };
                //Try catch to catch to check if the employee was added successfully
                try
                {
                    context.Employee.Add(newStaff);
                    context.SaveChanges();
                    Console.WriteLine($"You successfully added a new staff {staffName}, {staffPosition}");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to add the employee");
                    Console.WriteLine($"Error details: {e.Message}");
                }


            }
        }
    }
}
