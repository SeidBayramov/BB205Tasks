namespace ClassTask
{
    internal class Developer
    {
        public string Name; 
        public string Surname;
        public int Age;
        public int Experience;

        public Developer(string name, string surname, int experience)
        {
            Name = name;
            Surname = surname;
            Experience = experience;
        }

        public Developer(string name, string surname, int age, int experience)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Experience = experience;
        }
    }
}
