(function () {

    'use strict';

    angular
        .module('movieAuthorsServices', [])
        .service('movieAuthorsSvc', movieAuthorsSvc)
        ;

    movieAuthorsSvc.$inject = ['$http'];
    function movieAuthorsSvc($http) {
        this.getMovieAuthors = function () {
            return $http.get(`${serviceBase}/api/movieauthors`);
        }
        this.getMovieAuthor = function (id) {
            return $http.get(`${serviceBase}/api/movieauthors/${id}`);
        }
        this.deleteMovieAuthor = function (id) {
            return $http.delete(`${serviceBase}/api/movieauthors/${id}`);
        }
        this.createMovieAuthor = function (movieAuthor) {
            return $http.post(`${serviceBase}/api/movieauthors`, movieAuthor);
        }
        this.updateMovieAuthor = function (id, movieAuthor) {
            return $http.put(`${serviceBase}/api/movieauthors/${id}`, movieAuthor);
        }
    };

})();
