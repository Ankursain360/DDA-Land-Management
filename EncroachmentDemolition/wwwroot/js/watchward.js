

//For dropdown change event
$(document).ready(function () {
    GetLocalityList();

    $("#drpKhasra").change(function () {
        var courses = $("#txtArea");
        var semesterName = $("#drpKhasra option:selected").text();
        $.getJSON('/EncroachmentRegister/GetAreaFromKhasra', { semester: semesterName }, function (response) {
            console.log(response);
            var courseField = "";
            $.each(response, function (key, value) {
                if (value == null) {
                    $("#txtArea").empty();
                } else {
                    courseField += "<div class=\"input-field col s12 l12\">\n" +
                        "                        <input id=\"" + value.CourseId + "\" type=\"text\" value=\"" + value.CourseCode + "\">\n" +
                        "                        <label class=\"active\" for=\"course\">" + value.CourseCode + "</label>\n" +
                        "                    </div>";
                }
            });
            courses.html(courseField);
        })
    });
});

function GetLocalityList() {
    HttpGet(`/WatchWard/GetLocalityList/`, 'json', function (response) {
        var html = '<option value="">---Select---</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#LocalityId").val('').trigger('change');
        $("#LocalityId").html(html);
    });
}

function GetOtherDetails(id) {
    
    //HttpGet(`/WatchWard/GetOtherDetails/?propertyId=${id}`, 'json', function (response) {
        debugger;
        //$('#PlannedUnplanedDiv').show();
        //var value = response.plannedUnplannedLand;
        //if (value == 'Planned Land') {
        //  //  $('#DivLandUse').show();
        //    $('#divPlannedSelection').show();
        //  //  $("#divLayoutPlan").show();
        //    $('#divUnplannedSelection').hide();
        //    callSelect2();

        //    $("#PlannedUnplannedLand").val(response.plannedUnplannedLand);
        //    $("#PlannedUnplannedLand").trigger('change');
        //    $('#Colony').val(response.colony);
        //    $('#Sector').val(response.sector);
        //    $('#Block').val(response.block);
        //    $('#Pocket').val(response.pocket);
        //    $('#PlotNo').val(response.plotNo);
        //}
        //else {
        // //   $('#DivLandUse').hide();
        //    $('#divPlannedSelection').hide();
        // //   $("#divLayoutPlan").hide();
        //    $('#divUnplannedSelection').show();
        //    callSelect2();

        //  //  $(".link").attr('href', '/WatchWard/View/' + response.id);
        //    $("#PlannedUnplannedLand").val(response.plannedUnplannedLand);
        //    $("#PlannedUnplannedLand").trigger('change');
        //    $("#LocalityId").val(response.localityId);
        //    $("#LocalityId").trigger('change');
        //    $('#KhasraNo').val(response.khasraNo);
        //}

        HttpGet(`/WatchWard/InventoryView/?Id=${id}`, 'html', function (response) {
            $('#InventoryOtherDiv').html("");
            $('#InventoryOtherDiv').html(response);
        });
    //});
};
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
    $('#txtDate').attr('max', maxDate);
    $('#txtDateRep').attr('max', maxDate);
});



$('#Photo').change(function () {
    var fileInput = document.getElementById('Photo');
    var filePath = fileInput.value;
    const size = (Photo.files[0].size);
    fileValidation(filePath, fileInput, size);
});

function fileValidation(filePath, fileInput, size) {
    var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif)$/i;
    if (!allowedExtensions.exec(filePath)) {
        alert('Invalid file type');
        fileInput.value = '';
        return false;
    }
    else if (size > 10535049) {
        alert("File must be of 10 MB or Lesser Than 10 MB");
        fileInput.value = '';
        return false;
    }
    //else {
    //    HttpGet(`/WatchWard/GetLattLongDetails/?path=${filePath}`, 'string', function (response) {
    //        $("#Longitude").val(response);
    //        $("#Longitude").val();
    //    });
    //}

}

function callSelect2() {
    $("select").select2({
        placeholder: "Select",
        allowClear: true
    });
}

$("#collapse").click(function () {
    $('#collapseExample').collapse("toggle").promise().done(function () {
        $("select").select2({
            placeholder: "Select",
            allowClear: true
        });
    })
});


$("input[name='grpEncroachment']").click(function () {
    var selected = $("input[type='radio'][name='grpEncroachment']:checked");
    $("#Encroachment").val(selected.val());

});