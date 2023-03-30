var MtgDiscovery = MtgDiscovery || {};
MtgDiscovery.Pages = MtgDiscovery.Pages || {};

MtgDiscovery.Pages.Search = MtgDiscovery.Pages.Search || (function () {
    let loopCallback = () => { };
    let apiCall = () => { };
    let preLoop = () => { };

    const resultsDiv = $("#searchResults");

    const artistItem = (item) => {
        $.each(item.names,
            (_, name) => {
                resultsDiv.append(
                    `<div class="mtg-search-item d-inline-flex text-uppercase">
                                                            <a class="mtg-search-item-link" href="/ArtistCards?artist=${name}">${name}&nbsp;(${item.card_count})</a>
                                </div>`);
            });
    };

    const cardItem = (item) => {
        resultsDiv.append(
            `<div class="mtg-search-item d-inline-flex text-uppercase">
                                <a class="mtg-search-item-link" href="/CardVersions?cardName=${item.card_name}">${item.card_name}&nbsp;(${item.card_count})</a>
                    </div>`);
    };

    const func = async () => {
        resultsDiv.empty();
        MtgDiscovery.Controls.Spinner.show();
        const searchTerm = MtgDiscovery.Controls.SearchBox.entered();
        const results = await apiCall(searchTerm);

        if (MtgDiscovery.Controls.SearchBox.entered() !== searchTerm) { return; }

        resultsDiv.empty();

        if (results.value.length === 0) {
            resultsDiv.append("<div class='mtg-search-item d-inline-flex text-uppercase'>NO RESULTS</div>");
            MtgDiscovery.Controls.Spinner.hide();
            return;
        }

        preLoop(results);
        $.each(results.value, (_, item) => { loopCallback(item); });
        MtgDiscovery.Controls.Spinner.hide();
    }

    const artistSearch = () => {
        loopCallback = artistItem;
        apiCall = MtgDiscovery.Api.data.search.artist;
        preLoop = (results) => results.value.sort((a, b) => (a.names[0] > b.names[0]) ? 1 : -1);
        return debounce(func, 250, false);
    };

    const cardSearch = () => {
        loopCallback = cardItem;
        apiCall = MtgDiscovery.Api.data.search.card;
        return debounce(func, 250, false);
    };

    return {
        artistCallback: artistSearch,
        cardCallback: cardSearch
    };
})();