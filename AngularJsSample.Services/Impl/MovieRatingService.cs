using AngularJsSample.Model.MovieRatings;
using AngularJsSample.Model.Movies;
using AngularJsSample.Model.Users;
using AngularJsSample.Services.Mapping;
using AngularJsSample.Services.Messaging.MovieRatings.Requests;
using AngularJsSample.Services.Messaging.MovieRatings.Responses;
using AngularJsSample.Services.Validation;
using System;

namespace AngularJsSample.Services.Impl
{
    public class MovieRatingService : IMovieRatingService
    {
        private IMovieRatingRepository _repository;
        private IUserRepository _repository2;
        private IMovieRepository _repository3;
        public MovieRatingService(IMovieRatingRepository repository, IUserRepository repository2, IMovieRepository repository3)
        {
            _repository = repository;
            _repository2 = repository2;
            _repository3 = repository3;
        }
        public GetAllMovieRatingsResponse GetAllMovieRatings(GetAllMovieRatingsRequest request)
        {
            var response = new GetAllMovieRatingsResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                response.MovieRatings = _repository.FindAll().MapToViews();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public GetMovieRatingByMovieAndUserResponse GetMovieRatingByMovieAndUser(GetMovieRatingByMovieAndUserRequest request)
        {
            var response = new GetMovieRatingByMovieAndUserResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                request.CheckIfUserExists(_repository2);
                request.CheckIfMovieExists(_repository3);

                response.MovieRating = _repository.FindByUserAndMovie(request.MovieId, request.RequestedUser).MapToView();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public GetMovieRatingsByMovieResponse GetMovieRatingsByMovie(GetMovieRatingsByMovieRequest request)
        {
            var response = new GetMovieRatingsByMovieResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                request.CheckIfMovieExists(_repository3);

                response.MovieRatings = _repository.FindByMovie(request.MovieId).MapToViews();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public GetMovieRatingsByUserResponse GetMovieRatingsByUser(GetMovieRatingsByUserRequest request)
        {
            var response = new GetMovieRatingsByUserResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                request.CheckIfUserExists(_repository2);
                response.MovieRatings = _repository.FindByUser(request.RequestedUser).MapToViews();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public SaveMovieRatingResponse SaveMovieRating(SaveMovieRatingRequest request)
        {
            var response = new SaveMovieRatingResponse()
            {
                Request = request,
                ResponseToken = Guid.NewGuid()
            };
            try
            {
                request.CheckIfMovieExists(_repository3);

                request.CheckMovieRatingForInsert();

                _repository.Add(request.MovieRating.MapToModel());
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }
    }
}
