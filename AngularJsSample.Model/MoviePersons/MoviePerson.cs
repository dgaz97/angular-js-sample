﻿using AngularJsSample.Model.Users;
using System;

namespace AngularJsSample.Model.MoviePersons
{
    /// <summary>
    /// Model class for Movie Person
    /// </summary>
    public class MoviePerson
    {
        public int Id { get; set; }
        public UserInfo UserCreated { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public UserInfo UserLastModified { get; set; }
        public DateTimeOffset? DateLastModified { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Biography { get; set; }
        public string ImdbUrl { get; set; }
        public string ImageUrl { get; set; }
        public int Popularity { get; set; }
    }
}
