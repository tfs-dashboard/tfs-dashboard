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
                $scope.queriesList = null;
                $scope.projectList = null;
                $http.post('/connection/getprojectinfo', { collectionName: selectedCollection }).success(function (res) {
                    $scope.projectList = res;
                    $scope.isCollectionSelected = true;
                    $http.get('/connection/getworkitemstore');
                }).error(function () {
                    $scope.isCollectionSelected = false;
                })
            }

            $scope.changedSelectedProject = function (selectedProject) {
                $scope.queriesList = null;
                $http.post('/connection/getsharedquerieslist', { projectName: selectedProject }).success(function (res) {
                    $scope.queriesList = res;
                    $scope.isQuerySelected = true;
                }).error(function () {
                    $scope.isQuerySelected = false;
                    $scope.queriesList = null;
                })
            }

            $scope.getWorkItems = function (selectedQuery, selectedProject) {
                $http.post('/connection/getworkitems', { queryName: selectedQuery, projectName: selectedProject }).success(function (res) {

                })
            }
        },
        controllerAs: 'connection'
    }
})