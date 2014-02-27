<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Lab2_1Galleriet.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Style.css" rel="stylesheet" />
</head>
<body>
    <h1>Galleriet</h1>
    <form id="form1" runat="server">
    <div id="OkDiv" runat="server" visible="false">
        <asp:Label ID="LabelOk" runat="server" Text="Label"></asp:Label>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='~/Default.aspx'><asp:Image ImageUrl="~/DeleteRed.png" Width="20px" ID="Delete" runat="server" /></asp:HyperLink>
    </div>
    <div id="Gallery">
        <div id="PictureDiv">

            <asp:Image ID="ShowImage" ImageUrl="~/Images/Cat5.jpg" runat="server" />

        </div>
        <div id="Thumbnails">

            <asp:Repeater ID="GalleryRepeater" runat="server" ItemType="Lab2_1Galleriet.FileData" SelectMethod="GalleryRepeater_GetData">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "~/Default.aspx?name=" + Item.Name %>'><asp:Image ImageUrl="<%#Item.Name%>" Width="70px" ID="LOL" runat="server" /></asp:HyperLink>
                    
                </ItemTemplate>
                
            </asp:Repeater>

        </div>
    </div>
        
    <div id ="Upploads">
        <fieldset>
            <legend>Ladda upp bild</legend>
        
        <asp:FileUpload ID="ImageFileUpload" runat="server" />
       
        <asp:RequiredFieldValidator ControlToValidate="ImageFileUpload" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Ange en bild" Display="None"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ControlToValidate="ImageFileUpload" ID="RegularExpressionValidator1" runat="server" ErrorMessage="Måste Vara JPG, GIF eller PNG" ValidationExpression="(.*?)\.(jpg|png|gif)" Display="None"></asp:RegularExpressionValidator>
        <asp:Image ID="Image1" runat="server" />
        <asp:Button ID="OkButton" runat="server" Text="Ladda upp" OnClick="OkButton_Click" />
        <asp:HiddenField ID="CheckInput" runat="server" />
        <asp:Label ID="Label1"  runat="server" Text=""></asp:Label>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        </fieldset>
    </div>
    </form>

<%--    <script src="JavaScript.js"></script>--%>
</body>
</html>
