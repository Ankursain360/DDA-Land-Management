$(document).ready(function () {
   var LetterDate = sessionStorage.getItem("NewDate");
   var RefNumber = sessionStorage.getItem("RefN");

    $("#txtNewDate").val(LetterDate);
    $("#txtRefN").val(RefNumber);
    $("#txtRe").val(RefNumber);
});
$(function () {
    $("#btnPrint").click(function () {
        window.print();
    });
});