$('#ForgotPassword').on('click', function () {
    $('#Login').hide()
    $('#Password').show()
});

$('#BackToLogin').on('click', function () {
    $('#Login').show()
    $('#Password').hide()
})

$('#mobileMenu').on('click', function () {
    $('#leftMenu').toggleClass('small');
    $('#mobileMenu').toggleClass('open');
    $('#rightSection').toggleClass('menu-close');
    $('footer').toggleClass('menu-close');
});

$('.short-btn .btn').on('click', function () {
    $('.short-btn .btn').removeClass('active');
    $(this).addClass('active');
})
