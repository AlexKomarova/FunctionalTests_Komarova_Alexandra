using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests_Komarova_Alexandra;

public class Tests
{

    [Test]
    public void Authorization()
    {
        var options = new ChromeOptions();
        options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");

        var driver = new ChromeDriver(options);
        
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
        Thread.Sleep(5000);

        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("alex.lvova@skbkontur.ru");

        var password = driver.FindElement(By.Name("Password"));
        password.SendKeys("Im[lu]Art2695");

        var enter = driver.FindElement(By.Name("button"));
        enter.Click();
        
        Thread.Sleep(3000);

        var url = driver.Url;
        
        Assert.That(url == "https://staff-testing.testkontur.ru/news2");


        driver.Quit();
    }
}