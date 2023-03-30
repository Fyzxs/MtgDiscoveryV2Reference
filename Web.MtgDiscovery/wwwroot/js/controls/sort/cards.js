MtgDiscovery.Controls.Sort.Cards = MtgDiscovery.Controls.Sort.Cards || (function () {

        function baseSortFunctions(sortType) {
            if (sortType === "name") return alphaSort;
            if (sortType === "collector-number") return sortNumberWithVariant;
            if (sortType === "price") return price;
            if (sortType === "release-date") return releaseDateSort;
            if (sortType === "card-set") return setSort;
            return () => console.log(`unknown card sort [sortType=${sortType}] provided`);
        }

        function alphaSort(a, b) {
            return a.name.localeCompare(b.name);
        }

        function setSort(a, b) {
            const result = a.setCode.localeCompare(b.setCode);
            return result === 0 ? sortNumberWithVariant(a, b) : result;
        }

        function price(a, b) {
            if (a.price && !b.price) return 1;
            if (!a.price && b.price) return -1;
            if (!a.price && !b.price) return alphaSort(a, b);

            const result = a.price - b.price;
            return result === 0 ? alphaSort(a, b) : result;
        }

        function releaseDateSort(a, b, order) {
            const release = a.date.localeCompare(b.date);
            if (release !== 0) return release;
            return sortNumberWithVariant(a, b);
        }

        function sortNumberWithVariant(a, b) {
            const strA = a.collectorNumber;
            const strB = b.collectorNumber;
            const numA = strA.replace(/\D/g, "");
            const numB = strB.replace(/\D/g, "");
            const alphaNumPartA = strA.replace(numA.toString(), "");
            const alphaNumPartB = strB.replace(numB.toString(), "");
            const isAlphaAPrefix = strA.startsWith(alphaNumPartA) && alphaNumPartA !== "";
            const isAlphaBPrefix = strB.startsWith(alphaNumPartB) && alphaNumPartB !== "";
            let numCompare = numA - numB;

            //Put cards with AlphaPrefix last (Blame 9th Edition for this)
            if (isAlphaBPrefix && !isAlphaAPrefix) return -1;
            if (isAlphaAPrefix && !isAlphaBPrefix) return 1;

            //Put parents set cards first
            if (numCompare === 0) { //only if the same number
                numCompare = alphaNumPartA.localeCompare(alphaNumPartB);
            }

            //Sort Alpha
            return numCompare === 0 ? alphaSort(a, b) : numCompare;
        }

        function x(e) {
            const $e = $(e);
            const a = {};
            a.name = $e.data("name");
            a.rarity = $e.data("rarity");
            a.date = $e.data("release-date");
            a.setCode = $e.data("code");
            a.setCode = $e.data("code");
            a.price = parseFloat($e.data("price"));
            a.collectorNumber = $e.data("collector-number").toString();

            return a;
        }

        const adjustCardDateVisibility = (selector) => {
            const setReleaseDate = $(".set-card").data("released-at");
            if (!setReleaseDate) return;
            const setDate = setReleaseDate.trim();

            $.each($(`${selector}`).parent().children(),
                (index, obj) => {
                    const card = $(obj);
                    const cardDate = card.data("release-date");
                    const hasDifferentDates = cardDate !== setDate;
                    if (hasDifferentDates) {
                        $(`.mtg-card-release-date`).removeClass("itemHidden");
                        MtgDiscovery.Controls.Sort.Core.show.releaseDate();
                        return false;
                    }
                });
        };
    
        

        const apply = () => {
            MtgDiscovery.Controls.Sort.Core.apply(baseSortFunctions, x, () => $("#card-row"));

            MtgDiscovery.Controls.Sort.Core.show.name();
            MtgDiscovery.Controls.Sort.Core.show.number();
            MtgDiscovery.Controls.Sort.Core.show.price();
            MtgDiscovery.Controls.Sort.Core.show.releaseDate();
        };

        return {
            apply: apply,
            adjustDateVisibility: adjustCardDateVisibility
        };
    })();
