$(function () {
    $("input[name='grpLand']").click(function () {
       
        if ($("#A").is(":checked")) {
            $('#StatusOfLand').val('Alloted');
        }
       else if ($("#V").is(":checked")) {
            $('#StatusOfLand').val('Vacant');

        } else if ($("#E").is(":checked")) {
            $('#StatusOfLand').val('Enchroched');
        } 
        else if ($("#G").is(":checked")) {
            $('#StatusOfLand').val('Green');
        } 
            
        else {
            $('#StatusOfLand').val('Green');
        }
    });
});
function onChange(id) {
 
    HttpGet(`/bookTransferLand/GetKhasraList/?LocalityId=${id}`, 'json', function (response) {
        debugger
        
        var html = '';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }

        // $("#KhasraId").select2('val', '')
        $("#KhasraId").html(html);
    });
};

$("input[name='grpLand']").click(function () {
    var selected = $("input[type='radio'][name='grpLand']:checked");
    $("#StatusOfLand").val(selected.val());

});
