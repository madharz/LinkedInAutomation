using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedInApp
{
    public class CredentialsManager
    {
        private readonly string _credendialsFilePath = "credentials.txt";
        public string Username { get; private set; }
        public string Password { get; private set; }

        public CredentialsManager()
        {
            LoadCredentialsFromFile();
        }
        public void RequestCredentialsFromConsole()
        {
            Console.WriteLine("Enter your username:");
            Username = Console.ReadLine();

            Console.WriteLine("Enter your password:");
            Password = Console.ReadLine();

        }

        private void LoadCredentialsFromFile()
        {
            if (File.Exists(_credendialsFilePath))
            {
                string[] lines = File.ReadAllLines(_credendialsFilePath);

                if (lines.Length >= 2)
                {
                    Username = lines[0];
                    Password = lines[1];
                    return;
                }
            }

            Console.WriteLine("No valid credentials found. Please enter your username and password:");
            RequestCredentialsFromConsole();
            SaveCredentialsToFile();
        }



        public void SaveCredentialsToFile()
        {
            File.WriteAllText(_credendialsFilePath, $"{Username}\n{Password}");

        }

    }
}

