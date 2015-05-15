//APP INITIALIZATION, CONGIF, ROUTING
//Syntax convention used: https://github.com/johnpapa/angularjs-styleguide 

(function () {
    "use strict";

    var scenarioNavigator = angular.module('scenarioNavigator', [
                                    /*app modules*/
                                    'config',
                                    'auth',
                                    'mainmenu',
                                    'home',
                                    'form',
                                    'manager',
                                    'filtering',
                                    'loading',
                                    'ui.tree'
                                    /*##################*/
    ]);
}());