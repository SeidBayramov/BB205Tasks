using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
	internal class Group
	{
		private string _no;

		public Students[] students;

		public string No
		{
			get { return _no; }
			set
			{
				string nameParts = value;
				if (value.Length >= 3)
				{
					if (char.IsUpper(nameParts[0]) && char.IsUpper(nameParts[0]))
					{
						_no = value;
					}
					else
					{
						Console.WriteLine("Group number must start big letters");
					}
				}

			}

				

		}
		private int _studentlimit;

		public int StudentLimit
		{
			get { return _studentlimit; }
			set
			{
				if (value>=5 && value <= 18)
				{
					_studentlimit += value;

				}
				else
				{
					Console.WriteLine("Limiti asmisiz");
				}
			}

		}
        public Group(string No)
        {
				this.No = No;
			Students[] students= new Students[0];
				
        }
        public void AddStudent()
		{
			Array.Resize(ref students, _studentlimit);
		}
        public  Students[] FilterByName(string name)
		{
            int count = 0;
            Students[] result = new Students[students.Length];

            foreach (var student in students)
            {
                if (student != null && student.Name == name)
                {
                    result[count] = student;
                    count++;
                }
            }

            return result;
        }
    }
	}
       

        