﻿
$(function () {
    $("#btnPrint").click(function () {
        $('.jhide').hide();
        window.print();
        $('.jhide').show();
    });
});