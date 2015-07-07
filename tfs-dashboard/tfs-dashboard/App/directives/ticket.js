var app = angular.module('tfsApp')

app.directive("ticket", function () {
    return {
        restrict: 'E',
        templateUrl: '/home/ticket/'
    }
})