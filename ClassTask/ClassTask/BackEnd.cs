namespace ClassTask
{
    internal class BackEnd:Developer
    {
        public int SqlExperienceYear;

        public BackEnd(string name, string surname, int experience, int SqlExperienceYear)
            : base(name, surname,experience)
        {
          this.SqlExperienceYear = SqlExperienceYear;
        }
    }
}


