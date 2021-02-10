(function () {

    'use strict';

    angular
        .module('movieAuthors', ['movieAuthorsServices', 'angularValidator'])
        .controller('movieAuthorsOverviewCtrl', movieAuthorsOverviewCtrl)
        .controller('movieAuthorProfileCtrl', movieAuthorProfileCtrl)
        .controller('movieAuthorManageCtrl', movieAuthorManageCtrl)
        ;

    movieAuthorsOverviewCtrl.$inject = ['$scope', 'movieAuthorsSvc'];
    function movieAuthorsOverviewCtrl($scope, movieAuthorsSvc) {
        var vm = this;
        //k-ng-model za sortiranje
        $scope.sortParams = {};

        //Dohvaćamo podatke preko servisa
        movieAuthorsSvc.getMovieAuthors().then(function (result) {
            //Spremamo u DataSource
            $scope.movieAuthorGridData = new kendo.data.DataSource({
                data: result.data.authors,
                pageSize: 20
            });

            //postavke za grid
            $scope.movieAuthorGrid = {
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
                        template: '<a ui-sref="movieAuthorProfile({ id:#: data.id#})" href=" /movieauthors/#: data.id#">#: data.id#</a>'
                    },
                    {
                        template: '<a ui-sref="movieAuthorProfile({id:#: data.id#})" href="/movieauthors/#: data.id#"><div align="center"><img src="#: data.imageUrl#" style="max-width: 100%" /></div></a>',
                        title: "Foto"
                    },
                    {
                        field: "firstName",
                        title: "Ime",
                        template: '<a ui-sref="movieAuthorProfile({ id:#: data.id#})" href=" /movieauthors/#: data.id#">#: data.firstName#</a>'
                    },
                    {
                        field: "lastName",
                        title: "Prezime",
                        template: '<a ui-sref="movieAuthorProfile({ id:#: data.id#})" href=" /movieauthors/#: data.id#">#: data.lastName#</a>'
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
            $scope.movieAuthorGridData.sort({ field: $scope.sortParams.col, dir: $scope.sortParams.dir });
        }

    };


    movieAuthorProfileCtrl.$inject = ['$scope', '$state', 'SweetAlert', 'movieAuthor', 'movieAuthorsSvc', '$stateParams']
    function movieAuthorProfileCtrl($scope, $state, SweetAlert, movieAuthor, movieAuthorsSvc, $stateParams) {
        var vm = this;

        vm.movieAuthor = movieAuthor;


        //postavke za gumb brisanja
        $scope.deleteButtonOptions = {
            click: function (e) {
                //Modal za upozorenje o brisanju
                SweetAlert.swal({
                    title: "POZOR",
                    text: "Jeste li sigurni da želite obrisati redatelja " + vm.movieAuthor.id + "?",
                    showCancelButton: true,
                    confirmButtonText: "Da",
                    cancelButtonText: "Ne",
                    closeOnCancel: true,
                    closeOnConfirm: true,
                    closeOnEsc: true
                },
                    function (isConfirm) {
                        if (isConfirm) {
                            movieAuthorsSvc.deleteMovieAuthor($stateParams.id).then(function (data) {
                                //Premješta na pregled svih redatelja
                                $state.go("movieAuthorsOverview");
                                //Ili prikazuje modal ako dođe do greške
                            }, function (err) {
                                SweetAlert.swal("Greška", "Došlo je do greške kod brisanja: " + err.data.message, "error");
                            });
                        }
                    }
                );
            }
        };

    }

    movieAuthorManageCtrl.$inject = ['$scope', 'SweetAlert', 'movieAuthorsSvc', 'movieAuthor', '$state', '$stateParams']
    function movieAuthorManageCtrl($scope, SweetAlert, movieAuthorsSvc, movieAuthor, $state, $stateParams) {
        var vm = this;

        //Određuje uređivamo li postojećeg redatelja, ili stvaramo novog
        vm.movieAuthor = movieAuthor ? movieAuthor : null;

        $scope.setTitle = function () {
            if (vm.movieAuthor) return true;
            else return false;
        }

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
            if (vm.movieAuthor == null) {
                //Poziva se servis za stvaranje novog autora
                movieAuthorsSvc.createMovieAuthor(vm.movieAuthor).then(function (result) {
                    //Te se otvara pogled sa pregledom redatelja, ako je uspješno stvoren
                    $state.go("movieAuthorsOverview");
                },
                    //Ili se prikazuje poruka pogreške
                    function (err) {
                        SweetAlert.swal({
                            title: "Greška",
                            text: "Došlo je do greške: " + err.data.message,
                            type: "error"

                        });
                    }
                );
            }
            //Ako se uređuje postojeći redatelj
            else {
                //Poziva se servis za uređivanje postojećeg autora
                movieAuthorsSvc.updateMovieAuthor(vm.movieAuthor.id, vm.movieAuthor).then(function (result) {
                    //Te se otvara pogled sa pregledom redatelja, ako je uspješno uređen
                    $state.go("movieAuthorsOverview");
                },
                    //Ili se prikazuje poruka pogreške
                    function (err) {
                        SweetAlert.swal({
                            title: "Greška",
                            text: "Došlo je do greške: " + err.data.message,
                            type: "error"
                        });
                    }
                );
            }
        }
    }
})();
