/// <reference path="../../../assets/js/angular/angular.js" />
/// <reference path="../../../assets/js/angular/angular-mocks.js" />
/// <reference path="../../../app/components/home/homeModule.js" />
/// <reference path="../../../app/components/home/tabs/tab1/tab1Controller.js" />
/// <reference path="../../../app/components/home/tabs/tab2/tab2Controller.js" />

describe('unit: home_Controllers_', function () {

    var scope, controller;

    beforeEach(module('home'));

    describe('Tab1Controller', function () {
        
        beforeEach(inject(function ($controller, $rootScope) {
            // Create a new scope that's a child of the $rootScope
            scope = $rootScope.$new();
            // Create the controller with ControllerAs syntax
            controller = $controller('Tab1Controller as main', {
                $scope: scope
            });
        }));

        it('should_have_header_defined', function () {
            expect(scope.main.header).toBeDefined;
        });

        it('should_have_title_defined', function () {
            expect(scope.main.header.title).toEqual('AppTest MVC Angular');
        });
    });

    describe('Tab2Controller', function () {

        beforeEach(inject(function ($controller, $rootScope) {
            // Create a new scope that's a child of the $rootScope
            scope = $rootScope.$new();
            // Create the controller with ControllerAs syntax
            controller = $controller('Tab2Controller as list', {
                $scope: scope
            });
        }));

        it('should_have_elementList_defined', function () {
            expect(scope.list.elementList).toBeDefined;
        });

        it('should_have_getList_function_defined', function () {
            expect(scope.list.getList()).toBeDefined();
        });

    });
});
