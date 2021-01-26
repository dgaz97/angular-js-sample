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
            console.log(result.data.authors);
            //vm.movieAuthors = result.data.authors;
            //vm.data = new kendo.data.DataSource({ data: result.data.authors });

            $("#movieAuthorGrid").kendoGrid({
                dataSource: {
                    data: result.data.authors,
                    pageSize: 20
                },
                //height:500,
                pageable: {
                    refresh: true,
                    pageSizes: true,
                    buttonCount: 5
                },
                groupable: true,
                sortable: true,
                filterable: true,
                resizable:true,
                columns: [
                    {
                        field: "id",
                        title: "Id",
                        width: "5%"
                    },
                    {
                        template: '<div align="center"><img src="#: data.imageUrl#" style="max-height: 100px" /></div>',
                        //field: "id",
                        title: "Foto",
                        width: "15%"
                    },
                    {
                        field: "firstName",
                        title: "Ime",
                        width: "10%"
                    },
                    {
                        field: "lastName",
                        title: "Prezime",
                        width: "10%"
                    },
                    {
                        field: "birthPlace",
                        title: "Mjesto rođenja",
                        width: "10%"
                    },
                    {
                        field: "biography",
                        title: "Opis",
                    },
                    {
                        field: "popularity",
                        title: "Popularnost",
                        width: "5%"
                    }
                ]
            });

        });

    };

    movieAuthorProfileCtrl.$inject = ['$scope']
    function movieAuthorProfileCtrl($scope) {
        var vm = this;
    }

    movieAuthorManageCtrl.$inject = ['$scope', 'movieAuthorsSvc', 'movieAuthor']
    function movieAuthorManageCtrl($scope, movieAuthorsSvc, movieAuthor) {
        var vm = this;

        vm.movieAuthor = movieAuthor ? movieAuthor.data : null;

        if (vm.movieAuthor) {
            //update
        }
        else {
            //create
        }

    }

})();
