(function () {
    'use strict';

    angular.module('mainmenu')
    .controller('MainMenuController', MainMenuController);

    function MainMenuController($scope, $location) {
        var viewModel = this;

        viewModel.tabs = [
            { title: "Home" },
            { title: "Login" },
            { title: "Manager" }
        ];

        viewModel.switchTab = function (index) {
            switch (index) {
                case 0: $location.path('/home'); break;
                case 1: $location.path('/login'); break;
                case 2: $location.path('/manager'); break;
            };
        };
    };

}());