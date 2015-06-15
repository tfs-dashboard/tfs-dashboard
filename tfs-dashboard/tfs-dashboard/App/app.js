var app = angular.module('tfsApp', ['ngRoute', 'LocalStorageModule', 'ui.bootstrap'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
      .when('/', {
          templateUrl: '/home/home'
      })
      .otherwise({
          redirectTo: '/'
      });

}])