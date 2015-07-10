var app = angular.module('tfsApp')

app.controller("ConfigurationController", ['$scope', '$modalInstance', 'dashboard', 'localStorageService', function ($scope, $modalInstance, dashboard, localStorageService) {
    $scope.dashboard = dashboard;

    function submit(key, val) {
        return localStorageService.set(key, val);
    }

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

    $scope.submitMemberShow = function (member) {
        var key = member.Name + " in " + $scope.dashboard.selectedQuery;
        submit(key, member.Show);
    };

    $scope.submitLimit = function(column) {
        var key = column.Name + " in " + $scope.dashboard.selectedQuery;
        submit(key, column.Value);
    }
}])