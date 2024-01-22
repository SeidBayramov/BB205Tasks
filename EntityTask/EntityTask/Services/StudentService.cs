using EntityTask.DAL;
using EntityTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityTask.Services
{
    internal class StudentService
    {
        private AppDbContext context;

        public StudentService()
        {
            context = new AppDbContext();
        }

        public void AddStudent(Student student)
        {
            context.Students.Add(student);
            Console.WriteLine("Students added");
            context.SaveChanges();
        }

        public void RemoveStudent(int id)
        {
            Student student = context.Students.Find(id);
            if (student != null)
            {
                context.Students.Remove(student);
                Console.WriteLine("Student deleted");
            }
            else
            {
                Console.WriteLine("There is no such this ID");
            }
            context.SaveChanges();
        }
        public void GetAllStudents()
        {
            List<Student> students = context.Students.ToList();
            if (students.Count > 0)
            {
                foreach (Student student in students)
                {
                    Console.WriteLine($"Student ID: {student.Id} Student name: {student.Name} Student age: {student.Age}");
                }
            }
            else { Console.WriteLine("There is no such student"); }
        }


        public void UpdateStudent(int id)
        {
            var updatedStudent = context.Students.Find(id);
            if (updatedStudent != null)
            {
                Console.Write("Enter new name of student: ");
                string newName = Console.ReadLine();
                updatedStudent.Name = newName;
                Console.WriteLine($"Student name updated successfully:{newName}");
            }
            else 
            { 
                Console.WriteLine("There is no such student"); 
            }
            context.SaveChanges();
        }
    }
}