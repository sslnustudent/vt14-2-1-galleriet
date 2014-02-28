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
            if (Session["a"] != null)
            {
                var name = Request.QueryString["name"];
                name = name.Replace("Thumbnails/", "");
                LabelOk.Text = "Bilden " + name + " har blivit uppladdad!!!";
                OkDiv.Visible = true;
                HyperLink1.NavigateUrl = "~/Default.aspx?name=Thumbnails/" + name;
                Session.Remove("a");
            }

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
                try
                {
                    var name = Gal.SaveImage(ImageFileUpload.PostedFile.InputStream, ImageFileUpload.PostedFile.FileName);
                    Session["a"] = true;
                    Response.Redirect("~/Default.aspx?name=Thumbnails/" + name);
                }
                catch (Exception)
                {
                    CustomValidator cv = new CustomValidator();
                    cv.ErrorMessage = "Uppladningen misslyckades";
                    cv.IsValid = false;
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