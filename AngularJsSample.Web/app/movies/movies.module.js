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

        moviesSvc.getMovies().then(function (result) {
            console.log(result.data);
            $scope.movieGridData = new kendo.data.DataSource({
                data: result.data.movies,
                pageSize: 5
            })
        })

        $scope.movieGridOptions = {
            dataBound: function () {
                for (var i = 0; i < this.columns.length; i++) {
                    if (i == 1) continue;
                    this.autoFitColumn(i);
                }
            },
            columns: [
                {
                    field: "movieName",
                    title: "Naziv",
                    template: '<a ui-sref="movieProfile({ id:#: data.movieId#})" href=" /movies/#: data.movieId#">#: data.movieName#</a>'
                },
                {
                    field: "movieDescription",
                    title: "Opis",
                    //template: '<a ui-sref="movieProfile({ id:#: data.id#})" href=" /movies/#: data.id#">#: data.movieDescription#</a>'
                },
                {
                    field: "movieReleaseDate",
                    title: "Mjesto rođenja",
                    template: '#= kendo.toString(kendo.parseDate(data.movieReleaseDate), "dd.MM.yyyy.") #'
                },
                {
                    field: "movieRating",
                    title: "Ocjena",
                },
            ]
        }
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