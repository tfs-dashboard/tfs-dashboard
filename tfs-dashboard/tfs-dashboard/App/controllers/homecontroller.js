var app = angular.module('tfsApp')

app.controller("HomeController", ['$scope', 'dashboard', function ($scope, dashboard) {
    $scope.dashboard = dashboard;

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
}])
;