using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

public class InvoicePage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public InvoicePage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public static readonly By Heading = By.XPath("//h6[normalize-space(text())='List of Invoices']");
    public static readonly By GenerateInvoiceButton = By.XPath("//button[normalize-space()='GENERATE INVOICE']");
    public static readonly By CreateInvoiceDialogHeader = By.XPath("//h2[contains(@class, 'MuiDialogTitle-root') and contains(text(),'Create New Invoice')]");
    public static readonly By EditButton = By.XPath("//button[.//svg[@data-testid='EditIcon']]");

    public static readonly By SubmitButtonInDialog = By.XPath("//div[@role='dialog']//button[@type='submit']");
    public static readonly By ValidationMessage = By.XPath("//*[contains(text(),'required') or contains(text(),'Required')]");
    public static readonly By SearchInput = By.XPath("//input[contains(@placeholder, 'Search client or invoice no')]");
    public static readonly By FirstRow = By.XPath("//div[@role='row']");
    public static readonly By CreateInvoiceButton = By.XPath("//button[normalize-space()='Create Invoice']");
    public static readonly By InvoiceEntries = By.XPath("//div[contains(@class, 'MuiBox-root') and .//p]");

    public static readonly By EditDialogHeader = By.XPath("//h2[contains(text(),'Edit') or contains(text(),'Update')]");



    public void Navigate() => driver.Navigate().GoToUrl("http://localhost:3000/Invoice");

    public bool IsHeadingPresent() => IsVisible(Heading);
    public bool IsGenerateInvoiceButtonPresent() => IsVisible(GenerateInvoiceButton);

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
