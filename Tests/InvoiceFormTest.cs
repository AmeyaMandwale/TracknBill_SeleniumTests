using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;
using Xunit;

public class InvoiceFormTests : TestBase
{
    [Fact]
    public void Should_Login_And_NavigateToInvoicePageSuccessfully()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var invoicePage = new InvoicePage(driver);
        invoicePage.Navigate();

        Assert.True(invoicePage.IsHeadingPresent(), "Invoice page heading not found.");
        Assert.True(invoicePage.IsGenerateInvoiceButtonPresent(), "Generate Invoice button is not present.");
    }

    [Fact]
    public void Should_Display_GenerateInvoiceButton_OnInvoicePage()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var invoicePage = new InvoicePage(driver);
        invoicePage.Navigate();

        Assert.True(invoicePage.IsGenerateInvoiceButtonPresent(), "Generate Invoice button is not present on the Invoice page.");
    }

    [Fact]
    public void AddInvoiceForm_Should_ShowValidation_OnEmptyFields()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var invoicePage = new InvoicePage(driver);
        invoicePage.Navigate();

        // Step 1: Open "Add New Invoice" modal
        driver.FindElement(InvoicePage.GenerateInvoiceButton).Click();

        // Step 2: Wait for modal header to confirm modal is open
        new WebDriverWait(driver, TimeSpan.FromSeconds(5))
            .Until(d => d.FindElement(InvoicePage.CreateInvoiceDialogHeader));

        // 🔹 Step 3: Click the actual "Create Invoice" button to trigger validation
        driver.FindElement(InvoicePage.CreateInvoiceButton).Click();

        // Step 4: Wait for validation message to appear
        var validationMessage = new WebDriverWait(driver, TimeSpan.FromSeconds(5))
            .Until(d => d.FindElement(InvoicePage.ValidationMessage));

        // Step 5: Assert validation message is shown
        Assert.True(validationMessage.Displayed, "Validation message not shown for empty form.");
    }

    [Fact]
    public void InvoicePage_Should_DisplayCreateInvoiceButton()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var invoicePage = new InvoicePage(driver);
        invoicePage.Navigate();

        // Click GENERATE INVOICE button
        var generateBtn = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(d => d.FindElement(InvoicePage.GenerateInvoiceButton));
        generateBtn.Click();

        // Wait for modal heading - more robust XPath
        var modalHeader = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(d => d.FindElement(By.XPath("//h2[contains(@class, 'MuiDialogTitle-root') and contains(text(),'Create New Invoice')]")));

        // Wait for Create Invoice button inside modal
        var createInvoiceBtn = new WebDriverWait(driver, TimeSpan.FromSeconds(5))
            .Until(d => d.FindElement(InvoicePage.CreateInvoiceButton));

        Assert.True(createInvoiceBtn.Displayed, "Create Invoice button not found or not visible.");
    }
    //[Fact]
    //public void InvoicePage_Should_ShowEditButton_WhenInvoiceListIsPresent()
    //{
    //    var loginPage = new LoginPage(driver);
    //    loginPage.Navigate();
    //    loginPage.Login("admin@gmail.com", "admin");

    //    var invoicePage = new InvoicePage(driver);
    //    invoicePage.Navigate();

    //    // Wait for invoice list
    //    var invoiceList = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
    //        .Until(d => d.FindElements(InvoicePage.InvoiceEntries));
    //    Assert.True(invoiceList.Count > 0, "Invoice list not displayed.");

    //    // Check if edit button is present
    //    var editButton = new WebDriverWait(driver, TimeSpan.FromSeconds(5))
    //        .Until(d => d.FindElement(InvoicePage.EditButton));

    //    Assert.True(editButton.Displayed, "Edit button not found.");
    //}


    [Fact]
    public void InvoicePage_Should_DisplayInvoiceList()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var invoicePage = new InvoicePage(driver);
        invoicePage.Navigate();

        // Use the path from the page object
        var invoiceItems = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(d => d.FindElements(InvoicePage.InvoiceEntries));

        Assert.True(invoiceItems.Count > 0, "No invoice entries found.");
    }
    //[Fact]
    //public void InvoicePage_Should_Fail_When_NonExistentButton_IsChecked()
    //{
    //    var loginPage = new LoginPage(driver);
    //    loginPage.Navigate();
    //    loginPage.Login("admin@gmail.com", "admin");

    //    var invoicePage = new InvoicePage(driver);
    //    invoicePage.Navigate();

    //    try
    //    {
    //        var fakeButton = driver.FindElement(By.Id("non-existent-button"));
    //        Assert.True(fakeButton.Displayed, "Non-existent button is not displayed (unexpected).");
    //    }
    //    catch (NoSuchElementException)
    //    {
    //        // Fail the test with meaningful message
    //        Assert.True(false, "Expected button with id 'non-existent-button' was not found on the page.");
    //    }
    //}

    [Fact]
public void InvoicePage_Should_Fail_When_GenerateInvoiceCheckbox_IsNotChecked()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var invoicePage = new InvoicePage(driver);
        invoicePage.Navigate();

        // Try to find checkbox safely:
        var checkboxExists = invoicePage.IsVisible(InvoicePage.GenerateInvoiceCheckbox);
        Assert.True(checkboxExists, "Checkbox 'Generate Invoice' not found.");

        var checkbox = driver.FindElement(InvoicePage.GenerateInvoiceCheckbox);

        // Assert it is checked (expected, but will fail if unchecked)
        Assert.True(checkbox.Selected, "Expected 'Generate Invoice' checkbox to be checked by default, but it is not.");
    }


}
