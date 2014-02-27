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
            //Page.Validators.Add();
        }

        protected void OkButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                CustomValidator cv = new CustomValidator();
                cv.ErrorMessage = "Uppladningen misslyckades";
                cv.IsValid = false;
                CheckInput.Value = Gal.SaveImage(ImageFileUpload.PostedFile.InputStream, ImageFileUpload.PostedFile.FileName);
                if (CheckInput.Value != "FAIL")
                {
                    OkDiv.Visible = true;
                    LabelOk.Text = "Bilden " + ImageFileUpload.PostedFile.FileName + " har blivit uppladdad!!!";
                    ShowImage.ImageUrl = "~/Images/" + ImageFileUpload.PostedFile.FileName;
                    //HyperLink1.NavigateUrl = "http://localhost:5540/Default.aspx?name=Thumbnails/" + ImageFileUpload.PostedFile.FileName;
                    Response.Redirect("http://localhost:5540/Default.aspx?name=Thumbnails/" + ImageFileUpload.PostedFile.FileName);
                    //NameLabel.Text = ImageFileUpload.PostedFile.FileName;

                }
                else
                {
                    Page.Validators.Add(cv);
                }
            }
        }

        public IEnumerable<FileData> GalleryRepeater_GetData()
        {
            return Gal.GetImagesNames();          
        }
    }
}