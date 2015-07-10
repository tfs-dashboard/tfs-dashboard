var app = angular.module('tfsApp');

app.controller("HomeController", [
    '$scope', 'dashboard', '$interval', function ($scope, dashboard, $interval) {
        $scope.dashboard = dashboard;
        $scope.dashboard.itemsLoaded = false;
        $scope.details = {
            templateUrl: "home/taskpopover"
        };



        $scope.dashboard.columnList = [ { Name: "Backlog", Value: 4 },
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
            }
        };



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
            };
        }

        $scope.checkIfTooLong = function (item, length) {
            if (item.Title.length > length) {
                return true;
            } else {
                return false;
            };
        };

        $scope.ifOverLimit = function (member, column) {
            if (column.Value === 0)
                return false;
            if (member[column.Name.replace(/\s+/g, '')] > column.Value) {
                return true;
            }
            return false;
        }

        $scope.startRefresh = function () {
            $scope.stop();
            var promise = $interval(function () {

            }, $scope.dashboard.minutesBetweenRefresh);
        };

        $scope.checkPos = function (e) {
            var left = e.currentTarget.offsetParent.offsetLeft;
            if (left >= 1920 - 600) {
                $scope.PopoverPosition = "left";
            }
            if (left <= 600) {
                $scope.PopoverPosition = "right";
            }
        }
    }
]);