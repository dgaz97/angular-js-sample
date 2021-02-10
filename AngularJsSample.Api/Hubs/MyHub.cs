using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJsSample.Api.Hubs
{
    public class MyHub : Hub
    {
        /*public void Hello()
        {
            Clients.All.hello();
        }*/

        /*public void Send(string name, string message)
        {
            Clients.All.sendMessage(name, message);
        }*/
        public void Refresh()
        {
            Clients.All.updateMovieAuthors();
        }
        /*public void Subscribe(string customerId)
        {
            Groups.Add(Context.ConnectionId, customerId);
        }*/

        /*public void Unsubscribe(string customerId)
        {
            Groups.Remove(Context.ConnectionId, customerId);
        }*/
    }
}