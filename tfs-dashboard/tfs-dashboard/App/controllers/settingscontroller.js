var app = angular.module('tfsApp')

app.controller("SettingsController", function ($scope, $modal, $timeout) {

    $scope.openConnection = function () {
        var modalInstance = $modal.open({
            animation: true,
            show: false,
            templateUrl: 'home/connectionmodal',
            controller: 'ConnectionController'
        });
    }
})