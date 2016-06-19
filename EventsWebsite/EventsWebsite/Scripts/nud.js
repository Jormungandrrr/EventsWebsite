$(document).ready(function() {
    $("#fg1").hide();
    $("#fg2").hide();
    $("#fg3").hide();
    $("#fg4").hide();
    $("#fg5").hide();
    $("#fg6").hide();
    $("#buttongetnum").click(function () {
        var amount = $("#numinput").val();
        for (i = 0; i < amount; i++) {
            $("#fg" + i).show();
        }
    });
});
