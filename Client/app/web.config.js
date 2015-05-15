(function () {
    'use strict'

    var config_module = angular.module('config');

    var config_data = {
          'WEB_CONFIG': {
              'WEBAPI_URL': 'http://localhost:2071'
        }
    };

angular.forEach(config_data, function (key, value) {
    config_module.constant(value, key);
});

})();