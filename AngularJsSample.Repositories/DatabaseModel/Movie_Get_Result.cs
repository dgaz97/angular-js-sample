//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AngularJsSample.Repositories.DatabaseModel
{
    using System;
    
    public partial class Movie_Get_Result
    {
        public int MovieId { get; set; }
        public Nullable<int> UserCreated { get; set; }
        public string UserCreatedFirstName { get; set; }
        public string UserCreatedLastName { get; set; }
        public string UserCreatedFullName { get; set; }
        public string UserCreatedEmail { get; set; }
        public Nullable<int> UserLastModified { get; set; }
        public string UserLastModifiedFirstName { get; set; }
        public string UserLastModifiedLastName { get; set; }
        public string UserLastModifiedFullName { get; set; }
        public string UserLastModifiedEmail { get; set; }
        public System.DateTimeOffset MovieDateCreated { get; set; }
        public Nullable<System.DateTimeOffset> MovieDateLastModified { get; set; }
        public string MovieName { get; set; }
        public string MovieDescription { get; set; }
        public System.DateTime MovieReleaseDate { get; set; }
        public decimal MovieRating { get; set; }
        public string MoviePosterUrl { get; set; }
        public string MovieImdbUrl { get; set; }
    }
}
