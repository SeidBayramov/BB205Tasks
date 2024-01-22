using Microsoft.AspNetCore.Http;

namespace Carvilla.Helpers
{
    public static class FileManager
    {
        public static bool CheckImage(this IFormFile file)
        {
            return file.ContentType.Contains("image/") && file.Length / 1024 / 1024 <= 3;
        }

        public static string Upload(this IFormFile file, string path, string web)
        {
            var uploadpath = Path.Combine(path, web);
            if (!Directory.Exists(uploadpath))
            {
                Directory.CreateDirectory(uploadpath);
            }

            string fileName = Guid.NewGuid().ToString() + file.FileName;
            using (var stream = new FileStream(Path.Combine(uploadpath, fileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }
    }
}
