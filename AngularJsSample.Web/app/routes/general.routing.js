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
            .state('movieAuthorsOverview', {
                url: "/movieauthors",
                controller: "movieAuthorsOverviewCtrl",
                controllerAs: "vm",
                templateUrl: "app/movieauthors/partials/movieAuthorsOverview.html",
                resolve: {
                    loginRequired: loginRequired,
                    movieAuthorsServices: function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: "movieAuthorsServices",
                            files: [
                                "app/movieauthors/movieAuthorsServices.module.js"
                            ]
                        });
                    },
                    movieAuthors: function ($ocLazyLoad, movieAuthorsServices) {
                        return $ocLazyLoad.load({
                            name: "movieAuthors",
                            files: [
                                "app/movieauthors/movieAuthors.module.js"
                            ]
                        });
                    }

                }
            })
            .state('movieAuthorProfile', {
                url: "/movieauthors/:id",
                controller: "movieAuthorProfileCtrl",
                controllerAs: "vm",
                templateUrl: "app/movieauthors/partials/profile.html",
                resolve: {
                    loginRequired: loginRequired,
                    movieAuthor: function ($stateParams, movieAuthorsSvc) {
                        //TODO: fetch with authorsSvc
                        return movieAuthorsSvc.getMovieAuthor($stateParams.id).then(function (data) {
                            return data.data;
                        });
                    },
                    movieAuthorsServices: function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: "movieAuthorsServices",
                            files: [
                                "app/movieauthors/movieAuthorsServices.module.js"
                            ]
                        });
                    },
                    movieAuthors: function ($ocLazyLoad, movieAuthorsServices) {
                        return $ocLazyLoad.load({
                            name: "movieAuthors",
                            files: [
                                "app/movieauthors/movieAuthors.module.js"
                            ]
                        });
                    }
                }
            })
            .state('newMovieAuthor', {
                url: "/movieauthor/new",
                controller: "movieAuthorManageCtrl",
                controllerAs: "vm",
                templateUrl: "app/movieauthors/partials/manageMovieAuthor.html",
                cache: false,
                resolve: {
                    loginRequired: loginRequired,
                    movieAuthor: function () {
                        return null;
                    },
                    movieAuthorsServices: function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: "movieAuthorsServices",
                            files: [
                                "app/movieauthors/movieAuthorsServices.module.js"
                            ]
                        });
                    },
                    movieAuthors: function ($ocLazyLoad, movieAuthorsServices, movieAuthor) {
                        return $ocLazyLoad.load({
                            name: "movieAuthors",
                            files: [
                                "app/movieauthors/movieAuthors.module.js"
                            ]
                        });
                    }

                }
            })
            .state('updateMovieAuthor', {
                url: "/movieauthor/update/:id",
                controller: "movieAuthorManageCtrl",
                controllerAs: "vm",
                //templateUrl: "app/movieauthors/partials/updateMovieAuthor.html",//TODO: možda manageMovieAuthor?
                templateUrl: "app/movieauthors/partials/manageMovieAuthor.html",
                cache: false,
                resolve: {
                    loginRequired: loginRequired,
                    movieAuthorsServices: function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: "movieAuthorsServices",
                            files: [
                                "app/movieauthors/movieAuthorsServices.module.js"
                            ]
                        });
                    },
                    movieAuthor: function ($stateParams, movieAuthorsSvc) {
                        //TODO: fetch with authorsSvc
                        return movieAuthorsSvc.getMovieAuthor($stateParams.id).then(function (data) {
                            return data.data;
                        });
                    },
                    movieAuthors: function ($ocLazyLoad, movieAuthorsServices, movieAuthor) {
                        return $ocLazyLoad.load({
                            name: "movieAuthors",
                            files: [
                                "app/movieauthors/movieAuthors.module.js"
                            ]
                        });
                    }

                }
            })

            ;
    }

})();
