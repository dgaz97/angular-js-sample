(function() {

    'use strict';

    angular
        .module('movies', ['moviesServices', 'angularValidator', 'movieRatingsServices', 'genresServices'])
        .controller('moviesOverviewCtrl', moviesOverviewCtrl)
        .controller('movieProfileCtrl', movieProfileCtrl)
        .controller('movieManageCtrl', movieManageCtrl)
        ;

    moviesOverviewCtrl.$inject = ['$scope', 'moviesSvc'];
    function moviesOverviewCtrl($scope, moviesSvc) {
        var vm = this;

        moviesSvc.getMovies().then(function(result) {
            console.log(result.data);
            $scope.movieGridData = new kendo.data.DataSource({
                data: result.data.movies,
                pageSize: 5
            })
        })


        $scope.movieGridOptions = {
            dataBound: function() {
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

    movieManageCtrl.$inject = ['$scope', '$state', 'movie', 'moviesSvc', 'genresSvc', '$stateParams'];
    function movieManageCtrl($scope, $state, movie, moviesSvc, genresSvc, $stateParams) {
        var vm = this;
        vm.movie = movie ? movie : null;
        vm.title = vm.movie ? true : false;


        genresSvc.getGenres().then(function(result) {
            vm.startingGenres = [];
            if (vm.movie != null) vm.movie.genres.forEach(i => vm.startingGenres.push(i.genreId))
            $scope.selectedGenres = vm.startingGenres.slice();

            vm.genres = new kendo.data.DataSource({ data: result.data.genres });
            $scope.selectOptions = {
                dataTextField: "name",
                dataValueField: "genreId",
                dataSource: vm.genres,
                value: $scope.selectedGenres
            }
        });



        //Provjeravamo je li link slike ispravnog formata
        $scope.validateImageUrl = function(text) {
            if ((!text || /^\s*$/.test(text))) return true;
            var validHttp = /^https?:\/\//g.test(text);
            var validImg = /\.jpg$|\.jpeg$|\.png$|\.gif$/g.test(text);
            if (validImg && validHttp) return true;
            else return "URL slike nije ispravnog formata";
        }

        //Provjeravamo je li IMDb link ispravnog formata
        $scope.validateImdbUrl = function(text) {
            var validImdbLink = /^https?:\/\/(www\.)?imdb.com/g.test(text);
            if (validImdbLink) return true;
            else return "IMDb URL nije ispravnog formata";
        }

        //Submit
        $scope.submitForm = function() {

            if (movie) {//Ako ažuriramo film
                moviesSvc.updateMovie(vm.movie.movieId, vm.movie).then(function(result) { });//ažurirati film

                $scope.selectedGenres = $scope.selectedGenres.map(Number);//string array u number array
                var common = vm.startingGenres.filter(value => $scope.selectedGenres.includes(value));//Razlika - žanrovi koji se ne mijenjanju

                vm.startingGenres = vm.startingGenres.filter((el) => !common.includes(el));//maknuti zajedničke iz početnih
                $scope.selectedGenres = $scope.selectedGenres.filter((el) => !common.includes(el));//maknuti zajedničke iz novih

                $scope.selectedGenres.forEach(x => {//Dodati potrebne žanrove
                    moviesSvc.addGenreToMovie(vm.movie.movieId, x).then(function(result) { });
                });
                vm.startingGenres.forEach(x => {//Obrisati potrebne žanrove
                    moviesSvc.removeGenreFromMovie(vm.movie.movieId, x).then(function(result) { });
                });

                $state.go("moviesOverview");
            }
            else {//Ako stvaramo novi film
                moviesSvc.createMovie(vm.movie).then(function(result) {
                    var id = result.data.movieId;

                    $scope.selectedGenres = $scope.selectedGenres.map(Number);//string array u number array
                    var common = vm.startingGenres.filter(value => $scope.selectedGenres.includes(value));//Razlika - žanrovi koji se ne mijenjanju

                    vm.startingGenres = vm.startingGenres.filter((el) => !common.includes(el));//maknuti zajedničke iz početnih
                    $scope.selectedGenres = $scope.selectedGenres.filter((el) => !common.includes(el));//maknuti zajedničke iz novih

                    $scope.selectedGenres.forEach(x => {//Dodati potrebne žanrove
                        moviesSvc.addGenreToMovie(id, x).then(function(result) { });
                    });
                    vm.startingGenres.forEach(x => {//Obrisati potrebne žanrove
                        moviesSvc.removeGenreFromMovie(id, x).then(function(result) { });
                    });
                    $state.go("moviesOverview");
                });
            }
        }

    }

})();