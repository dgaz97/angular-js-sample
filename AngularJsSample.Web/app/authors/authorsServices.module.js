(function () {

    'use strict';

    angular
        .module('authorsServices', [])
        .service('authorsSvc', authorsSvc)
        ;

    authorsSvc.$inject = ['$http'];
    function authorsSvc($http) {
        this.getAuthors = function () {
            return $http.get(`${serviceBase}/api/authors`);
        }
        this.getAuthor = function (id) {
            return $http.get(`${serviceBase}/api/authors/${id}`);
        }
        this.deleteAuthor = function (id) {
            return $http.delete(`${serviceBase}/api/authors/${id}`);
        }
        this.createAuthor = function (author) {
            return $http.post(`${serviceBase}/api/authors`, author);
        }
        this.updateAuthor = function (id, author) {
            return $http.put(`${serviceBase}/api/authors/${id}`, author);
        }
    };

})();
