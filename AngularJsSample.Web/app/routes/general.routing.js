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
            .state('authorsOverview', {
                url: "/authors",
                controller: "authorsOverviewCtrl",
                controllerAs: "vm",
                templateUrl: "app/authors/partials/authorsOverview.html",
                resolve: {
                    loginRequired: loginRequired,
                    authorsServices: function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: "authorsServices",
                            files: [
                                "app/authors/authorsServices.module.js"
                            ]
                        });
                    },
                    authors: function ($ocLazyLoad, authorsServices) {
                        return $ocLazyLoad.load({
                            name: "authors",
                            files: [
                                "app/authors/authors.module.js"
                            ]
                        });
                    }

                }
            })
            .state('authorProfile', {
                url: "/authors/:id",
                controller: "authorProfileCtrl",
                controllerAs: "vm",
                templateUrl: "app/authors/partials/profile.html",
                resolve: {
                    loginRequired: loginRequired,
                    authorsServices: function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: "authorsServices",
                            files: [
                                "app/authors/authorsServices.module.js"
                            ]
                        });
                    },
                    authors: function ($ocLazyLoad, authorsServices) {
                        return $ocLazyLoad.load({
                            name: "authors",
                            files: [
                                "app/authors/authors.module.js"
                            ]
                        });
                    }

                }
            })
            .state('newAuthor', {
                url: "/author/new",
                controller: "authorManageCtrl",
                controllerAs: "vm",
                templateUrl: "app/authors/partials/manageAuthor.html",
                cache: false,
                resolve: {
                    loginRequired: loginRequired,
                    author: function () {
                        return null
                    },
                    authorsServices: function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: "authorsServices",
                            files: [
                                "app/authors/authorsServices.module.js"
                            ]
                        });
                    },
                    authors: function ($ocLazyLoad, authorsServices, author) {
                        return $ocLazyLoad.load({
                            name: "authors",
                            files: [
                                "app/authors/authors.module.js"
                            ]
                        });
                    }

                }
            })
            .state('updateAuthor', {
                url: "/author/update/:id",
                controller: "authorManageCtrl",
                controllerAs: "vm",
                templateUrl: "app/authors/partials/updateAuthor.html",
                cache: false,
                resolve: {
                    loginRequired: loginRequired,
                    authorsServices: function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: "authorsServices",
                            files: [
                                "app/authors/authorsServices.module.js"
                            ]
                        });
                    },
                    author: function ($stateParams,authorsServices, authorsSvc) {
                        //TODO: fetch with authorsSvc
                        return null;
                    },
                    authors: function ($ocLazyLoad, authorsServices, author) {
                        return $ocLazyLoad.load({
                            name: "authors",
                            files: [
                                "app/authors/authors.module.js"
                            ]
                        });
                    }

                }
            })

            ;
    }

})();
