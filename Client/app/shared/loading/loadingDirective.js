(function () {
    'use strict'

    angular.module('loading')
    .directive('spinner', spinner);

    /* Restrictions: 
   'A' - only matches attribute name
   'E' - only matches element name
   'C' - only matches class name
   'AEC' - matches either attribute or element or class name
   */

    function spinner () {
        return {
            restrict: 'E',
            template: [
                 '<span>',
                  '  <img ng-show="showSpinner" src="/assets/img/loading/wait.gif" style="width: {{ spinnerSize }};" />',
                  '  <span ng-show="loadingText && showSpinner">{{ loadingText }}</span>',
                  '  <span ng-show="doneText && !showSpinner">{{ doneText }}</span>',
                  '</span>'
            ].join(''),
            replace: true,
            scope: {
                id: '@',
                group: '@?',
                showSpinner: '@?',
                loadingText: '@?',
                doneText: '@?',
                onRegisterComplete: '&?'
            },
            controller: function ($scope, $attrs, SpinnerFactory) {
                // Register the spinner with the spinner service.
                SpinnerFactory._register($scope);

                // Invoke the onRegisterComplete expression, if any.
                // Expose the spinner service for easy access.
                $scope.onRegisterComplete({ $spinnerFactory: SpinnerFactory });
            },
            link: function (scope, elem, attrs) {
                // Check for pre-defined size aliases and set pixel width accordingly.
                if (attrs.hasOwnProperty('size')) {
                    attrs.size = attrs.size.toLowerCase();
                }
                switch (attrs.size) {
                    case 'tiny':
                        scope.spinnerSize = '15px';
                        break;
                    case 'small':
                        scope.spinnerSize = '25px';
                        break;
                    case 'medium':
                        scope.spinnerSize = '35px';
                        break;
                    case 'large':
                        scope.spinnerSize = '64px';
                        break;
                    default:
                        scope.spinnerSize = '50px';
                        break;
                }
            }
        };
    };

})();