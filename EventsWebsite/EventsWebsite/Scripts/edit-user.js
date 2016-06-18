$(document).ready(function() {
    $("a").click(function() {
        var clicked_id = $(this).attr("id");
        $("p." + clicked_id).replaceWith('<input type="text" class="form-control" id="' + clicked_id + '">');
        $("i." + clicked_id).replaceWith('<i class="glyphicon glyphicon-check""></i></a></span> ');
    });   
});