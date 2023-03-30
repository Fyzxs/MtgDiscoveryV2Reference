var MtgDiscovery = MtgDiscovery || {};
MtgDiscovery.Pages = MtgDiscovery.Pages || {};
MtgDiscovery.Pages.Binder = MtgDiscovery.Pages.Binder || {};
MtgDiscovery.Pages.Binder.Core = MtgDiscovery.Pages.Binder.Core || (function () {
    const setUpCards = () => {
        const totalCards = MtgDiscovery.Containers.Cards.totalCards();
        if (totalCards <= 9) {
            $('#binderPageItem').remove();
            return;
        };
        const pages = Math.trunc(Math.ceil(totalCards / 9.0));
        const buttonHolder = $('#binderPageButtons');
        const padLength = pages.toString().length;
        for (var i = 1; i <= pages; i++) {
            const buttonHtml = `<input type="radio" class="btn-check binder-control mtg-binder-button itemHidden" name="btnBinderRadio" id="btnBinderPage${i}" autocomplete="off" data-filter-page="${i}" ${i === 1 ? "checked" : ""}>` +
                `<label class="btn btn-outline-light ms-1 mt-1" for="btnBinderPage${i}" id="btnBinderPage${i}Label">${String(i).padStart(padLength, '0')}</label>`;
            buttonHolder.append(buttonHtml);
        }

        $(".mtg-binder-button").click(function () {
            MtgDiscovery.Pages.Binder.Filter.apply();
        });

        $("#binderPrev").click(function () {
            const button = $(".mtg-binder-button:checked").prev().prev();
            if (button.length === 0) return;
            button.prop("checked", true);
            MtgDiscovery.Pages.Binder.Filter.apply();
        });

        $("#binderNext").click(function () {
            const button = $(".mtg-binder-button:checked").next().next();
            if (button.length === 0) return;
            button.prop("checked", true);
            MtgDiscovery.Pages.Binder.Filter.apply();
        });

        $(document).keyup(function (event) {
            if (event.key === ',') $("#binderPrev").trigger("click");
            if (event.key === ' ') $("#binderNext").trigger("click");
        });
    };

    const loadSet = async (setCode, collector) => {
        MtgDiscovery.Containers.Sets.addOne(setCode, collector);
    };
    const loadCards = async (setCode) => {
        const result = await MtgDiscovery.Api.data.card.setCardsSummary({ code: setCode }, MtgDiscovery.Url.collector());
        const adding = MtgDiscovery.Containers.Cards.addAll(result.value,
            (selector) => {
                $(`${selector} .mtg-card-set`).addClass("itemHidden");
                $(`${selector} .mtg-card-name`).addClass("itemHidden");
                $(`${selector} .mtg-card-artist`).addClass("itemHidden");
                MtgDiscovery.Pages.Binder.Filter.apply();
                const data = $(selector).data("total-count");
                if (data === 0) $(`${selector} .mtg-card-img`).addClass("mtg-card-binder-none");
                
            });
        MtgDiscovery.Controls.Spinner.hide();
        await Promise.all([adding]);
    };

    return {
        loadSet: loadSet,
        loadCards: loadCards,
        setUp: setUpCards
    };
})();

MtgDiscovery.Pages.Binder.Filter = MtgDiscovery.Pages.Binder.Filter || (function () {
    const filterCards = () => {
        const page = $(".mtg-binder-button:checked").data("filter-page");
        const container = MtgDiscovery.Containers.Cards.container();
        const cards = container.children();
        $.each(cards, function (index, cardItem) {
            const cardPage = Math.ceil((index + 1) / 9);
            if (page !== cardPage) $(cardItem).addClass("itemHidden");
            else $(cardItem).removeClass("itemHidden");
        });
    };

    return {
        apply:filterCards
    };
})();