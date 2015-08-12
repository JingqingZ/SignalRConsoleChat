using System;
using Microsoft.AspNet.SignalR.Client;

namespace ChatClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var connection = new HubConnection("http://127.0.0.1:8088/");
            var myHub = connection.CreateHubProxy("ChatHub");

            connection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}",
                                      task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine("Connected");
                }

            }).Wait();

            myHub.On<string>("addMessage", param =>
            {
                Console.WriteLine(param);
            });


            Console.Write("Your name: ");
            string name = Console.ReadLine();

            myHub.Invoke<string>("Chat", "Online: " + name).Wait();

            while (true)
            {
                string message = Console.ReadLine();
                myHub.Invoke<string>("Chat", name + ":" + message).Wait();
                if (message == "quit")
                    break;
            }
            connection.Stop();
        }
    }
}