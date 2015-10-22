
$.wait = function (callback, seconds) {
    return window.setTimeout(callback, seconds * 1000);
}
//$(document).ready(function () {
//    $(document).ajaxStart(function () {
//        $("#loading-image").css("display", "block");
//    });
//    $(document).ajaxComplete(function () {
//        $("#loading-image").css("display", "none");
//    });
//});

//$(document).on({
//    ajaxStart: function () { $("#loading-image").show() },
//    ajaxStop: function () { $("#loading-image").hide() }
//});