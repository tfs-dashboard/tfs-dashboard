var app = angular.module('tfsApp');

app.controller('HomeController', [
    '$scope', 'dashboard', '$interval', 'tfsService', 'localStorageService', function ($scope, dashboard, $interval, tfsService, localStorageService) {
        $scope.dashboard = dashboard;
        $scope.dashboard.itemsLoaded = false;
        $scope.details = {
            templateUrl: "home/taskpopover"
        };

        $scope.dashboard.columnList = [{ Name: "Backlog", Value: 4 },
                                        { Name: "In Work", Value: 2 },
                                        { Name: "Waiting For Test", Value: 0 },
                                        { Name: "For Test", Value: 1 },
                                        { Name: "Waiting For Release", Value: 0 }];


        $scope.color = function (workItem) {
            switch (workItem.Type) {
                case "Bug":
                    workItem.color = "red";
                    break;
                case "Requirement":
                    workItem.color = "green";
                    break;
                case "ChangeRequest":
                    workItem.color = "blue";
                    break;
            };
        };


        function checkShowStatus(memberList) {
            angular.forEach(memberList, function (member) {
                var tempValue = localStorageService.get(member.Name + " in " + $scope.dashboard.selectedQuery);
                if (!(tempValue === null)) {
                    member.Show = tempValue;
                };
            });
        };


        function checkForColumnLimits() {
            angular.forEach($scope.dashboard.columnList, (function (column) {
                var tempValue = localStorageService.get(column.Name + " in " + $scope.dashboard.selectedQuery);
                if (!(tempValue === null)) {
                    column.Value = tempValue;
                };
            }));
        };


        function reloadDashboard(selectedQuery, selectedProject) {
            var gotWorkItemsPromise = tfsService.GetWorkItems(selectedQuery, selectedProject.Name);
            gotWorkItemsPromise.then(function (res) {
                $scope.dashboard.testList = res;
                checkShowStatus($scope.dashboard.testList.Members);
                checkForColumnLimits();
                $scope.dashboard.itemsLoaded = true;
            })
                .catch(function () {
                    $scope.dashboard.itemsLoaded = false;
                });
        };

        var dashReload;
        function startReload(tick) {
            dashReload = setInterval(function () { reloadDashboard($scope.dashboard.selectedQuery, $scope.dashboard.selectedProject) }, tick);
        };

        $scope.$watch("dashboard.refreshRate", function () {
            if ($scope.dashboard.itemsLoaded === true) {
                clearInterval(dashReload);
                if (!($scope.dashboard.refreshRate === 0))
                    startReload($scope.dashboard.refreshRate * 60000);
            } else {
                clearInterval(dashReload);
            }
        });

        $scope.$watch("dashboard.itemsLoaded", function () {
            if ($scope.dashboard.itemsLoaded === true) {
                clearInterval(dashReload);
                if (!($scope.dashboard.refreshRate === 0))
                    startReload($scope.dashboard.refreshRate * 60000);
            } else {
                clearInterval(dashReload);
            }
        });



        $scope.ifWorkItemOverdue = function (workItem) {
            workItem.hoursMsg = "hour";
            if (workItem.OverallCompletedTime + workItem.OverallRemainingTime > workItem.OverallEstimatedTime) {
                workItem.overdueTime = workItem.OverallCompletedTime + workItem.OverallRemainingTime - workItem.OverallEstimatedTime;
                if (workItem.overdueTime > 1) {
                    workItem.hoursMsg = "hours";
                };
                return true;
            } else {
                return false;
            }
        };

        $scope.checkIfTooLong = function (item, length) {
            if (item.Title.length > length) {
                return true;
            } else {
                return false;
            }
        };

        $scope.ifOverLimit = function (member, column) {
            if (member[column.Name.replace(/\s+/g, '')] > column.Value && !(column.Value === 0)) {
                return true;
            };
            return false;
        };
    }
]);