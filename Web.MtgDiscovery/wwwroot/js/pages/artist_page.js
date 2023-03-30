var MtgDiscovery = MtgDiscovery || {};
MtgDiscovery.Pages = MtgDiscovery.Pages || {};

MtgDiscovery.Pages.Artist = MtgDiscovery.Pages.Artist || (function () {
    const loadCards = async (artist) => {
        const result = await MtgDiscovery.Api.data.card.artistCardsSummary(artist);
        const adding = MtgDiscovery.Containers.Cards.addAll(result.value,
            () => {
                $(".mtg-card-artist").addClass("itemHidden");
                $(".mtg-card-release-date").removeClass("itemHidden");
                MtgDiscovery.Controls.Sort.Core.show.releaseDate();
                const artistNames = [];
                $("span.mtg-card-artist").each((i, e) => artistNames.push(e.textContent));
                const challenger = mode(artistNames);
                const artistElement = $("#artist");
                const champion = artistElement.html();
                if (!challenger.includes(champion)) {
                    artistElement.html(challenger);
                }
                MtgDiscovery.Pages.Artist.collectorDisplay();
            });
        MtgDiscovery.Controls.Spinner.hide();
        await Promise.all([adding]);
    };

    function mode(arr) {
        return arr.sort((a, b) =>
            arr.filter(v => v === a).length
            - arr.filter(v => v === b).length
        ).pop();
    }

    const totalCollectorDisplay = () => {
        //Set 
        const cardCount = MtgDiscovery.Containers.Cards.totalCards();
        const uniqueCards = MtgDiscovery.Containers.Cards.totalUniqueCard();
        $("#collected-artist-count").html(uniqueCards.toLocaleString());

        const artistCardsPercent = parseInt(uniqueCards / cardCount*100);
        $("#collected-artist-percent").html(artistCardsPercent.toLocaleString());
        $(".mtg-artist-collection-progress-bar").css("width", artistCardsPercent);

        //show it
        $(".mtg-artist-collector-container").removeClass("itemHidden");
    };

    const collectorInfo = () => {
        if (MtgDiscovery.Url.noCollector()) return;
        totalCollectorDisplay();
    };

    return {
        loadCards: loadCards,
        collectorDisplay: collectorInfo
    };
})();