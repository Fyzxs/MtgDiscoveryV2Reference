MtgDiscovery.Controls.Sort.AllSets = MtgDiscovery.Controls.Sort.AllSets || (function () {
    function baseSortFunctions(sortType) {
        if (sortType === "name") return alphaSort;
        if (sortType === "release-date") return releaseDateSort;
        if (sortType === "set-size") return setSizeSort;
        if (sortType === "percent-complete") return pctCollectedSort;
        if (sortType === "cards-collected") return countCollectedSort;
        return () => console.log(`unknown all sets sort [sortType=${sortType}] provided`);
    }

    function alphaSort(a, b) {
        return a.name.localeCompare(b.name);
    }

    function setSizeSort(a, b) {
        const initial = a.setSize - b.setSize;
        return initial === 0 ? alphaSort(a, b) : initial;
    }

    function setCodeCompare(a, b) {
        if (a.originalCode === undefined) a.originalCode = a.setCode.split("-").pop();
        if (b.originalCode === undefined) b.originalCode = b.setCode.split("-").pop();

        if (!a.parentSetCode && !b.parentSetCode) return 0; //Neither have parents, don't care

        if (a.setCode === b.parentSetCode) return -1;
        if (b.setCode === a.parentSetCode) return 1;

        if (a.parentSetCode !== b.parentSetCode) return 0; // We don't care about non-connected set codes.

        //Promotional
        if (`p${a.setCode}` === b.setCode) return -1;
        if (`p${b.setCode}` === a.setCode) return 1;

        //Art Series
        if (`a${a.setCode}` === b.setCode) return -1;
        if (`a${b.setCode}` === a.setCode) return 1;

        //Tokens
        if (`t${a.setCode}` === b.setCode) return -1;
        if (`t${b.setCode}` === a.setCode) return 1;

        //Commander oversized
        if (`o${a.setCode}` === b.setCode) return -1;
        if (`o${b.setCode}` === a.setCode) return 1;

        //Mini games
        if (`m${a.setCode}` === b.setCode) return -1;
        if (`m${b.setCode}` === a.setCode) return 1;

        //Substitute cards
        if (`s${a.setCode}` === b.setCode) return -1;
        if (`s${b.setCode}` === a.setCode) return 1;

        if (a.setCode === a.originalCode) return -1;
        if (b.setCode === b.originalCode) return 1;

        if (a.setCode === `f-${a.originalCode}`) return -1;
        if (b.setCode === `f-${b.originalCode}`) return 1;

        if (a.setCode === `e-${a.originalCode}`) return -1;
        if (b.setCode === `e-${b.originalCode}`) return 1;

        if (a.setCode === `f-e-${a.originalCode}`) return -1;
        if (b.setCode === `f-e-${b.originalCode}`) return 1;

        if (a.setCode === `v-${a.originalCode}`) return -1;
        if (b.setCode === `v-${b.originalCode}`) return 1;

        if (a.setCode === `f-v-${a.originalCode}`) return -1;
        if (b.setCode === `f-v-${b.originalCode}`) return 1;

        return 0;//We can't say much about it, pretend they're equal
    }

    function releaseDateSort(a, b, order) {
        const release = a.date.localeCompare(b.date);
        if (release !== 0) return release;
        const setCodeResult = setCodeCompare(a, b);//Inverts to have the value we like when descending
        if (setCodeResult !== 0) return setCodeResult;
        return alphaSort(a, b); //Inverting forces default to be first in the list
    }

    function pctCollectedSort(a, b, order) {
        if (a.pctCollected === 0 && b.pctCollected === 0) return 0;
        if (a.pctCollected === 0 && 0 < b.pctCollected) return order;
        if (0 < a.pctCollected && b.pctCollected === 0) return -order;

        const initial = parseInt(a.pctCollected) - parseInt(b.pctCollected);
        if (initial === 0) return releaseDateSort(a, b);
        if (initial < 0) return -1;
        if (0 < initial) return 1;
    }

    function countCollectedSort(a, b, order) {
        if (a.countCollected === 0 && b.countCollected === 0) return 0;
        if (a.countCollected === 0 && 0 < b.countCollected) return order;
        if (0 < a.countCollected && b.countCollected === 0) return -order;

        const initial = parseInt(a.countCollected) - parseInt(b.countCollected);
        if (initial === 0) return pctCollectedSort(a, b, order);
        if (initial < 0) return -1;
        if (0 < initial) return 1;
    }

    function x(e) {
        const $e = $(e);
        const a = {};
        a.name = $e.data("name");
        a.setCode = $e.data("code");
        a.setSize = $e.data("set-size");
        a.date = $e.data("released-at");
        a.parentSetCode = $e.data("parent-code");
        a.originalCode = $e.data("original-code");
        a.pctCollected = $e.data("set-percent");
        a.countCollected = $e.data("collected-count");
        a.imageCode = $e.data("image-code");

        return a;
    }

    const apply = () => {
        MtgDiscovery.Controls.Sort.Core.apply(baseSortFunctions, x, () => $("#set-row"));
        MtgDiscovery.Controls.Sort.Core.show.name();
        MtgDiscovery.Controls.Sort.Core.show.releaseDate();
        MtgDiscovery.Controls.Sort.Core.show.size();
        MtgDiscovery.Controls.Sort.Core.show.setPercent();
        MtgDiscovery.Controls.Sort.Core.show.cardsCollected();
    };

    return {
        apply: apply
    };
})();