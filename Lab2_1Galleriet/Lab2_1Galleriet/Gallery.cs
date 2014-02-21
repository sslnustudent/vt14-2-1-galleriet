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
        private static readonly string PhysicalUploadImagePath;
        private static readonly Regex SantizePath;

        static Gallery()
        {
            PhysicalUploadImagePath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), @"Thumbnails");
            ApprovedExenstions = new Regex(@"^.*\.(gif|jpg|png)$");

            var invalidChars = new string(Path.GetInvalidFileNameChars());
            SantizePath = new Regex(string.Format("[{0}]", Regex.Escape(invalidChars))); 

        }

        public IEnumerable<FileData> GetImagesNames()
        {

            var di = new DirectoryInfo(PhysicalUploadImagePath);

            return (from fi in di.GetFiles()
                    select new FileData
                    {
                        Name = "Thumbnails/" + fi.Name,
                    }
                        ).AsEnumerable();
        }

        static bool ImageExists(string name)
        {
            var di = new DirectoryInfo(PhysicalUploadImagePath);
            var list = di.GetFiles();

            for (int i = 0; i < list.Count(); i++)
            {
                if (Convert.ToString(list[i]) == name)
                {
                    return false;
                }
 
            }

            return true;
 
        }

        bool IsValidImage(Image image)
        {
            var di = new DirectoryInfo(PhysicalUploadImagePath);
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
            //if (ImageExists(fileName) == true)
            //{
 
            //}
            //if (IsValidImage(Image.FromStream(stream)) == true)
            //{
 
            //} 
            //Image img = System.Drawing.Image.FromStream(stream);
            //img.Save(PhysicalUploadImagePath  + fileName, System.Drawing.Imaging.ImageFormat.Jpeg);

            //FileStream fileStream = File.Create(PhysicalUploadImagePath, (int)stream.Length);
            // Initialize the bytes array with the stream length and then fill it with data
            //byte[] bytesInStream = new byte[stream.Length];
            //stream.Read(bytesInStream, 0, bytesInStream.Length);
            //// Use write method to write to the file specified above
            //fileStream.Write(bytesInStream, 0, bytesInStream.Length);

            var image = System.Drawing.Image.FromStream(stream); // stream -> ström med bild 
            var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero);
            thumbnail.Save(PhysicalUploadImagePath +  fileName); // path -> fullständig fysisk filnamn inklusive sökväg 

            return fileName;
        }

    }
}