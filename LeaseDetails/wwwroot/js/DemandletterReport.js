
$("#btnGenerate").click(function () {
    //var id = $("#ApplicationId").val();
 
    
    var id = $("#ddlRefNo").children("option:selected").val(); 
   
    
   

    if (id) {

        var today = new Date();
        var dd = String(today.getDate()).padStart(2, '0');
        var mm = String(today.getMonth() + 1).padStart(2, '0');
        var yyyy = today.getFullYear();

        var LetterDate = dd + '-' + mm + '-' + yyyy;


      
        var param = {


            applicationid: id,
            demanddate: LetterDate
          

        }
        HttpPost(`/DemandLetter/List`, 'html', param, function (response) {

            alert(response);
           $('#Vieww').html("");
            $('#Vieww').html(response);
         

        });

   }
})

















