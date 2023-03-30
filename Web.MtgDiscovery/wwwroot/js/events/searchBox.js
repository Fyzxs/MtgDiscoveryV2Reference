var MtgDiscovery = MtgDiscovery || {};
MtgDiscovery.Events = MtgDiscovery.Events || {};

MtgDiscovery.Events.SearchBox = MtgDiscovery.Events.SearchBox || (function () {
    
    let filterCallback = () => { };

    const clearInput = () => {
        MtgDiscovery.Controls.SearchBox.focus();
        if (MtgDiscovery.Controls.SearchBox.isEmpty()) return;

        MtgDiscovery.Controls.SearchBox.empty();
        filterCallback();
    };

    const applyInputEvents = () => MtgDiscovery.Controls.SearchBox.keyupCallback(debounce(filterCallback, 250));
    const applyClearEvents = () => MtgDiscovery.Controls.SearchBox.clearCallback(clearInput);

    let textEnterActivateCount = 0;
    const applyDoubleShift = () => {
        $(document).keyup(function (event) {
            if (event.key !== "Shift") return;
            textEnterActivateCount++;
            setTimeout(function () {
                if (textEnterActivateCount === 2) clearInput();
                textEnterActivateCount = 0;
            }, 250);
        });
    };

    const applyEvents = (callback) => {
        filterCallback = callback;
        applyInputEvents();
        applyClearEvents();
        applyDoubleShift();
    };

    return {
        apply: applyEvents
    };
})();
