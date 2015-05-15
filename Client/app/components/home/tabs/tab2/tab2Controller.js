(function () {
    'use strict';

    angular.module('home')
    .controller('Tab2Controller', Tab2Controller);

    function Tab2Controller($scope) {
        var viewModel = this;
        viewModel.elementList = [
            { name: 'List Element 1' },
            { name: 'List Element 2' },
            { name: 'List Element 3' },
            { name: 'List Element 4' }
        ];

        viewModel.getList = function () {
            return viewModel.elementList;
        };
    };

})();