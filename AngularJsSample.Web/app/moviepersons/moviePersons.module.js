(function () {

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


    moviePersonProfileCtrl.$inject = ['$scope', '$state', 'moviePerson', 'moviePersonsSvc', '$stateParams']
    function moviePersonProfileCtrl($scope, $state, moviePerson, moviePersonsSvc, $stateParams) {
        var vm = this;

        vm.moviePerson = moviePerson;


        //postavke za gumb brisanja
        $scope.deleteButtonOptions = {
            click: function (e) {
                //Modal za upozorenje o brisanju
                swal.fire({
                    title: "POZOR",
                    text: "Jeste li sigurni da želite obrisati osobu `" + vm.moviePerson.firstName + " " + vm.moviePerson.lastName + "`?",
                    showCancelButton: true,
                    confirmButtonText: "Da",
                    cancelButtonText: "Ne",
                    closeOnCancel: true,
                    closeOnConfirm: true,
                    closeOnEsc: true
                },
                    function (isConfirm) {
                        if (isConfirm) {
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
                                    swal.fire("Greška", "Došlo je do greške kod brisanja: " + err.data.messageDetail, "error");
                            });
                        }
                    }
                );
            }
        };

    }

    moviePersonManageCtrl.$inject = ['$scope', 'moviePersonsSvc', 'moviePerson', '$state', '$stateParams']
    function moviePersonManageCtrl($scope, moviePersonsSvc, moviePerson, $state, $stateParams) {
        var vm = this;

        //Određuje uređivamo li postojećeg redatelja, ili stvaramo novog
        vm.moviePerson = moviePerson ? moviePerson : null;

        vm.title = vm.moviePerson ? true : false;

        //Trenutno vrijeme, treba da ograničimo datum rođenja autora
        vm.now = new Date().toISOString();

        //Provjeravamo je li link slike ispravnog formata
        $scope.validateImageUrl = function (text) {
            if ((!text || /^\s*$/.test(text))) return true;
            var validHttp = /^https?:\/\//g.test(text);
            var validImg = /\.jpg$|\.jpeg$|\.png$|\.gif$/g.test(text);
            if (validImg && validHttp) return true;
            else return "URL slike nije ispravnog formata";
        }

        //Provjeravamo je li IMDb link ispravnog formata
        $scope.validateImdbUrl = function (text) {
            var validImdbLink = /^https?:\/\/(www\.)?imdb.com/g.test(text);
            if (validImdbLink) return true;
            else return "IMDb URL nije ispravnog formata";
        }

        $scope.submitForm = function () {
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
                            text: "Došlo je do greške: " + err.data.messageDetail,
                            type: "error"

                        });
                    }
                );
            }
            //Ako se uređuje postojeći redatelj
            else {
                //Poziva se servis za uređivanje postojećeg autora
                moviePersonsSvc.updateMoviePerson(vm.moviePerson.id, vm.moviePerson).then(function (result) {
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
                            text: "Došlo je do greške: " + err.data.messageDetail,
                            type: "error"
                        });
                    }
                );
            }
        }
    }
})();
