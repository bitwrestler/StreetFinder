function formatToolTip(streetObj) {
    var z = streetObj.zipCodeRange[0];
    if (streetObj.zipCodeRange.length > 1)
        z = z + "-" + streetObj.zipCodeRange[1];
    return "zipcode: " + z;
}

function street_searcher(min_search_pattern) {
    var pat = $('#searcher').val();
    if (pat.length >= min_search_pattern) {
        $('#results').empty();
        $.getJSON('/api/StreetRecord/Search', { pattern: pat }).done(
            function (results) {
                $.each(
                    results, function (i, street) {
                        var num = i + 1;
                        var item = `<li title="${formatToolTip(street)}">${num}. <strong>${street.name}</strong></li>`;
                        $('#results').append(item);
                }
                );
            }
        );
    } else if (pat.length == 0) {
        $('#results').empty();
    }
}