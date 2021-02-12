using System;

namespace AngularJsSample.Services.Messaging
{
    public class ResponseBase<T> where T : RequestBase
    {
        public T Request { get; set; }
        public Guid ResponseToken { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
