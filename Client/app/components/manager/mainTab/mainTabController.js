(function () {
    'use strict';

    angular.module('manager')
   .controller('MainTabController', MainTabController);
 
    //Syntax for minimize javascript with no module reference problems douring injection
    /*
    .controller('ManagerController',[
        '$scope', 'JobInfoService', 'JobRunnerService', 
        'JobAbortService', 'ScenarioListService', 'SpinnerFactory',

        function($scope, 
            JobInfoService, JobRunnerService, JobAbortService, 
            ScenarioListService, SpinnerFactory){

        }
    ]);
    */

    function MainTabController($scope, JobInfoService, JobRunnerService, JobAbortService, ScenarioListService, SpinnerFactory) {
        var viewModel = this;
        viewModel.job = {};
        viewModel.scenarioList = {};
        viewModel.selectedElement = {};
        viewModel.groupBy = 'Date';

        viewModel.getJobStatus = function () {
            viewModel.job = null;

            SpinnerFactory.show('spinnerJobStatus', '');

            var promise = JobInfoService.getJobStatus();
            promise.then(function (response) {
                
                viewModel.job = response;

                SpinnerFactory.hide('spinnerJobStatus', '');
            });
        };

        viewModel.runJob = function (id) {
            var promise = JobRunnerService.runJob(id);
            promise.then(function (response) {
            });
        };

        viewModel.abortJob = function (id) {
            var promise = JobAbortService.abortJob(id);
            promise.then(function (response) {
            });
        };

        viewModel.getScenarioList = function () {
            viewModel.selectedElement = null;

            SpinnerFactory.show('spinnerScenarioList', '');

            var promise = ScenarioListService.get();
            promise.then(function (response) {

                SpinnerFactory.hide('spinnerScenarioList', '');

                viewModel.scenarioList = response;
                viewModel.selectedElement = viewModel.scenarioList;
                viewModel.groupBy = 'Date';
            })
            .catch(function (err) {
                SpinnerFactory.hide('spinnerScenarioList', "An error occurred while trying to loading scenarios.");
            });
        };

        viewModel.notSortedJson = function (obj) {
            if (!obj) {
                return [];
            }
            return Object.keys(obj);
        };
    };
}());