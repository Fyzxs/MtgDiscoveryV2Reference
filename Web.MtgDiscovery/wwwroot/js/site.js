// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var MtgDiscovery = MtgDiscovery || {};

MtgDiscovery.Pages = MtgDiscovery.Pages || {};

function debounce(func, wait, immediate) {
    var timeout;
    return function () {
        const context = this;
        const args = arguments;
        const later = function () {
            timeout = null;
            if (!immediate) func.apply(context, args);
        };
        const callNow = immediate && !timeout;
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
        if (callNow) func.apply(context, args);
    };
};

const addCountToToggle = (selector, count) => {
    const rarityOrg = $(selector);
    const rarityLabels = rarityOrg.next().children();
    const rarityOn = $(rarityLabels[0]);
    const rarityOff = $(rarityLabels[1]);
    rarityOn.html(`${rarityOrg.data("on")} (${count})`);
    rarityOff.html(`${rarityOrg.data("off")} (${count})`);
    if (count === 0) {
        rarityOrg.bootstrapToggle('off');
        rarityOrg.bootstrapToggle('disable');
    } else if (rarityOrg.parent().hasClass('disabled')) {
        rarityOrg.bootstrapToggle('enable');
        rarityOrg.bootstrapToggle('on');
    }
};

window.addEventListener("keydown", function (e) {
    //if (e.keyCode === 37 || e.keyCode === 38 || e.keyCode === 39 || e.keyCode === 40) e.preventDefault();
    if (e.keyCode === 32 && e.target === document.body) {
        e.preventDefault();
    }
});