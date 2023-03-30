
MtgDiscovery.Controls.Filter.Cards = MtgDiscovery.Controls.Filter.Cards || (function () {
    function x(e) {
        const $e = $(e);
        const a = {};
        a.name = $e.data("name");
        a.collectorNumber = $e.data("collector-number");
        a.rarity = $e.data("rarity");
        a.totalCount = $e.data("total-count");
        return a;
    }
    const cardTextFilter = function (set) {
        const enteredText = MtgDiscovery.Controls.SearchBox.entered();
        if (!enteredText) return false;

        const compareText = enteredText.toLowerCase();
        const nameDoesNotContain = !set.name.toLowerCase().includes(compareText);
        return nameDoesNotContain;
    };
    const cardRarityFilter = function (card) {
        return MtgDiscovery.Controls.Filter.Rarity.Core.isFiltered(card);
    };
    const cardQuantityFilter = function (card) {
        return MtgDiscovery.Controls.Filter.Quantity.Core.isFiltered(card);
    };

    const isItemFiltered = function (srcItem) {
        const item = x(srcItem);
        return cardTextFilter(item) || cardRarityFilter(item) || cardQuantityFilter(item);
    };

    const apply = () => {
        MtgDiscovery.Controls.Filter.Core.apply(isItemFiltered, MtgDiscovery.Containers.Cards.container);

        MtgDiscovery.Controls.Filter.Core.show.rarity();
        MtgDiscovery.Controls.Filter.Core.show.collector();
        MtgDiscovery.Controls.Filter.Core.show.text();
    };

    return {
        apply: apply
    };
})();
