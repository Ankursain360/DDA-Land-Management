function onChange(id) {
    $.ajax({
        type: 'post',
        url: '/Locality/GetZoneList',
        data: { DepartmentId: id },
        dataType: 'json',
        success: function (data) {
            var html = '<option value="">Select</option>';
            for (var i = 0; i < data.length; i++) {
                html = html + '<option value=' + data[i].id + '>' + data[i].name + '</option>';
            }
            $("#ZoneId").html(html);
        }
    });
};