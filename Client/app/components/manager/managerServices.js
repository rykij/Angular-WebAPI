(function () {
    'use strict'

    angular.module('manager')
   .service('JobInfoService', JobInfoService)
   .service('JobRunnerService', JobRunnerService)
   .service('JobAbortService', JobAbortService)
   .service('ScenarioListService', ScenarioListService);

    function JobInfoService($http, $q, $log, WEB_CONFIG) {
        var JobInfoService = {};

        JobInfoService.getJobStatus = function () {
            //$q -> manage async operations
            var deferred = $q.defer();

            $http({
                url: WEB_CONFIG.WEBAPI_URL + '/api/Job/GetJobStatus',
                method: 'GET',
                async: true,
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    // 'Content-Type': 'text/xml; charset=utf-8',
                    'Accept': 'application/json'
                    //'Accept': 'text/xml'
                }
            })
            .success(function (response) {
                if (typeof response === 'object') {
                    $log.debug(response);
                    deferred.resolve(
                        angular.fromJson(response)
                    );
                }
            })
            .error(function (msg, code) {
                deferred.reject(msg);
                //write message in browser console
                throw msg;
            });

            return deferred.promise;
        };

        return JobInfoService;
    };

    function JobRunnerService($http, $q, $log, WEB_CONFIG) {
        var JobRunnerService = {};

        JobRunnerService.runJob = function (id) {
            var deferred = $q.defer();

            $http({
                url: WEB_CONFIG.WEBAPI_URL + '/api/Job/RunJob',
                dataType: 'json',
                method: 'POST',
                data: id,
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                }
            })
            .success(function (response) {
                $log.debug(response);
                deferred.resolve(
                    response
                );
            })
            .error(function (msg, code) {
                deferred.reject(msg);
                throw msg;
            });

            return deferred.promise;
        };

        return JobRunnerService;
    };

    function JobAbortService($http, $q, $log, WEB_CONFIG) {
        var JobAbortService = {};

        JobAbortService.abortJob = function (id) {
            var deferred = $q.defer();

            $http({
                url: WEB_CONFIG.WEBAPI_URL + '/api/Job/AbortJob',
                dataType: 'json',
                method: 'POST',
                data: id,
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                }
            })
            .success(function (response) {
                $log.debug(response);
                deferred.resolve(
                    response
                );
            })
            .error(function (msg, code) {
                deferred.reject(msg);
                throw msg;
            });

            return deferred.promise;
        };

        return JobAbortService;
    };

    function ScenarioListService($http, $q, $log, WEB_CONFIG) {
        var ScenarioListService = {};

        ScenarioListService.get = function () {
            var deferred = $q.defer();

            $http({
                url: WEB_CONFIG.WEBAPI_URL + '/api/Entity/GetAllScenarios',
                method: 'GET',
                async: true,
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'Accept': 'application/json'
                }
            })
            .success(function (response) {
                if (typeof response === 'object') {
                    $log.debug(response);
                    deferred.resolve(
                        angular.fromJson(response)
                    );
                }
            })
            .error(function (msg, code) {
                deferred.reject(msg);
                throw msg;
            });

            return deferred.promise;
        };

        return ScenarioListService;
    };

})();