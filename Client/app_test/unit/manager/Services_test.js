/// <reference path="../../../assets/js/angular/angular.js" />
/// <reference path="../../../assets/js/angular/angular-mocks.js" />
/// <reference path="../../../app/components/manager/managerModule.js" />
/// <reference path="../../../app/components/manager/managerServices.js" />
/// <reference path="../../../app/web.config.js" />

describe('unit: manager_Services_', function () {
   
    var createService = null;
    var webConfig = null;

    var service = null;
    var result = null;
    var httpBackend = null;

    beforeEach(module('manager'));
    var $controller;

    //service inject (mock)
    beforeEach(module(function ($provide) {
        webConfig = {
            WEBAPI_URL: 'http://localhost:2071'
        };
        $provide.value('WEB_CONFIG', webConfig);
    }));

    beforeEach(inject(function ($controller) {
        $controller = $controller;
    }));

    describe('JobInfoService', function () {
       
        //registering component
        beforeEach(inject(function ($injector, JobInfoService) {
            createService = function () {
                return $injector.get('JobInfoService');
            }

            service = JobInfoService;
            result = service.getJobStatus();

            httpBackend = $injector.get('$httpBackend');

        }));

        it('should_be_defined', function () {
            var service = createService();
            expect(service).toBeTruthy();
        });

        it('should_getJobStatus_function_be_defined', function() {
            expect(service.getJobStatus).toBeDefined()
            expect(service.getJobStatus).toEqual(jasmine.any(Function))
        });

        it('should_have_true_returned_for_getJobStatus_function', function () {
            var get = { ID: 1234, Name: 'Job Test' ,Status: 100, Group: 'Group31' };
            var url = webConfig.WEBAPI_URL + '/api/Job/GetJobStatus';
            httpBackend.when('GET', url,
                function (getData) {
                    jsonData = angular.fromJson(getData);
                    expect(jsonData.ID).toBe(get.ID);
                    expect(jsonData.Name).toBe(get.Name);
                    expect(jsonData.Status).toBe(get.Status);
                    expect(jsonData.Group).toBe(get.Group);
                    return true;
                }
            ).respond(200, true);

            service.getJobStatus(get).then(function (response) {
                expect(response).toBeTruthy();
            });

            httpBackend.flush();
        });
    });

    describe('JobRunnerService', function () {

        //registering component
        beforeEach(inject(function ($injector, JobRunnerService) {
            createService = function () {
                return $injector.get('JobRunnerService');
            }

            service = JobRunnerService;
            result = service.runJob();

            httpBackend = $injector.get('$httpBackend');

        }));

        it('should_be_defined', function () {
            var service = createService();
            expect(service).toBeTruthy();
        });

        it('should_runJob_function_be_defined', function () {
            expect(service.runJob).toBeDefined()
            expect(service.runJob).toEqual(jasmine.any(Function))
        });

        it('should_have_true_returned_for_runJob_function', function () {
            var post = 'Job is running!';
            var url = webConfig.WEBAPI_URL + '/api/Job/RunJob';
            httpBackend.when('POST', url,
                function (postData) {
                    expect('Job is running!').toBe(post);
                    return true;
                }
            ).respond(200, true);

            service.runJob(post).then(function (response) {
                expect(response).toBeTruthy();
            });

            httpBackend.flush();
        });

    });

    describe('ScenarioListService', function () {

        //registering component
        beforeEach(inject(function ($injector, ScenarioListService) {
            createService = function () {
                return $injector.get('ScenarioListService');
            }

            service = ScenarioListService;
            result = service.get();

            httpBackend = $injector.get('$httpBackend');

        }));

        it('should_be_defined', function () {
            var service = createService();
            expect(service).toBeTruthy();
        });

        it('should_get_function_be_defined', function () {
            expect(service.get).toBeDefined()
            expect(service.get).toEqual(jasmine.any(Function))
        });

        it('should_have_true_returned_for_get_function', function () {
            var get = [
                { scenario1: 'scenario1'},
                { scenario2: 'scenario2' },
                { scenario3: 'scenario3' },
                { scenario4: 'scenario4' }
            ];
            var url = webConfig.WEBAPI_URL + '/api/Entity/GetAllScenarios';
            httpBackend.when('GET', url,
                function (getData) {
                    jsonData = angular.fromJson(getData);
                    expect(get.length).toBeGreaterThan(0);
                    return true;
                }
            ).respond(200, true);

            service.get(get).then(function (response) {
                expect(response).toBeTruthy();
            });

            httpBackend.flush();
        });

    });
});