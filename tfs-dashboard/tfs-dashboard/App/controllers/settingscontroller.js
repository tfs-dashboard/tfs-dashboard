var app = angular.module('tfsApp')

app.controller("SettingsController", function ($scope, $modal) {

    $scope.openConnection = function () {

        var modalInstance = $modal.open({
            animation: true,
            templateUrl: 'home/connectionmodal',
            controller: 'ConnectionController'
        });
    }
})