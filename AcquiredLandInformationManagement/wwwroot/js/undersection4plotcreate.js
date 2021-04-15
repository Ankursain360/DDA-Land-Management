function onChange(id) {

    HttpGet(`/UnderSection4PlotForm/GetKhasraList/?villageId=${id}`, 'json', function (response) {
        var html = '<option value=""> select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }

       // $("#KhasraId").select2('val', '')
        $("#KhasraId").html(html);
    });
};
$("#KhasraId").change(function () {
    var kid = $(this).val();
    if (kid) {
        HttpGet(`/UnderSection4PlotForm/GetAreaList/?khasraid=${kid}`, 'json', function (response) {

            $("#Bigha1").val(response.bigha);
            $("#Biswa1").val(response.biswa);
            $("#Biswanshi1").val(response.biswanshi);

            $("#Bigha").val(response.bigha);
            $("#Biswa").val(response.biswa);
            $("#Biswanshi").val(response.biswanshi);
           // alert(JSON.stringify(response));
        });

    }
});

$("#Bigha").keyup(function () {
    var Bigha_value = $(this).val();
    var Bigha1_value = $("#Bigha1").val();
    if (Bigha_value > Bigha1_value) {
        alert("Enter the correct value");
        $(this).val('');
        return false;
    }
   
});
$("#Biswa").keyup(function () {
    var Biswa_value = $(this).val();
    var Biswa1_value = $("#Biswa1").val();
    if (Biswa_value > Biswa1_value) {
        alert("Enter the correct value");
        $(this).val('');
        return false;
    }

});

$("#Biswanshi").keyup(function () {
    var Biswanshi_value = $(this).val();
    var Biswanshi1_value = $("#Biswanshi1").val();
    if (Biswanshi_value > Biswanshi1_value) {
        alert("Area is not greater than actual area");
        $(this).val('');
        return false;
    }

});