(function () {

    'use strict';

    angular
        .module('moviePersonsServices', [])
        .service('moviePersonsSvc', moviePersonsSvc)
        ;

    moviePersonsSvc.$inject = ['$http'];
    function moviePersonsSvc($http) {
        this.getMoviePersons = function () {
            return $http.get(`${serviceBase}/api/moviepersons`);
        }
        this.getMoviePerson = function (id) {
            return $http.get(`${serviceBase}/api/moviepersons/${id}`);
        }
        this.deleteMoviePerson = function (id) {
            return $http.delete(`${serviceBase}/api/moviepersons/${id}`);
        }
        this.createMoviePerson = function (moviePerson) {
            return $http.post(`${serviceBase}/api/moviepersons`, moviePerson);
        }
        this.updateMoviePerson = function (moviePerson) {
            return $http.put(`${serviceBase}/api/moviepersons`, moviePerson);
        }

        //Roles
        this.getRolesOfMoviePerson = function (id) {
            return $http.get(`${serviceBase}/api/moviepersons/roles/${id}`);
        }
        this.addMovie = function (movieRole) {
            return $http.post(`${serviceBase}/api/moviepersons/roles`,movieRole);
        }
        this.removeMovie = function (movieId, movieRoleId, moviePersonId) {
            return $http.delete(`${serviceBase}/api/moviepersons/roles/${movieId}/${movieRoleId}/${moviePersonId}`);
        }
    };

})();
