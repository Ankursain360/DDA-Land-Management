$(document).ready(function () {

    var allotmentid = parseInt($("#AllotmentId option:selected").val());

    if (allotmentid > 0) {
        HttpGet(`/Leasedeed/ApplicationDetails/?Id=${allotmentid}`, 'html', function (response) {
            $('#View').html("");
            $('#View').html(response);

        });

    }
});


$("#AllotmentId").change(function () {
    var id = $(this).val();

    if (id) {

        HttpGet(`/Leasedeed/ApplicationDetails/?Id=${id}`, 'html', function (response) {
            $('#View').html("");
            $('#View').html(response);

        });

    }
});