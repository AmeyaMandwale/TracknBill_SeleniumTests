// Tests/CustomerPageTests.cs
using OpenQA.Selenium.Support.UI;
using Xunit;

public class CustomerPageTests : TestBase
{
    [Fact]
    public void CustomerPage_Should_Load_And_Display_Heading()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        Console.WriteLine("Current URL after login: " + driver.Url);
        Console.WriteLine("Page Source:\n" + driver.PageSource);

        var customerPage = new CustomerPage(driver);
        customerPage.Navigate();

        Console.WriteLine("URL after navigating to Customer page: " + driver.Url);
        Console.WriteLine("Page Source after navigating:\n" + driver.PageSource);


        Assert.True(customerPage.IsHeadingPresent("List of Customers"), "Customer page heading not found.");
    }

    [Fact]
    public void CustomerPage_Should_Open_AddCustomerDialog_OnButtonClick()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var customerPage = new CustomerPage(driver);
        customerPage.Navigate();

        customerPage.OpenAddCustomerDialog();

        Assert.True(customerPage.IsAddCustomerDialogVisible(), "'Add New Customer' dialog was not opened.");
    }



    [Fact]
    public void CustomerPage_Should_DisplayCustomerList()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var customerPage = new CustomerPage(driver);
        customerPage.Navigate();

        var customerItems = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(d => d.FindElements(CustomerPage.CustomerEntries));

        Assert.True(customerItems.Count > 0, "No customer entries found.");
    }

    [Fact]
    public void CustomerPage_Should_Fail_When_Heading_Is_Wrong()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var customerPage = new CustomerPage(driver);
        customerPage.Navigate();

        // Expecting wrong heading text on purpose to trigger failure
        Assert.True(customerPage.IsHeadingPresent("Customers That Do Not Exist"), "Incorrect: Heading 'Customers That Do Not Exist' not found.");
    }

    [Fact]
    public void CustomerPage_Should_Fail_When_ValidationMessage_Is_NotDisplayed()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var customerPage = new CustomerPage(driver);
        customerPage.Navigate();

        customerPage.OpenAddCustomerDialog();
        customerPage.SubmitForm();

        // Expecting validation for a made-up field
        Assert.True(customerPage.IsValidationMessageDisplayed("Customer Email is required"), "Validation for 'Customer Phone is required' not found.");
    }


}
