class Group
{
    public string No { get; set; }
    public int StudentLimit { get; set; }
    private Student[] Students;
    private int studentCount;

    public Group(string groupNo, int studentLimit)
    {
        No = groupNo;
        StudentLimit = Math.Max(5, Math.Min(18, studentLimit));
        Students = new Student[StudentLimit];
        studentCount = 0;
    }

    public void AddStudent(Student student)
    {
        if (studentCount < StudentLimit)
        {
            Students[studentCount] = student;
            studentCount++;
        }
        else
        {
            Console.WriteLine("Group is full. Cannot add more students.");
        }
    }
    public Student[] FilterByName(string filterString)
    {
        Student[] filteredStudents = new Student[StudentLimit];
        int count = 0;

        foreach (var student in Students)
        {
            if (student != null && (student.Name + " " + student.Surname).ToLower().Contains(filterString.ToLower()))
            {
                filteredStudents[count] = student;
                count++;
            }
        }

        return filteredStudents;
    }
}