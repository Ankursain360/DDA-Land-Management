
    $(document).ready(function () {
        var value = document.getElementById('loginname').value;
        if (value === '') {
        $("#loginname").focus();
        }

    });

    $('#submit').click(function () {
        var value = document.getElementById('loginname').value;
        if (value === '') {
        //alert('Enter your name');
        $("#loginname").focus();
            //$("#error").show('slow');
            //$("p").html("focus event triggered");
        }
    })


