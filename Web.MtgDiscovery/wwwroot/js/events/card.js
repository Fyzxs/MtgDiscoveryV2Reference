var MtgDiscovery = MtgDiscovery || {};
MtgDiscovery.Events = MtgDiscovery.Events || {};
MtgDiscovery.Events.Card = MtgDiscovery.Events.Card || {};

MtgDiscovery.Events.Card.Update = MtgDiscovery.Events.Card.Update || (function () {
    const updateFilters = () => {
        MtgDiscovery.Controls.Filter.Rarity.Show.adapt();
        MtgDiscovery.Controls.Filter.Quantity.Show.adapt();
        MtgDiscovery.Controls.Filter.SearchBox.Show.adapt();
    };

    const updateCounts = async (selector, usQuantity) => {
        const regex = /[^-0-9]/g;
        const quantity = (usQuantity) ? usQuantity.replace(regex, "") : "0";
        const cardId = $(selector).data("id");
        $(selector).data("total-count", parseInt($(selector).data("total-count")) + parseInt(quantity));
        const collectorId = MtgDiscovery.Url.collector();
        const result = await MtgDiscovery.Api.collection.counts(cardId, collectorId);

        $(`#card-${cardId}-collector`).html(result.value);
    }

    const filterCard = (selector) => MtgDiscovery.Controls.Filter.Core.run(selector);

    const counts = async (selector, quantity) => {
        await updateCounts(selector, quantity);
        updateFilters();
        filterCard(selector);
    };

    return {
        counts: counts
    };

})();

MtgDiscovery.Events.Card.Image = MtgDiscovery.Events.Card.Image || (function () {

    const removeZoom = () => {
        $(`#centerDisplay`).addClass("itemHidden");
    }

    $("#centerDisplay").click(removeZoom);

    const apply = (selector) => {
        const embiggen = $(`${selector} .mtg-card-embiggen`);
        embiggen.click(() => {
            if (embiggen.hasClass("zoomed")) {
                removeZoom();
                const container = $(`#centerDisplay`);
                container.addClass("itemHidden");
                $("#contentsGoHere").html("");
                embiggen.removeClass("zoomed");
            } else {
                const content = $(`${selector}-englarged-content`);
                const container = $(`#centerDisplay`);
                container.removeClass("itemHidden");
                $("#contentsGoHere").html(content.html());
                embiggen.addClass("zoomed");
            }
        });
    };

    return {
        apply: apply,
        removeZoom: removeZoom
    };

})();

MtgDiscovery.Events.Card.Flip = MtgDiscovery.Events.Card.Flip || (function () {

    const apply = (selector) => {
        const flipper = $(`${selector} .mtg-card-flippen`);
        flipper.click((e) => {
            const clicked = $(e.currentTarget);
            const images = $(clicked.parent().find(`.mtg-card-img`));
            const frontFace = "front";
            const backFace = "back";
            const currentFace = "current-face";
            const toFace = $(images[0]).data(currentFace) === frontFace ? backFace : frontFace;
            const fromFace = toFace === frontFace ? backFace : frontFace;
            clicked.removeClass(`flippen-${fromFace}`);
            clicked.addClass(`flippen-${toFace}`);
            images.each(function (index, element) {
                const img = $(element);
                img.attr("src", img.data(`${toFace}-src`));
                img.data(currentFace, toFace);
            });
        });
    };

    return {
        apply: apply
    };

})();

