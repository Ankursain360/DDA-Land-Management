
    $(document).ready(function () {

        if ($("#rRoleWise").is(":checked")) {
        $("#DivForRoleWise").show();
        } else {
        $("#DivForRoleWise").hide();
        }

    });
    $(function () {
        $("input[name='grpRoleFor']").click(function () {
            if ($("#rRoleWise").is(":checked")) {
                $("#DivForRoleWise").show();
                $("#DivforUserWise").hide();
            } else {
                $("#DivforUserWise").show();
                $("#DivForRoleWise").hide();
            }
        });
    });
    $(document).ready(function () {
        $('.a').DataTable();
    });
