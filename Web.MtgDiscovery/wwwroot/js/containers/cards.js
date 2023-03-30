var MtgDiscovery = MtgDiscovery || {};
MtgDiscovery.Containers = MtgDiscovery.Containers || {};

MtgDiscovery.Containers.Cards = MtgDiscovery.Containers.Cards || (function () {
    const container = () => $("#card-row");

    const addAllCards = async function (data, finalLoadCallback) {
        const locking = new Semaphore(20);
        const tempId = (id) => `card-${id}`;
        const tempSelector = (id) => `#${tempId(id)}`;
        const loadingSelector = (id) => `#card-${id}`;

        const observerInstance = (callback) => new IntersectionObserver(function (entries) {
            $.each(entries, (index, entry) => {
                if (entry.intersectionRatio <= 0) return;
                callback($(entry.target).data("src"));
            });
        }, { rootMargin: "0px 0px 30% 0px" }
        );

        const baseDisplay = async (value) => {
            try {
                if (!value) return;
                await locking.acquire();
                const displayData = await MtgDiscovery.Api.view.card.display(value, MtgDiscovery.Url.collector());
                $(loadingSelector(value.id)).replaceWith(displayData.value);
                const selector = `#card-${value.id}`;
                if (finalLoadCallback) finalLoadCallback(selector);
                MtgDiscovery.Controls.Sort.Cards.adjustDateVisibility(selector);
                MtgDiscovery.Events.Card.Apply.apply(selector);
                MtgDiscovery.Controls.Filter.Core.run(selector);
            } finally {
                locking.release();
            }
        };

        const loadingDisplay = async (value) => {
            try {
                if (!value) return;
                await locking.acquire();
                const loadingData = await MtgDiscovery.Api.view.card.loading(value);
                $(tempSelector(value.id)).replaceWith(loadingData.value);
                observerInstance(baseDisplay).observe(document.querySelector(loadingSelector(value.id)));
                $(loadingSelector(value.id)).data("src", value);
            } finally {
                locking.release();
            }
        };

        const cardArray = data[0];
        $.each(cardArray, function (index, value) {
            const id = tempId(value.id);
            const tempSel = tempSelector(value.id);
            const collection = data[1].find(x => x.id === value.id);
            value.total = collection ? collection.counts : 0;
            container().append(`<div id="${id}" class="mtg-bg-toggle mtg-card mtg-card-dark align-items-center g-0 itemShown placeholder-glow" 
                                    data-id="${value.id}" 
                                    data-code="${value.code}" 
                                    data-name="${value.name}" 
                                    data-collector-number="${value.collector_number}" 
                                    data-rarity="${value.rarity}"
                                    data-total-count="${value.total}"
                                    data-price="${value.current_price}"
                                    data-release-date="${value.released_at}"
                                    data-stage="initial"
                                ><span aria-hidden="true">${value.name}</span>
    <div class="mtg-card-img-container" style="opacity: .3; height: inherit;">
        <div>
            <img class="mtg-card-img d-none d-lg-block" loading="lazy" src="/img/cardback.jpg">
        </div>
    </div>
    <div class="placeholder">
        <div class="mtg-card-badge mtg-card-set-badge-loading"><span class="mtg-card-set-badge-text"></span></div>
    </div>
    <div class="container">
        <div class="row p-1">
            <div class="col-7 placeholder mx-auto">
                <span class="badge placeholder text-bg-light col-8"></span>
            </div>
        </div>
        <div class="row p-1">
            <div class="col-11 placeholder mx-auto">
                <span class="badge placeholder text-bg-light col-8"></span>
            </div>
        </div>
        <div class="row p-1">
            <div class="col-3 placeholder mx-auto">
                <span class="badge placeholder text-bg-light col-8"></span>
            </div>
        </div>
        <div class="row p-1 justify-content-center">
            <div class="col-2 placeholder mx-1">
                <span class="badge placeholder text-bg-light col-2"></span>
            </div>
            <div class="col-2 placeholder mx-1">
                <span class="badge placeholder text-bg-light col-2"></span>
            </div>
            <div class="col-2 placeholder mx-1">
                <span class="badge placeholder text-bg-light col-2"></span>
            </div>
            <div class="col-2 placeholder mx-1">
                <span class="badge placeholder text-bg-light col-2"></span>
            </div>
        </div>
    </div></div>`);
            $(tempSel).data("src", value);
            MtgDiscovery.Controls.Sort.Cards.adjustDateVisibility(id);

            observerInstance(loadingDisplay).observe(document.querySelector(tempSel));
        });

        MtgDiscovery.Controls.ItemsVisible.total(cardArray.length);
        MtgDiscovery.Controls.ItemsVisible.visible(cardArray.length);
    };

    const totalUniqueCardCount = () => {
        let val = 0;
        container().children().each(function () {
            const item = $(this);
            val += 0 !== item.data("total-count");
        });
        return val;
    }
    const cardCount = () => container().children().length;

    return {
        addAll: addAllCards,
        container: container,
        totalUniqueCard: totalUniqueCardCount,
        totalCards: cardCount
    };
})();