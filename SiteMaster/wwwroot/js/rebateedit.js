$(document).ready(function () {
	if (ViewBag.Radiobutton == 0) {
		$("#rbdPricipal").prop('checked', true);
		$("#rbdInterest").prop('checked', false);
		$("#rbdBoth").prop('checked', false);	
	}
	else if (ViewBag.Radiobutton == 1) {
		$("#rbdPricipal").prop('checked', false);
		$("#rbdInterest").prop('checked', true);
		$("#rbdBoth").prop('checked', false);	
	}
	else if (ViewBag.Radiobutton == 2) {
		$("#rbdPricipal").prop('checked', false);
		$("#rbdInterest").prop('checked', true);
		$("#rbdBoth").prop('checked', false);	
	}
});
