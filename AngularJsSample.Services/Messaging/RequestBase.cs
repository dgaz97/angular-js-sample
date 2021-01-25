using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsSample.Services.Messaging
{
    public abstract class RequestBase
    {
        public Guid RequestToken { get; set; }
        public int UserId { get; set; }
    }
}
