$(document).ready(function () {
    // var LetterDate = sessionStorage.getItem("NewDate");
   // var RefNumber = sessionStorage.getItem("RefN");

    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0');
    var yyyy = today.getFullYear();

    var LetterDate = dd + '-' + mm + '-' + yyyy;
    $("#txtNewDate").html(LetterDate);
  
});