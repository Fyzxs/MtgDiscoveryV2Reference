var MtgDiscovery = MtgDiscovery || {};

MtgDiscovery.Api = MtgDiscovery.Api || (function () {
    var unloading = false;

    $(window).on("unload", function () { unloading = true; });

    const apiPaths = (function () {
        const paths = {};
        paths.data = {};
        paths.data.set = {};
        paths.data.card = {};
        paths.view = {};
        paths.view.set = {};
        paths.view.card = {};
        paths.collection = {};
        paths.search = {};

        paths.data.set.allSummary = (collector) => `/api/sets/AllSetCodes?collectorInUrl=${collector}`;
        paths.view.set.loading = () => `/api/allsets/Loading2`;
        paths.view.set.display = (src, collector) => `/api/allsets/Display?setCode=${src.code}&setId=${src.id}&collectorInUrl=${collector}`;
        paths.view.set.displayLimited = (src, collector) => `/api/allsets/DisplayLimited?setCode=${src.code}&collectorInUrl=${collector}`;

        paths.data.card.setCardsSummary = (src, collector) => `/api/cards/SetCardsSummary?setCode=${src.code}&collectorInUrl=${collector}`;
        paths.data.card.artistCardsSummary = (artist) => `/api/cards/ArtistCardsSummary?artist=${artist}`;
        paths.data.card.cardVersionsSummary = (cardName) => `/api/cards/CardVersionsSummary?cardName=${cardName}`;
        paths.view.card.loading = (src) => `/api/cards/Loading?code=${src.code}&id=${src.id}&name=${src.name}&collectorNumber=${src.collector_number}&rarity=${src.rarity}&total=${src.total}&price=${src.current_price}&releasedAt=${src.released_at}`;
        paths.view.card.display = (src, collector) => `/api/cards/Display?id=${src.id}&collectorInUrl=${collector}`;

        paths.collection.adjust = (cardId, setId, input, collector) => `/api/collector/adjustcard?cardId=${cardId}&setId=${setId}&userInput=${input}&collectorInUrl=${collector}`;
        paths.collection.cardCounts = (cardId, collector) => `/api/collector/cardcounts?cardId=${cardId}&collectorInUrl=${collector}`;
        paths.collection.setCounts = (setId, setCode, collector) => `/api/collector/SetCollection?setId=${setId}&setCode=${setCode}&collectorInUrl=${collector}`;

        paths.search.artist = (searchTerm) => `/api/search/artist?searchTerm=${searchTerm}`;
        paths.search.card = (searchTerm) => `/api/search/card?searchTerm=${searchTerm}`;


        return {
            data: paths.data,
            view: paths.view,
            collection: paths.collection,
            search: paths.search
        }
    })();

    // ReSharper disable once InconsistentNaming
    const _errorLogging = async function (url, runMe) {
        try {
            return await runMe();
        } catch (error) {
            if (unloading) return {}; // Ignore errors caused by navigating away
            console.log(`Error calling [url=${url}] with ${error}`);
            console.log({ error });
            return { err: error };
        }
    };
    const doWork = (url) => async () => {
        const result = await $.get(url);
        return { value: result };
    };

    const setData = (function () {
        async function allSetsSummary(collector) {
            const url = apiPaths.data.set.allSummary(collector);

            return await _errorLogging(url, doWork(url));
        }

        return {
            allSetsSummary: allSetsSummary
        }
    })();

    const cardData = (function () {
        async function setCardsSummary(src, collector) {
            const url = apiPaths.data.card.setCardsSummary(src, collector);

            return await _errorLogging(url, doWork(url));
        }

        async function artistCardsSummary(artist) {
            const url = apiPaths.data.card.artistCardsSummary(artist);

            return await _errorLogging(url, doWork(url));
        }

        async function cardVersionsSummary(cardName) {
            const url = apiPaths.data.card.cardVersionsSummary(cardName);

            return await _errorLogging(url, doWork(url));
        }

        return {
            setCardsSummary: setCardsSummary,
            artistCardsSummary: artistCardsSummary, 
            cardVersionsSummary: cardVersionsSummary
        }
    })();

    const setViews = (function () {

        async function loading(setCode) {
            const url = apiPaths.view.set.loading(setCode);

            return await _errorLogging(url, doWork(url));
        }

        async function display(src, collector) {
            const url = apiPaths.view.set.display(src, collector);

            return await _errorLogging(url, doWork(url));
        }

        async function displayLimited(src, collector) {
            const url = apiPaths.view.set.displayLimited(src, collector);

            return await _errorLogging(url, doWork(url));
        }

        return {
            loading: loading,
            display: display,
            displayLimited: displayLimited
        }
    })();

    const cardViews = (function () {

        async function loading(src) {
            const url = apiPaths.view.card.loading(src);

            return await _errorLogging(url, doWork(url));
        }

        async function display(src, collector) {
            const url = apiPaths.view.card.display(src, collector);

            return await _errorLogging(url, doWork(url));
        }

        return {
            loading: loading,
            display: display
        }
    })();

    const searchData = (function () {

        async function artist(searchTerm) {
            const url = apiPaths.search.artist(searchTerm);

            return await _errorLogging(url, doWork(url));
        }
        async function card(searchTerm) {
            const url = apiPaths.search.card(searchTerm);

            return await _errorLogging(url, doWork(url));
        }
        return {
            artist: artist,
            card: card
        }
    })();

    const dataApi = (function () {
        return {
            set: setData,
            card: cardData,
            search: searchData
        }
    })();

    const viewApi = (function () {
        return {
            set: setViews,
            card: cardViews
        }
    })();

    const collectionApi = (function () {
        async function adjustCard(cardId, setId, input, collectorId) {
            const url = apiPaths.collection.adjust(cardId, setId, input, collectorId);

            return await _errorLogging(url, doWork(url));
        }
        async function cardCounts(cardId, collectorId) {
            const url = apiPaths.collection.cardCounts(cardId, collectorId);

            return await _errorLogging(url, doWork(url));
        }
        async function setCounts(setId, setCode, collectorId) {
            const url = apiPaths.collection.setCounts(setId, setCode, collectorId);

            return await _errorLogging(url, doWork(url));
        }
        return {
            adjust: adjustCard,
            counts: cardCounts,
            setCounts: setCounts
        }
    })();

    return {
        data: dataApi,
        view: viewApi,
        user: () => { },
        collection: collectionApi
    };
})();