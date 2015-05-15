(function () {
    'use strict'

    angular.module('loading')
    .factory('SpinnerFactory', SpinnerFactory);

    /*
        * The spinner-factory is used by the spinner-directive to register new spinners
        * Can be used also to hide/show spinners on the page
    */
    function SpinnerFactory() {

        var SpinnerFactory = {};
        var cache = {};

        // (private) function intended for spinner directives to register themselves with the factory
        SpinnerFactory._register = function (spinnerScope) {

            // If no id is passed in, throw an exception
            if (!spinnerScope.id) {
                throw new Error('A spinner must have an ID to register with the spinner service.');
            }

            // Add spinner directive's scope to the cache
            cache[spinnerScope.id] = spinnerScope;
        },

        // (private) Exposed for manually unregister a spinner
        SpinnerFactory._unregister = function (spinnerId) {
            delete cache[spinnerId];
        },

        // (private) Remove an entire spinner group
        SpinnerFactory._unregisterGroup = function (group) {
            for (var spinnerId in cache) {
                if (cache.hasOwnProperty(spinnerId)) {
                    if (cache[spinnerId].group === group) {
                        delete cache[spinnerId];
                    }
                }
            }
        },

        // (private) Clear all spinners from the cache
        SpinnerFactory._unregisterAll = function () {
            for (var spinnerId in cache) {
                if (cache.hasOwnProperty(spinnerId)) {
                    delete cache[spinnerId];
                }
            }
        },

        // (public) Show the specified spinner
        SpinnerFactory.show = function (spinnerId, loadingText) {
            if (cache.hasOwnProperty(spinnerId)) {
                var spinnerScope = cache[spinnerId];
                spinnerScope.showSpinner = true;
                if (loadingText !== undefined) {
                    spinnerScope.loadingText = loadingText;
                }
            }
        },

        // (public) Hide the specified spinner.
        SpinnerFactory.hide = function (spinnerId, doneText) {
            if (cache.hasOwnProperty(spinnerId)) {
                var spinnerScope = cache[spinnerId];
                spinnerScope.showSpinner = false;
                if (doneText !== undefined) {
                    spinnerScope.doneText = doneText;
                }
            }
        },

        // (public) Show all spinners contained in a specified group
        SpinnerFactory.showGroup = function (group, loadingTest) {
            for (var spinnerId in cache) {
                if (cache.hasOwnProperty(spinnerId)) {
                    var spinnerScope = cache[spinnerId];
                    if (spinnerScope.group === group) {
                        this.show(spinnerId, loadingText);
                    }
                }
            }
        },

        // (public) Hide all spinners contained in a specified group
        SpinnerFactory.hideGroup = function (group, doneTest) {
            for (var spinnerId in cache) {
                if (cache.hasOwnProperty(spinnerId)) {
                    var spinnerScope = cache[spinnerId];
                    if (spinnerScope.group === group) {
                        this.hide(spinnerId, doneText);
                    }
                }
            }
        },

        // (public) Show all spinners
        SpinnerFactory.showAll = function (loadingText) {
            for (var spinnerId in cache) {
                if (cache.hasOwnProperty(spinnerId)) {
                    this.show(spinnerId, loadingText);
                }
            }
        },

        // (public) Hide all spinners
        SpinnerFactory.hideAll = function (doneText) {
            for (var spinnerId in cache) {
                if (cache.hasOwnProperty(spinnerId)) {
                    this.hide(spinnerId, doneText);
                }
            }
        }
        
        return SpinnerFactory;
    };

})();