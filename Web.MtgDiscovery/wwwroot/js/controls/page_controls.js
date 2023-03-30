var MtgDiscovery = MtgDiscovery || {};
MtgDiscovery.Controls = MtgDiscovery.Controls || {};

MtgDiscovery.Controls.Box = MtgDiscovery.Controls.Box || (function() {

    const show = () => $("#pageControls").addClass("itemShown").removeClass("itemHidden");
    
    return {
        show : show
    };
})();