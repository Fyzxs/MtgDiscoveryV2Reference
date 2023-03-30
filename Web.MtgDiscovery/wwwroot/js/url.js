var MtgDiscovery = MtgDiscovery || {};

MtgDiscovery.Url = MtgDiscovery.Url || (function () {
    const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop)
    });
    const collectorValue = () => params["collector"];
    const hasCollector = () => {
        const collector = collectorValue();
        return collector != undefined && collector !== "";
    };

    const setCodeValue = () => params["code"];
    const hasSetCode = () => {
        const value = setCodeValue();
        return value != undefined && value !== "";
    };

    const artistValue = () => params["artist"];
    const cardNameValue = () => params["cardName"];

    const collectorResponse = () => hasCollector() ? collectorValue() : "";
    return {
        hasCollector: hasCollector,
        noCollector: () => !hasCollector(),
        collector: collectorResponse,
        hasSetCode: hasSetCode,
        noSetCode: () => !hasSetCode(),
        artist: artistValue,
        cardName: cardNameValue,
        setCode: setCodeValue
    };
})();

function applyHash(sortType, orderType) {
    if (window.location.hash === "") window.location.hash = `#`;
    else window.location.hash += "&";
    window.location.hash += `sort=${sortType}&order=${orderType}`;

}
MtgDiscovery.Hash = MtgDiscovery.Hash || (function () {

    const doStuff = () => {
        const hashElements = window.location.hash.split("&");

    };

    const sort = (opt) => {
        const hashElements = window.location.hash.replace("#", "").split("&");
        let newHash = "#";
        for (let i = 0; i < hashElements.length; i++) {
            if (hashElements[i].startsWith("sort=")) {
                newHash += `sort=${opt.sort};${opt.order}`;
            } else {
                newHash += hashElements;
            }
        }
        window.location.hash = newHash;
    }

    const filter = (opt) => {
        const hashElements = window.location.hash.replace("#", "").split("&");
        let newHash = "#";
        for (let i = 0; i < hashElements.length; i++) {
            if (hashElements[i].startsWith("sort=")) {
                newHash += `sort=${opt.sort};${opt.order}`;
            } else {
                newHash += hashElements;
            }
        }
        window.location.hash = newHash;
    }

    return {
        getSort: () => {},
        setSort: () => {}
    };
})();