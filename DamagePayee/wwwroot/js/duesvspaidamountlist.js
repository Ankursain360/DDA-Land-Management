$(document).ready(function () {

    $("#btnGenerate").click(function () {



        var fromDate = $('#FromDateMsg').val();
        var toDate = $('#ToDateMsg').val();
        var Fileno = $('#FileNo').val();
        if (fromDate == "" || toDate == "" || Fileno == "") {
            alert("Please Fill All Fields");
        } else {
            var param = GetSearchParam();
          //  alert(JSON.stringify(param));

            HttpPost(`/DuesVsPaidAmountReport/GetPaidVsDemandList`, 'html', param, function (response) {
                $('#LoadDuesVsPaidAmount').html("");
                $('#LoadDuesVsPaidAmount').html(response);
            });
        }
        });
        

});


function GetDetails() {
    var param = GetSearchParam();
   
        HttpPost(`/LoadDuesVsPaidAmount/GetPaidVsDemandList`, 'html', param, function (response) {
            $('#LoadDuesVsPaidAmount').html("");
            $('#LoadDuesVsPaidAmount').html(response);
        });
   
}

function GetSearchParam() {

  

    var model = {
      
        fileno: $('#FileNo option:selected').val(),
        fromdate: $('#FromDateMsg').val(),
        todate: $('#ToDateMsg').val()
       
    }

    return model;
}
