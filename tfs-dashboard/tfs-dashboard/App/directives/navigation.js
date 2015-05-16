var app = angular.module('tfsApp')

app.directive("navigationBar", function () {
    return {
        restrict: 'E',
        templateUrl: '/home/navigationbar/'
    }
})