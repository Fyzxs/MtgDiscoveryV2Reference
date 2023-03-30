var MtgDiscovery = MtgDiscovery || {};
MtgDiscovery.Events = MtgDiscovery.Events || {};
MtgDiscovery.Events.Set = MtgDiscovery.Events.Set || {};

MtgDiscovery.Events.Set.Update = MtgDiscovery.Events.Set.Update || (function () {

    const collection = async (selector) => {
        if (MtgDiscovery.Url.noCollector()) return;
        const setItem = $(selector);
        const id = setItem.data("id");
        const code = setItem.data("code");
        const collectorId = MtgDiscovery.Url.collector();
        const result = await MtgDiscovery.Api.collection.setCounts(id, code, collectorId);
        $(`${selector} .mtg-set-details`).html(result.value);

        const setCollectionCount = parseInt($(`${selector} .mtg-set-details .mtg-set-progress-bar`).data("value-now"));
        setItem.data("set-count", setCollectionCount);
        if (setCollectionCount === 100) {
            setItem.addClass("setCompleted");
        } else if (75 <= setCollectionCount) {
            setItem.addClass("setInProgress");
        }
    };
    return {
        collection: collection
    };
})();