var MtgDiscovery = MtgDiscovery || {};
MtgDiscovery.Pages = MtgDiscovery.Pages || {};

MtgDiscovery.Pages.FullSet = MtgDiscovery.Pages.FullSet || (function () {
    const loadSet = async (setCode, collector) => {
        MtgDiscovery.Containers.Sets.addOne(setCode, collector);
    };
    const loadCards = async (setCode) => {
        const result = await MtgDiscovery.Api.data.card.setCardsSummary({ code: setCode }, MtgDiscovery.Url.collector());
        const adding = MtgDiscovery.Containers.Cards.addAll(result.value,
            () => {
                $(".mtg-card-set").addClass("itemHidden");
            });
        await Promise.all([adding]);

        MtgDiscovery.Controls.Spinner.hide();
    };

    return {
        loadSet: loadSet,
        loadCards: loadCards
    };
})();