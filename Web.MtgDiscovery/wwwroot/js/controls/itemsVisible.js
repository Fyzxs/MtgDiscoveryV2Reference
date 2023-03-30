var MtgDiscovery = MtgDiscovery || {};
MtgDiscovery.Controls = MtgDiscovery.Controls || {};

MtgDiscovery.Controls.ItemsVisible = MtgDiscovery.Controls.ItemsVisible || (function() {

    const setTotal = (total) => $("#itemTotalCount").html(total);
    const setVisible = (shown) => $("#itemShownCount").html(shown);
    
    return {
        total: setTotal,
        visible: setVisible
    };

})();