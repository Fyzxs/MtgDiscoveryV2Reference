var MtgDiscovery = MtgDiscovery || {};
MtgDiscovery.Controls = MtgDiscovery.Controls || {};
MtgDiscovery.Controls.Filter = MtgDiscovery.Controls.Filter || {};

MtgDiscovery.Controls.Filter.Core = MtgDiscovery.Controls.Filter.Core || (function () {
    let isFiltered = () => false;
    let containerFunc = () => [];

    const filterFunc = (item) => {
        if (isFiltered(item)) {
            $(item).addClass("itemHidden").removeClass("itemShown");
            return 0;
        } else {
            $(item).addClass("itemShown").removeClass("itemHidden");
            return 1;
        }

    }
    const filter = () => {
        const container = containerFunc();
        const sets = container.children();
        let shown = 0;
        $.each(sets, function (index, setItem) {
            shown += filterFunc(setItem);
        });

        MtgDiscovery.Controls.ItemsVisible.visible(shown);
    };

    const apply = (filterCallback, container) => {
        isFiltered = filterCallback;
        containerFunc = container;
        MtgDiscovery.Events.SearchBox.apply(filter);
        $(".filter-control").change(filter);

    };

    const run = (selector) => filterFunc($(selector));;

    const showRarity = () => MtgDiscovery.Controls.Filter.Rarity.Show.adapt();
    const showCollector = () => MtgDiscovery.Controls.Filter.Quantity.Show.adapt();
    const showText = () => MtgDiscovery.Controls.Filter.SearchBox.Show.adapt();
    const showCompletion = () => MtgDiscovery.Controls.Filter.AllSets.adapt();

    const hideFilterControl = () => $("#filterControlCollapseHeader").parent().addClass("itemHidden");
    return {
        apply: apply,
        run: run,
        show: {
            collector: showCollector,
            rarity: showRarity,
            text: showText,
            completion: showCompletion
        },
        hide: hideFilterControl
    };
})();