using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LinkedInApp
{
    class LinkedInAutomation
    {
        private readonly Configuration _configuration;
        private readonly CredentialsManager _credentialsManager;
        private IWebDriver _driver;
        private const string url = "https://www.linkedin.com/login";

        public LinkedInAutomation(Configuration config, CredentialsManager credentialsManager)
        {
            _configuration = config;
            _credentialsManager = credentialsManager;
        }

        public void Login()
        {
            while (true)
            {
                SetupWebDriver();

                _driver.Navigate().GoToUrl(url);
                FillLoginForm();

                // Проверяем успешность авторизации
                if (_driver.Url != url)
                {
                    // Авторизация прошла успешно, сохраняем учетные данные
                    _credentialsManager.SaveCredentialsToFile();
                    return;
                }

                // Авторизация не удалась, запрашиваем у пользователя новые учетные данные
                Console.WriteLine("Login failed. Please enter your username and password again.");
                _credentialsManager.RequestCredentialsFromConsole();
                _driver.Quit(); // Закрываем драйвер перед повторной попыткой
            }
        }
        private void SetupWebDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");
            _driver = new ChromeDriver(_configuration.DriverPath, options);
        }

        private void FillLoginForm()
        {
            var usernameInput = _driver.FindElement(By.Id("username"));
            usernameInput.SendKeys(_credentialsManager.Username);

            var passwordInput = _driver.FindElement(By.Id("password"));
            passwordInput.SendKeys(_credentialsManager.Password);
            passwordInput.SendKeys(Keys.Enter);

            // Ожидание загрузки страницы
            System.Threading.Thread.Sleep(5000);
        }


        public void ExtractProfilePhoto()
        {
            // Находим элемент с профильным фото и получаем ссылку на изображение
            var profilePhotoElement = _driver.FindElement(By.CssSelector(".evi-image"));
            string profilePhotoUrl = profilePhotoElement.GetAttribute("src");

            // Создаем объект WebClient для загрузки файла
            using (WebClient client = new WebClient())
            {
                // Генерируем имя файла на основе временной метки
                string fileName = $"profile_photo_{DateTime.Now.Ticks}.jpg";

                // Сохраняем изображение локально
                client.DownloadFile(profilePhotoUrl, fileName);

                // Выводим сообщение об успешном сохранении
                Console.WriteLine($"Профильное фото сохранено как {fileName}");
            }
        }
    }
}
