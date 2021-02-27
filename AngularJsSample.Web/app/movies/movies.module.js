(function () {

    'use strict';

    angular
        .module('movies', ['moviesServices', 'angularValidator', 'movieRatingsServices', 'genresServices'])
        .controller('moviesOverviewCtrl', moviesOverviewCtrl)
        .controller('movieProfileCtrl', movieProfileCtrl)
        .controller('movieManageCtrl', movieManageCtrl)
        ;

    moviesOverviewCtrl.$inject = ['$scope', 'moviesSvc', 'genresSvc'];
    //$scope.movieGridData = {};
    function moviesOverviewCtrl($scope, moviesSvc, genresSvc) {
        var vm = this;

        moviesSvc.getMovies().then(function (result) {
            vm.movies = result.data.movies;
            vm.moviesOriginalCopy = result.data.movies.slice();
        }
        ).then(function () {
            //Set up grid
            $scope.movieGridOptions = {
                dataSource: {
                    data: vm.movies,
                    pageSize: 5
                },
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
                        title: "Kratki opis",
                    },
                    {
                        field: "movieReleaseDate",
                        title: "Datum izlaska",
                        template: '#= kendo.toString(kendo.parseDate(data.movieReleaseDate), "dd.MM.yyyy.") #'
                    },
                    {
                        field: "movieRating",
                        title: "Ocjena",
                    },
                    //{
                    //    field: "genres",
                    //    title: "Žanrovi",
                    //    template: `# data.genres.forEach(x=>{#
                    //               <span class="badge badge-secondary">
                    //                   #: x.name#
                    //               </span><br>
                    //               # })#`
                    //}
                ]
            }

            //Genre dropdown
            genresSvc.getGenres().then(function (result) {
                vm.genres = new kendo.data.DataSource({ data: result.data.genres });
                $scope.selectOptions = {
                    dataTextField: "name",
                    dataValueField: "genreId",
                    dataSource: vm.genres,
                    value: $scope.selectedGenres,
                    placeholder: "Odaberite žanrove",
                    change: function (e) {//You're entering the cringe zone

                        var listOfMovies = vm.moviesOriginalCopy.slice();//Kopiramo početnu listu filmova, po vrijednosti

                        if ($scope.selectedGenres != null) //Ako ima odabranih žanrova
                        {
                            var selected = $scope.selectedGenres.map(Number);//string array ---> number array
                            var toRemove = [];//Array filmova koje treba maknuti
                            for (var i = 0; i < listOfMovies.length; i++) {//Prolazimo kroz listu filmova
                                var genreIdsOfMovie = [];
                                listOfMovies[i].genres.forEach(y => genreIdsOfMovie.push(y.genreId));//Vadimo ID-jeve za lakše iteriranje

                                if (selected.some(x => !genreIdsOfMovie.includes(x))) {//Ako neki žanr od traženih nije prisutan u filmu
                                    toRemove.push(listOfMovies[i].movieId);//Stavljamo ga u array za brisanje
                                }
                            }

                            var purifiedList = listOfMovies.filter(x => !toRemove.includes(x.movieId));//Čistimo početnu listu, makivamo nepotrebne filmove
                            listOfMovies = purifiedList.slice();
                        }

                        vm.movies = listOfMovies.slice();

                        angular.element('#movieGrid').data('kendoGrid').dataSource.data(vm.movies);//Zapisujemo nove podatke u grid

                    }
                }
            });
        });
        $scope.selectedGenres = {};


    }

    movieProfileCtrl.$inject = ['$scope', '$state', 'movie', 'moviesSvc', 'movieRating', 'movieRatingsSvc', '$stateParams', 'moviePersons','movieRoles'];
    function movieProfileCtrl($scope, $state, movie, moviesSvc, movieRating, movieRatingsSvc, $stateParams, moviePersons, movieRoles) {
        var vm = this;
        vm.movie = movie;
        vm.movieRating = movieRating;

        vm.moviePersons = moviePersons;
        vm.allMovieRoles = movieRoles;

        kendo.culture('de-DE');//"hr-HR" kvari rating, izbacuje decimalnu točku (kaže npr. 32 umjesto 3,2)

        //Options for average movie rating
        $scope.ratingOptions = {
            min: 1,
            max: 5,
            label: { template: "<span>#=value# od #=maxValue#</span>" },
            precision: "half",
            readonly: true
        };

        //Options for movie rating of user
        $scope.userRatingOptions = {
            min: 1,
            max: 5,
            label: { template: "<span>#=value# od #=maxValue#</span>" },
            precision: "half",
            change: function (e) {
                movieRatingsSvc.addMovieRating({ "userRating": e.newValue, "movie": { "movieId": vm.movie.movieId } }).then(function (result) {
                    moviesSvc.getMovie(vm.movie.movieId).then(function (data) {
                        vm.movie = data.data;
                        //$scope.ratingOptions.value(data.data.movieRating);
                    }, function (err) {
                        swal.fire("Greška", "Došlo je do greške kod ažuriranja filma: " + err.data.messageDetail, "error");
                    });
                }, function (err) {
                    vm.movieRating.userRating = e.oldValue;
                    swal.fire("Greška", "Došlo je do greške kod dodavanja ratinga, pokušajte ponovno", "error");
                });
            }
        };

        //On start get roles of movie
        moviesSvc.getRolesOfMovie(vm.movie.movieId).then(function (result) {
            vm.movieRoles = result.data.movieRoles;
            $scope.movieRoles = new kendo.data.DataSource({
                data: result.data.movieRoles,
                pageSize: 5
            });
        //Then, set up a grid for movie roles
        }).then(function () {
            $scope.roleGrid = {
                dataSource: $scope.movieRoles,
                columns: [
                    {
                        field: "movieRoleName",
                        title: "Uloga"
                    },
                    {
                        field: "moviePersonName",
                        title: "Naziv osobe",
                        template: '<a ui-sref="moviePersonProfile({id:#: data.moviePersonId#})" ui-sref-opts="{inherit: true}" href=" /moviepersons/#: data.moviePersonId#/">#: data.moviePersonName#</a>'
                    },
                    {
                        field: "dateCreated",
                        title: "Datum kreiranja",
                        template: '#= kendo.toString(kendo.parseDate(data.dateCreated), "dd.MM.yyyy.") #'
                    },
                    {
                        title: "Naredbe",
                        //definiramo gumb
                        command: [
                            {
                                className: "btn btn-danger btn-outline",
                                name: "obrisi",
                                text: "Obriši",
                                click: function (e) {
                                    //Uzmemo red
                                    var tr = $(e.target).closest("tr");
                                    //Dohvatimo podatke iz reda
                                    var data = this.dataItem(tr);
                                    //Otvaramo modal
                                    $scope.deleteMovieRoleOfMovie(data);
                                }
                            }]
                    }
                ]
            }
        });
        
        //A function that generates the layout of SWAL for adding roles
        $scope.generateSwalHtml = function () {
            var result = ``;
            //beginning divs
            result += `<div class="container-fluid">
                       <div class="form-group row">`;

            //Movie person dropdown
            result += `<div class="col-md-6">`
            result += `<label for="moviePerson" class="control-label">Film osoba</label>
                       <select class="form-control" id="moviePerson" name="moviePerson">`

            var options = ``;
            vm.moviePersons.forEach(option => {
                options += `<option value="${option.id}">${option.id} - ${option.firstName} ${option.lastName}</option>`
            });
            result += options;
            result += `</select>`
            result += `</div>`

            //Role dropdown
            result += `<div class="col-md-6">`
            result += `<label for="role" class="control-label">Uloga</label>
                       <select class="form-control" id="role" name="role">`
            var options2 = ``;
            vm.allMovieRoles.forEach(option => {
                options2 += `<option value="${option.movieRoleId}">${option.movieRoleId} - ${option.movieRoleName}</option>`
            });
            result += options2;
            result += `</select>`
            result += `</div>`

            //closing the divs
            result += `</div>
                        </div>`
            return result;
        }

        //Function that opens a SWAL for adding role to movie
        $scope.onAddRoleClick = function () {
            swal.fire({
                title: "Dodaj ulogu",
                width: "60vw",
                //Izgled modala
                html: $scope.generateSwalHtml(),
                confirmButtonText: "Spremi",
                showCancelButton: true,
                cancelButtonText: "Prekini",
                allowOutsideClick: "true",
                //Vratiti će vrijednosti input elemenata
                preConfirm: () => {
                    return {
                        moviePerson: document.getElementById('moviePerson').value,
                        role: document.getElementById('role').value
                    }
                },
                //BS tema na gumbe u modalu
                buttonsStyling: false,
                customClass: {
                    confirmButton: 'btn btn-primary btn-outline',
                    cancelButton: 'btn btn-danger btn-outline'
                }
            }).then(function (result) {
                if (result.isConfirmed) {
                    moviesSvc.addMoviePerson({ movieId: vm.movie.movieId, movieRoleId: result.value.role, moviePersonId: result.value.moviePerson }).then(function (result) {
                        moviesSvc.getRolesOfMovie(vm.movie.movieId).then(function (result2) {
                            var grid = angular.element("#rolesGrid").data("kendoGrid");
                            grid.setDataSource(new kendo.data.DataSource({
                                data: result2.data.movieRoles,
                                pageSize: 5
                            }));
                            //grid.refresh();
                        })
                    }, function (err) {
                        swal.fire("Greška", "Došlo je do greške kod brisanja: " + err.data.message, "error");
                    });
                }
            })
        }

        //Opens a warning SWAL, to check whether the person really wants to delete movie role
        $scope.deleteMovieRoleOfMovie = function (data) {
            console.log(data);
            swal.fire({
                title: "POZOR",
                icon: "warning",
                text: `Jeste li sigurni da želite obrisati ulogu ${data.movieRoleName} osobe ${data.moviePersonName}?`,
                confirmButtonText: "Da, obriši",
                showCancelButton: true,
                cancelButtonText: "Prekini",
                allowOutsideClick: "true",
                //BS tema na gumbe u modalu
                buttonsStyling: false,
                customClass: {
                    confirmButton: 'btn btn-danger btn-outline',
                    cancelButton: 'btn btn-primary btn-outline'
                },
            }).then(function (result) {
                //Ako je kliknuto Da, obriši
                if (result.isConfirmed) {
                    moviesSvc.removeMoviePerson(vm.movie.movieId, data.movieRoleId, data.moviePersonId).then(function (result) {
                        moviesSvc.getRolesOfMovie(vm.movie.movieId).then(function (result2) {
                            var grid = angular.element("#rolesGrid").data("kendoGrid");
                            grid.setDataSource(new kendo.data.DataSource({
                                data: result2.data.movieRoles,
                                pageSize: 5
                            }));
                        });
                    }, function (err) {
                        swal.fire("Greška", "Došlo je do greške kod brisanja: " + err.data.message, "error");
                    })
                }
            });
        }
        //Opens a warning SWAL, to check whether the person really wants to delete movie
        $scope.onDeleteButtonClick = function (e) {
            //Modal za upozorenje o brisanju
            swal.fire({
                title: "POZOR",
                icon: "warning",
                text: "Jeste li sigurni da želite obrisati film `" + vm.movie.movieName + "`?",
                showCancelButton: true,
                confirmButtonText: "Da",
                cancelButtonText: "Ne",
                buttonsStyling: false,
                customClass: {
                    confirmButton: 'btn btn-danger btn-outline',
                    cancelButton: 'btn btn-primary btn-outline'
                },
            })
                .then(function (result) {
                    console.log(result);
                    if (result.isConfirmed) {
                        moviesSvc.deleteMovie(vm.movie.movieId).then(function (data) {
                            //Premješta na pregled svih redatelja
                            $state.go("moviesOverview");
                            //Ili prikazuje modal ako dođe do greške
                        }, function (err) {
                            swal.fire("Greška", "Došlo je do greške kod brisanja: " + err.data.messageDetail, "error");
                        });
                    }
                });
        }
    }

    movieManageCtrl.$inject = ['$scope', '$state', 'movie', 'moviesSvc', 'genresSvc', 'genres', '$stateParams'];
    function movieManageCtrl($scope, $state, movie, moviesSvc, genresSvc, genres, $stateParams) {
        var vm = this;
        vm.title = movie ? true : false;
        vm.movie = movie ? movie : {};
        if (vm.title && vm.movie.moviePosterUrl == 'https://via.placeholder.com/250x400') {
            vm.movie.moviePosterUrl = "";
        }

        vm.startingGenres = [];
        vm.genres = genres;

        if (vm.title) vm.movie.genres.forEach(i => vm.startingGenres.push(i.genreId));
        $scope.selectedGenres = vm.startingGenres.slice();


        $scope.selectOptions = {
            dataTextField: "name",
            dataValueField: "genreId",
            dataSource: { data: vm.genres },
            value: $scope.selectedGenres
        }



        //$scope.genreReady = false;

        $scope.errors = {};
        $scope.errors.movieNameError = false;
        $scope.errors.movieReleaseDateError = false;
        $scope.errors.movieDescriptionError = false;
        $scope.errors.genresError = false;
        $scope.errors.movieRatingError = false;
        $scope.errors.userRatingError = false;

        $scope.validationFunctions = {};

        //Movie name
        $scope.validationFunctions.validateMovieName = function (text) {
            if (typeof text === 'undefined' || !text) {
                $scope.errors.movieNameError = true;
                return "Naziv ne smije biti prazan";
            }
            if (text.length > 200) {
                $scope.errors.movieNameError = true;
                return "Naziv ne smije biti dulji od 200 znakova";
            }
            $scope.errors.movieNameError = false;
            return false;
        }
        //Movie release date
        $scope.validationFunctions.validateMovieReleaseDate = function (text) {
            if (typeof text === 'undefined' || !text) {
                $scope.errors.movieReleaseDateError = true;
                return "Datum izlaska ne smije biti prazan";
            }
            if (text < "1800-01-01") {
                $scope.errors.movieReleaseDateError = true;
                return "Datum ne smije biti prije 1. siječnja 1800.";
            }
            $scope.errors.movieReleaseDateError = false;
            return false;
        }
        //Movie description
        $scope.validationFunctions.validateMovieDescription = function (text) {
            if (typeof text === 'undefined' || !text) {
                $scope.errors.movieDescriptionError = false;
                return false;
            }
            if (text.length > 2000) {
                $scope.errors.movieDescriptionError = true;
                return "Opis ne smije biti dulji od 2000 znakova";
            }
            $scope.errors.movieDescriptionError = false;
            return false;
        }
        //Movie genres
        $scope.validationFunctions.validateMovieGenres = function () {
            var d = angular.element("#movieGenres").data("kendoMultiSelect").value();
            //console.log(d);
            if (d == null || d.length == 0) {
                $scope.errors.genresError = true;
                return "Morate odabrati barem jedan žanr";
            }
            //if (text.length > 2000) {
            //    $scope.errors.moviegenres = true;
            //    return "Opis ne smije biti dulji od 2000 znakova";
            //}
            $scope.errors.genresError = false;
            return false;
        }
        //Poster url
        $scope.validationFunctions.validateImageUrl = function (text) {
            if ((!text || /^\s*$/.test(text))) {
                $scope.errors.imageUrlError = false;
                return false;
            }
            if (text.length > 200) {
                $scope.errors.imageUrlError = true;
                return "Link slike ne smije biti dulji od 200 znakova";
            }
            var validHttp = /^https?:\/\//g.test(text);
            var validImg = /\.jpg$|\.jpeg$|\.png$|\.gif$/g.test(text);
            if (validImg && validHttp) {
                $scope.errors.imageUrlError = false;
                return false;
            }
            else {
                $scope.errors.imageUrlError = true;
                return "URL slike nije ispravnog formata";
            }
        }
        //IMDb url
        $scope.validationFunctions.validateImdbUrl = function (text) {
            if (text.length > 100) {
                $scope.errors.imdbUrlError = true;
                return "IMDb link ne smije biti dulji od 100 znakova";
            }
            var validImdbLink = /^https?:\/\/(www\.)?imdb.com/g.test(text);
            if (validImdbLink) {
                $scope.errors.imdbUrlError = false;
                return false;
            }
            else {
                $scope.errors.imdbUrlError = true;
                return "IMDb URL nije ispravnog formata";
            }
        }


        //Submit
        $scope.submitForm = function ($event) {

            $scope.validationFunctions.validateMovieName(vm.movie.movieName);
            $scope.validationFunctions.validateMovieReleaseDate(vm.movie.movieReleaseDate);
            $scope.validationFunctions.validateMovieDescription(vm.movie.movieDescription);
            $scope.validationFunctions.validateMovieGenres($scope.selectedGenres);
            $scope.validationFunctions.validateImageUrl(vm.movie.moviePosterUrl);
            $scope.validationFunctions.validateImdbUrl(vm.movie.movieImdbUrl);

            //If any control has error
            if (Object.values($scope.errors).some(x => x === true)) {
                return;
            }

            //$event.preventDefault();
            if (vm.title) {//Ako ažuriramo film
                moviesSvc.updateMovie(vm.movie).then(function (result) { });//ažurirati film
                $scope.selectedGenres = angular.element("#movieGenres").data("kendoMultiSelect").value();//Get genres from multiselect

                var common = vm.startingGenres.filter(value => $scope.selectedGenres.includes(value));//Razlika - žanrovi koji se ne mijenjanju

                vm.startingGenres = vm.startingGenres.filter((el) => !common.includes(el));//maknuti zajedničke iz početnih
                $scope.selectedGenres = $scope.selectedGenres.filter((el) => !common.includes(el));//maknuti zajedničke iz novih

                $scope.selectedGenres.forEach(x => {//Dodati potrebne žanrove
                    moviesSvc.addGenreToMovie({ "movie": { "movieId": vm.movie.movieId }, "genre": { "genreId": x } }).then(function (result) { });
                });
                vm.startingGenres.forEach(x => {//Obrisati potrebne žanrove
                    moviesSvc.removeGenreFromMovie(vm.movie.movieId, x).then(function (result) { });
                });

                $state.go("moviesOverview");
            }
            else {//Ako stvaramo novi film
                moviesSvc.createMovie(vm.movie).then(function (result) {
                    var id = result.data.movieId;

                    $scope.selectedGenres = angular.element("#movieGenres").data("kendoMultiSelect").value();//string array u number array
                    var common = vm.startingGenres.filter(value => $scope.selectedGenres.includes(value));//Razlika - žanrovi koji se ne mijenjanju

                    vm.startingGenres = vm.startingGenres.filter((el) => !common.includes(el));//maknuti zajedničke iz početnih
                    $scope.selectedGenres = $scope.selectedGenres.filter((el) => !common.includes(el));//maknuti zajedničke iz novih

                    $scope.selectedGenres.forEach(x => {//Dodati potrebne žanrove
                        moviesSvc.addGenreToMovie({ "movie": { "movieId": id }, "genre": { "genreId": x } }).then(function (result) { });
                    });
                    vm.startingGenres.forEach(x => {//Obrisati potrebne žanrove
                        moviesSvc.removeGenreFromMovie(id, x).then(function (result) { });
                    });
                    $state.go("moviesOverview");
                });
            }
        }

    }

})();