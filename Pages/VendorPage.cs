using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

public class VendorPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public VendorPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public static readonly By Heading = By.XPath("//h6[normalize-space(text())='List of Vendors']");
    public static readonly By Table = By.XPath("//table[contains(@class,'MuiTable-root')]");
    public static readonly By AddNewVendorButton = By.XPath("//button[normalize-space()='ADD NEW VENDOR']");
    public static readonly By VendorEntries = By.XPath("//div[contains(@class, 'MuiBox-root') and .//span[contains(text(),'Vendor Name')]]");

    public void Navigate()
    {
        driver.Navigate().GoToUrl("http://localhost:3000/Vendor");
        wait.Until(d => d.FindElement(Heading));
    }

    public bool IsHeadingPresent() => IsVisible(Heading);
    public bool IsTableVisible() => IsVisible(Table);
    public bool IsAddNewVendorButtonVisible() => IsVisible(AddNewVendorButton);

    private bool IsVisible(By selector)
    {
        try
        {
            return wait.Until(d => d.FindElement(selector)).Displayed;
        }
        catch
        {
            return false;
        }
    }
}
