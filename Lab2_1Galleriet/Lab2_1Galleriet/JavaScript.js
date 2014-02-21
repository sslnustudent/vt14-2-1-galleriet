"use strict";
var gallery = {


    init: function () {


        var div = document.getElementById('PictureDiv');
        var img = document.createElement('img');
        img.id = "imageView";
        img.setAttribute('src', '');
        div.appendChild(img);

        
        var elements = document.getElementsByTagName('a');
        
        for (var i = 0, len = elements.length; i < len; i++) {

            elements[i].setAttribute("href", "#");
            
            elements[i].onclick = function () {  gallery.click(elements[i].firstChild.getAttribute('src')) };
        }
    },

    click: function (src)
    {
        alert(src);
        var img = document.getElementById("imageView");
        img.setAttribute('src', "Images" + src.substring(10));
    }
};

window.onload = gallery.init;
