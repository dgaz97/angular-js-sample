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
        movieAuthorsSvc.getMovieAuthors().then(function (result) {
            $("#movieAuthorGrid").kendoGrid({
                dataSource: {
                    data: result.data.authors,
                    pageSize: 20
                },
                pageable: {
                    refresh: true,
                    pageSizes: true,
                    buttonCount: 5
                },
                //groupable: true,
                //sortable: true,
                filterable: true,
                resizable: true,
                dataBound: function () {
                    for (var i = 0; i < this.columns.length; i++) {
                        if (i == 5) continue;
                        this.autoFitColumn(i);
                    }
                },
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
            });
        });

        $("#dropdownlist").kendoDropDownList({
            dataSource: [
                {
                    "text": "Datum kreiranja uzlazno",
                    "sortParams": {
                        "value": "dateCreated",
                        "dir": "asc"
                    }
                },
                {
                    "text": "Datum kreiranja silazno",
                    "sortParams": {
                        "value": "dateCreated",
                        "dir": "desc"
                    }
                },
                {
                    "text": "Popularnost uzlazno",
                    "sortParams": {
                        "value": "popularity",
                        "dir": "asc"
                    }
                },
                {
                    "text": "Popularnost silazno",
                    "sortParams": {
                        "value": "popularity",
                        "dir": "desc"
                    }
                }
            ],
            dataTextField: "text",
            dataValueField: "sortParams",
            optionLabel: "Sortiraj prema...",
            change: function (e) {
                $("#movieAuthorGrid").data("kendoGrid").dataSource.sort({ field: this.value().value, dir: this.value().dir });
            }

        });

    };


    movieAuthorProfileCtrl.$inject = ['$scope', '$state', 'movieAuthor', 'movieAuthorsSvc', '$stateParams']
    function movieAuthorProfileCtrl($scope, $state, movieAuthor, movieAuthorsSvc, $stateParams) {
        var vm = this;
        vm.movieAuthor = movieAuthor;
        $("#delete-dialog").kendoDialog({
            width: "450px",
            title: "POZOR",
            closable: false,
            visible: false,
            modal: true,
            content: "<p>Jeste li sigurni da želite obrisati redatelja " + $stateParams.id+"?<p/>",
            actions: [
                {
                    text: "Da",
                    action: function (e) {
                        movieAuthorsSvc.deleteMovieAuthor($stateParams.id).then(function (data) {
                            $("#delete-dialog").data("kendoDialog").destroy();
                            $state.go("movieAuthorsOverview");
                        });
                    }
                },
                {
                    text: "Ne",
                    primary: true
                }
            ]
        });

        $("#deleteButton").kendoButton({
            click: function (e) {
                $("#delete-dialog").data("kendoDialog").open();
            }
        });
        $("#deleteButton").removeClass("k-button");

    }

    movieAuthorManageCtrl.$inject = ['$scope', 'movieAuthorsSvc', 'movieAuthor', '$state', '$stateParams']
    function movieAuthorManageCtrl($scope, movieAuthorsSvc, movieAuthor, $state, $stateParams) {
        var vm = this;
        vm.movieAuthor = movieAuthor ? movieAuthor : null;

        $("#movieAuthorForm").kendoForm({
            orientation: "vertical",
            layout: "grid",
            grid: {
                cols: 2,
                gutter: 20
            },
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
            buttonsTemplate: "",//Maknuti default gumbe za submit i clear
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
                        paste: function (ev) {
                            ev.html = $(ev.html).text();
                        }

                    },
                    validation: {
                        //required: true,
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
            submit: function (e) {
                e.preventDefault();
                if (vm.movieAuthor == null) {
                    movieAuthorsSvc.createMovieAuthor(e.model).then(function (result) {
                        $state.go("movieAuthorsOverview");
                    },
                        function (err) {
                            $("#errorNotification").kendoNotification({
                                appendTo:"#errorNotification"
                            }).data("kendoNotification").show(err.data.message, "error");
                        }
                    );
                }
                else {
                    movieAuthorsSvc.updateMovieAuthor(vm.movieAuthor.id, e.model).then(function (result) {
                        $state.go("movieAuthorsOverview");
                    },
                        function (err) {
                            $("#errorNotification").kendoNotification({
                                appendTo: "#errorNotification"
                            }).data("kendoNotification").show(err.data.message, "error");
                        }
                    );
                }

            }
        });


        $("#submit-button").kendoButton({
            click: function (e) {
                $("#movieAuthorForm").submit();
            }
        });
        $("#submit-button").removeClass("k-button");

    }

})();
