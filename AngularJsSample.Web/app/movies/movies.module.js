(function () {

    'use strict';

    angular
        .module('movies', ['moviesServices', 'angularValidator'])
        .controller('moviesOverviewCtrl', moviesOverviewCtrl)
        .controller('movieProfileCtrl', movieProfileCtrl)
        .controller('movieManageCtrl', movieManageCtrl)
        ;

    moviesOverviewCtrl.$inject = ['$scope', 'moviesSvc'];
    function moviesOverviewCtrl($scope, moviesSvc) {
        var vm = this;
    }

    movieProfileCtrl.$inject = ['$scope', '$state', 'movie', 'moviesSvc', '$stateParams'];
    function movieProfileCtrl($scope, $state, movie, moviesSvc, $stateParams) {
        var vm = this;
        vm.movie = movie;
    }

    movieManageCtrl.$inject = ['$scope', '$state', 'movie', 'moviesSvc', '$stateParams'];
    function movieManageCtrl($scope, $state, movie, moviesSvc, $stateParams) {
        var vm = this;

        vm.movie = movie ? movie : null;
        vm.title = vm.movie ? true : false;


    }

}) ();