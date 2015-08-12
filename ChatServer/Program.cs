using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;

//[assembly: OwinStartup(typeof(Startup))]

namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://*:8088";
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Server running on {0}", url);
                Console.ReadLine();
            }
        }
    }

    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }

    [HubName("ChatHub")]
    public class MyHub : Hub
    {
        public void Chat(string param)
        {
            Console.WriteLine(param);
            Clients.All.addMessage(param);
        }
    }
}