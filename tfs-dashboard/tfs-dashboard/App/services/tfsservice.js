var app = angular.module('tfsApp')

app.service("tfsService", function($http){
    this.GetCollectionInfo = function (conUrl) {
        return $http.post('/connection/getcollectioninfo', { url: conUrl })
    }
})