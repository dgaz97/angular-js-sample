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
            //MoviePersons begin
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
                    moviePersons: function ($ocLazyLoad) {
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
                    moviePersons: function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: "moviePersons",
                            files: [
                                "app/moviepersons/moviePersons.module.js"
                            ]
                        });
                    }

                }
            })
            //MoviePersons end
            //Genres begin
            .state('genresOverview',{
                url: "/genres",
                controller:"genresOverviewCtrl",
                controllerAs: "vm",
                templateUrl: "app/genres/partials/genresOverview.html",
                cache: false,
                resolve: {
                    loginRequired: loginRequired,
                    genresServices: function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: "genresServices",
                            files: [
                                "app/genres/genresServices.module.js"
                            ]
                        });
                    },
                    genres: function ($ocLazyLoad, genresServices) {
                        return $ocLazyLoad.load({
                            name: "genres",
                            files: [
                                "app/genres/genres.module.js"
                            ]
                        });
                    }

                }
            })
            //Genres end
            //Movies begin
            .state('moviesOverview', {
                url: "/movies",
                controller: "moviesOverviewCtrl",
                controllerAs: "vm",
                templateUrl: "app/movies/partials/moviesOverview.html",
                resolve: {
                    loginRequired: loginRequired,
                    moviesServices: function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: "moviesServices",
                            files: [
                                "app/movies/moviesServices.module.js"
                            ]
                        });
                    },
                    movieRatingsServices: function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: "movieRatingsServices",
                            files: [
                                "app/movieratings/movieRatingsServices.module.js"
                            ]
                        });
                    },
                    genresServices: function($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: "genresServices",
                            files: [
                                "app/genres/genresServices.module.js"
                            ]
                        });
                    },
                    movies: function ($ocLazyLoad, moviesServices) {
                        return $ocLazyLoad.load({
                            name: "movies",
                            files: [
                                "app/movies/movies.module.js"
                            ]
                        });
                    }

                }
            })
            .state('movieProfile', {
                url: "/movies/:id",
                controller: "movieProfileCtrl",
                controllerAs: "vm",
                templateUrl: "app/movies/partials/profile.html",
                resolve: {
                    loginRequired: loginRequired,
                    movie: function ($stateParams, moviesSvc) {
                        return moviesSvc.getMovie($stateParams.id).then(function (data) {
                            return moviesSvc.getGenresOfMovie($stateParams.id).then(function (data2) {
                                data.data.genres = data2.data.genres;
                                return data.data;
                            })
                        });
                    },
                    movieRating: function ($stateParams, movieRatingsSvc) {
                        return movieRatingsSvc.getMovieRatingByUserAndMovie(0,$stateParams.id).then(function (data) {
                            return data.data.movieRating;
                        });
                    },
                    moviesServices: function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: "moviesServices",
                            files: [
                                "app/movies/moviesServices.module.js"
                            ]
                        });
                    },
                    movieRatingsServices: function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: "movieRatingsServices",
                            files: [
                                "app/movieratings/movieRatingsServices.module.js"
                            ]
                        });
                    },
                    genresServices: function($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: "genresServices",
                            files: [
                                "app/genres/genresServices.module.js"
                            ]
                        });
                    },
                    movies: function ($ocLazyLoad, moviesServices) {
                        return $ocLazyLoad.load({
                            name: "movies",
                            files: [
                                "app/movies/movies.module.js"
                            ]
                        });
                    }
                }
            })
            .state('newMovie', {
                url: "/movie/new",
                controller: "movieManageCtrl",
                controllerAs: "vm",
                templateUrl: "app/movies/partials/manageMovie.html",
                cache: false,
                resolve: {
                    loginRequired: loginRequired,
                    movie: function () {
                        return null;
                    },
                    moviesServices: function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: "moviesServices",
                            files: [
                                "app/movies/moviesServices.module.js"
                            ]
                        });
                    },
                    movieRatingsServices: function () {
                        return null;
                    },
                    genresServices: function($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: "genresServices",
                            files: [
                                "app/genres/genresServices.module.js"
                            ]
                        });
                    },
                    genres: function (genresSvc) {
                        return genresSvc.getGenres().then(function (data) {
                            return data.data.genres;
                        })
                    },
                    movies: function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: "movies",
                            files: [
                                "app/movies/movies.module.js"
                            ]
                        });
                    }

                }
            })
            .state('updateMovie', {
                url: "/movie/update/:id",
                controller: "movieManageCtrl",
                controllerAs: "vm",
                templateUrl: "app/movies/partials/manageMovie.html",
                cache: false,
                resolve: {
                    loginRequired: loginRequired,
                    moviesServices: function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: "moviesServices",
                            files: [
                                "app/movies/moviesServices.module.js"
                            ]
                        });
                    },
                    genresServices: function($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: "genresServices",
                            files: [
                                "app/genres/genresServices.module.js"
                            ]
                        });
                    },
                    movieRatingsServices: function () {
                        return null;
                    },
                    movie: function ($stateParams, moviesSvc) {
                        return moviesSvc.getMovie($stateParams.id).then(function (data) {
                            return moviesSvc.getGenresOfMovie($stateParams.id).then(function (data2) {
                                data.data.genres = data2.data.genres;
                                return data.data;
                            })
                        });
                    },
                    genres: function (genresSvc) {
                        return genresSvc.getGenres().then(function (data) {
                            return data.data.genres;
                        })
                    },
                    movies: function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: "movies",
                            files: [
                                "app/movies/movies.module.js"
                            ]
                        });
                    }

                }
            })
            //Movies end

            ;
    }

})();
