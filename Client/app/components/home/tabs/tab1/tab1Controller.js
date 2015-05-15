(function () {
    'use strict';

    angular.module('home')
    .controller('Tab1Controller', Tab1Controller);

    function Tab1Controller($scope) {
        /* The 'this' keyword is contextual and when used within a function inside a controller 
        may change its context. Capturing the context of this avoids encountering this problem */
        var viewModel = this;
        viewModel.header = {}
        viewModel.header.title = 'AppTest MVC Angular'
    };
})();