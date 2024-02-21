// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

function street_searcher(min_search_pattern) {
    var pat = $('#searcher').val();
    if (pat.length >= min_search_pattern) {
        $('#results').empty();
        $.getJSON('/api/StreetRecord/Search', { pattern: pat }).done(
            function (results) {
                $.each(
                    results, function (i, street) {
                        var num = i + 1;
                        var item = `<li>${num}. <strong>${street.name}</strong></li>`;
                        $('#results').append(item);
                    }
                );
            }
        );
    } else if (pat.length == 0) {
        $('#results').empty();
    }
}