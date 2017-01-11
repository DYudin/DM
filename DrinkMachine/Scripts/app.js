(function () {

    ko.observable.fn.store = function () {
        var self = this;
        var oldValue = self();

        var observable = ko.computed({
            read: function () {
                return self();
            },
            write: function (value) {
                oldValue = self();
                self(value);
            }
        });

        this.revert = function () {
            self(oldValue);
        }
        this.commit = function () {
            oldValue = self();
        }
        return this;
    }

    // Creates an observable version of the movie model.
    // Initialize with a JSON object fetched from the server.
    function drink(data) {
        var self = this;
        data = data || {};

        // Data from model
        self.ID = data.ID;
        self.Name = ko.observable(data.Name).store();
        self.Count = ko.observable(data.Count).store();
        self.Cost = ko.observable(data.Cost).store();
        //self.Genre = ko.observable(data.Genre).store();

        // Local (client) data
        //self.editing = ko.observable(false);
    };

    var ViewModel = function () {
        var self = this;

        // View-model observables
        self.drinks = ko.observableArray();
        self.error = ko.observable();
        //self.genre = ko.observable();  // Which genre the user is currently browsing.

        //self.genres = ['Action', 'Drama', 'Fantasy', 'Horror', 'Romantic Comedy'];
        //self.ratings = ['G', 'PG', 'PG-13', 'R', 'NC-17'];

        // Adds a JSON array of movies to the view model.
        function addDrinks(data) {
            var mapped = ko.utils.arrayMap(data, function (item) {
                return new drink(item);
            });
            self.drinks(mapped);
        }

        // Callback for error responses from the server.
        function onError(error) {
            self.error('Error: ' + error.status + ' ' + error.statusText);
        }

        // Fetches a list of movies by genre and updates the view model.
        //self.getByGenre = function (genre) {
        //    self.error(''); // Clear the error
        //    self.genre(genre);
        //    app.service.byGenre(genre).then(addMovies, onError);
        //};

        self.buy = function (item) {
            app.service.buyDrink(item).then(
               function () {
                   //commitChanges(item);
               },
               function (error) {
                   onError(error);
                   //revertChanges(item);
               }).always(function () {
                   //item.editing(false);
               });
        }

        //self.edit = function (item) {
        //    item.editing(true);
        //};

        //self.cancel = function (item) {
        //    revertChanges(item);
        //    item.editing(false);
        //};

        //self.save = function (item) {
        //    app.service.update(item).then(
        //        function () {
        //            commitChanges(item);
        //        },
        //        function (error) {
        //            onError(error);
        //            revertChanges(item);
        //        }).always(function () {
        //            item.editing(false);
        //        });
        //}

        function applyFn(item, fn) {
            for (var prop in item) {
                if (item.hasOwnProperty(prop) && item[prop][fn]) {
                    item[prop][fn].apply();
                }
            }
        }

        function commitChanges(item) { applyFn(item, 'commit'); }
        function revertChanges(item) { applyFn(item, 'revert'); }
        // Initialize the app by getting the first genre.
        //self.getByGenre(self.genres[0]);
    }

    // Create the view model and tell Knockout to apply the data-bindings.
    ko.applyBindings(new ViewModel());
})();