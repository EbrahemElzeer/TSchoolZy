window.initNavExpander = () => {
    setTimeout(() => {
        var navexpander = $('#nav-expander');
        if (navexpander.length) {
            $('#nav-expander, #nav-close, #nav-close2, .offwrap').on('click', function (e) {
                e.preventDefault();
                $('body').toggleClass('nav-expanded');
            });
        }
    }, 500);
};
