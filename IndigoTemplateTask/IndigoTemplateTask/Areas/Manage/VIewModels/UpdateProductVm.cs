using System.ComponentModel.DataAnnotations;

namespace IndigoTemplateTask.Areas.Manage.VIewModels
{
    public class UpdateProductVm
    {
        public int Id { get; set; }
        public string TItle { get; set; }


        public string Description { get; set; }

        public IFormFile? Image { get; set; }

        public List<ProductImagesVm>? ProductImages { get; set; }

        public List<int>? ImageIds { get; set; }


        public class ProductImagesVm
        {
            public int Id { get; set; }
            public string ImgUrl { get; set; }
            public bool? IsPrime { get; set; }

        }
    }
}

