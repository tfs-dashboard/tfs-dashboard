var app = angular.module('tfsApp')

app.controller("HomeController", function ($scope, dashboard) {
    $scope.dashboard = dashboard;
});