

$("#AllotmentId").change(function () {
    var id = $(this).val();

    if (id) {

        HttpGet(`/Leasedeed/ApplicationDetails/?Id=${id}`, 'html', function (response) {
            $('#View').html("");
            $('#View').html(response);

        });

    }
});