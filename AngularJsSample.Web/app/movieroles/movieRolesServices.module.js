(function () {

    'use strict';

    angular
        .module('movieRolesServices', [])
        .service('movieRolesSvc', movieRolesSvc)
        ;

    movieRolesSvc.$inject = ['$http'];
    function movieRolesSvc($http) {
        this.getMovieRoles = function () {
            return $http.get(`${serviceBase}/api/movieroles`);
        }
        this.getMovieRole = function (id) {
            return $http.get(`${serviceBase}/api/movieroles/${id}`);
        }
        this.deleteMovieRole = function (id) {
            return $http.delete(`${serviceBase}/api/movieroles/${id}`);
        }
        this.createMovieRole = function (movieRole) {
            return $http.post(`${serviceBase}/api/movieroles`, movieRole);
        }
        this.updateMovieRole = function (movieRole) {
            return $http.put(`${serviceBase}/api/movieroles`, movieRole);
        }

        this.getRoleOfPersonInMovie = function (movieId, moviePersonId) {
            return $http.get(`${serviceBase}/api/movieroles/${movieId}/${moviePersonId}`);
        }
    }
}) ();