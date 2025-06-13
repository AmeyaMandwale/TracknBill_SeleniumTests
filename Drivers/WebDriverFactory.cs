using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

public static class WebDriverFactory
{
    public static IWebDriver Create()
    {
        var options = new ChromeOptions();
        options.AddArgument("start-maximized");
        // options.AddArgument("headless"); // Optional
        return new ChromeDriver(options);
    }
}
