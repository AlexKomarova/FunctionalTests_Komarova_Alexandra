using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests_Komarova_Alexandra;

public class Tests
{
    private ChromeDriver driver;

    [SetUp]
    public void SetUp()
    {
        var options = new ChromeOptions();
        options.AddArguments("--no-sandbox",
            "--disable-extensions"); //options.AddArguments("--headless"); //Вынес отдельно,чтобы была возможность быстро скрыть браузер
        driver = new ChromeDriver(options);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        Authorization();
    }

    private void Authorization()
    {
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("alex.lvova@skbkontur.ru");

        var password = driver.FindElement(By.Name("Password"));
        password.SendKeys("Im[lu]Art2695");

        var enter = driver.FindElement(By.Name("button"));
        enter.Click();
    }

    [Test]
    public void Test()
    {
        Assert.That(true);
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}