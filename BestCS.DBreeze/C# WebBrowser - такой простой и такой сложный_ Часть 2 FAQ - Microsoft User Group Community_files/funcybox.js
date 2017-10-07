$(document).ready(function () {
    $('#examples a').each(function (index) {
        $(this).fancybox({
            'width': '85%',
            'height': '85%',
            'autoScale': true,
            'transitionIn': 'none',
            'transitionOut': 'none',
            'type': 'iframe'
        });
    });
});