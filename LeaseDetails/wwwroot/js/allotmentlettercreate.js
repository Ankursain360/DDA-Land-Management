$(document).ready(function () {
  // var LetterDate = sessionStorage.getItem("NewDate");
   var RefNumber = sessionStorage.getItem("RefN");
  
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0');
    var yyyy = today.getFullYear();

    var LetterDate = dd + '-' + mm + '-' + yyyy;
    $("#txtNewDate").val(LetterDate);
    $("#txtRefN").val(RefNumber);
    $("#txtRe").val(RefNumber);
});
//$(function () {
//    $("#btnPrint").click(function () {
//        window.print();
//    });
//});

$(function () {
    $("#btnPrint").click(function () {
        $('.jhide').hide();
        print.window();
     //   nWin($("#View").html(), $("#pagename").html());
    //    $('.jhide').show();
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