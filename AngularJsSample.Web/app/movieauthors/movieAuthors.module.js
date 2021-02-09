(function () {

    'use strict';

    angular
        .module('movieAuthors', ['movieAuthorsServices'])
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


    movieAuthorProfileCtrl.$inject = ['$scope', '$state', 'movieAuthor', 'movieAuthorsSvc', '$stateParams']
    function movieAuthorProfileCtrl($scope, $state, movieAuthor, movieAuthorsSvc, $stateParams) {
        var vm = this;

        vm.movieAuthor = movieAuthor;

        //postavke za modal brisanja
        $scope.deleteDialogOptions={
            actions: [
                {
                    text: "Da",
                    action: function (e) {
                        //Poziva servis za brisanje redatelja
                        movieAuthorsSvc.deleteMovieAuthor($stateParams.id).then(function (data) {
                            //Gasi modal
                            angular.element("#delete-dialog").data("kendoDialog").destroy();
                            //Premješta na pregled svih redatelja
                            $state.go("movieAuthorsOverview");
                        });
                    }
                },
                {
                    text: "Ne",
                    primary: true
                }
            ]
        };

        //postavke za gumb brisanja
        $scope.deleteButtonOptions={
            click: function (e) {
                //Otvara modal
                angular.element("#delete-dialog").data("kendoDialog").open();
            }
        };

    }

    movieAuthorManageCtrl.$inject = ['$scope', 'movieAuthorsSvc', 'movieAuthor', '$state', '$stateParams']
    function movieAuthorManageCtrl($scope, movieAuthorsSvc, movieAuthor, $state, $stateParams) {
        var vm = this;
        //Određuje uređivamo li postojećeg redatelja, ili stvaramo novog
        vm.movieAuthor = movieAuthor ? movieAuthor : null;

        $scope.notificationOptions = {
            appendTo: "#appendto"
        };

        //Nije preko direktive
        angular.element("#movieAuthorForm").kendoForm({
            orientation: "vertical",
            layout: "grid",
            grid: {
                cols: 2,
                gutter: 20
            },
            //Ternarni operator - puni formu podacima, ako uređujemo redatelja
            formData: vm.movieAuthor ? {
                firstName: vm.movieAuthor.firstName,
                lastName: vm.movieAuthor.lastName,
                birthDate: vm.movieAuthor.birthDate,
                birthPlace: vm.movieAuthor.birthPlace,
                biography: vm.movieAuthor.biography,
                imageUrl: vm.movieAuthor.imageUrl,
                imdbUrl: vm.movieAuthor.imdbUrl,
                popularity: vm.movieAuthor.popularity
            } : {},
            validatable: {
                validationSummary: true
            },
            //Maknuti default gumbe za submit i clear
            buttonsTemplate: "",
            items: [
                {
                    field: "firstName",
                    label: "Ime",
                    validation: {
                        required: true
                    },
                    colSpan: 1
                },
                {
                    field: "lastName",
                    label: "Prezime",
                    validation: {
                        required: true
                    },
                    colSpan: 1
                },
                {
                    field: "birthDate",
                    label: "Datum rođenja",
                    editor: "DatePicker",
                    editorOptions: {
                        max: new Date(),
                        min: new Date(1800, 0, 1)//1.1.1800
                    },
                    validation: {
                        required: true
                    },
                    colSpan: 1,
                    format: "yyyy-MM-dd"
                },
                {
                    field: "birthPlace",
                    label: "Mjesto rođenja",
                    validation: {
                        required: true
                    },
                    colSpan: 1
                },
                {
                    field: "biography",
                    label: "Opis",
                    editor: "Editor",
                    hint: "Max 2000 characters, may be empty",
                    editorOptions: {
                        tools: [],
                        resizable: {
                            min: 50,
                            max: 300
                        },
                        //Ako se lijepi tekst s linkovima (npr. sa wikipedije), lijepi se samo plaintext
                        paste: function (ev) {
                            ev.html = $(ev.html).text();
                        }

                    },
                    validation: {
                        //required: true,
                        //Provjerava je li opis preko 2000 znakova
                        validSize: function (input) {
                            if (input.is("[name='biography']")) {
                                input.attr("data-validSize-msg", "String is too long, 2000 characters max");
                                if (input.val().length > 2000) return false;
                                else return true;
                            }
                            return true;
                        }
                    },
                    colSpan: 2
                },
                {
                    field: "imageUrl",
                    label: "Foto URL",
                    validation: {
                        required: false,
                        validImageLink: function (input) {
                            if (input.is("[name='imageUrl']")) {
                                input.attr("data-validImageLink-msg", "Image link is invalid");
                                if (!input.val() || input.val().length === 0) return true;
                                var validHttp = /^https?:\/\//g.test(input.val());
                                var validImg = /\.jpg$|\.jpeg$|\.png$|\.gif$/g.test(input.val());
                                return validHttp && validImg;
                            }
                            return true;
                        }
                    },
                    hint: "May be empty, if not, must be .jpg, .jpeg, .png or .gif, and must start with http(s)",
                    colSpan: 2
                },
                {
                    field: "imdbUrl",
                    label: "IMDB URL",
                    validation: {
                        required: true,
                        //Provjerava je li link IMDb link
                        validImdbLink: function (input) {
                            if (input.is("[name='imdbUrl']")) {
                                input.attr("data-validImdbLink-msg", "IMDB link is invalid");
                                //input.attr("imdbUrl-form-error", "IMDb url can't be empty");
                                var validImdbLink = /^https?:\/\/(www\.)?imdb.com/g.test(input.val());
                                return validImdbLink;
                            }
                            return true;
                        }
                    },
                    hint: "Must start with http(s)",
                    colSpan: 2
                }
                ,
                {
                    field: "popularity",
                    label: "Popularnost:",
                    editor: "NumericTextBox",
                    editorOptions: {
                        decimals: 0,
                        min: 1,
                        format: "n0"
                    },
                    validation: {
                        required: true
                    },
                    colSpan: 1
                }
            ],
            //Submit funkcija, ovdje se poziva servis za stvaranje
            submit: function (e) {
                e.preventDefault();
                //Ako se stvara novi redatelj
                if (vm.movieAuthor == null) {
                    //Poziva se servis za stvaranje novog autora
                    movieAuthorsSvc.createMovieAuthor(e.model).then(function (result) {
                        //Te se otvara pogled sa pregledom redatelja, ako je uspješno stvoren
                        $state.go("movieAuthorsOverview");
                    },
                        //Ili se prikazuje poruka pogreške
                        function (err) {
                            angular.element("#errorNotification").data("kendoNotification").show(err.data.message, "error");
                        }
                    );
                }
                //Ako se uređuje postojeći redatelj
                else {
                    //Poziva se servis za uređivanje postojećeg autora
                    movieAuthorsSvc.updateMovieAuthor(vm.movieAuthor.id, e.model).then(function (result) {
                        //Te se otvara pogled sa pregledom redatelja, ako je uspješno uređen
                        $state.go("movieAuthorsOverview");
                    },
                        //Ili se prikazuje poruka pogreške
                        function (err) {
                            angular.element("#errorNotification").data("kendoNotification").show(err.data.message, "error");
                        }
                    );
                }

            }
        });


        $scope.submitButtonOptions={
            click: function (e) {
                angular.element("#movieAuthorForm").submit();
            }
        };

    }

})();
