angular.module('login.controllers', [])
    .controller('LoginCtrl', [
        '$scope','authService','$window', function ($scope,authService,$window) {

            $scope.loginData = {};

            $scope.disableFormMainButtons = false;

            $scope.login = function ($event) {
                $scope.disableFormMainButtons = true;
                $event.preventDefault();

                $scope.result = "";

                authService.login($scope.loginData).then(function (response) {
                    $window.location.href = "/";
                },
                    function (err) {
                        $scope.result = 'Pogrešno korisničko ime ili lozinka.';
                        $scope.disableFormMainButtons = false;
                    }
                );
            };
        }
    ]);