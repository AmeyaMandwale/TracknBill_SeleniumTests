using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using OpenQA.Selenium.Support.UI;


public class VendorPageTests : IDisposable
{
    private readonly IWebDriver driver;

    public VendorPageTests()
    {
        driver = new ChromeDriver();
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var vendorPage = new VendorPage(driver);
        vendorPage.Navigate();
    }

    [Fact]
    public void VendorPage_Should_Display_Heading()
    {
        var vendorPage = new VendorPage(driver);
        Assert.True(vendorPage.IsHeadingPresent(), "List of Vendors heading not found.");
    }

    [Fact]
    public void VendorPage_Should_DisplayVendorList()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var vendorPage = new VendorPage(driver);
        vendorPage.Navigate();

        var vendorItems = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(d => d.FindElements(VendorPage.VendorEntries));

        Assert.True(vendorItems.Count > 0, "No vendor entries found.");
    }

    [Fact]
    public void VendorPage_Should_Display_AddNewVendorButton()
    {
        var vendorPage = new VendorPage(driver);
        Assert.True(vendorPage.IsAddNewVendorButtonVisible(), "Add New Vendor button is not visible.");
    }

    public void Dispose()
    {
        driver.Quit();
    }
}
