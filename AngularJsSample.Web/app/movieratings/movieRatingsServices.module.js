(function () {
    'use strict';

    angular
        .module("movieRatingsServices", [])
        .service("movieRatingsSvc", movieRatingsSvc);

    movieRatingsSvc.$inject = ["$http"];
    function movieRatingsSvc($http) {
        this.getMovieRatingByUserAndMovie=function(idUser, idMovie){
            return $http.get(`${serviceBase}/api/movieRatings/${idUser}/${idMovie}`);
        }
        this.addMovieRating = function (movieRating) {
            return $http.post(`${serviceBase}api/movieRatings`, movieRating);
        }
    }
}) ();