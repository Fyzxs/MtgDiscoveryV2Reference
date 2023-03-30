var MtgDiscovery = MtgDiscovery || {};
MtgDiscovery.Controls = MtgDiscovery.Controls || {};

MtgDiscovery.Controls.SearchBox = MtgDiscovery.Controls.SearchBox || (function () {
    const inputId = "searchInput";
    const inputSelector = `#${inputId}`;

    const clearId = "clearSearch";
    const clearSelector = `#${clearId}`;

    const buttonCallback = (callback) => $(clearSelector).click(callback);
    const focusInput = () => $(inputSelector).focus();
    const isEmptyInput = () => $(inputSelector).val() === "";
    const emptyInput = () => $(inputSelector).val("");
    const enteredInput = () => $(inputSelector).val();
    const keyupCallback = (callback) => $(inputSelector).keyup(callback);
    const scrollTo = () => $(inputSelector)[0].scrollIntoView();

    return {
        empty: emptyInput,
        isEmpty: isEmptyInput,
        focus: focusInput,
        entered: enteredInput,
        clearCallback: buttonCallback,
        keyupCallback: keyupCallback
    };
})();
