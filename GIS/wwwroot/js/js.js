$('#TransparencyRange, #MapOption').on('click', function () {
    if ($(this).hasClass('show')) {
        $('.actionBtn').removeClass('show');
    } else {
        $('.actionBtn').removeClass('show');
        $(this).toggleClass('show');
    }

});

$('#LeftNavTab a').on('click', function (e) {
    e.preventDefault()
    $(this).tab('show')
});

$('.MenuToggle').on('click', function () {
    $('#leftSection').toggleClass('goleft');
});
$('#RouteDetail').on('click', function () {
    $('#RouteDetailShow').show();
});
$('#HideRouteDetail').on('click', function () {
    $('#RouteDetailShow').hide();
});
