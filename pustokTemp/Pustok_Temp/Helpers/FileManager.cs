namespace Pustok_Temp.Helpers
{

        public static class FileManager
        {
            public static string Upload(this IFormFile file, string envPath, string folderName)
            {
                string filname = file.FileName;
                if (filname.Length > 64)
                {
                    filname = filname.Substring(filname.Length - 64);
                }
                filname = Guid.NewGuid().ToString() + filname;


                string path = envPath + folderName + filname;
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return filname;
            }

            public static void DeleteFile(string imgUrl, string envPath, string folderName)
            {
                string path = envPath + folderName + imgUrl;

                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }
    }

