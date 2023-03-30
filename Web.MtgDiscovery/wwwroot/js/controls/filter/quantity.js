var MtgDiscovery = MtgDiscovery || {};
MtgDiscovery.Controls = MtgDiscovery.Controls || {};
MtgDiscovery.Controls.Filter = MtgDiscovery.Controls.Filter || {};
MtgDiscovery.Controls.Filter.Quantity = MtgDiscovery.Controls.Filter.Quantity || {};

MtgDiscovery.Controls.Filter.Quantity.Core = MtgDiscovery.Controls.Filter.Quantity.Core || (function () {
    const isFiltered = (card) => {

        function quantityFilterZero(card) {
            const filterActive = $("#filterCountZero:checked").length === 0;
            const filterMatch = parseInt(card.totalCount) === 0;
            return filterActive && filterMatch;
        }
        function quantityFilterOne(card) {
            const filterActive = $("#filterCountOne:checked").length === 0;
            const filterMatch = parseInt(card.totalCount) === 1;
            return filterActive && filterMatch;
        }

        function quantityFilterTwo(card) {
            const filterActive = $("#filterCountTwo:checked").length === 0;
            const filterMatch = parseInt(card.totalCount) === 2;
            return filterActive && filterMatch;
        }

        function quantityFilterThreePlus(card) {
            const filterActive = $("#filterCountThreePlus:checked").length === 0;
            const filterMatch = 3 <= parseInt(card.totalCount);
            return filterActive && filterMatch;
        }

        return MtgDiscovery.Url.hasCollector() && (
            quantityFilterZero(card) ||
            quantityFilterOne(card) ||
            quantityFilterTwo(card) ||
            quantityFilterThreePlus(card)
        );
    };

    const toggles = () => $(".filter-quantity:checkbox");

    return {
        isFiltered: isFiltered,
        toggles: toggles
    };

})();

MtgDiscovery.Controls.Filter.Quantity.Show = MtgDiscovery.Controls.Filter.Quantity.Show || (function () {

    const quantityCount = (quantity) => {
        let tally = 0;
        $.each($(`.mtg-card`),
            (index, item) => {
                const $item = $(item);
                const itemCount = $item.data("total-count");
                const hasQuantity = parseInt(itemCount) === parseInt(quantity);
                if (hasQuantity) tally += 1;
            });
        return tally;
    };


    const adaptControls = () => {
        const zeroCount = quantityCount("0");
        const oneCount = quantityCount("1");
        const twoCount = quantityCount("2");
        const allCount = $(`.mtg-card`).length;
        if (allCount < 2) return;

        const moreCount = allCount - (zeroCount + oneCount + twoCount);

        addCountToToggle('#filterCountZero', zeroCount);
        addCountToToggle('#filterCountOne', oneCount);
        addCountToToggle('#filterCountTwo', twoCount);
        addCountToToggle('#filterCountThreePlus', moreCount);

        $("#filterControlsCollector").removeClass("itemHidden");
    }

    return {
        adapt: adaptControls
    };
})();

MtgDiscovery.Controls.Filter.Quantity.ShowAll = MtgDiscovery.Controls.Filter.Quantity.ShowAll || (function () {

    const apply = () => {
        $("#filterShowAll").click(() => {
            const toggles = MtgDiscovery.Controls.Filter.Quantity.Core.toggles();
            toggles.each(function () {
                $(this).bootstrapToggle("on");
            });
        });
    }

    return {
        apply: apply
    };
})();

MtgDiscovery.Controls.Filter.Quantity.ShowMissing = MtgDiscovery.Controls.Filter.Quantity.ShowMissing || (function () {

    const apply = () => {
        $("#filterShowMissing").click(() => {
            const toggles = MtgDiscovery.Controls.Filter.Quantity.Core.toggles();
            toggles.each(function () {

                const ctrl = $(this);
                if (ctrl.attr("id") === "filterCountZero") {
                    ctrl.bootstrapToggle("on");
                } else {
                    ctrl.bootstrapToggle("off");
                }
            });
        });
    }

    return {
        apply: apply
    };
})();