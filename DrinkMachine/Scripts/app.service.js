window.app = window.todoApp || {};

window.app.service = (function () {
    var baseUri = '/api/drinks/';
    var serviceUrls = {
        drinks: function () { return baseUri ; },
        //byGenre: function (genre) { return baseUri + '?genre=' + genre; },
        buy: function (id) { return baseUri + id; }
    }

    function ajaxRequest(type, url, data) {
        var options = {
            url: url,
            headers: {
                Accept: "application/json"
            },
            contentType: "application/json",
            cache: false,
            type: type,
            data: data ? ko.toJSON(data) : null
        };
        return $.ajax(options);
    }

    return {
        allDrinks: function () {
            return ajaxRequest('get', serviceUrls.drinks());
        },
        //byGenre: function (genre) {
        //    return ajaxRequest('get', serviceUrls.byGenre(genre));
        //},
        buyDrink: function (item) {
            return ajaxRequest('post', serviceUrls.buy(item.ID), item);
        }
    };
})();