(function () {
    'use strict'
 
    angular.module('filtering')
    /* Stabilize $filter that throw “Infite $diggest Loop Error”.
      "groupBy"(in filteringFilter.js) returns a new object each 
       time which will cause an infinite digest cycle.
       N.B: $digest tells angular to update bindings and fire any watches.
    */
    .factory('filterStabilize', ['memoize', 
      function (memoize) {

          function service(fn) {

              function filter() {
                  var args = [].slice.call(arguments);
                  // always pass a copy of the args so that the original input can't be modified
                  args = angular.copy(args);
                  // return the `fn` return value or input reference (makes `fn` return optional)
                  var filtered = fn.apply(this, args) || args[0];
                  return filtered;
              }

              var memoized = memoize(filter);

              return memoized;

          }

          return service;

      }])
})();