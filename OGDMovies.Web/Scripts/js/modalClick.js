function onModalClick(json, link) {
    var obj = jQuery.parseJSON(json);
    $("#ModalHeader").text(obj.Title);
    $("#ModalBody").text(obj.Description);

    //Type
    //1 = Movie
    //2 = Youtube
    if (obj.Type == 2) {
        $("#ReleaseDateRow").hide();
        $("#RatingRow").hide();
        $("#ModalVideo").attr("src", link);
        $("#ModalVideo").show();
        $("#ModalImage").attr("src", "");
        $("#ModalImage").hide();
    } else {
        $("#ModalImage").attr("src", link);
        $("#ModalImage").show();
        $("#ModalVideo").attr("src", "");
        $("#ModalVideo").hide();
        $("#ReleaseDateRow").show();
        $("#RatingRow").show();
        $("#ModalReleaseDate").text(obj.ReleaseDate);
        $("#ModalRating").text(obj.Vote);
    }

    
}
