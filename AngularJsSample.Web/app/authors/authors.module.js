(function () {

    'use strict';

    angular
        .module('authors', ['authorsServices'])
        .controller('authorsOverviewCtrl', authorsOverviewCtrl)
        .controller('authorProfileCtrl', authorProfileCtrl)
        .controller('authorManageCtrl', authorManageCtrl)
        ;

    authorsOverviewCtrl.$inject = ['$scope'];
    function authorsOverviewCtrl($scope) {
        var vm = this;


    };

    authorProfileCtrl.$inject=['$scope']
    function authorProfileCtrl($scope) {
        var vm = this;
    }

    authorManageCtrl.$inject = ['$scope', 'authorsSvc', 'author']
    function authorManageCtrl($scope, authorsSvc, author) {
        var vm = this;

        vm.author = author ? author.data : null;

        if (vm.author) {
            //update
        }
        else {
            //create
        }

    }

})();
