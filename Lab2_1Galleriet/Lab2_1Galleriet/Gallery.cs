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
            return File.Exists(Path.Combine(PhysicalUploadImagePath, name));
        }

        bool IsValidImage(Image image)
        {
            return image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Gif.Guid || 
                image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Jpeg.Guid || 
                image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Png.Guid;
        }

        public string SaveImage(Stream stream, string fileName)
        {
            var image = System.Drawing.Image.FromStream(stream);
            
            if (!IsValidImage(image))
            {
                throw new ArgumentException();
            }
            
            var counter = 2;
            var end = fileName.Substring(fileName.Length - 4);
            while (ImageExists(fileName))
            {
                if (counter > 2)
                {
                    fileName = fileName.Remove(fileName.Length - 7) + "(" + counter + ")" + end;
                }
                else
                {
                    fileName = Path.GetFileNameWithoutExtension(fileName) + "(" + counter + ")" + end;
                }
                counter++;
            }

            image.Save(Path.Combine(PhysicalUploadImagePath, fileName));
            
            var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero);
            thumbnail.Save(Path.Combine(PhysicalUploadThumbImagePath, fileName));
            
            return fileName;
        }
    }
}