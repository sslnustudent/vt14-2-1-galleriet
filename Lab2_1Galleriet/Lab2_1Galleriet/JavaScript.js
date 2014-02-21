"use strict";
var gallery = {


    init: function () {
        var btn = document.getElementById("Button1");
        btn.onclick = gallery.click;
    },

    click: function ()
    {
        var div = document.getElementById("OkDiv");
    }
};

window.onload = gallery.init;
