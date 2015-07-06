var app = angular.module('tfsApp')

app.controller("ConfigurationController", ['$scope', '$modalInstance', 'dashboard', function ($scope, $modalInstance, dashboard) {
    $scope.dashboard = dashboard;

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

}])