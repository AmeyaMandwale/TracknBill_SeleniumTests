using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

public class BillsPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public BillsPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    // Locators
    public static readonly By isHeadingVisible = By.XPath("//div[contains(@class, 'MuiGrid-root') and contains(@class, 'MuiGrid-container')]//h6[contains(text(), 'List of Bills')]");

    public static readonly By AddBillButton = By.XPath("//button[contains(normalize-space(), 'ADD NEW BILL')]");
    public static readonly By DialogHeader = By.XPath("//h2[contains(text(), 'Add New Bill')]");
    public static readonly By SubmitButton = By.XPath("//div[@role='dialog']//button[@type='submit']");
    public static readonly By ValidationMessage_BillName = By.XPath("//p[contains(@class,'Mui-error') or contains(@class,'MuiFormHelperText-root')]");
    public static readonly By BillEntries = By.XPath("//div[contains(@class, 'MuiBox-root') and contains(@class, 'css-18mx9et')]");
    public static readonly By EditButton = By.XPath("(//div[contains(@class,'css-18mx9et')]//button)[1]");
    public static readonly By EditModalHeading = By.XPath("//h2[starts-with(text(), 'Update Bill Status -')]");


    public void Navigate()
    {
        driver.Navigate().GoToUrl("http://localhost:3000/Bills");
    }

    public bool IsHeadingPresent()
    {
        try
        {
            return wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'MuiGrid-root') and contains(@class, 'MuiGrid-container')]//h6[contains(text(), 'List of Bills')]"))).Displayed;
        }
        catch
        {
            return false;
        }
    }

    public int GetBillEntriesCount()
    {
        try
        {
            var entries = wait.Until(driver => driver.FindElements(BillEntries));
            return entries.Count;
        }
        catch
        {
            return 0;
        }
    }

    public void ClickFirstEditButton()
    {
        wait.Until(driver => driver.FindElements(BillEntries).Count > 0);

        var editButton = wait.Until(driver =>
            driver.FindElement(By.XPath("(//div[contains(@class,'css-18mx9et')]//button)[1]"))
        );

        // Scroll into view
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", editButton);

        // Click the button
        editButton.Click();
    }


    public bool IsEditModalVisible()
    {
        try
        {
            return wait.Until(driver => driver.FindElement(EditModalHeading)).Displayed;
        }
        catch
        {
            return false;
        }
    }



    public bool IsFieldRequired(string fieldLabel)
    {
        try
        {
            return wait.Until(d =>
                d.FindElement(By.XPath($"//label[contains(text(),'{fieldLabel}') and contains(@class,'Mui-required')]"))
            ).Displayed;
        }
        catch
        {
            return false;
        }
    }

    public bool IsValidationMessageDisplayed(string validationText)
    {
        try
        {
            return wait.Until(d =>
                d.FindElement(By.XPath($"//*[contains(text(),'{validationText}')]"))
            ).Displayed;
        }
        catch
        {
            return false;
        }
    }
}
