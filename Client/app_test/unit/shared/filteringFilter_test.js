/// <reference path="../../../assets/js/angular/angular.js" />
/// <reference path="../../../assets/js/angular/angular-mocks.js" />
/// <reference path="../../../assets/js/angular/angular-mocks.js" />
/// <reference path="../../../app/components/manager/managerModule.js" />
/// <reference path="../../../app/shared/filtering/filteringModule.js" />
/// <reference path="../../../app/shared/filtering/filteringFilter.js" />
/// <reference path="../../../app/shared/filtering/filteringFactory.js" />
/// <reference path="../../../app/shared/filtering/memoizeFactory.js" />

describe('unit: filters_: ', function () {

    var $filterProvider, $compile, $scope, $filter;

    beforeEach(module('filtering'));

    describe('groupBy', function () {

        beforeEach(function () {
            inject(function (_$filter_, _$rootScope_, _$compile_) {
                $filter = _$filter_;
                $compile = _$compile_;
                $scope = _$rootScope_;
            });

            jasmine.addMatchers({
                toEqualData: function (angular) {
                    return {
                        compare: function (actual, expected) {
                            if (expected === undefined)
                                expected = '';
                            var result = {};
                            result.pass = angular.equals(actual, expected);
                            return result;
                        }
                    }
                }
            });

            $scope.students = [
                { ID: 1, name: 'Josep', class: 'A' },
                { ID: 2, name: 'Carles', class: 'B' },
                { ID: 3, name: 'Xavi', class: 'A' },
                { ID: 4, name: 'Pere', class: 'B' },
                { ID: 5, name: 'Adrià', class: 'C' }
            ];
        });

        it('should group students by class', function () {
            var grouppedStudents = $filter('groupBy')($scope.students, 'class');
            var expectedResult = {
                'A': [
                  { ID: 1, name: 'Josep', class: 'A' },
                  { ID: 3, name: 'Xavi', class: 'A' }
                ],
                'B': [
                  { ID: 2, name: 'Carles', class: 'B' },
                  { ID: 4, name: 'Pere', class: 'B' }
                ],
                'C': [
                  { ID: 5, name: 'Adrià', class: 'C' }
                ],
            };
            expect(grouppedStudents).toEqualData(expectedResult);
        });

        it('should work in a view combined with ngRepeat', function () {
            var elem = $compile('<div>' +
                                  '<ul>' +
                                    '<li ng-repeat="(class, classStudents) in students | groupBy: \'class\'">' +
                                       '{{class}}' +
                                       '<ul>' +
                                          "<li ng-repeat=\"student in classStudents\">" +
                                               "{{student.name}}" +
                                          "</li>" +
                                       '</ul>' +
                                     '</li>' +
                                  '</ul>' +
                                 '</div>')($scope);
            $scope.$digest();
            expect(elem.children().children().length).toBe(3);
            expect(elem.children().children().children().children().length).toBe(5);
        });
    });

    describe('format', function () {

    });
});