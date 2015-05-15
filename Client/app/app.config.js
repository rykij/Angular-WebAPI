(function () {
    'use strict';

    angular.module('config', ['ui.router', 'LocalStorageModule'])
    .config(config, debugMessages)

    // TODO: authentication module
    //.run(['auth', function (auth) {
    //    auth.fillAuthData();
    //}]);

    function debugMessages($logProvider) {
        $logProvider.debugEnabled(true);
    };

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider
            //****HOME TAB****

            .state('home', {
                 url: '/home',
                 //An abstract state will never be directly activated, but can provide inherited properties to its common children states
                 abstract: true,
                 views: {
                     "": {
                         templateUrl: '/app/components/home/homeView.html'
                     },
                 }
             })
            .state('home.tabs', {
                //Using an empty url means that this child state will become active when its parent's url is navigated to
                url: "",
                views: {
                    'tab1@home': {
                        templateUrl: '/app/components/home/tabs/tab1/tab1.html',
                        controller: 'Tab1Controller',
                        controllerAs: 'main'
                    },
                    'tab2@home': {
                        templateUrl: '/app/components/home/tabs/tab2/tab2.html',
                        controller: 'Tab2Controller',
                        controllerAs: 'list'
                    }
                }
            })
            //****LOGIN TAB****
   
            .state('login', {
                url: '/login',
                templateUrl: '/app/components/login/loginView.html',
                controller: 'LoginController',
                controllerAs: 'form'
            })

            //****MANAGER TAB****

            .state('manager', {
                url: '/manager',
                abstract: true,
                views:{
                    "": {
                        templateUrl: '/app/components/manager/managerView.html'
                    }
                }
            })
            .state('manager.mainTab', {
                url: "",
                views: {
                    'mainTab@manager': {
                        templateUrl: '/app/components/manager/mainTab/mainTabView.html',
                        controller: 'MainTabController',
                        controllerAs: 'manager' 
                        }
                }
            })
           
        $urlRouterProvider
            .otherwise('/home'); 
    };

})();