MtgDiscovery.Events.Card.Collect = MtgDiscovery.Events.Card.Collect || (function () {
    const toasts = (function() {
        function countUpdateSuccessToast(msg) {
            return Toastify({
                text: msg,
                duration: 10 * 1000,
                gravity: "top",
                position: "right",
                style: {
                    background: "#00b09b"
                },
                stopOnFocus: true
            });
        }

        function countUpdateFailedToast(msg) {
            return Toastify({
                text: msg,
                duration: 20 * 1000,
                gravity: "top",
                position: "right",
                style: {
                    background: "#ff5f6d"
                },
                stopOnFocus: true
            });
        }

        return {
            success: countUpdateSuccessToast,
            failure: countUpdateFailedToast
        }
    })();

    const internalUpdate = (() => {

        const displayUpdateResult = async (cardObj, resultObj, updateValue, cardSelector, options) => {

            const displayUpdateFailure = (cardObj, resultObj, updateValue) => {
                if (resultObj.err === undefined) return;

                toasts.failure(`[${cardObj.data("name")}] update failed!`).showToast();

                const delay = 300;
                cardObj.addClass("updateFailed");
                cardObj.fadeOut(delay / 3).fadeIn(delay).fadeOut(delay / 3).fadeIn(delay).fadeOut(delay / 3)
                    .fadeIn(delay).fadeOut(delay / 3).fadeIn(delay, () => cardObj.removeClass("updateFailed"));
                cardObj.find(".last-entered").html(`<span style="color:red;">${updateValue}</span>`);
            }

            const displayUpdateSuccess = async (cardObj, resultObj, updateValue) => {
                if (resultObj.value !== "") return;
                if(options === undefined || options.toastSuccess === undefined || options.toastSuccess === true) toasts.success(`[${cardObj.data("name")}] successfully updated!`).showToast();

                const delay = 300;
                cardObj.fadeOut(delay / 2).fadeIn(delay).fadeOut(delay / 2).fadeIn(delay);
                await MtgDiscovery.Events.Card.Update.counts(cardSelector, updateValue);
                cardObj.find(".last-entered").html(updateValue);
            }

            displayUpdateFailure(cardObj, resultObj, updateValue);
            await displayUpdateSuccess(cardObj, resultObj, updateValue, cardSelector);
        };

        const dynamicSubmit = async (cardSelector, updateInput, options) => {
            const cardObject = $(cardSelector);
            const cardId = cardObject.data("id");
            const setId = cardObject.data("set-id");
            const collectorId = MtgDiscovery.Url.collector();

            const promise = MtgDiscovery.Api.collection.adjust(cardId, setId, updateInput, collectorId);

            const updateResult = await promise;
            await displayUpdateResult(cardObject, updateResult, updateInput, cardSelector, options);
        };

        return {
            submit: dynamicSubmit
        };
    })();

    const cardUpdates = (selector) => {

        const targetSelector = selector;
        let userInput = "";
        let inProgress = false;
        const display = () => $(`${targetSelector}-input`);

        const displayInput = () => userInput
            .replace("s", "(signed) ")
            .replace("a", "(artist alter) ")
            .replace("p", "(artist proof) ");
        const clearDisplay = () => {
            userInput = "";
            display().html(displayInput());
            $(`${targetSelector} .submitButton`).addClass("itemHidden");
        };
        const updateDisplay = () => display().html(displayInput());

        const fromButton = (val) => {
            const oldVal = parseInt(userInput);
            let newVal = parseInt(val);
            if (Number.isInteger(oldVal)) newVal += oldVal;
            userInput = newVal.toString();
            if (newVal === 0) userInput = "";

            updateDisplay();

            if (newVal === 0) $(`${targetSelector} .submitButton`).addClass("itemHidden");
            else $(`${targetSelector} .submitButton`).removeClass("itemHidden");
        }

        const lostFocus = () => {
            userInput = "";
            updateDisplay();
        }

        const isCardAdjustmentValue = (e) => {
            const key = e.keyCode;
            return 48 <= key && key <= 57 // Keyboard 0-9
                || 96 <= key && key <= 105 //num pad 0-9
                ;
        }
        const isCardAdjustmentModifier = (e) => {
            const key = e.key;
            return key === "-" || key === "+";
        }
        const isCardAdjustmentTypeValue = (key) => {
            return key === "s" //signed
                || key === "p" //artist proof
                || key === "a" //altered
                ;
        }
        const isCardAdjustmentType = (e) => {
            const key = e.key;
            return isCardAdjustmentTypeValue(key);
        }

        const acceptInput = (e) => {
            const isValue = isCardAdjustmentValue(e);
            const isModifier = isCardAdjustmentModifier(e);
            const isType = isCardAdjustmentType(e);
            const invalidKey = !isValue && !isModifier && !isType;
            const hasUserInput = userInput !== "";
            const hasOnlyType = isCardAdjustmentTypeValue(userInput);

            if (hasUserInput && isModifier && !hasOnlyType) return false;
            if (hasUserInput && isType) return false;
            if (invalidKey) return false;

            return true;
        }
        const rejectInput = (e) => !acceptInput(e);


        var championDate = new Date();
        const submit = async () => {
            const challengerDate = new Date();
            if (challengerDate - championDate < 250) return;
            inProgress = true;
            //const cardObject = $(targetSelector);
            //const cardId = cardObject.data("id");
            //const setId = $(".set-card").data("id");
            //const collectorId = MtgDiscovery.Url.collector();
            //const updateValue = userInput;
            const updatePromise = internalUpdate.submit(targetSelector, userInput);//Added

            //const promise = MtgDiscovery.Api.collection.adjust(cardId, setId, updateValue, collectorId);

            userInput = "...working...";
            updateDisplay();
            await updatePromise;//added
            //const updateResult = await promise;
            //displayUpdateResult(cardObject, updateResult, updateValue);

            clearDisplay();
            inProgress = false;
        }
        const handleClear = (e) => {
            if (e.keyCode !== 27) return;//esc
            clearDisplay();
        }
        const handleBackspace = (e) => {
            if (e.keyCode !== 8) return;//esc

            userInput = userInput.slice(0, -1);
            updateDisplay();
        }
        const handleEnter = async (e) => {
            if (e.keyCode !== 13) return;//enter
            await submit();
        }
        const handleInput = (e) => {
            if (rejectInput(e)) return;
            userInput += e.key;
            updateDisplay();
        }

        const prepToCollect = async (e) => {
            if (inProgress) return;
            handleClear(e);
            handleBackspace(e);
            handleInput(e);
            await handleEnter(e);
        }

        const applyEvents = () => {
            $(targetSelector).unbind('keyup');
            $(targetSelector).keyup(prepToCollect);
            $(targetSelector).blur(lostFocus);
            $(targetSelector).focus(lostFocus);
            $(`${targetSelector} .incButton`).unbind('click');
            $(`${targetSelector} .decButton`).unbind('click');
            $(`${targetSelector} .submitButton`).unbind('click');
            $(`${targetSelector} .incButton`).click(() => fromButton(1));
            $(`${targetSelector} .decButton`).click(() => fromButton(-1));
            $(`${targetSelector} .submitButton`).click(submit);
        };

        return {
            apply: applyEvents
        }
    }

    const applyThings = (selector) => {
        cardUpdates(selector).apply();
    }
    return {
        apply: applyThings,
        internal: internalUpdate
    };
})();

