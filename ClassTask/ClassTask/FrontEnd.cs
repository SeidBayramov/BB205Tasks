namespace ClassTask
{
    internal class FrontEnd : Developer
    {
        public int ReactExperienceYear;

        public FrontEnd(string name, string surname, int experience, int ReactExperienceYear)
         : base(name, surname, experience)
        {
            this.ReactExperienceYear = ReactExperienceYear;
        }
    }
}

