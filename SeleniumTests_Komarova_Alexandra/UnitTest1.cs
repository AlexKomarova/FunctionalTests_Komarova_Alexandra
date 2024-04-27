using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Shouldly;

namespace SeleniumTests_Komarova_Alexandra;

public class Tests
{
    private ChromeDriver driver;

    [SetUp]
    public void SetUp()
    {
        var options = new ChromeOptions();
        options.AddArguments("--no-sandbox", "--disable-extensions", "--start-fullscreen");
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
        driver.FindElement(By.CssSelector("[data-tid='Title']"));
    }

    [Test]
    public void КликаемПоЛоготипу_ПерешлиНаГлавную()
    {
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/communities");

        var logo = driver.FindElement(By.TagName("header")).FindElement(By.TagName("img"));
        logo.Click();
        
        driver.Url.ShouldBe("https://staff-testing.testkontur.ru/",
            "Кликнули по логотипу и не перешли на главную");
    }

    [Test]
    public void СГлавнойРедиректит_НаНовости()
    {
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
        var title = driver.FindElement(By.CssSelector("[data-tid='Title']"));
        
        title.Text.ShouldBe("Новости", "Не средиректило на страницу новостей");
    }

    [Test]
    public void РедактируемДополнительныйEmail_УспешноОбновился()
    {
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/profile/settings/edit");
        var input = driver.FindElement(By.CssSelector("[data-tid='AdditionalEmail'] input"));
        
        input.SendKeys(Keys.Control + "a");
        input.SendKeys(Keys.Backspace);
        const string newEmail = "new@email.ru";
        input.SendKeys(newEmail);
        input.SendKeys(Keys.PageUp);
        input.SendKeys(Keys.PageUp);
        
        var saveButton = driver.FindElement(By.CssSelector("[data-tid='PageHeader'] button"));
        saveButton.Click();
        var extraEmail = driver.FindElements(By.CssSelector("[data-tid='ContactCard'] a")).ToArray()[2];
        
        extraEmail.Text.ShouldBe(newEmail, "Дополнительный Email не обновился");
    }

    [Test]
    public void РедактируемДополнительныйEmail_ОтменилиРедактирование()
    {
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/profile/settings/edit");
        var input = driver.FindElement(By.CssSelector("[data-tid='AdditionalEmail'] input"));

        input.SendKeys(Keys.Control + "a");
        input.SendKeys(Keys.Backspace);
        const string newEmail = "defolt@email.ru";
        input.SendKeys(newEmail);
        input.SendKeys(Keys.PageUp);
        input.SendKeys(Keys.PageUp);

        var canselButton = driver.FindElements(By.CssSelector("[data-tid='PageHeader'] button"))[1];
        canselButton.Click();
        var extraEmail = driver.FindElements(By.CssSelector("[data-tid='ContactCard'] a")).ToArray()[2];

        extraEmail.Text.ShouldNotBe(newEmail, "Дополнительный Email обновился");
    }

    [Test]
    public void РедактируемРабочийТелефон_УспешноОбновился()
    {
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/profile/settings/edit");
        var input = driver.FindElement(By.CssSelector("[data-tid='WorkPhone'] input"));
        
        input.SendKeys(Keys.Control + "a");
        input.SendKeys(Keys.Backspace);
        const string newPhone = "+73431213123";
        input.SendKeys(newPhone);
        input.SendKeys(Keys.PageUp);
        input.SendKeys(Keys.PageUp);
        
        var saveButton = driver.FindElement(By.CssSelector("[data-tid='PageHeader'] button"));
        saveButton.Click();
        var workPhone = driver.FindElements(By.CssSelector("[data-tid='ContactCard'] div")).Select(x => x.Text).ToArray();

        workPhone.ShouldContain(newPhone, "Рабочий телефон не обновился");

    }

    [TearDown]
    public void TearDown()
    {
        driver.Close();
        driver.Quit();
    }
}