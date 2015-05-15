/// <reference path="../../../assets/js/angular/angular.js" />
/// <reference path="../../../assets/js/angular/angular-route.js" />
/// <reference path="../../../assets/js/angular/angular-ui-router.js" />
/// <reference path="../../../assets/js/angular/angular-mocks.js" />
/// <reference path="../../../assets/js/angular/angular-loading-bar.js" />
/// <reference path="../../../assets/js/angular/angular-local-storage.js" />
/// <reference path="../../../app/app.auth.js" />
/// <reference path="../../../app/app.config.js" />

/*info: http://www.rosher.co.uk/post/Unit-Testing-AngularJS-with-Jasmine-Chutzpah-and-Visual-Studio*/

describe('unit: mainmenu route', function () {

    var currState;
    var location;

    //beforeEach(function () {
    //    module('authservice', function ($provide) {
    //        $provide.value('auth', {
    //            fillAuthData: jasmine.createSpy('fillAuthData')
    //        });
    //    });

    //    inject(function (_auth_) {
    //        auth = _auth_;
    //    })
    //});

    beforeEach(module('config'));

    beforeEach(inject(function ($state, $location, $httpBackend) {
        state = $state;
        location = $location;
        httpBackend = $httpBackend;
    }));

    it('should map routes to controllers', function () {

        currState = state.get('home');
        expect(currState.url).toEqual('/home');
        expect(currState.abstract).toBe(true);
        expect(currState.views[''].templateUrl).toEqual('/app/components/home/homeView.html');

        currState = state.get('home.tabs');
        expect(currState.views['tab1@home'].templateUrl).toEqual('/app/components/home/tabs/tab1/tab1.html');
        expect(currState.views['tab1@home'].controller).toBe('Tab1Controller');
        expect(currState.views['tab1@home'].controllerAs).toBe('main');

        expect(currState.views['tab2@home'].templateUrl).toEqual('/app/components/home/tabs/tab2/tab2.html');
        expect(currState.views['tab2@home'].controller).toBe('Tab2Controller');
        expect(currState.views['tab2@home'].controllerAs).toBe('list');

        currState = state.get('login');
        expect(currState.templateUrl).toEqual('/app/components/login/loginView.html');
        expect(currState.controller).toBe('LoginController');
        expect(currState.controllerAs).toBe('form');

        currState = state.get('manager');
        expect(currState.url).toEqual('/manager');
        expect(currState.abstract).toBe(true);
        expect(currState.views[''].templateUrl).toEqual('/app/components/manager/managerView.html');

        currState = state.get('manager.mainTab');
        expect(currState.views['mainTab@manager'].templateUrl).toEqual('/app/components/manager/mainTab/mainTabView.html');
        expect(currState.views['mainTab@manager'].controller).toBe('MainTabController');
        expect(currState.views['mainTab@manager'].controllerAs).toBe('manager');

        // otherwise redirect to
        location.path('wrong_url');
        httpBackend.expectGET("/");
    });

    //it('should call authService.fillAuthData on module run', function () {
    //        expect(auth.fillAuthData).toHaveBeenCalled();
    //});

});