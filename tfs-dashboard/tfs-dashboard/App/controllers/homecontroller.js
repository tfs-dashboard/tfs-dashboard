var app = angular.module('tfsApp')

app.controller("HomeController", ['$scope', 'dashboard', '$interval', function ($scope, dashboard, $interval) {
    $scope.dashboard = dashboard;
    $scope.dashboard.itemsLoaded = false;
    $scope.details = {
        templateUrl: "home/taskpopover"
    }

    $scope.ifWorkItemOverdue = function (workItem) {
        workItem.hoursMsg = "hour";
        if(workItem.OverallCompletedTime + workItem.OverallRemainingTime > workItem.OverallEstimatedTime)
        {
            workItem.overdueTime = workItem.OverallCompletedTime + workItem.OverallRemainingTime - workItem.OverallEstimatedTime;
            if (workItem.overdueTime > 1) {
                workItem.hoursMsg = "hours";
            }
            return true;
        } else {
            return false;
        }
    }

    $scope.checkIfTooLong = function (item, length) {
        if (item.Title.length > length) {
            return true;
        }
        else {
            return false;
        }
    }

    $scope.startRefresh = function () {
        $scope.stop();

        promise = $interval(function () {

        }, $scope.dashboard.minutesBetweenRefresh)
    }
}])
;