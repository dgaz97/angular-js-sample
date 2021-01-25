(function () {

    'use strict';

    angular
        .module('dashboardServices', [])
        .service('dashboardSvc', dashboardSvc)
        ;

    dashboardSvc.$inject = ['$http'];
    function dashboardSvc($http) {
        this.test = function () {

        }
    };

})();
