

    function DeleteData(id) {

        var Param = id;

        $.ajax({
        url: "/Designation/DeleteConfirmed",
            // type: "POST",
            data: {id: Param },
            traditional: true,
            //dataType: "Boolean",
            //contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            dataType: "text",
            contentType: "application/text; charset=utf-8",
            success: function (result) {
                if (result) {
        //  alert("Done");
        $('#myModaldelete').modal('show');
                } else {
        alert("Error occurs on the Database level!");
                }
            },
            error: function () {
        alert("An error has occured!!!");
            }
        });
    }
    $(function () {
        $('[name=deletepopup]').click(function () {

            $('#myModal').modal('show');
            var href = $(this).attr('href');
            var myStringArray = href.split('=');
            var Findid = myStringArray[1];
            var s = $('#popupid').attr('href', href);
            return false;
        });
        if (window.location.search == "?result=DeleteSuccess") {
        $('#myModaldelete').modal('show');
            $.gritter.add({
        title: 'Record Deleted',
                text: 'Record Deleted Successfully !!!',
                class_name: 'with-icon exclamation-circle warning'
            });


        }
    });

      $(document).ready(function () {
        $('.a').DataTable();

    });
