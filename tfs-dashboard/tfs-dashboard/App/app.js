var app = angular.module('tfsApp', ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
      .when('/', {
          templateUrl: '/home/home'
      })
      .otherwise({
          redirectTo: '/'
      });

}])