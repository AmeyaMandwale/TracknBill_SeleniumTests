using OpenQA.Selenium;
using Xunit;

public abstract class TestBase : IDisposable
{
    protected IWebDriver driver;

    public TestBase()
    {
        driver = WebDriverFactory.Create();
    }

    public void Dispose()
    {
        driver.Quit();
    }
}
