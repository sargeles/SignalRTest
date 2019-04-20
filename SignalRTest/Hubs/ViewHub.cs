using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRTest.Hubs
{
    [HubName("ViewHub")]
    public class ViewHub : Hub
    {
        //this is just called by client and return the value for it .
        public string Hello()
        {
            return "hello";
        }



        //this fucntion will be called by client and the inside function 
        //Clients.Others.talk(message);
        //will be called by clinet javascript function .
        public void SendMessag(string message)
        {
            Clients.All.talk(message);
        }
        public class SignalrServerToClient
        {
            static readonly IHubContext _myHubContext = GlobalHost.ConnectionManager.GetHubContext<ViewHub>();
            public static void BroadcastMessage(string message)
            {
                _myHubContext.Clients.All.talk(message);
            }
        }

    }
}