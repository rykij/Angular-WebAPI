/// <reference path="../../../assets/js/angular/angular.js" />
/// <reference path="../../../assets/js/angular/angular-mocks.js" />
/// <reference path="../../../app/components/login/loginModule.js" />
/// <reference path="../../../app/components/login/loginController.js" />
/// <reference path="../../../app/components/login/loginDirective.js" />

describe('unit: form_Controllers_', function () {
    var scope, controller;

    beforeEach(module('form'));

    //service inject (mock)
    beforeEach(module(function ($provide) {
        var service = {
            //getJobStatus: function () { }
        };
        $provide.value('RegisterInfoService', service);
    }));

    describe('LoginController', function () {

        beforeEach(inject(function ($controller, $rootScope) {
            // Create a new scope that's a child of the $rootScope
            scope = $rootScope.$new();
            // Create the controller with ControllerAs syntax
            controller = $controller('LoginController as form', {
                $scope: scope
            });
        }));

        it('should_be_defined', function () {
            expect(scope.form.login).toBeDefined;
        });

        it('should_have_emailAddress_defined', function () {
            scope.form.login.username = 'corpgen\\e3rporto';
            expect(scope.form.login.username).toEqual('corpgen\\e3rporto');
        });

        it('should_have_password_defined', function () {
            scope.form.login.pwd = 'abc123';
            expect(scope.form.login.pwd).toEqual('abc123');
        });

        it('should_have_password_confirmed_defined', function () {
            scope.form.login.confirmpwd = 'abc123';
            expect(scope.form.login.confirmpwd).toEqual('abc123');
        });

        it('should_change_checkbox_state', function () {
            scope.form.login.rememberMe = true;
            expect(scope.form.login.rememberMe).toEqual(true);

            scope.form.login.rememberMe = false
            expect(scope.form.login.rememberMe).toEqual(false);
        });

        it('should_message_be_defined', function () {
            expect(scope.form.message).toBeDefined;
        });

    });
});