// ---------------------------------------------------
// Copyright 2017 - Johan Boström
// File: Program.cs
// ---------------------------------------------------

using System;
using Microsoft.Owin.Hosting;
using Serilog;

namespace IdentityServer.SelfHosted
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "IdentityServer.SelfHosted";

            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .LiterateConsole(
                    outputTemplate: "{Timestamp:HH:MM} [{Level}] ({Name:l}){NewLine} {Message}{NewLine}{Exception}")
                .CreateLogger();

            const string url = "https://localhost:44333/core";
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("\n\nServer listening at {0}. Press enter to stop", url);
                Console.ReadLine();
            }
        }
    }
}