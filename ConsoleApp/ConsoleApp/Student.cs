using System;
using System.Collections.Generic;

class Student
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public double AvgPoint { get; set; }

    public Student(string name, string surname, double avgPoint)
    {
        Name = name;
        Surname = surname;
        AvgPoint = Math.Max(0, Math.Min(100, avgPoint));
    }
}