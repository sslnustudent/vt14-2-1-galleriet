using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Lab2_1Galleriet
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void OkButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                Gallery gal = new Gallery();
                Label1.Text = gal.SaveImage(ImageFileUpload.PostedFile.InputStream, ImageFileUpload.PostedFile.FileName);
            }
        }

        public IEnumerable<FileData> GalleryRepeater_GetData()
        {
            Gallery gal = new Gallery();
            return gal.GetImagesNames();
        }
    }
}