MtgDiscovery.Events.Card.Navigate = MtgDiscovery.Events.Card.Navigate || (function () {
    function navigateGrid(targetElement, direction) {
        if (targetElement === "") return;
        const active = $(`#${targetElement}`);
        const grid = active.parent();
        const gridChildren = Array.from(grid.children());
        let activeIndex = -1;
        const gridNum = gridChildren.length;

        for (let index = 0; index < gridChildren.length; index++) {
            if (gridChildren[index].id !== targetElement) continue;
            activeIndex = index;
            break;
        }

        let offSetItem = undefined;
        for (let i = 0; i < gridNum; i++) {
            const temp = gridChildren[i];
            const item = $(temp);
            if (!item.hasClass("itemShown")) continue;

            offSetItem = temp;
            break;
        }
        if (offSetItem === undefined) return;

        const baseOffset = offSetItem.offsetTop;
        const breakIndexInitial = gridChildren.findIndex(item => item.offsetTop > baseOffset);
        let hiddenChildren = 0;
        for (let x = 0; x < breakIndexInitial; x++) {
            if ($(gridChildren[x]).hasClass("itemHidden")) {
                hiddenChildren++;
            }
        }
        const breakIndex = breakIndexInitial - hiddenChildren;

        const numPerRow = (breakIndex === -1 ? gridNum : breakIndex);

        const updateActiveItem = (curIndex, moveCount) => {
            let moved = 0;
            let hiddenCount = 0;
            const stepDirection = moveCount < 0 ? -1 : 1;
            let champion = undefined;
            const movedCheck = moveCount < 0 ? -moveCount : moveCount;
            const startIndex = curIndex + stepDirection;
            while (moved < movedCheck && hiddenCount <= gridChildren.length) {
                const nextStep = (moved + hiddenCount) * stepDirection;
                const curIndex = startIndex + nextStep;
                if (gridNum <= curIndex) break;
                const challenger = $(gridChildren[curIndex]);
                if (!challenger.hasClass("itemShown")) {
                    hiddenCount++;
                    continue;
                }
                moved++;
                champion = challenger;
            }
            if (champion !== undefined) champion.focus();
        };

        const isTopRow = activeIndex <= numPerRow - 1;
        const isBottomRow = activeIndex >= gridNum - numPerRow;
        const isLeftColumn = activeIndex % numPerRow === 0;
        const isRightColumn = activeIndex % numPerRow === numPerRow - 1 || activeIndex === gridNum - 1;

        switch (direction) {
            case "up":
                if (!isTopRow)
                    updateActiveItem(activeIndex, -numPerRow);
                break;
            case "down":
                if (!isBottomRow)
                    updateActiveItem(activeIndex, numPerRow);
                break;
            case "left":
                if (0 < activeIndex)
                    updateActiveItem(activeIndex, -1);
                break;
            case "right":
                if (activeIndex + 1 < gridNum)
                    updateActiveItem(activeIndex, 1);
                break;
        }
    }

    const cardKeyup = (e) => {
        if (e.repeat) return;
        /* nav by arrows */
        let direction = undefined;
        switch (e.keyCode) {
            case 37: //<-
            case 74: //j
            case 106: //* (on numpad)
                direction = "left";
                break;
            case 38:
            case 73: //i
                direction = "up";
                break;
            case 39: //->
            case 76: //l
                direction = "right";
                break;
            case 40:
            case 75: //k
                direction = "down";
                break;

        }
        if (direction !== undefined) {
            navigateGrid(e.target.id, direction);
        }
    };

    const focusEvent = (selector) => () => {
        const $this = $(selector);
        const number = $this.offset().top - (window.innerHeight / 2) + ($this.height() / 2);
        $([document.documentElement, document.body]).animate({
            scrollTop: number
        },
            1);
    };

    const applyFocusEvents = (selector) => $(selector).focus(focusEvent(selector));
    const applyNavEvents = (selector) => $(selector).keydown(cardKeyup);

    const applyEvents = (selector) => {
        applyFocusEvents(selector);
        applyNavEvents(selector);
    };


    return {
        apply: applyEvents
    };
})();

MtgDiscovery.Events.Card.Apply = MtgDiscovery.Events.Card.Apply || (function () {

    const applyEvents = (selector) => {
        MtgDiscovery.Events.Card.Navigate.apply(selector);
        MtgDiscovery.Events.Card.Collect.apply(selector);
        MtgDiscovery.Events.Card.Image.apply(selector);
        MtgDiscovery.Events.Card.Flip.apply(selector);
    };
    return {
        apply: applyEvents
    };
})();