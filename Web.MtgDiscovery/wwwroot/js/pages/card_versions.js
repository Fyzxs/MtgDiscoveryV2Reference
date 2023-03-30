var MtgDiscovery = MtgDiscovery || {};
MtgDiscovery.Pages = MtgDiscovery.Pages || {};

MtgDiscovery.Pages.CardVersions = MtgDiscovery.Pages.CardVersions || (function () {
    const totalCollectorDisplay = () => {
        //Set 
        const cardCount = MtgDiscovery.Containers.Cards.totalCards();
        const uniqueCards = MtgDiscovery.Containers.Cards.totalUniqueCard();
        $("#collected-card-name-count").html(uniqueCards.toLocaleString());

        const cardVersionPercent = parseInt(uniqueCards / cardCount * 100);
        $("#collected-card-name-percent").html(cardVersionPercent.toLocaleString());
        $(".mtg-card-name-collection-progress-bar").css("width", cardVersionPercent);

        //show it
        $(".mtg-card-name-collector-container").removeClass("itemHidden");
    };

    const collectorInfo = () => {
        if (MtgDiscovery.Url.noCollector()) return;
        totalCollectorDisplay();
    };

    const loadCards = async (cardName) => {
        const result = await MtgDiscovery.Api.data.card.cardVersionsSummary(cardName);
        const adding = MtgDiscovery.Containers.Cards.addAll(result.value,
            () => {
                $(".mtg-card-name").addClass("itemHidden");
                $(".mtg-card-release-date").removeClass("itemHidden");
                MtgDiscovery.Controls.Sort.Core.show.releaseDate();
                collectorInfo();
            });
        await Promise.all([adding]);

        MtgDiscovery.Controls.Spinner.hide();
    };

    return {
        loadCards: loadCards
    };
})();