using System;

namespace AngularJsSample.Services.Messaging
{
    public abstract class RequestBase
    {
        public Guid RequestToken { get; set; }
        public int UserId { get; set; }
    }
}
