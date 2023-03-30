var MtgDiscovery = MtgDiscovery || {};
MtgDiscovery.Pages = MtgDiscovery.Pages || {};

MtgDiscovery.Pages.AllSets = MtgDiscovery.Pages.AllSets || (function () {

    const load = async () => {
        const result = await MtgDiscovery.Api.data.set.allSetsSummary(MtgDiscovery.Url.collector());
        const adding = MtgDiscovery.Containers.Sets.addAll(result.value);
        MtgDiscovery.Controls.Spinner.hide();
        await Promise.all([adding]);
    };

    const totalCardDisplay = () => {
        const totalCards = MtgDiscovery.Containers.Sets.systemCards();
        $("#system-card-count").html(totalCards.toLocaleString());
        $(".mtg-set-cards-in-system").removeClass("itemHidden");
    };

    const totalCollectorDisplay = () => {
        //Cards in collection
        const collectionCards = MtgDiscovery.Containers.Sets.totalCollectorCard();
        $("#system-collection-count").html(collectionCards.toLocaleString());

        //Unique cards of System card count
        const systemCards = MtgDiscovery.Containers.Sets.systemCards();
        $("#system-card-count2").html(systemCards.toLocaleString());
        const collectorUnique = MtgDiscovery.Containers.Sets.totalCollectorUnique();
        $("#collected-unique-count").html(collectorUnique.toLocaleString());

        const uniqueCompletePercent = parseInt(collectorUnique / systemCards * 100);
        $("#collected-unique-percent").html(uniqueCompletePercent.toLocaleString());
        $(".mtg-allsets-unique-collection-progress-bar").css("width", uniqueCompletePercent);

        //Set 
        const setCount = MtgDiscovery.Containers.Sets.setCount();
        const completeSets = MtgDiscovery.Containers.Sets.completeSets();
        $("#collected-sets-count").html(completeSets.toLocaleString());
        
        const setCompletePercent = parseInt(completeSets / setCount * 100);
        $("#collected-sets-percent").html(setCompletePercent.toLocaleString());
        $(".mtg-allsets-set-collection-progress-bar").css("width", setCompletePercent);

        //show it
        $(".mtg-allsets-collector-container").removeClass("itemHidden");
    };

    const systemInfo = () => {
        if (MtgDiscovery.Url.hasCollector()) {
            totalCollectorDisplay();
        } else {
            totalCardDisplay();
        }
    };

    return {
        load: load,
        displaySystemInfo: systemInfo
    };
})();

