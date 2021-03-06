﻿(function () {

    'use strict';

    angular
        .module('moviePersons', ['moviePersonsServices', 'angularValidator'])
        .controller('moviePersonsOverviewCtrl', moviePersonsOverviewCtrl)
        .controller('moviePersonProfileCtrl', moviePersonProfileCtrl)
        .controller('moviePersonManageCtrl', moviePersonManageCtrl)
        ;

    moviePersonsOverviewCtrl.$inject = ['$scope', 'moviePersonsSvc'];
    function moviePersonsOverviewCtrl($scope, moviePersonsSvc) {
        var vm = this;
        //k-ng-model za sortiranje
        $scope.sortParams = {};

        var signalRConn = $.connection;
        // Proxy created on the fly
        signalRConn.hub.url = `${serviceBase}/signalr`;
        var hub = signalRConn.myHub;


        // Declare a function on the chat hub so the server can invoke it
        hub.client.updateMoviePersons = function () {
            //Dohvati nove podatke
            moviePersonsSvc.getMoviePersons().then(function (result) {
                $scope.moviePersonGridData = new kendo.data.DataSource({
                    data: result.data.persons,
                    pageSize: 20
                });
            });
        }
        // Start the connection
        signalRConn.hub.start().done(function () {
        });

        //Dohvaćamo podatke preko servisa
        moviePersonsSvc.getMoviePersons().then(function (result) {
            //Spremamo u DataSource
            $scope.moviePersonGridData = new kendo.data.DataSource({
                data: result.data.persons,
                pageSize: 20
            });

            //postavke za grid
            $scope.moviePersonGrid = {
                //groupable: true,
                //sortable: true,

                //Auto-Fit sve stupce osim petog (opis)
                dataBound: function () {
                    for (var i = 0; i < this.columns.length; i++) {
                        if (i == 5) continue;
                        this.autoFitColumn(i);
                    }
                },
                //Definiranje stupaca
                columns: [
                    {
                        field: "id",
                        title: "Id",
                        template: '<a ui-sref="moviePersonProfile({ id:#: data.id#})" href=" /moviepersons/#: data.id#">#: data.id#</a>'
                    },
                    {
                        template: '<a ui-sref="moviePersonProfile({id:#: data.id#})" href="/moviepersons/#: data.id#"><div align="center"><img src="#: data.imageUrl#" style="max-width: 100%" /></div></a>',
                        title: "Foto"
                    },
                    {
                        field: "firstName",
                        title: "Ime",
                        template: '<a ui-sref="moviePersonProfile({ id:#: data.id#})" href=" /moviepersons/#: data.id#">#: data.firstName#</a>'
                    },
                    {
                        field: "lastName",
                        title: "Prezime",
                        template: '<a ui-sref="moviePersonProfile({ id:#: data.id#})" href=" /moviepersons/#: data.id#">#: data.lastName#</a>'
                    },
                    {
                        field: "birthPlace",
                        title: "Mjesto rođenja"
                    },
                    {
                        field: "biography",
                        title: "Opis",
                    },
                    {
                        field: "popularity",
                        title: "Popularnost",
                        template: "<p>\\##: data.popularity#</p>"
                    }
                ]
            };
        });

        //Parametri sortiranja, koriste se za DropDownList
        $scope.dropdownlistData = new kendo.data.DataSource({
            data: [
                {
                    "text": "Datum kreiranja uzlazno",
                    "value":
                    {
                        col: "dateCreated",
                        dir: "asc"
                    }
                },
                {
                    "text": "Datum kreiranja silazno",
                    "value":
                    {
                        col: "dateCreated",
                        dir: "desc"
                    }
                },
                {
                    "text": "Popularnost uzlazno",
                    "value":
                    {
                        col: "popularity",
                        dir: "asc"
                    }
                },
                {
                    "text": "Popularnost silazno",
                    "value":
                    {
                        col: "popularity",
                        dir: "desc"
                    }
                }
            ]
        })

        $scope.onSelectChanged = function (kendoEvent) {
            //Dohvaćamo podatke grida, te ih sortiramo: field je po kojem stupce, dir je u kojem smjeru (uzlazno ili silazno)
            $scope.moviePersonGridData.sort({ field: $scope.sortParams.col, dir: $scope.sortParams.dir });
        }

    };


    moviePersonProfileCtrl.$inject = ['$scope', '$state', 'moviePerson', 'moviePersonsSvc', '$stateParams', 'movies', 'movieRoles']
    function moviePersonProfileCtrl($scope, $state, moviePerson, moviePersonsSvc, $stateParams, movies, movieRoles) {
        var vm = this;

        vm.moviePerson = moviePerson;

        vm.movies = movies;
        vm.allMovieRoles = movieRoles;


        $scope.movieRoles = {};
        moviePersonsSvc.getRolesOfMoviePerson(vm.moviePerson.id).then(function (result) {
            vm.movieRoles = result.data.movieRoles;
            $scope.movieRoles = new kendo.data.DataSource({
                data: result.data.movieRoles,
                pageSize: 5
            });
        }).then(function () {
            $scope.roleGrid = {
                dataSource: $scope.movieRoles,
                columns: [
                    {
                        field: "movieRoleName",
                        title: "Uloga"
                    },
                    {
                        field: "movieName",
                        title: "Naziv filma",
                        template: '<a ui-sref="movieProfile({id:#: data.movieId#})" ui-sref-opts="{inherit: true}" href=" /movies/#: data.movieId#/">#: data.movieName#</a>'
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
                                    $scope.deleteMovieRoleOfPerson(data);
                                }
                            }]
                    }
                ]
            }
        });

        $scope.deleteMovieRoleOfPerson = function (data) {
            swal.fire({
                title: "POZOR",
                icon: "warning",
                text: `Jeste li sigurni da želite obrisati ulogu ${data.movieRoleName} u filmu ${data.movieName}?`,
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
                    moviePersonsSvc.removeMovie(data.movieId, data.movieRoleId, vm.moviePerson.id).then(function (result) {
                        moviePersonsSvc.getRolesOfMoviePerson(vm.moviePerson.id).then(function (result2) {
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
        $scope.selectOptions = {
            dataTextField: "movieName",
            dataValueField: "movieId",
            dataSource: { data: vm.movies }
        }

        $scope.generateSwalHtml = function () {
            var result = ``;
            //beginning divs
            result += `<div class="container-fluid">
                       <div class="form-group row">`;

            //Movie dropdown
            result += `<div class="col-md-6">`
            result += `<label for="movie" class="control-label">Film</label>
                       <select class="form-control" id="movie" name="movie">`

            var options = ``;
            vm.movies.forEach(option => {
                options += `<option value="${option.movieId}">${option.movieId} - ${option.movieName}</option>`
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
                        movie: document.getElementById('movie').value,
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
                    moviePersonsSvc.addMovie({ movieId: result.value.movie, movieRoleId: result.value.role, moviePersonId: vm.moviePerson.id }).then(function (result) {
                        moviePersonsSvc.getRolesOfMoviePerson(vm.moviePerson.id).then(function (result2) {
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



        //Kendo button sucks, can't remove k-button class, so i'm using a normal button with click listener
        $scope.onDeleteButtonClick = function (e) {
            swal.fire({
                title: "POZOR",
                icon: "warning",
                text: "Jeste li sigurni da želite obrisati osobu `" + vm.moviePerson.firstName + " " + vm.moviePerson.lastName + "`?",
                showCancelButton: true,
                confirmButtonText: "Da",
                cancelButtonText: "Ne",
                closeOnCancel: true,
                closeOnConfirm: true,
                closeOnEsc: true,
                buttonsStyling: false,
                customClass: {
                    confirmButton: 'btn btn-danger btn-outline',
                    cancelButton: 'btn btn-primary btn-outline'
                },
            }).then(function (result) {
                if (result.isConfirmed) {
                    moviePersonsSvc.deleteMoviePerson($stateParams.id).then(function (data) {

                        // Proxy created on the fly
                        var signalRConn = $.connection;
                        signalRConn.hub.url = `${serviceBase}/signalr`;
                        //Dohvaćamo Hub (MyHub klasa u Api projektu)
                        var hub = signalRConn.myHub;

                        signalRConn.hub.start().done(function () {
                            hub.server.refresh();
                        });
                        signalRConn.hub.stop();

                        //Premješta na pregled svih redatelja
                        $state.go("moviePersonsOverview");
                        //Ili prikazuje modal ako dođe do greške
                    }, function (err) {
                        swal.fire("Greška", "Došlo je do greške kod brisanja: " + err.data.message, "error");
                    });
                }
            })
        };

    }

    moviePersonManageCtrl.$inject = ['$scope', 'moviePersonsSvc', 'moviePerson', '$state', '$stateParams']
    function moviePersonManageCtrl($scope, moviePersonsSvc, moviePerson, $state, $stateParams) {
        var vm = this;

        //Određuje uređivamo li postojećeg redatelja, ili stvaramo novog
        vm.title = moviePerson ? true : false;
        vm.moviePerson = moviePerson ? moviePerson : {};
        if (vm.title && moviePerson.imageUrl == "https://via.placeholder.com/200x200") {
            moviePerson.imageUrl = "";

        }


        //Trenutno vrijeme, treba da ograničimo datum rođenja autora
        vm.now = new Date().toISOString();

        $scope.errors = {};
        $scope.errors.firstNameError = false;
        $scope.errors.lastNameError = false;
        $scope.errors.birthDateError = false;
        $scope.errors.birthPlaceError = false;
        $scope.errors.biographyError = false;
        $scope.errors.imageUrlError = false;
        $scope.errors.imdbUrlError = false;
        $scope.errors.popularityError = false;

        $scope.validationFunctions = {};//if function returns false, it means that there's no error
        //First name
        $scope.validationFunctions.validateFirstName = function (text) {
            if (typeof text === 'undefined' || !text) {
                $scope.errors.firstNameError = true;
                return "Ime ne smije biti prazno";
            }
            if (text.length > 50) {
                $scope.errors.firstNameError = true;
                return "Ime ne smije biti dulje od 50 znakova";
            }
            $scope.errors.firstNameError = false;
            return false;
        }
        //Last name
        $scope.validationFunctions.validateLastName = function (text) {
            if (typeof text === 'undefined' || !text) {
                $scope.errors.lastNameError = true;
                return "Prezime ne smije biti prazno";
            }
            if (text.length > 50) {
                $scope.errors.lastNameError = true;
                return "Prezime ne smije biti dulje od 50 znakova";
            }
            $scope.errors.lastNameError = false;
            return false;
        }
        //Birth date
        $scope.validationFunctions.validateBirthDate = function (text) {
            if (typeof text === 'undefined' || !text) {
                $scope.errors.birthDateError = true;
                return "Datum rođenja ne smije biti prazan";
            }
            if (text > vm.now) {
                $scope.errors.birthDateError = true;
                return "Datum rođenja ne smije biti nakon današnjeg";
            }
            if (text < "1800-01-01") {
                $scope.errors.birthDateError = true;
                return "Datum rođenja ne smije biti prije 1. siječnja 1800.";
            }
            $scope.errors.birthDateError = false;
            return false;
        }
        //Birth place
        $scope.validationFunctions.validateBirthPlace = function (text) {
            if (typeof text === 'undefined' || !text) {
                $scope.errors.birthPlaceError = true;
                return "Mjesto rođenja ne smije biti prazno";
            }
            if (text.length > 50) {
                $scope.errors.birthPlaceError = true;
                return "Mjesto rođenja ne smije biti dulje od 50 znakova";
            }
            $scope.errors.birthPlaceError = false;
            return false;
        }
        //Biography
        $scope.validationFunctions.validateBiography = function (text) {
            if (typeof text === 'undefined' || !text) {
                $scope.errors.biographyError = false;
                return false;
            }
            if (text.length > 2000) {
                $scope.errors.biographyError = true;
                return "Opis ne smije biti dulji od 2000 znakova";
            }
            $scope.errors.biographyError = false;
            return false;
        }
        //Image url
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
        //Popularity
        $scope.validationFunctions.validatePopularity = function (text) {
            if (typeof text === 'undefined' || !text) {
                $scope.errors.popularityError = true;
                return "Popularnost ne smije biti prazna";
            }
            if (text < 1) {
                $scope.errors.popularityError = true;
                return "Popularnost ne smije biti manja od 1";
            }
            $scope.errors.popularityError = false;
            return false;
        }






        $scope.submitForm = function ($event) {
            //Validate all inputs
            $scope.validationFunctions.validateFirstName(vm.moviePerson.firstName);
            $scope.validationFunctions.validateLastName(vm.moviePerson.lastName);
            $scope.validationFunctions.validateBirthDate(vm.moviePerson.birthDate);
            $scope.validationFunctions.validateBirthPlace(vm.moviePerson.birthPlace);
            $scope.validationFunctions.validateBiography(vm.moviePerson.biography);
            $scope.validationFunctions.validateImageUrl(vm.moviePerson.imageUrl);
            $scope.validationFunctions.validateImdbUrl(vm.moviePerson.imdbUrl);
            $scope.validationFunctions.validatePopularity(vm.moviePerson.popularity);

            //If any control has error
            if (Object.values($scope.errors).some(x => x === true)) {
                return;
            }

            //if (Object.values($scope.validationFunctions).any)

            //Ako stvaramo novog redatelja
            if (!vm.title) {
                //Poziva se servis za stvaranje novog autora
                moviePersonsSvc.createMoviePerson(vm.moviePerson).then(function (result) {
                    // Proxy created on the fly
                    var signalRConn = $.connection;
                    signalRConn.hub.url = `${serviceBase}/signalr`;
                    //Dohvaćamo Hub (MyHub klasa u Api projektu)
                    var hub = signalRConn.myHub;

                    signalRConn.hub.start().done(function () {
                        hub.server.refresh();
                    });
                    signalRConn.hub.stop();

                    //Te se otvara pogled sa pregledom redatelja, ako je uspješno stvoren
                    $state.go("moviePersonsOverview");
                },
                    //Ili se prikazuje poruka pogreške
                    function (err) {
                        swal.fire({
                            title: "Greška",
                            text: "Došlo je do greške: " + err.data.message,
                            type: "error",
                            buttonsStyling: false,
                            customClass: {
                                confirmButton: 'btn btn-primary btn-outline'
                            }

                        });
                    }
                );
            }
            //Ako se uređuje postojeći redatelj
            else {
                //Poziva se servis za uređivanje postojećeg autora

                moviePersonsSvc.updateMoviePerson(vm.moviePerson).then(function (result) {
                    // Proxy created on the fly
                    var signalRConn = $.connection;
                    signalRConn.hub.url = `${serviceBase}/signalr`;
                    //Dohvaćamo Hub (MyHub klasa u Api projektu)
                    var hub = signalRConn.myHub;

                    signalRConn.hub.start().done(function () {
                        hub.server.refresh();
                    });
                    signalRConn.hub.stop();

                    //Te se otvara pogled sa pregledom redatelja, ako je uspješno uređen
                    $state.go("moviePersonsOverview");
                },
                    //Ili se prikazuje poruka pogreške
                    function (err) {
                        swal.fire({
                            title: "Greška",
                            text: "Došlo je do greške: " + err.data.message,
                            type: "error",
                            buttonsStyling: false,
                            customClass: {
                                confirmButton: 'btn btn-primary btn-outline'
                            }
                        });
                    }
                );
            }
        }
    }
})();
