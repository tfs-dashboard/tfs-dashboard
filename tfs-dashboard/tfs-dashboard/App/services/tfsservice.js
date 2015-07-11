var app = angular.module('tfsApp');

app.service("tfsService", function ($http) {

    var tfsServiceFactory = {};
    var getCollectionInfo = function (conUrl) {
        return $http.post('/connection/getcollectioninfo', { url: conUrl });
    }

    var getProjectInfo = function (selectedCollection) {
        return $http.post('/connection/getprojectinfo', { collectionName: selectedCollection }).then(function (result) {
            return result.data;
        })
    }

    var getSharedQueries = function (selectedProject) {
        return $http.post('/connection/getsharedquerieslist', { projectName: selectedProject }).then(function (result) {
            return result.data;
        })
    }

    var getWorkItems = function (selectedQuery, selectedProject) {
        return $http.post('/connection/getworkitems', { queryName: selectedQuery, projectName: selectedProject }).then(function (result) {
            return result.data;
        })
    }

    tfsServiceFactory.GetCollectionInfo = getCollectionInfo;
    tfsServiceFactory.GetProjectInfo = getProjectInfo;
    tfsServiceFactory.GetSharedQueries = getSharedQueries;
    tfsServiceFactory.GetWorkItems = getWorkItems;

    return tfsServiceFactory;
})