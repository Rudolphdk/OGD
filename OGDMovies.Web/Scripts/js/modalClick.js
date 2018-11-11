function onModalClick(json, link) {
    var obj = jQuery.parseJSON(json);
    $("#ModalHeader").text(obj.Title);
    $("#ModalBody").text(obj.Description);
    if (obj.Type == 2) {
        $("#ModalVideo").attr("src", link);
        $("#ModalVideo").show();
        $("#ModalImage").attr("src", "");
        $("#ModalImage").hide();
    } else {
        $("#ModalImage").attr("src", link);
        $("#ModalImage").show();
        $("#ModalVideo").attr("src", "");
        $("#ModalVideo").hide();
    }

    
}
