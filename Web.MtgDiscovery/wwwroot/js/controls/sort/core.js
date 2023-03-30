var MtgDiscovery = MtgDiscovery || {};
MtgDiscovery.Controls = MtgDiscovery.Controls || {};
MtgDiscovery.Controls.Sort = MtgDiscovery.Controls.Sort || {};

MtgDiscovery.Controls.Sort.Core = MtgDiscovery.Controls.Sort.Core || (function () {
    let sortCallback = () => { };
    let dataTransform = () => { };
    let containerFunc = () => [];

    function sortFunction(sortType, orderType) {
        const orderMultiple = orderType === "desc" ? -1 : 1;
        return (a, b) => sortCallback(sortType)(dataTransform(a), dataTransform(b), orderMultiple) * (orderMultiple);
    }

    function sort() {
        const container = containerFunc();
        const sets = container.children();
        const sortType = $(".sort-control:checked").data("sort-key");
        const orderType = $(".sort-control-direction:checked").data("sort-direction");
        sets.sort(sortFunction(sortType, orderType));
        sets.detach().appendTo(container);
    }

    const apply = (callback, transform, container) => {
        sortCallback = callback;
        dataTransform = transform;
        containerFunc = container;

        $(".sort-control").change(sort);
        $(".sort-control-direction").change(sort);
        $(".sort-control:checked").trigger("change");
    };

    const showName = () => $("#btnradioNameLabel").removeClass("itemHidden");
    const hideName = () => $("#btnradioNameLabel").addClass("itemHidden");
    const showReleaseDate = () => $("#btnradioReleaseDateLabel").removeClass("itemHidden");
    const hideReleaseDate = () => $("#btnradioReleaseDateLabel").addClass("itemHidden");
    const showSize = () => $("#btnradioSizeLabel").removeClass("itemHidden");
    const showNumber = () => $("#btnradioCollectoNumberLabel").removeClass("itemHidden");
    const hideNumber = () => $("#btnradioCollectoNumberLabel").addClass("itemHidden");
    const showSetPercent = () => {
        if (MtgDiscovery.Url.hasCollector()) $("#btnradioSetPercentLabel").removeClass("itemHidden");
    };
    const showCardsCollected = () => {
        if (MtgDiscovery.Url.hasCollector()) $("#btnradioCardsCollectedLabel").removeClass("itemHidden");
    };
    const showPrice = () => $("#btnradioPriceLabel").removeClass("itemHidden");
    const sortAsc = () => $("#btnradioSortAsc").prop("checked", true);

    const checkNumber = () => $("#btnradioCollectorNumber").prop("checked", true);

    return {
        apply: apply,
        show: {
            name: showName,
            releaseDate: showReleaseDate,
            size: showSize,
            setPercent: showSetPercent,
            cardsCollected: showCardsCollected,
            number: showNumber,
            price: showPrice
        },
        hide: {
            name: hideName,
            number: hideNumber,
            release: hideReleaseDate
        },
        check: {
            number:checkNumber
        },
        order: {
            asc: sortAsc
        },
        sort: sort
    };
})();