var MtgDiscovery = MtgDiscovery || {};
MtgDiscovery.Events = MtgDiscovery.Events || {};
MtgDiscovery.Events.DangerZone = MtgDiscovery.Events.DangerZone || {};

MtgDiscovery.Events.DangerZone.Card = MtgDiscovery.Events.DangerZone.Card || {};

MtgDiscovery.Events.DangerZone.Card.Apply = MtgDiscovery.Events.DangerZone.Card.Apply || (function () {

    const bulkAdd = async () => {
        const locking = new Semaphore(5);
        const container = MtgDiscovery.Containers.Cards.container();
        const cards = container.children();
        const cardsCount = cards.length;
        
        const progress = $("#dangerZoneProgress");
        let index = 0;
        for (const cardItem of cards) {
            try {
                const $cardItem = $(cardItem);
                if ($cardItem.hasClass("ItemHidden")) return;

                await locking.acquire();
                const cardId = $cardItem.prop("id");
                const cardSelector = `#${cardId}`;
                document.getElementById(cardId).scrollIntoView(true);
                progress.html(`Progress ${index++} of ${cardsCount}`);
                await MtgDiscovery.Events.Card.Collect.internal.submit(cardSelector, "1", { toastSuccess: false });
                
            } finally {
                locking.release();
            }
        }
    };
    const applyDangerZone = () => {
        $(document).keyup(function (event) {
            if (event.ctrlKey && event.altKey && event.shiftKey && event.key === "D")
                $("#dangerZone").toggleClass("itemHidden");
        });
        $("#btnOneOfVisible").click(async () => {
            const doIt =
                confirm(
                    "This will attempt to add 1 of each visible card into the collection.\nThere's no undo.\nDo you want to continue?");
            if (doIt === false) {
                $("#dangerZone").toggleClass("itemHidden");
                return;
            }
            const modal = new bootstrap.Modal('#dangerZoneModal');
            modal.show();
            await bulkAdd();
            console.log("DONE");
            modal.hide();
            $("#dangerZone").toggleClass("itemHidden");
        });
    };

    return {
        apply: applyDangerZone
    };
})();