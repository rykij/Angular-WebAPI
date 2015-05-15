/// <reference path="../../../assets/js/angular/angular.js" />
/// <reference path="../../../assets/js/angular/angular-mocks.js" />
/// <reference path="../../../app/components/login/loginModule.js" />
/// <reference path="../../../app/components/login/loginServices.js" />
/// <reference path="../../../app/web.config.js" />

describe('unit: form_Services_', function () {
    describe('RegisterInfoService', function () {
        
        var $controller = null;
        var createService = null;
        var webConfig = null;

        var service = null;
        var result = null;
        var httpBackend = null;

        beforeEach(module('form'));
        
        //service inject (mock)
        beforeEach(module(function ($provide) {
            webConfig = {
                WEBAPI_URL: 'http://localhost:2071'
            };
            $provide.value('WEB_CONFIG', webConfig);
        }));

        beforeEach(inject(function ($controller, $injector, RegisterInfoService) {
            $controller = $controller;

            createService = function () {
                return $injector.get('RegisterInfoService');
            }

            service = RegisterInfoService;
            result = service.getRegisterInfo();

            httpBackend = $injector.get('$httpBackend');

        }));

        it('should_be_defined', function () {
            var service = createService();
            expect(service).toBeTruthy();
        });

        it('should_getRegisterInfo_function_be_defined', function () {
            expect(service.getRegisterInfo).toBeDefined()
            expect(service.getRegisterInfo).toEqual(jasmine.any(Function))
        });

        it('should_have_true_returned_for_getRegisterInfo_function', function () {
            var post = { UserName: 'abc', Password: '123' };
            var url = webConfig.WEBAPI_URL + '/api/DomainAuth/IsDomainUser';
            httpBackend.when('POST', url,
                function (postData) {
                    jsonData = angular.fromJson(postData);
                    expect(jsonData.UserName).toBe(post.UserName);
                    expect(jsonData.Password).toBe(post.Password);
                    return true;
                }
            ).respond(200, true);

            service.getRegisterInfo(post).then(function (response) {
                expect(response).toBeTruthy();
            });

            httpBackend.flush();
        });

    });
});