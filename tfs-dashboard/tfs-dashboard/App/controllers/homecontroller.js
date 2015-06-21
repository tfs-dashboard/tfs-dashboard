var app = angular.module('tfsApp')

app.controller("HomeController", ['$scope', 'dashboard', function ($scope, dashboard) {
    $scope.dashboard = dashboard;
}]);