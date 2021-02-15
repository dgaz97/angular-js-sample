(function () {
    'use strict';

    angular.module('genres', ['genresServices', 'angularValidator'])
        .controller('genresOverviewCtrl', genresOverviewCtrl)
        ;

    genresOverviewCtrl.$inject = ['$scope', 'genresSvc'];
    function genresOverviewCtrl($scope, genresSvc) {
        var vm = this;
        genresSvc.getGenres().then(function (result) {
            $scope.genresData = new kendo.data.DataSource({
                data: result.data.genres,
                pageSize: 5
            });
        });

        $scope.manageGenre = function (genre) {
            //Ako žanr nije proslijeđen, stvara se novi objekt
            if (genre == undefined) {
                genre = {};
                genre.name = "";
                genre.description = "";
            }

            //SweetAlert definicija
            swal.fire({
                title: genre.genreId == undefined ? "Unesi novi žanr" : "Uredi žanr",
                width: "60vw",
                //Izgled modala
                html:
                    `<div class="container">
                        <div class="form-group row">
                            <label for="name">Naziv</label>
                            <input id="name" class="form-control" value="`+ genre.name + `"/>
                            <label for="description">Opis</label>
                            <textarea id="description" class="form-control"
                            style="max-width:100%; min-width:100%;min-height:10vh; max-height:30vh">`+ genre.description + `</textarea>
                        </div>
                    </div>`,
                confirmButtonText: "Spremi",
                showCancelButton: true,
                cancelButtonText: "Prekini",
                allowOutsideClick: "true",
                //Vratiti će vrijednosti input elemenata
                preConfirm: () => {
                    return {
                        name: document.getElementById('name').value,
                        description: document.getElementById('description').value
                    }
                }
                //Dok se klikne neki gumb
            }).then(function (result) {
                //Ako je kliknuto Spremi
                if (result.isConfirmed) {
                    if (result.value.name == "") {
                        swal.fire("Žanr nije spremljen", "Ime ne smije biti prazno", "error");
                        return;
                    }
                    if (result.value.name.length > 50) {
                        swal.fire("Žanr nije spremljen", "Ime ne smije biti dulje od 50 znakova", "error");
                        return;
                    }
                    if (result.value.description.length > 1000) {
                        swal.fire("Žanr nije spremljen", "Opis ne smije biti dulji od 1000 znakova", "error");
                        return;
                    }
                    genre.name = result.value.name;
                    genre.description = result.value.description;

                    //Ako stvaramo novi žanr
                    if (genre.genreId == undefined) {
                        genresSvc.createGenre(genre).then(function (result) {
                            genresSvc.getGenres().then(function (result2) {
                                $scope.genresData = new kendo.data.DataSource({
                                    data: result2.data.genres,
                                    pageSize: 5
                                });
                            });
                        }, function (err) {
                            swal.fire("Greška", "Došlo je do greške kod stvaranja: " + err.data.messageDetail, "error");
                        });
                    }
                    //Ako uređujemo žanr
                    else {
                        genresSvc.updateGenre(genre.genreId, genre).then(function (result) {
                            genresSvc.getGenres().then(function (result2) {
                                $scope.genresData = new kendo.data.DataSource({
                                    data: result2.data.genres,
                                    pageSize: 5
                                });
                            });
                        }, function (err) {
                            swal.fire("Greška", "Došlo je do greške kod uređivanja: " + err.data.messageDetail, "error");
                        });
                    }
                }
            });
        };

        $scope.deleteGenre = function (genre) {
            console.log(genre);
            swal.fire({
                title: "POZOR",
                text: "Jeste li sigurni da želite obrisati žanr " + genre.name + "?",
                confirmButtonText: "Da, obriši",
                showCancelButton: true,
                cancelButtonText: "Prekini",
                allowOutsideClick: "true"
            }).then(function (result) {
                //Ako je kliknuto Da, obriši
                if (result.isConfirmed) {
                    genresSvc.deleteGenre(genre.genreId).then(function (result) {
                        genresSvc.getGenres().then(function (result2) {
                            $scope.genresData = new kendo.data.DataSource({
                                data: result2.data.genres,
                                pageSize: 5
                            });
                        });
                    }, function (err) {
                        swal.fire("Greška", "Došlo je do greške kod brisanja: " + err.data.messageDetail, "error");
                    })
                }
            });
        }

        $scope.genresGrid = {
            //groupable: true,
            //sortable: true,
            dataBound: function () {
                for (var i = 0; i < this.columns.length; i++) {
                    if (i == 1) continue;
                    this.autoFitColumn(i);
                }
            },
            //Definiranje stupaca
            columns: [
                {
                    field: "name",
                    title: "Naziv"
                },
                {
                    field: "description",
                    title: "Opis",
                },
                {
                    field: "dateCreated",
                    title: "Datum kreiranja",
                    template: '#= kendo.toString(kendo.parseDate(data.dateCreated), "dd.MM.yyyy.") #'
                },
                {
                    field: "userCreated.fullName",
                    title: "Kreirao"
                },
                {
                    title: "Naredbe",
                    //definiramo gumb
                    command: [{
                        className: "edit",
                        name: "uredi",
                        text: "Uredi",
                        click: function (e) {
                            //Uzmemo red
                            var tr = $(e.target).closest("tr");
                            //Dohvatimo podatke iz reda
                            var data = this.dataItem(tr);
                            //Otvaramo modal
                            $scope.manageGenre(data);
                        }
                    },
                    {
                        className: "destroy",
                        name: "obrisi",
                        text: "Obriši",
                        click: function (e) {
                            //Uzmemo red
                            var tr = $(e.target).closest("tr");
                            //Dohvatimo podatke iz reda
                            var data = this.dataItem(tr);
                            //Otvaramo modal
                            $scope.deleteGenre(data);
                        }
                    }]
                }
            ]
        };

    };
})();