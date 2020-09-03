

    //For dropdown change event
    $(document).ready(function () {
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




