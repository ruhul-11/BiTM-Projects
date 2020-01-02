
$(document).ready(function () {

    $("#stockInForm").validate({
        rules: {
            reorderLevel: {
                required: true
            }
        },
        messages: {
            reorderLevel: {
                required: "Rewuired"
            }
        }
    });

    //Sticky Menu
    //$(function () {
    //    var $dis = $(".fullnav").offset().top;

    //    $(window).scroll(function () {

    //        var $scrolling = $(this).scrollTop();

    //        if ($scrolling > $dis) {
    //            $(".custom-h2").addClass("sticky");
    //        }
    //        else {
    //            $(".custom-h2").removeClass("sticky");
    //        }
    //    })
    //});
});

