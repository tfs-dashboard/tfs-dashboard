var app = angular.module('tfsApp')

app.controller("ConnectionController", ['$scope', 'tfsService', 'localStorageService', '$modalInstance', 'dashboard', '$q', '$timeout', function ($scope, tfsService, localStorageService, $modalInstance, dashboard, $q, $timeout) {

    $scope.dashboard = dashboard;

    $scope.selectedCollection;
    $scope.selectedProject;
    $scope.selectedQuery;
    $scope.isUrlValid;
    $scope.showModal = false;
    $scope.loadingQuery = false;
    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

    function submit(key, val) {
        return localStorageService.set(key, val);
    }


    //conn on startup

    $scope.init = function () {
        var defer = $q.defer();
        defer.promise
            .then(function () {
                var collectionPromise = tfsService.GetCollectionInfo(localStorageService.get("connectionUrl"))
                collectionPromise.then(function (res) {
                    $scope.collectionList = res.data;
                    $scope.isUrlValid = true;
                    $scope.conUrl = localStorageService.get("connectionUrl");
                    var temp = localStorageService.get("selectedCollection");
                    var projectPromise = tfsService.GetProjectInfo(temp.Name);
                    projectPromise.then(function (res) {
                        $scope.projectList = res;
                        $scope.isCollectionSelected = true;
                        $scope.dashboard.selectedCollection = temp;
                        temp = localStorageService.get("selectedProject");
                        var queryPromise = tfsService.GetSharedQueries(temp.Name);
                        queryPromise.then(function (res) {
                            $scope.dashboard.selectedProject = temp;
                            $scope.isProjectSelected = true;
                            $scope.queriesList = res;
                            $scope.dashboard.selectedQuery = localStorageService.get("selectedQuery");
                        })
                    })
                })
            })
        defer.resolve();

        $timeout(function () {
            $scope.showModal = true;
        }, 2000)
    }
    //initial connection with url
    $scope.connect = function (conUrl) {
        validUrlPromise = tfsService.GetCollectionInfo(conUrl);
        submit('connectionUrl', conUrl);
        $scope.collectionList = null;
        validUrlPromise.then(function (res) {
            $scope.collectionList = res.data;
            $scope.isUrlValid = true;
            $scope.conUrl = conUrl;
        },
        (function (errorP1) {
            $scope.collectionList = null;
            $scope.isUrlValid = false;
            $scope.isCollectionSelected = false;
        }))

        $scope.dashboard.selectedCollection = null;
        $scope.dashboard.selectedProject = null;
        $scope.dashboard.selectedQuery = null;
        $scope.projectList = null;
        $scope.queriesList = null;
    }
    //changing selected collection
    $scope.selectCollection = function (selectedCollection) {
        $scope.projectList = null;
        var validCollectionPromise = tfsService.GetProjectInfo(selectedCollection.Name);
        submit('selectedCollection', selectedCollection);

        $scope.selectedProject = null;

        validCollectionPromise.then(function (res) {
            $scope.projectList = res;
            $scope.isCollectionSelected = true;
            $scope.dashboard.selectedCollection = selectedCollection;
        })
        .catch(function (errorP1) {
            $scope.isCollectionSelected = false;
        })
        $scope.dashboard.selectedProject = null;
        $scope.dashboard.selectedQuery = null;
        $scope.queriesList = null;
    }

    //changing selected project
    $scope.selectProject = function (selectedProject) {
        $scope.queriesList = null;
        var validProjectPromise = tfsService.GetSharedQueries(selectedProject.Name)

        $scope.dashboard.selectedProject = selectedProject;
        validProjectPromise.then(function (res) {
            submit('selectedProject', selectedProject);

            $scope.queriesList = res;
            $scope.isProjectSelected = true;

        })
        .catch((function (errorP1) {
            $scope.isProjectSelected = false;
            $scope.queriesList = null;
        }))

        $scope.dashboard.selectedQuery = null;
    }

    $scope.getWorkItems = function (selectedQuery, selectedProject) {
        $scope.loadingQuery = true;
        var gotWorkItemsPromise = tfsService.GetWorkItems(selectedQuery, selectedProject.Name);
        gotWorkItemsPromise.then(function (res) {
            $scope.dashboard.selectedQuery = selectedQuery;
            submit('selectedQuery', selectedQuery);
            $scope.dashboard.testList = res;
            $modalInstance.dismiss();
            $scope.loadingQuery = false;
        })
        .catch(function () {
            $scope.loadingQuery = false;
        })
    }
}]);