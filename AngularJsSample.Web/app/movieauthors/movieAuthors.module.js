(function () {

    'use strict';

    angular
        .module('movieAuthors', ['movieAuthorsServices'])
        .controller('movieAuthorsOverviewCtrl', movieAuthorsOverviewCtrl)
        .controller('movieAuthorProfileCtrl', movieAuthorProfileCtrl)
        .controller('movieAuthorManageCtrl', movieAuthorManageCtrl)
        ;

    movieAuthorsOverviewCtrl.$inject = ['$scope'];
    function movieAuthorsOverviewCtrl($scope) {
        var vm = this;


    };

    movieAuthorProfileCtrl.$inject=['$scope']
    function movieAuthorProfileCtrl($scope) {
        var vm = this;
    }

    movieAuthorManageCtrl.$inject = ['$scope', 'movieAuthorsSvc', 'movieAuthor']
    function movieAuthorManageCtrl($scope, movieAuthorsSvc, movieAuthor) {
        var vm = this;

        vm.movieAuthor = movieAuthor ? movieAuthor.data : null;

        if (vm.movieAuthor) {
            //update
        }
        else {
            //create
        }

    }

})();
