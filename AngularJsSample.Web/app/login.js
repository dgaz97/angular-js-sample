'use strict';

var loginApp = angular.module('LoginApp', ['ui.router', 'oc.lazyLoad', 'LocalStorageModule', 'common']);

loginApp.config(function ($httpProvider, $ocLazyLoadProvider,$stateProvider, $urlRouterProvider, $locationProvider) {

    if (!$httpProvider.defaults.headers.get) {
        $httpProvider.defaults.headers.get = {};
    }
    $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
    $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';

    $ocLazyLoadProvider.config({
        debug: true
    });

    $stateProvider
        .state('index', {
            url: "/",
            template: "<ui-view/>"
        })
    .state('login', {
        url: "/account/login",
        controller: "LoginCtrl",
        templateUrl: "app/login/partials/login.html",
        resolve: {
            dashboard: function ($ocLazyLoad) {
                return $ocLazyLoad.load({
                    name: "login",
                    files: [
                        "app/login/controllers/loginCtrl.js",
                        "app/login/loginModule.js"
                    ]
                });
            }
        }
    })
    ;

    $locationProvider.html5Mode(true);
});
