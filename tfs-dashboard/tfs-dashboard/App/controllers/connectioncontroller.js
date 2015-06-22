var app = angular.module('tfsApp')

app.controller("ConnectionController", ['$scope', 'tfsService', 'localStorageService', '$modalInstance', 'dashboard', '$q', '$timeout', function ($scope, tfsService, localStorageService, $modalInstance, dashboard, $q, $timeout) {

    $scope.dashboard = dashboard;

    $scope.selectedCollection;
    $scope.selectedProject;
    $scope.selectedQuery;
    $scope.isUrlValid;
    $scope.showModal = false;
    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

    function submit(key, val) {
        return localStorageService.set(key, val);
    }


    //conn on startup


    var defer = $q.defer();
    defer.promise
        .then(function () {
            $scope.connect(localStorageService.get("connectionUrl"));
        })
        .then(function () {
            $scope.selectCollection(localStorageService.get("selectedCollection"));
        })
         .then(function () {
             $scope.selectProject(localStorageService.get("selectedProject"))
         })
         .then(function () {
             $scope.dashboard.selectedQuery = (localStorageService.get("selectedQuery"))
         })
    defer.resolve();

    $timeout(function () {
        $scope.showModal = true;
    }, 3000)

    //initial connection with url
    $scope.connect = function (conUrl) {
        validUrlPromise = tfsService.GetCollectionInfo(conUrl);
        submit('connectionUrl', conUrl);

        validUrlPromise.then(function (res) {
            $scope.collectionList = res.data;
            $scope.isUrlValid = true;
            $scope.conUrl = conUrl;
        },
        (function (errorP1) {
            $scope.collectionList = null;
            $scope.projectList = null;
            $scope.isUrlValid = false;
            $scope.isCollectionSelected = false;
        }))
    }
    //changing selected collection
    $scope.selectCollection = function (selectedCollection) {
        var validCollectionPromise = tfsService.GetProjectInfo(selectedCollection);

        $scope.projectList = null;

        validCollectionPromise.then(function (res) {
            submit('selectedCollection', selectedCollection);
            $scope.projectList = res;
            $scope.isCollectionSelected = true;
            $scope.dashboard.selectedCollection = selectedCollection;
        },
        (function (errorP1) {
            $scope.isCollectionSelected = false;
        }))
    }

    //changing selected project
    $scope.selectProject = function (selectedProject) {
        var validProjectPromise = tfsService.GetSharedQueries(selectedProject)

        $scope.queriesList = null;

        $scope.dashboard.selectedProject = selectedProject;
        validProjectPromise.then(function (res) {
            submit('selectedProject', selectedProject);

            $scope.queriesList = res;
            $scope.isProjectSelected = true;

            (function (errorP1) {
                $scope.isProjectSelected = false;
                $scope.queriesList = null;
            })
        })
    }

    $scope.getWorkItems = function (selectedQuery, selectedProject) {
        var gotWorkItemsPromise = tfsService.GetWorkItems(selectedQuery, selectedProject);
        gotWorkItemsPromise.then(function (res) {
            $scope.dashboard.selectedQuery = selectedQuery;
            submit('selectedQuery', selectedQuery);
            $scope.dashboard.testList = res;
            $modalInstance.dismiss();
        })
    }
}]);