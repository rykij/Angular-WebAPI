/// <reference path="../../../assets/js/angular/angular.js" />
/// <reference path="../../../assets/js/angular/angular-mocks.js" />
/// <reference path="../../../app/components/manager/managerModule.js" />
/// <reference path="../../../app/components/manager/mainTab/mainTabController.js" />

describe('unit: manager_Controllers_', function () {
    var scope = null;
    var controller = null;
   
    beforeEach(module('manager'));

    //modules injection
    beforeEach(module(function ($provide) {
        var jobInfoService = {
            //getJobStatus: function () {}
        };
        var jobRunnerService = {};
        var scenarioListService = {};
        var spinnerFactoryService = {};

        $provide.value('JobInfoService', jobInfoService);
        $provide.value('JobRunnerService', jobRunnerService);
        $provide.value('JobAbortService', jobRunnerService);
        $provide.value('ScenarioListService', scenarioListService);
        $provide.value('SpinnerFactory', spinnerFactoryService);
    }));
  
    describe('MainTabController', function () {
        beforeEach(inject(function ($controller, $rootScope) {
            // Create a new scope that's a child of the $rootScope
            scope = $rootScope.$new();
            // Create the controller with ControllerAs syntax
            controller = $controller('MainTabController as manager', {
                $scope: scope
            });

            //object returned from service response(mock)
            scope.manager.job = {
                Id: '',
                Name: '',
                Status: '',
                Group: ''
            };
        }));

        it('should_job_be_defined', function () {
            expect(scope.manager.job).toBeDefined();
        });

        it('should_have_id_defined', function () {
            scope.manager.job.Id = 1234;
            expect(scope.manager.job.Id).toEqual(1234);
        });

        it('should_have_name_defined', function () {
            scope.manager.job.Name = 'Job Test';
            expect(scope.manager.job.Name).toEqual('Job Test');
        });

        it('should_have_status_defined', function () {
            scope.manager.job.Status = 100;
            expect(scope.manager.job.Status).toEqual(100);
        });

        it('should_have_group_defined', function () {
            scope.manager.job.Group = 'Group31';
            expect(scope.manager.job.Group).toEqual('Group31');
        });
    });
    
});