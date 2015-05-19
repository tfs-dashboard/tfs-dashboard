var app = angular.module('tfsApp')

app.directive("connectionModal", function () {
    return {
        restrict: 'E',
        templateUrl: '/home/connectionmodal/',
        controller: function ($scope, $http) {
            $scope.conUrl = "";

            $scope.connect = function (conUrl) {
                $http.post('/connection/getcollectioninfo', { url: conUrl }).success(function (res) {
                    $scope.collectionList = res;
                    $scope.isUrlValid = true;
                }
            ).error(function () {
                $scope.collectionList = null;
                $scope.projectList = null;
                $scope.isUrlValid = false;
                $scope.isCollectionSelected = false;
            })
            }

            $scope.changedSelectedCollection = function (selectedCollection) {
                $http.post('/connection/getprojectinfo', { collectionName: selectedCollection }).success(function (res) {
                    $scope.projectList = res;
                    $scope.isCollectionSelected = true;
                }).error(function () {
                    $scope.isCollectionSelected = false;
                })
            }
        },
        controllerAs: 'connection'
    }
})