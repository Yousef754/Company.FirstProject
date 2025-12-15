using System.Linq.Expressions;

namespace Company.PL.Helpers
{
    public class DocumnetSettings
    {

        public static string UploadFile(IFormFile file, string folername)
        {
            var FolerPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\File", folername);

            var FileName = $"{Guid.NewGuid}{file.FileName}";

            var FilePath = Path.Combine(FolerPath, FileName);

            var FileStream = new FileStream(FilePath, FileMode.Create);

            file.CopyTo(FileStream);



            return FileName;
        }

        public static void DeleteFile(string FileName, string folerName) 
        {

            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\File", folerName, FileName);
            if (File.Exists(FilePath))
            { 
            
            File.Delete(FilePath);
            }
        
        
        }


    }
}
