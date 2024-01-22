using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
	internal class Students
	{
		private string _name;

		public string Name
		{
			get { return _name; }

			set { _name = value; }
		}

		private string _surname;

		public string Surname
		{
			get { return _surname; }
			set { _surname = value; }
		}


		private int _avgPoint;

		public int AvgPoint
		{
			get { return _avgPoint; }
			set
			{
				if (value >= 0 && value <= 100)
				{
					_avgPoint = value;
				}
				else
				{
					Console.WriteLine("Point must be 0 -100");
				}
			}

		}
        public Students(string Name,string surname,int AvgPoint)

		{
			this._name = Name;
			this._surname = surname;	
			this._avgPoint = AvgPoint;
            
        }
    }
}
