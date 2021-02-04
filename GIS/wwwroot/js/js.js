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

$('.MenuToggle').on('click', function (e) {
    $('#leftSection').toggleClass('goleft');
});



//$('#MapOption').on('click', function () {
//    if ($(this).hasClass('show')) {
//        $('.actionBtn').removeClass('show');
//    } else {
//        $('.actionBtn').removeClass('show');
//        $(this).toggleClass('show');
//    }
//});



//
//$('#BackToLogin').on('click', function () {
//    $('#Login').show()
//    $('#Password').hide()
//})
//
//$('#mobileMenu').on('click', function () {
//    $('#leftMenu').toggleClass('small');
//    $('#mobileMenu').toggleClass('open');
//    $('#rightSection').toggleClass('menu-close');
//    $('footer').toggleClass('menu-close');
//});
//
//$('.short-btn .btn').on('click', function () {
//    $('.short-btn .btn').removeClass('active');
//    $(this).addClass('active');
//})
