(function () {
    'use strict'

    angular.module('filtering')
    .filter('groupBy', ['$parse', 'filterStabilize',
      function ($parse, filterStabilize) {

          function groupBy(input, prop) {

              //if (!input) { return; }
              if (!angular.isArray(input)) { return; }

              var grouped = {};

              input.forEach(function (item) {
                  var key = $parse(prop)(item);
                  grouped[key] = grouped[key] || [];
                  grouped[key].push(item);
              });

              return grouped;

          }

          return filterStabilize(groupBy);

      }])

    .filter('format', ['$parse', 'filterStabilize',
      function ($parse, filterStabilize) {

          function format(input, prop) {

              //if (!input) { return; }
              if (!angular.isArray(input)) { return; }

              var dormatted = {};

              input.forEach(function (item) {
                  var key = $parse(prop)(item);
                  formatted[key] = formatted[key] || [];
                  formatted[key].push(item);
              });

              return formatted;

          }

          return filterStabilize(groupBy);

      }])

})();