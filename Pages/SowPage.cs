
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

public class SowPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public SowPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    // Updated heading XPath based on your inspect HTML
    public static readonly By PageHeading = By.XPath("//h6[normalize-space()='Scope of Work (SOW)']");

    // Updated New SOW button XPath using visible text
    public static readonly By NewSowButton = By.XPath("//button[normalize-space()='New SOW']");
    private static readonly By FirstEditButton = By.XPath("(//div[contains(@class,'css-xine5j')]//button)[2]");




    // Locator for Edit SOW modal heading
    private readonly By EditSowModalHeading = By.XPath("//h2[contains(text(),'Edit SOW') or contains(text(),'Update Bill Status')]");






    // Modal heading that appears after clicking New SOW
    public static readonly By CreateSowModalHeading = By.XPath("//h2[normalize-space()='Create New SOW']");

    public bool IsPageLoaded()
    {
        try
        {
            return wait.Until(d => d.FindElement(PageHeading).Displayed);
        }
        catch
        {
            return false;
        }
    }

    public void ClickNewSowButton()
    {
        try
        {
            // Ensure SOW page is loaded
            wait.Until(d => d.FindElement(PageHeading));

            // Allow time for animations/render
            Thread.Sleep(1000);

            var button = wait.Until(d =>
            {
                var el = d.FindElement(By.XPath("//button[contains(., 'New SOW')]"));
                return (el.Displayed && el.Enabled) ? el : throw new NoSuchElementException("New SOW button not clickable.");
            });

            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", button);
            button.Click();

            Thread.Sleep(1000); // wait for modal
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error clicking New SOW button: " + ex.Message);
            throw;
        }
    }
    public void ClickEditButton()
    {
        try
        {
            // Wait for at least one entry to be present
            wait.Until(d => d.FindElements(By.XPath("//div[contains(@class,'css-xine5j')]")).Count > 0);

            // Click the 2nd button in the first row (Edit)
            var editButton = wait.Until(d =>
            {
                var el = d.FindElement(FirstEditButton);
                return (el.Displayed && el.Enabled) ? el : throw new NoSuchElementException("Edit button not clickable.");
            });

            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", editButton);
            editButton.Click();

            Console.WriteLine("✅ Clicked edit button");
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Failed to click edit button: " + ex.Message);
            throw;
        }
    }



    // Add this method to wait for the Edit SOW modal to be visible
    public bool WaitForEditSowModal()
    {
        try
        {
            wait.Until(d => d.FindElement(EditSowModalHeading).Displayed);
            return true;
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }

    public string GetEditSowModalHeadingText()
    {
        var heading = wait.Until(d => d.FindElement(EditSowModalHeading));
        return heading.Text.Trim();
    }



    public bool IsCreateSowModalVisible()
    {
        try
        {
            return wait.Until(d => d.FindElement(CreateSowModalHeading).Displayed);
        }
        catch
        {
            return false;
        }
    }
}
