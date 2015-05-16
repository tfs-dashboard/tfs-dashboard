var app = angular.module('tfsApp')

app.directive("connectionModal", function () {
    return {
        restrict: 'E',
        templateUrl: '/home/connectionmodal/',
        controller: function ($scope, $http) {
            $scope.message = "Now viewing about!";
            $scope.connect = function (url) {
                $http.get('/connection/getcollectioninfo').success(function (data) {
                    $scope.nameList = data
                }
            )
            }
        },
        controllerAs: 'connection'
    }
})