(function () {

    'use strict';

    angular
        .module('dashboard', ['dashboardServices'])
        .controller('dashboardCtrl', dashboardCtrl)
        ;

    dashboardCtrl.$inject = ['$scope'];
    function dashboardCtrl($scope) {
        var vm = this;

    };

})();
