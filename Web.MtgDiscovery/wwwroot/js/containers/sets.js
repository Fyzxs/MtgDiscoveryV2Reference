var MtgDiscovery = MtgDiscovery || {};
MtgDiscovery.Containers = MtgDiscovery.Containers || {};

MtgDiscovery.Containers.Sets = MtgDiscovery.Containers.Sets || (function () {
    const container = () => $("#set-row");

    const addOne = async function (setCode, collector) {
        const result = await MtgDiscovery.Api.view.set.displayLimited({ code: setCode }, collector);
        container().html(result.value);
        MtgDiscovery.Events.Set.Update.collection(`#${setCode}-set`);
    }

    const addAllSets = async function (data) {
        const locking = new Semaphore(20);
        const tempId = (code) => `temp-${code}`;
        const tempSelector = (code) => `#${tempId(code)}`;

        const observerInstance = (callback) => new IntersectionObserver(function (entries) {
            $.each(entries, (index, entry) => {
                if (entry.intersectionRatio <= 0) return;
                callback($(entry.target).data("src"));
            });
        });

        const baseDisplay = async (value) => {
            await locking.acquire();
            const displayData = await MtgDiscovery.Api.view.set.display(value, MtgDiscovery.Url.collector());
            $(tempSelector(value.code)).replaceWith(displayData.value);
            MtgDiscovery.Events.Set.Update.collection(`#${value.code}-set`);
            MtgDiscovery.Controls.Filter.Core.run(`#${value.code}-set`);
            locking.release();
        };

        const loadingDisplay = async (value) => {
            await locking.acquire();
            const loadingData = await MtgDiscovery.Api.view.set.loading(value);
            $(tempSelector(value.code)).html(loadingData.value);
            observerInstance(baseDisplay).observe(document.querySelector(tempSelector(value.code)));
            $(tempSelector(value.code)).data("src", value);
            locking.release();
        };

        const setArray = data[0];
        $.each(setArray, function (index, value) {
            const id = tempId(value.code);
            const tempSel = tempSelector(value.code);
            let collection = data[1].find(x => x.id === value.id);
            if (!collection) //mmmm falsey
                collection = {
                    altered: 0,
                    artistProof: 0,
                    id: value.id,
                    signed: 0,
                    total: 0,
                    uniqueCount: 0,
                    unmodified: 0
                };
            container().append(`<div style="height:350px" id="${id}" class="set-card" 
                                        data-id="${value.id}" 
                                        data-code="${value.code}" 
                                        data-released-at='${value.released_at}' 
                                        data-name="${value.name}" 
                                        data-set-size="${value.base_set_size}"
                                        data-parent-code="${value.parent_set_code}" 
                                        data-original-code="${value.original_code}"
                                        data-set-type="${value.set_type}"
                                        data-unique-count="${collection.uniqueCount}"
                                        data-collected-count="${collection.total}"
                                        data-set-percent="${(collection.uniqueCount * 100) / value.base_set_size}"
                                ><span aria-hidden="true">${value.name}</span></div>`);
            $(tempSel).data("src", value);

            observerInstance(loadingDisplay).observe(document.querySelector(tempSel));
        });

        MtgDiscovery.Controls.ItemsVisible.total(setArray.length);
        MtgDiscovery.Controls.ItemsVisible.visible(setArray.length);
    };

    const systemCards = () => {
        let val = 0;
        container().children().each(function () { val += $(this).data("set-size"); });
        return val;
    }

    const totalCollectorCardCount = () => {
        let val = 0;
        container().children().each(function () { val += $(this).data("collected-count"); });
        return val;
    }

    const totalCollectorUniqueCount = () => {
        let val = 0;
        container().children().each(function () { val += $(this).data("unique-count"); });
        return val;
    }

    const collectorCompleteSet = () => {
        let val = 0;
        container().children().each(function () {
            const item = $(this);
            val += item.data("set-size") === item.data("unique-count") ;
        });
        return val;
    }

    const setCount = () => container().children().length;

    return {
        addAll: addAllSets,
        container: container,
        addOne: addOne,
        systemCards: systemCards,
        totalCollectorCard: totalCollectorCardCount,
        totalCollectorUnique: totalCollectorUniqueCount,
        completeSets: collectorCompleteSet,
        setCount: setCount
    };
})();