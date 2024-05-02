using LinkedInApp;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
    static void Main()
    {
        var config = new Configuration();
        var credentials = new CredentialsManager();
        var linkedInAutomation = new LinkedInAutomation(config, credentials);

        linkedInAutomation.Login();
        Console.WriteLine("Press Enter to continue after logging in to your account...");
        Console.ReadLine();
        linkedInAutomation.ExtractProfilePhoto();
    }
}



