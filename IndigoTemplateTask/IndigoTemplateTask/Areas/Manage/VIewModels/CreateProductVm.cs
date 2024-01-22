using System.ComponentModel.DataAnnotations;

namespace IndigoTemplateTask.Areas.Manage.VIewModels
{
    public class CreateProductVm
    {
        public string TItle { get; set; }


        public string Description { get; set; }

        [Required]
        public  IFormFile  Image { get; set; }


    }
}
