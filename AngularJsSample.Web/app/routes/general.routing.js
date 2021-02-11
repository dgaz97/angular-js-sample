(function () {

    'use strict';

    angular
        .module('general.routing', [])
        .config(config);

    config.$inject = ['$stateProvider'];
    function config(
        $stateProvider) {

        $stateProvider
            .state('dashboard', {
                url: "/",
                controller: "dashboardCtrl",
                controllerAs: "vm",
                templateUrl: "app/dashboard/partials/dashboard.html",
                resolve: {
                    loginRequired: loginRequired,
                    dashboardServices: function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: "dashboardServices",
                            files: [
                                "app/dashboard/dashboardServices.module.js"
                            ]
                        });
                    },
                    dashboard: function ($ocLazyLoad, dashboardServices) {
                        return $ocLazyLoad.load({
                            name: "dashboard",
                            files: [
                                "app/dashboard/dashboard.module.js"
                            ]
                        });
                    }

                }
            })
            .state('moviePersonsOverview', {
                url: "/moviepersons",
                controller: "moviePersonsOverviewCtrl",
                controllerAs: "vm",
                templateUrl: "app/moviepersons/partials/moviePersonsOverview.html",
                resolve: {
                    loginRequired: loginRequired,
                    moviePersonsServices: function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: "moviePersonsServices",
                            files: [
                                "app/moviepersons/moviePersonsServices.module.js"
                            ]
                        });
                    },
                    moviePersons: function ($ocLazyLoad, moviePersonsServices) {
                        return $ocLazyLoad.load({
                            name: "moviePersons",
                            files: [
                                "app/moviepersons/moviePersons.module.js"
                            ]
                        });
                    }

                }
            })
            .state('moviePersonProfile', {
                url: "/moviepersons/:id",
                controller: "moviePersonProfileCtrl",
                controllerAs: "vm",
                templateUrl: "app/moviepersons/partials/profile.html",
                resolve: {
                    loginRequired: loginRequired,
                    moviePerson: function ($stateParams, moviePersonsSvc) {
                        return moviePersonsSvc.getMoviePerson($stateParams.id).then(function (data) {
                            return data.data;
                        });
                    },
                    moviePersonsServices: function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: "moviePersonsServices",
                            files: [
                                "app/moviepersons/moviePersonsServices.module.js"
                            ]
                        });
                    },
                    moviePersons: function ($ocLazyLoad, moviePersonsServices) {
                        return $ocLazyLoad.load({
                            name: "moviePersons",
                            files: [
                                "app/moviePersons/moviePersons.module.js"
                            ]
                        });
                    }
                }
            })
            .state('newMoviePerson', {
                url: "/moviePerson/new",
                controller: "moviePersonManageCtrl",
                controllerAs: "vm",
                templateUrl: "app/moviepersons/partials/manageMoviePerson.html",
                cache: false,
                resolve: {
                    loginRequired: loginRequired,
                    moviePerson: function () {
                        return null;
                    },
                    moviePersonsServices: function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: "moviePersonsServices",
                            files: [
                                "app/moviepersons/moviePersonsServices.module.js"
                            ]
                        });
                    },
                    moviePersons: function ($ocLazyLoad, moviePersonsServices, moviePerson) {
                        return $ocLazyLoad.load({
                            name: "moviePersons",
                            files: [
                                "app/moviepersons/moviePersons.module.js"
                            ]
                        });
                    }

                }
            })
            .state('updateMoviePerson', {
                url: "/movieperson/update/:id",
                controller: "moviePersonManageCtrl",
                controllerAs: "vm",
                templateUrl: "app/moviepersons/partials/manageMoviePerson.html",
                cache: false,
                resolve: {
                    loginRequired: loginRequired,
                    moviePersonsServices: function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: "moviePersonsServices",
                            files: [
                                "app/moviepersons/moviePersonsServices.module.js"
                            ]
                        });
                    },
                    moviePerson: function ($stateParams, moviePersonsSvc) {
                        return moviePersonsSvc.getMoviePerson($stateParams.id).then(function (data) {
                            return data.data;
                        });
                    },
                    moviePersons: function ($ocLazyLoad, moviePersonsServices, moviePerson) {
                        return $ocLazyLoad.load({
                            name: "moviePersons",
                            files: [
                                "app/moviepersons/moviePersons.module.js"
                            ]
                        });
                    }

                }
            })

            ;
    }

})();
