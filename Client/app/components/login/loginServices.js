(function () {
    'use strict'

    angular.module('form')
    .service('RegisterInfoService', RegisterInfoService);

    function RegisterInfoService($http, $q, $log, WEB_CONFIG) {
        var RegisterInfoService = {};

        RegisterInfoService.getRegisterInfo = function (user) {
            //$q -> manage async operations
            var deferred = $q.defer();

            $http({
                url: WEB_CONFIG.WEBAPI_URL + '/api/DomainAuth/IsDomainUser',
                dataType: 'json',
                method: 'POST',
                data: angular.toJson(user),
                headers: {
                    'Content-Type': 'application/json',
                    // 'Content-Type': 'text/xml; charset=utf-8',
                    'Accept': 'application/json'
                    //'Accept': 'text/xml'
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
                //write message in browser console
                throw msg;
            });

            return deferred.promise;
        };

        return RegisterInfoService;
    };

})();