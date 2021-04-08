$(document).ready(function () {
    var RefNumber = sessionStorage.getItem("Vid");
    //if (RefNumber) {
    //    HttpGet(`/LetterofAllotment/Receipt/?ApplicationId=${RefNumber}`, 'html', function (response) {
    //        debugger
    //        $('#View').html("");
    //        $('#View').html(response);

    //    });
});


$(function () {
    $("#btnPrint").click(function () {
        $('.jhide').hide();
        nWin($("#View").html(), $("#pagename").html());
        $('.jhide').show();
    });
});
function nWin(context, title) {
    var printWindow = window.open('', '');
    var doc = printWindow.document;
    var printContents = document.getElementById("View").innerHTML;
    doc.write(document.getElementById("View").innerHTML);
    //doc.write(printContents);
    doc.close();
    function show() {
        if (doc.readyState === "complete") {
            printWindow.focus();
            printWindow.print();
            printWindow.close();
        } else {
            setTimeout(show, 100);
        }
    };
    show();
};