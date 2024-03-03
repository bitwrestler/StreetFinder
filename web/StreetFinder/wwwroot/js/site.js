function formatToolTip(streetObj) {
    var z = streetObj.zipCodeRange[0];
    if (streetObj.zipCodeRange.length > 1)
        z = z + "-" + streetObj.zipCodeRange[1];
    return "zipcode: " + z;
}

function getSearchType() {
    return $('input[name="search-type-radio"]:checked').val();
}

function handleMap(streetID, streetName) {
    $.getJSON('/api/StreetRecord/Coordinates', { id: streetID }).done(
        function (results) {
            if (results.length > 0) {
                var url = `https://maps.google.com/maps?z=16&t=m&hl=en&output=embed&q=loc:${results[0]}`;
                $('#mapContainer').show();
                $('#mapCaption').text(streetName);
                $('#map').attr('src', url);
            }
        }
    );
}

function formatMapLink(streetObj) {
    return `<IMG CLASS="clickable" SRC="icons8-map-48.png" width="16" height="16" alt="Open Map" ONCLICK="handleMap(${streetObj.id}, '${streetObj.name}');">`;
}

function street_searcher(min_search_pattern) {
    var pat = $('#searcher').val();
    var st = getSearchType();
    if (st == "Phonetic") { $('#wildcardLegend').hide(); } else { $('#wildcardLegend').show(); }
    if (pat.length >= min_search_pattern) {
        $('#results').empty();
        $.getJSON('/api/StreetRecord/Search', { pattern: pat, searchType: st }).done(
            function (results) {
                $.each(
                    results, function (i, street) {
                        var num = i + 1;
                        var item = `<li title="${formatToolTip(street)}">${num}. ${formatMapLink(street)} <strong>${street.name}</strong></li>`;
                        $('#results').append(item);
                }
                );
            }
        );
    } else if (pat.length == 0) {
        $('#results').empty();
    }
}