$("input[name='villageradio']").click(function () {
    var selected = $("input[type='radio'][name='villageradio']:checked");
    $("#AppealByDept").val(selected.val());

});