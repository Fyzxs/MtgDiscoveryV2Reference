MtgDiscovery.Controls.Filter.AllSets = MtgDiscovery.Controls.Filter.AllSets || (function () {
    function x(e) {
        const $e = $(e);
        const a = {};
        a.name = $e.data("name");
        a.setCode = $e.data("code");
        a.setPercent = $e.data("set-percent");
        a.collectedCount = $e.data("collected-count");
        return a;
    }

    const setTextFilter = (set) => {
        const enteredText = MtgDiscovery.Controls.SearchBox.entered();
        if (!enteredText) return false;

        const compareText = enteredText.toLowerCase();
        const nameDoesNotContain = !set.name.toLowerCase().includes(compareText);
        const codeDoesNotContain = !set.setCode.toLowerCase().includes(compareText);
        return nameDoesNotContain && codeDoesNotContain;
    };

    const completionFilter = (set) => {

        const controlsVisible = $("#filterControlsCompletion").length === 1 && !$("#filterControlsCompletion").hasClass("itemHidden");

        const completedFilter = (item) => {
            const filterActive = $("#filterCompleted:checked").length === 0;
            const filterMatch = parseInt(item.setPercent) === 100;
            return filterActive && filterMatch;
        }
        
        const partialFilter = (item) => {
            const filterActive = $("#filterCollecting:checked").length === 0;
            const filterMatch = 0 < parseInt(item.collectedCount) && parseInt(item.setPercent) < 100;
            return filterActive && filterMatch;
        }
        
        const noneFilter = (item) => {
            const filterActive = $("#filterNone:checked").length === 0;
            const filterMatch = parseInt(item.collectedCount) === 0;
            return filterActive && filterMatch;
        }

        return controlsVisible && (completedFilter(set) || partialFilter(set) || noneFilter(set));
    }

    const isSetFiltered = function (setItem) {
        const set = x(setItem);
        return setTextFilter(set) || completionFilter(set);
    };

    const apply = () => {
        MtgDiscovery.Controls.Filter.Core.apply(isSetFiltered, MtgDiscovery.Containers.Sets.container);
        MtgDiscovery.Controls.Filter.Core.show.completion();
        MtgDiscovery.Controls.Filter.Core.show.text();
    };

    const adaptControls = () => {
        const allCount = $(`.set-card`).length;
        const completeCount = $(`.set-card[data-set-percent="100"]`).length;
        const noneCount = $(`.set-card[data-unique-count="0"]`).length;
        const partialCount = allCount - (completeCount + noneCount);

        if (allCount === 1) return;

        addCountToToggle("#filterNone", noneCount);
        addCountToToggle("#filterCollecting", partialCount);
        addCountToToggle("#filterCompleted", completeCount);

        $("#filterControlsCompletion").removeClass("itemHidden");
    }

    return {
        apply: apply,
        adapt: adaptControls
    };
})();