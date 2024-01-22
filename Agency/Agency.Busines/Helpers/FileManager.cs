using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Busines.Helpers
{
    public static class FileManager
    {
        public static string Upload(this IFormFile file, string envPath, string folderName)
        {
            string fileName = file.FileName.Length > 64 ? file.FileName.Substring(file.FileName.Length - 64, 64) : file.FileName;
            fileName = Guid.NewGuid().ToString() + fileName;

            string path = envPath + folderName + fileName;
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }
        public static void DeleteFile(this string imgUrl, string envPath, string folderName)
        {
            string path = envPath + folderName + imgUrl;

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        public static bool CheckContent(this IFormFile file, string content)
        {
            return file.ContentType.Contains(content);
        }
        public static bool CheckLength(this IFormFile file, int length)
        {
            return file.Length <= length;
        }
    }
}