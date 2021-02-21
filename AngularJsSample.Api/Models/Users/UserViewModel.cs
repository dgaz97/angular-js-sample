namespace AngularJsSample.Api.Models.Users
{
    /// <summary>
    /// ViewModel for User
    /// </summary>
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}