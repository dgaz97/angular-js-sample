(function () {
    'use strict';

    angular.module('movieroles', ['movieRolesServices'])
        .controller('movieRolesOverviewCtrl', movieRolesOverviewCtrl);

    movieRolesOverviewCtrl.$inject = ['$scope', 'movieRolesSvc'];

    function movieRolesOverviewCtrl($scope, movieRolesSvc) {
        var vm = this;
        movieRolesSvc.getMovieRoles().then(function (result) {
            $scope.movieRolesData = new kendo.data.DataSource({
                data: result.data.movieRoles,
                pageSize: 5
            });
        });

        $scope.manageMovieRole = function (role) {
            //Ako uloga nije proslijeđena, stvara se novi objekt
            if (role == undefined) {
                role = {};
                role.movieRoleName = "";
                role.movieRoleDescription = "";
            }

            //SweetAlert definicija
            swal.fire({
                title: role.movieRoleId == undefined ? "Unesi novu film ulogu" : "Uredi film ulogu",
                width: "60vw",
                //Izgled modala
                html:
                    `<div class="container-fluid">
                        <div class="form-group row">
                            <label for="name" class="control-label">Naziv</label>
                            <input id="name" class="form-control" value="`+ role.movieRoleName + `"/>
                            <label for="description" class="control-label">Kratki opis</label>
                            <textarea id="description" class="form-control"
                            style="max-width:100%; min-width:100%;min-height:10vh; max-height:30vh">`+ role.movieRoleDescription + `</textarea>
                        </div>
                    </div>`,
                confirmButtonText: "Spremi",
                showCancelButton: true,
                cancelButtonText: "Prekini",
                allowOutsideClick: "true",
                //BS tema na gumbe u modalu
                buttonsStyling: false,
                customClass: {
                    confirmButton: 'btn btn-primary btn-outline',
                    cancelButton: 'btn btn-info btn-outline'
                },
                //Vratiti će vrijednosti input elemenata
                preConfirm: () => {
                    return {
                        movieRoleName: document.getElementById('name').value,
                        movieRoleDescription: document.getElementById('description').value
                    }
                }
                //Dok se klikne neki gumb
            }).then(function (result) {
                //Ako je kliknuto Spremi
                if (result.isConfirmed) {
                    if (result.value.movieRoleName == "") {
                        swal.fire("Film uloga nije spremljena", "Ime ne smije biti prazno", "error");
                        return;
                    }
                    if (result.value.movieRoleName.length > 50) {
                        swal.fire("Film uloga nije spremljena", "Ime ne smije biti dulje od 50 znakova", "error");
                        return;
                    }
                    if (result.value.movieRoleDescription.length > 1000) {
                        swal.fire("Film uloga nije spremljena", "Opis ne smije biti dulji od 1000 znakova", "error");
                        return;
                    }
                    role.movieRoleName = result.value.movieRoleName;
                    role.movieRoleDescription = result.value.movieRoleDescription;

                    //Ako stvaramo novu film ulogu
                    if (role.movieRoleId == undefined) {
                        movieRolesSvc.createMovieRole(role).then(function (result) {
                            movieRolesSvc.getMovieRoles().then(function (result2) {
                                $scope.movieRolesData = new kendo.data.DataSource({
                                    data: result2.data.movieRoles,
                                    pageSize: 5
                                });
                            });
                        }, function (err) {
                            swal.fire("Greška", "Došlo je do greške kod stvaranja: " + err.data.messageDetail, "error");
                        });
                    }
                    //Ako uređujemo film ulogu
                    else {
                        movieRolesSvc.updateMovieRole(role).then(function (result) {
                            movieRolesSvc.getMovieRoles().then(function (result2) {
                                $scope.movieRolesData = new kendo.data.DataSource({
                                    data: result2.data.movieRoles,
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

        $scope.deleteMovieRole = function (role) {
            swal.fire({
                title: "POZOR",
                icon: "warning",
                text: "Jeste li sigurni da želite obrisati ulogu " + role.movieRoleName + "?",
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
                    movieRolesSvc.deleteMovieRole(role.movieRoleId).then(function (result) {
                        movieRolesSvc.getMovieRoles().then(function (result2) {
                            $scope.movieRolesData = new kendo.data.DataSource({
                                data: result2.data.movieRoles,
                                pageSize: 5
                            });
                        });
                    }, function (err) {
                        swal.fire("Greška", "Došlo je do greške kod brisanja: " + err.data.messageDetail, "error");
                    })
                }
            });
        }

        $scope.movieRolesGrid = {
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
                    field: "movieRoleName",
                    title: "Naziv"
                },
                {
                    field: "movieRoleDescription",
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
                        className: "btn btn-info btn-outline",
                        name: "uredi",
                        text: "Uredi",
                        click: function (e) {
                            //Uzmemo red
                            var tr = $(e.target).closest("tr");
                            //Dohvatimo podatke iz reda
                            var data = this.dataItem(tr);
                            //Otvaramo modal
                            $scope.manageMovieRole(data);
                        }
                    },
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
                            $scope.deleteMovieRole(data);
                        }
                    }]
                }
            ]
        };
    }
}) ();