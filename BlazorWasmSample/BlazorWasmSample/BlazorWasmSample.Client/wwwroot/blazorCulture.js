window.blazorCulture = {
    get: () => {
        const params = new URLSearchParams(window.location.search);
        const culture = params.get('ui-culture') || params.get('culture') || window.localStorage['BlazorCulture'];

        if (culture) {
            window.localStorage['BlazorCulture'] = culture;
        }

        return culture;
    },
    set: (culture) => {
        window.localStorage['BlazorCulture'] = culture;
    }
};
