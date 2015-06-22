var app = angular.module('tfsApp')

app.service("tfsService", function ($http) {

    var tfsServiceFactory = {};
    var _GetCollectionInfo = function (conUrl) {
        return $http.post('/connection/getcollectioninfo', { url: conUrl })
    }

    var _GetProjectInfo = function (selectedCollection) {
        return $http.post('/connection/getprojectinfo', { collectionName: selectedCollection }).then(function (result) {
            return result.data
        })
    }

    var _GetSharedQueries = function (selectedProject) {
        return $http.post('/connection/getsharedquerieslist', { projectName: selectedProject }).then(function (result) {
            return result.data
        })
    }

    var _GetWorkItems = function (selectedQuery, selectedProject) {
        return $http.post('/connection/getworkitems', { queryName: selectedQuery, projectName: selectedProject }).then(function (result) {
            return result.data
        })
    }

    tfsServiceFactory.GetCollectionInfo = _GetCollectionInfo;
    tfsServiceFactory.GetProjectInfo = _GetProjectInfo;
    tfsServiceFactory.GetSharedQueries = _GetSharedQueries;
    tfsServiceFactory.GetWorkItems = _GetWorkItems;

    return tfsServiceFactory;
})