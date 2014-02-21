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

        private Gallery _gal;

        public Gallery Gal 
        {
            get { return _gal ?? (_gal = new Gallery()); }
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string url = Request.QueryString["name"];
            if (url != null)
            {
                url = url.Replace("Thumbnails", "Images"); ;
                ShowImage.ImageUrl = url;
            }
            
        }

        protected void OkButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                CheckInput.Value = Gal.SaveImage(ImageFileUpload.PostedFile.InputStream, ImageFileUpload.PostedFile.FileName);
                if (CheckInput.Value != "FAIL")
                {
                    OkDiv.Visible = true;
                    
                    NameLabel.Text = ImageFileUpload.PostedFile.FileName;
 
                }
            }
        }

        public IEnumerable<FileData> GalleryRepeater_GetData()
        {
            return Gal.GetImagesNames();
            
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            OkDiv.Visible = false;
        }
    }
}