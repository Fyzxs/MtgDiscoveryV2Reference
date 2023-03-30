var MtgDiscovery = MtgDiscovery || {};
MtgDiscovery.Controls = MtgDiscovery.Controls || {};

MtgDiscovery.Controls.Spinner = MtgDiscovery.Controls.Spinner || (function () {
    const selector = ".loading-spinner ";
    const show = () => $(selector).show();
    const hide = () => $(selector).hide();
    return {
        show: show,
        hide: hide
    }
})();