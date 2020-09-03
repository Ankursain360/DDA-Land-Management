


    $(function () {
        $("input[name='search']").click(function () {
            if ($("#searchname").is(":checked")) {
                $("#divname").show();
                $("#divfile").hide();
                $("#DivForGenerate").show();
            }
            if ($("#searchno").is(":checked")) {
                $("#divfile").show();
                $("#divname").hide();
                $("#DivForGenerate").show();

            }
        });
    });
