$(document).ready(function () {
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    var todayN = yyyy + '-' + mm + '-' + dd;
    $("#txttodayDate").val(todayN);
    var HearingDate = sessionStorage.getItem("NewDate");
    var HearingTime = sessionStorage.getItem("RefN");

    $("#txtNewDate").val(HearingDate);
    $("#txtRefN").val(HearingTime);
   
});
$(function () {
    $("#btnPrint").click(function () {
        window.print();
    });
});