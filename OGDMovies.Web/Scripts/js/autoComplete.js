var requestHeaders = {
    "accept": "application/json;odata=verbose"
}
$(document).ready(function () {
    $("#search").autocomplete({
        source: function (request, response) {
            $.ajax({
                dataType: 'json',
                async: true,
                headers: requestHeaders,
                url: '@Url.Action("AutoCompleteMovieTitles", "Home")',
                data: {
                    query: request.term
                },
                success: function (data) {
                    response(data);
                }
            });
        },
        minLength: 3
    });
});
