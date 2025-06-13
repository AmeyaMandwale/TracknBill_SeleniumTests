using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

public class CustomerPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public CustomerPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    // public static readonly By AddCustomerButton = By.XPath("//button[contains(normalize-space(), 'ADD NEW CUSTOMER')]");
    public static readonly By DialogHeader = By.XPath("//h2[contains(text(), 'Add New Customer')]");
    public static readonly By SubmitButton = By.XPath("//div[@role='dialog']//button[@type='submit']");
    public static readonly By ValidationMessage_CustomerName = By.XPath("//p[contains(@class,'Mui-error') or contains(@class,'MuiFormHelperText-root')]");
    public static readonly By AddCustomerButton = By.XPath("//button[contains(normalize-space(), 'ADD NEW CUSTOMER')]");
    public static readonly By CustomerEntries = By.XPath("//div[contains(@class, 'MuiBox-root') and .//span]");



    public void Navigate()
    {
        driver.Navigate().GoToUrl("http://localhost:3000/Customer");
    }

    public bool IsHeadingPresent(string headingText)
    {
        try
        {
            return wait.Until(d =>
                d.FindElement(By.XPath($"//h6[contains(translate(text(), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), '{headingText.ToLower()}')]"))
            ).Displayed;
        }
        catch
        {
            return false;
        }
    }

    public bool IsAddCustomerDialogVisible()
    {
        try
        {
            return wait.Until(driver => driver.FindElement(DialogHeader)).Displayed;
        }
        catch
        {
            return false;
        }
    }

    public bool IsTablePresent()
    {
        try
        {
            return wait.Until(d => d.FindElement(By.XPath("//div[@role='table']"))).Displayed;
        }
        catch
        {
            return false;
        }
    }

    public void OpenAddCustomerDialog()
    {
        var addButton = wait.Until(driver => driver.FindElement(AddCustomerButton));
        addButton.Click();

        // Wait for dialog header
        wait.Until(driver => driver.FindElement(DialogHeader));
    }

    public void SubmitForm()
    {
        wait.Until(d => d.FindElement(By.XPath("//div[@role='dialog']//button[@type='submit']"))).Click();
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
