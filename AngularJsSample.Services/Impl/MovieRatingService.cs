using AngularJsSample.Model.MovieRatings;
using AngularJsSample.Model.Movies;
using AngularJsSample.Model.Users;
using AngularJsSample.Services.Mapping;
using AngularJsSample.Services.Messaging.MovieRatings.Requests;
using AngularJsSample.Services.Messaging.MovieRatings.Responses;
using System;

namespace AngularJsSample.Services.Impl
{
    public class MovieRatingService : IMovieRatingService
    {
        private IMovieRatingRepository _repository_;
        private IUserRepository _repository2_;
        private IMovieRepository _repository3_;
        public MovieRatingService(IMovieRatingRepository repository, IUserRepository repository2, IMovieRepository repository3)
        {
            _repository_ = repository;
            _repository2_ = repository2;
            _repository3_ = repository3;
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
                response.MovieRatings = _repository_.FindAll().MapToViews();
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
                if (_repository2_.FindBy(request.RequestedUser) == null)
                    throw new Exception($"User {request.RequestedUser} doesn't exist");
                if (_repository3_.FindBy(request.MovieId) == null)
                    throw new Exception($"Movie {request.MovieId} doesn't exist");
                response.MovieRating = _repository_.FindByUserAndMovie(request.MovieId, request.RequestedUser).MapToView();
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
                if (_repository3_.FindBy(request.MovieId) == null)
                    throw new Exception($"Movie {request.MovieId} doesn't exist");
                response.MovieRatings = _repository_.FindByMovie(request.MovieId).MapToViews();
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
                if (_repository2_.FindBy(request.RequestedUser) == null)
                    throw new Exception($"User {request.RequestedUser} doesn't exist");
                response.MovieRatings = _repository_.FindByUser(request.RequestedUser).MapToViews();
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
                if (_repository3_.FindBy(request.MovieRating.Movie.MovieId) == null)
                    throw new Exception($"Movie {request.MovieRating.Movie.MovieId} doesn't exist");
                if (request.MovieRating.UserRating < 1 || request.MovieRating.UserRating > 5)//TODO ili možda 10
                    throw new Exception("Movie rating must be between 1 and 5");
                _repository_.Add(request.MovieRating.MapToModel());
                response.MovieRating = request.MovieRating;
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
