var app = angular.module('tfsApp', ['ngRoute', 'LocalStorageModule'])
.config(['$routeProvider', function ($routeProvider, localStorageServiceProvider) {
    $routeProvider
      .when('/', {
          templateUrl: '/home/home'
      })
      .otherwise({
          redirectTo: '/'
      });
    localStorageServiceProvider
        .setStorageType('sessionStorage')
        .setStorageCookie(30);
}]) 