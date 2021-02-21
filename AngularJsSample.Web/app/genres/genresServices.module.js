(function () {

    'use strict';

    angular
        .module('genresServices', [])
        .service('genresSvc', genresSvc)
        ;

    genresSvc.$inject = ['$http'];
    function genresSvc($http) {
        this.getGenres = function () {
            return $http.get(`${serviceBase}/api/genres`);
        }
        this.getGenre = function (id) {//TODO delete?
            return $http.get(`${serviceBase}/api/genres/${id}`);
        }
        this.deleteGenre = function (id) {
            return $http.delete(`${serviceBase}/api/genres/${id}`);
        }
        this.createGenre = function (genre) {
            return $http.post(`${serviceBase}/api/genres`, genre);
        }
        this.updateGenre = function (genre) {
            return $http.put(`${serviceBase}/api/genres`, genre);
        }
    }

}) ();