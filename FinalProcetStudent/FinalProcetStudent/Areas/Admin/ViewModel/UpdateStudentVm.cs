namespace FinalProcetStudent.Areas.Admin.ViewModel
{
    public class UpdateStudentVm
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public byte Age { get; set; }

        public IFormFile? Image { get; set; }

        public DateTime BornTime { get; set; }

        public double Point { get; set; }

        public List<StudentsImagesVm>? StudentsImagesVms { get; set; }

        public List<int>? ImageIds { get; set; }


        public class StudentsImagesVm
        {
            public int Id { get; set; }
            public string ImgUrl { get; set; }
            public bool? IsPrime { get; set; }

        }
    }
}
