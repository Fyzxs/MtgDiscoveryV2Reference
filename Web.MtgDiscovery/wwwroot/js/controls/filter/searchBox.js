var MtgDiscovery = MtgDiscovery || {};
MtgDiscovery.Controls = MtgDiscovery.Controls || {};
MtgDiscovery.Controls.Filter = MtgDiscovery.Controls.Filter || {};
MtgDiscovery.Controls.Filter.SearchBox = MtgDiscovery.Controls.Filter.SearchBox || {};

MtgDiscovery.Controls.Filter.SearchBox.Show = MtgDiscovery.Controls.Filter.SearchBox.Show || (function() {

    const adaptControls = () => {
        const cards = $(`.mtg-card`).length;
        if (cards === 1) return;

        $("#filterControlsTextBox").removeClass("itemHidden");
    }

    return {
        adapt: adaptControls
    }
})();