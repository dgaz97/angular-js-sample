﻿(function () {

    'use script';

    angular
        .module('moviesServices', [])
        .service('moviesSvc', moviesSvc)
        ;

    moviesSvc.$inject = ['$http'];
    function moviesSvc($http) {
        this.getMovies = function () {
            return $http.get(`${serviceBase}/api/movies`);
        }
        this.getMovie = function (id) {
            return $http.get(`${serviceBase}/api/movies/${id}`);
        }
        this.getGenresOfMovie = function (id) {
            return $http.get(`${serviceBase}/api/movies/genres/${id}`);
        }
        this.deleteMovie = function (id) {
            return $http.delete(`${serviceBase}/api/movies/${id}`);
        }
        this.createMovie = function (movie) {
            return $http.post(`${serviceBase}/api/movies`, movie);
        }
        this.updateMovie = function (movie) {
            return $http.put(`${serviceBase}/api/movies`, movie);
        }
        this.addGenreToMovie = function (movieGenre) {
            return $http.post(`${serviceBase}/api/movies/genres`, movieGenre);
        }
        this.removeGenreFromMovie = function (movieId, genreId) {
            return $http.delete(`${serviceBase}/api/movies/${movieId}/${genreId}`);
        }
    }




})();