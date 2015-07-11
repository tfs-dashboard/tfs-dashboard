var app = angular.module('tfsApp')

app.controller("ConnectionController", ['$scope', 'tfsService', 'localStorageService', '$modalInstance', 'dashboard', '$q', '$timeout', function ($scope, tfsService, localStorageService, $modalInstance, dashboard, $q, $timeout) {

    $scope.dashboard = dashboard;

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
                var connUrl = localStorageService.get("connectionUrl");
                if (!(connUrl === null)) {
                    var collectionPromise = tfsService.GetCollectionInfo(connUrl);
                    collectionPromise.then(function (res) {
                        $scope.collectionList = res.data;
                        $scope.isUrlValid = true;
                        $scope.conUrl = localStorageService.get("connectionUrl");
                        var tempCollection = localStorageService.get("selectedCollection");
                        if (!(tempCollection === null)) {
                            var projectPromise = tfsService.GetProjectInfo(tempCollection.Name);
                            projectPromise.then(function(res) {
                                $scope.projectList = res;
                                $scope.isCollectionSelected = true;
                                $scope.dashboard.selectedCollection = tempCollection;
                                var tempProject = localStorageService.get("selectedProject");
                                if (!(tempProject === null)) {
                                    var queryPromise = tfsService.GetSharedQueries(tempProject.Name);
                                    queryPromise.then(function(res) {
                                        $scope.dashboard.selectedProject = tempProject;
                                        $scope.isProjectSelected = true;
                                        $scope.queriesList = res;
                                        $scope.dashboard.selectedQuery = localStorageService.get("selectedQuery");
                                        $scope.showModal = true;
                                    });
                                } else {
                                    $scope.showModal = true;
                                };
                            });
                        } else {
                            $scope.showModal = true;
                        }
                    });
                } else {
                    $scope.showModal = true;
                }
            });
        defer.resolve();
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
            $scope.isProjectSelected = false;
            $scope.isCollectionSelected = false;
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
            $scope.isProjectSelected = false;
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

    function checkShowStatus(memberList) {
        angular.forEach(memberList, function (member) {
            var tempValue = localStorageService.get(member.Name + " in " + $scope.dashboard.selectedQuery);
            if (!(tempValue === null)) {
                member.Show = tempValue;
            };
        });
    }


    function checkForColumnLimits() {
        angular.forEach($scope.dashboard.columnList, (function (column) {
            var tempValue = localStorageService.get(column.Name + " in " + $scope.dashboard.selectedQuery);
            if (!(tempValue === null)) {
                column.Value = tempValue;
            }
        }));
    }
    $scope.getWorkItems = function (selectedQuery, selectedProject) {
        $scope.loadingQuery = true;
        var gotWorkItemsPromise = tfsService.GetWorkItems(selectedQuery, selectedProject.Name);
        gotWorkItemsPromise.then(function (res) {
            $scope.dashboard.selectedQuery = selectedQuery;
            submit('selectedQuery', selectedQuery);
            $scope.dashboard.testList = res;
            checkShowStatus($scope.dashboard.testList.Members);
            checkForColumnLimits();
            $modalInstance.dismiss();
            $scope.loadingQuery = false;
            $scope.dashboard.itemsLoaded = true;
        })
        .catch(function () {
            $scope.loadingQuery = false;
            $scope.dashboard.itemsLoaded = false;
        })
    }
}]);