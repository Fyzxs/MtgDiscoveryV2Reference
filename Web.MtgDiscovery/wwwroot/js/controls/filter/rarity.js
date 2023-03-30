var MtgDiscovery = MtgDiscovery || {};
MtgDiscovery.Controls = MtgDiscovery.Controls || {};
MtgDiscovery.Controls.Filter = MtgDiscovery.Controls.Filter || {};
MtgDiscovery.Controls.Filter.Rarity = MtgDiscovery.Controls.Filter.Rarity || {};

MtgDiscovery.Controls.Filter.Rarity.Core = MtgDiscovery.Controls.Filter.Rarity.Core || (function () {
    const commonText = "common";
    const uncommonText = "uncommon";
    const rareText = "rare";
    const mythicText = "mythic";

    const commonSelector = "#filterCommon";
    const uncommonSelector = "#filterUncommon";
    const rareSelector = "#filterRare";
    const mythicSelector = "#filterMythic";

    const isFiltered = (card) => {

        function rarityMythicFiltered(card) {
            const filterRarity = $(`${mythicSelector}:checked`).length === 0;
            const isRarity = card.rarity === mythicText;
            return filterRarity && isRarity;
        }


        function rarityRareFiltered(card) {
            const filterRarity = $(`${rareSelector}:checked`).length === 0;
            const isRarity = card.rarity === rareText;
            return filterRarity && isRarity;
        }


        function rarityUncommonFiltered(card) {
            const filterRarity = $(`${uncommonSelector}:checked`).length === 0;
            const isRarity = card.rarity === uncommonText;
            return filterRarity && isRarity;
        }


        function rarityCommonFiltered(card) {
            const filterRarity = $(`${commonSelector}:checked`).length === 0;
            const isRarity = card.rarity === commonText;
            return filterRarity && isRarity;
        }

        return rarityMythicFiltered(card) ||
            rarityRareFiltered(card) ||
            rarityUncommonFiltered(card) ||
            rarityCommonFiltered(card);
    };
    return {
        isFiltered: isFiltered
    };

})();

MtgDiscovery.Controls.Filter.Rarity.Show = MtgDiscovery.Controls.Filter.Rarity.Show || (function () {
    const commonText = "common";
    const uncommonText = "uncommon";
    const rareText = "rare";
    const mythicText = "mythic";

    const commonSelector = "#filterCommon";
    const uncommonSelector = "#filterUncommon";
    const rareSelector = "#filterRare";
    const mythicSelector = "#filterMythic";

    const rarityCount = (rarity) => $(`.mtg-card[data-rarity="${rarity}"]`).length;

    const onlyOne = (a, b, c, d) => {
        if ((a + b + c) === 0) return true;
        if ((a + b + d) === 0) return true;
        if ((a + c + d) === 0) return true;
        if ((b + c + d) === 0) return true;
        return false;
    }
    
    const adaptControls = () => {
        const commonCount = rarityCount(commonText);
        const uncommonCount = rarityCount(uncommonText);
        const rareCount = rarityCount(rareText);
        const mythicCount = rarityCount(mythicText);
        if (onlyOne(commonCount, uncommonCount, rareCount, mythicCount)) return;

        addCountToToggle(commonSelector, commonCount);
        addCountToToggle(uncommonSelector, uncommonCount);
        addCountToToggle(rareSelector, rareCount);
        addCountToToggle(mythicSelector, mythicCount);

        $("#filterControlsRarity").removeClass("itemHidden");
    };

    return {
        adapt: adaptControls
    };
})();