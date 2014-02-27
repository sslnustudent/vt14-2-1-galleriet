using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Drawing;

namespace Lab2_1Galleriet
{
    public class Gallery
    {
        private static readonly Regex ApprovedExenstions;
        private static readonly string PhysicalUploadThumbImagePath;
        private static readonly string PhysicalUploadImagePath;
        private static readonly Regex SantizePath;

        static Gallery()
        {
            PhysicalUploadThumbImagePath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), @"Thumbnails");
            ApprovedExenstions = new Regex(@"^.*\.(gif|jpg|png)$");
            PhysicalUploadImagePath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), @"Images");

            var invalidChars = new string(Path.GetInvalidFileNameChars());
            SantizePath = new Regex(string.Format("[{0}]", Regex.Escape(invalidChars))); 


        }

        public IEnumerable<FileData> GetImagesNames()
        {

            var di = new DirectoryInfo(PhysicalUploadThumbImagePath);

            return (from fi in di.GetFiles()
                    select new FileData
                    {
                        Name = "Thumbnails/" + fi.Name,
                    }
                        ).AsEnumerable();
        }

        static bool ImageExists(string name)
        {
            var di = new DirectoryInfo(PhysicalUploadThumbImagePath);
            var list = di.GetFiles();

            for (int i = 0; i < list.Count(); i++)
            {
                if (list[i].Name == name)
                {
                    return true;
                }
 
            }

            return false;
 
        }

        bool IsValidImage(Image image)
        {
            var di = new DirectoryInfo(PhysicalUploadThumbImagePath);
            var list = di.GetFiles();
            if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Gif.Guid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string SaveImage(Stream stream, string fileName)
        {
           
            if (IsValidImage(Image.FromStream(stream)) == true)
            {
                return "FAIL";
            }
            else if (ImageExists(fileName) == true)
            {
                var end = fileName.Substring(fileName.Length - 4);

                var image = System.Drawing.Image.FromStream(stream); // stream -> ström med bild 
                image.Save(PhysicalUploadImagePath + @"/" + fileName.Remove(fileName.Length - 4) + "(2)" + end);
                var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero);
                thumbnail.Save(PhysicalUploadThumbImagePath + @"/" + fileName.Remove(fileName.Length - 4) + "(2)" + end); // path -> fullständig fysisk filnamn inklusive sökväg 
                return fileName;
            }
            else
            {
                var image = System.Drawing.Image.FromStream(stream); // stream -> ström med bild 
                image.Save(PhysicalUploadImagePath + @"/" + fileName);
                var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero);
                thumbnail.Save(PhysicalUploadThumbImagePath + @"/" + fileName); // path -> fullständig fysisk filnamn inklusive sökväg 
                return fileName;
            }
        }

    }
}