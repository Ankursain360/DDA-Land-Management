



$(function () {
    var dtToday = new Date();

    var month = dtToday.getMonth() + 1;
    var day = dtToday.getDate();
    var year = dtToday.getFullYear();
    if (month < 10)
        month = '0' + month.toString();
    if (day < 10)
        day = '0' + day.toString();

    var maxDate = year + '-' + month + '-' + day;
    //alert(maxDate);
    $('#FromDate').attr('max', maxDate);
    $('ToDate').attr('max', maxDate);
});


//$(document).ready(function () {
//	if (ViewBag.Radiobutton == 0) {
//		$("#rbdPricipal").prop('checked', true);
//		$("#rbdInterest").prop('checked', false);
//		$("#rbdBoth").prop('checked', false);	
//	}
//	else if (ViewBag.Radiobutton == 1) {
//		$("#rbdPricipal").prop('checked', false);
//		$("#rbdInterest").prop('checked', true);
//		$("#rbdBoth").prop('checked', false);	
//	}
//	else if (ViewBag.Radiobutton == 2) {
//		$("#rbdPricipal").prop('checked', false);
//		$("#rbdInterest").prop('checked', true);
//		$("#rbdBoth").prop('checked', false);	
//	}
//});
