var app = angular.module('tfsApp')

app.controller("ConnectionController", ['$scope', 'tfsService', function ($scope, $modalInstance, $http, localStorageService, dashboard, tfsService) {
    $scope.dashboard = dashboard;
    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

    function submit(key, val) {
        return localStorageService.set(key, val);
    }

    $scope.connect = function (conUrl) {
        $http.post('/connection/getcollectioninfo', { url: conUrl }).success(function (res) {
            submit('connectionUrl', conUrl);
            $scope.collectionList = res;
            $scope.isUrlValid = true;
            $scope.conUrl = conUrl;
        }
    ).error(function () {
        $scope.collectionList = null;
        $scope.projectList = null;
        $scope.isUrlValid = false;
        $scope.isCollectionSelected = false;
    })
    }

    $scope.selectCollection = function (selectedCollection) {
        $scope.queriesList = null;
        $scope.projectList = null;
        $http.post('/connection/getprojectinfo', { collectionName: selectedCollection }).success(function (res) {
            $scope.projectList = res;
            submit('selectedCollection', selectedCollection);
            $scope.isCollectionSelected = true;
            $scope.dashboard.selectedCollection = selectedCollection;
        }).error(function () {
            $scope.isCollectionSelected = false;
        })
    }

    $scope.selectProject = function (selectedProject) {
        $scope.queriesList = null;
        $http.post('/connection/getsharedquerieslist', { projectName: selectedProject }).success(function (res) {
            $scope.queriesList = res;
            $scope.isQuerySelected = true;
            submit('selectedProject', selectedProject);
        }).error(function () {
            $scope.isQuerySelected = false;
            $scope.queriesList = null;
        })
    }

    $scope.getWorkItems = function (selectedQuery, selectedProject) {
        $http.post('/connection/getworkitems', { queryName: selectedQuery, projectName: selectedProject }).success(function (res) {
            $scope.dashboard.testList = res;
            $modalInstance.dismiss();
        })
        //}
        //$scope.init = function () {
        //    $scope.connect(localStorageService.get("connectionUrl"));
        //    $scope.selectCollection(localStorageService.get("selectedCollection"));
        //    $scope.selectProject(localStorageService.get("selectedProject"));
        //}
    }
});