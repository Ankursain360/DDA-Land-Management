


$("#ApplicationId").change(function () {
    var id = $(this).val();
    if (id) {
        HttpGet(`/CalculationSheet/GetApplicationAreaDetails/?ApplicationId=${id}`, 'json', function (response) {

            $("#AllotedArea").val(response.allotedArea);
            $("#BuildingArea").val(response.buildingArea);
            $("#PlayGroundArea").val(response.playGroundArea);

        });

    }
});