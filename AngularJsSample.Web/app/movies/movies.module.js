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

    movieProfileCtrl.$inject = ['$scope', '$state', 'movie', 'moviesSvc', 'movieRating', 'movieRatingsSvc', '$stateParams'];
    function movieProfileCtrl($scope, $state, movie, moviesSvc, movieRating, movieRatingsSvc, $stateParams) {
        var vm = this;
        vm.movie = movie;
        vm.movieRating = movieRating;

        kendo.culture('de-DE');//"hr-HR" kvari rating, izbacuje decimalnu točku (kaže npr. 32 umjesto 3,2)

        $scope.ratingOptions = {
            min: 1,
            max: 5,
            label: { template: "<span>#=value# od #=maxValue#</span>" },
            precision: "half",
            readonly: true
        };

        $scope.genreOptions = {
            dataTextField: "name",
            dataSource: {
                data: vm.movie.genres
            }
        }

        $scope.userRatingOptions = {
            min: 1,
            max: 5,
            label: { template: "<span>#=value# od #=maxValue#</span>" },
            precision: "half",
            change: function(e) {
                movieRatingsSvc.addMovieRating(vm.movie.movieId, { "userRating": e.newValue }).then(function(result) {
                    moviesSvc.getMovie(vm.movie.movieId).then(function(data) {
                        vm.movie = data.data;
                        //$scope.ratingOptions.value(data.data.movieRating);
                    }, function(err) {
                        swal.fire("Greška", "Došlo je do greške kod ažuriranja filma: " + err.data.messageDetail, "error");
                    });
                }, function(err) {
                    vm.movieRating.userRating = e.oldValue;
                    swal.fire("Greška", "Došlo je do greške kod dodavanja ratinga, pokušajte ponovno", "error");
                });
            }
        };

        $scope.deleteButtonOptions = {
            click: function(e) {
                //Modal za upozorenje o brisanju
                swal.fire({
                    title: "POZOR",
                    text: "Jeste li sigurni da želite obrisati film `" + vm.movie.movieName + "`?",
                    showCancelButton: true,
                    confirmButtonText: "Da",
                    cancelButtonText: "Ne",
                    closeOnCancel: true,
                    closeOnConfirm: true,
                    closeOnEsc: true
                })
                    .then(function(isConfirm) {
                        if (isConfirm) {
                            moviesSvc.deleteMovie(vm.movie.movieId).then(function(data) {
                                //Premješta na pregled svih redatelja
                                $state.go("moviesOverview");
                                //Ili prikazuje modal ako dođe do greške
                            }, function(err) {
                                swal.fire("Greška", "Došlo je do greške kod brisanja: " + err.data.messageDetail, "error");
                            });
                        }
                    });
            }
        };

    }

    movieManageCtrl.$inject = ['$scope', '$state', 'movie', 'moviesSvc', '$stateParams'];
    function movieManageCtrl($scope, $state, movie, moviesSvc, $stateParams) {
        var vm = this;

        vm.movie = movie ? movie : null;
        vm.title = vm.movie ? true : false;


    }

}) ();