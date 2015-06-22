var app = angular.module('tfsApp')

app.controller("ConnectionController", ['$scope', 'tfsService', 'localStorageService', '$modalInstance', 'dashboard', function ($scope, tfsService, localStorageService, $modalInstance, dashboard) {

    $scope.dashboard = dashboard;

    $scope.selectedCollection;
    $scope.selectedProject;
    $scope.selectedQuery;

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

    function submit(key, val) {
        return localStorageService.set(key, val);
    }


    //conn on startup
    //$scope.init = function () {
    //    connOK = $scope.connect(localStorageService.get("connectionUrl"));
    //    if (connOK) {
    //        $scope.selectCollection(localStorageService.get("selectedCollection"));
    //    }
    //}

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
        submit('selectedCollection', selectedCollection);

        validCollectionPromise.then(function (res) {
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
        submit('selectedProject', selectedProject);

        validProjectPromise.then(function (res) {
            $scope.queriesList = res;
            $scope.isQuerySelected = true;
            submit('selectedProject', selectedProject);
            (function (errorP1) {
                $scope.isQuerySelected = false;
                $scope.queriesList = null;
            })
        })
    }

    $scope.getWorkItems = function (selectedQuery, selectedProject) {
        var gotWorkItemsPromise = tfsService.GetWorkItems(selectedQuery, selectedProject);

        gotWorkItemsPromise.then(function (res) {
            $scope.dashboard.testList = res;
            $modalInstance.dismiss();
        })
    }

    $scope.init = function () {
        $scope.connect(localStorageService.get("connectionUrl"));
        $scope.selectCollection(localStorageService.get("selectedCollection"));
        $scope.selectProject(localStorageService.get("selectedProject"));

        //ifurlvalid
    }
}
]);