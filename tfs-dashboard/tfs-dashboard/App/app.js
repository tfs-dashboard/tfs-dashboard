var app = angular.module('tfsApp', ['ngRoute', 'LocalStorageModule'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
      .when('/', {
          templateUrl: '/home/home'
      })
      .otherwise({
          redirectTo: '/'
      });

}
.config(function(localStorageServiceProvider){
    localStorageServiceProvider
        .setStorageType('sessionStorage')
        .setStorageCookie(30);
})])