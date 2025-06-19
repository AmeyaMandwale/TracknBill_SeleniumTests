
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit;

public class EmployeesPageTests : TestBase
{
    [Fact]
    public void Should_Login_And_NavigateToEmployeesPageSuccessfully()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var employeesPage = new EmployeesPage(driver);
        employeesPage.Navigate();

        Assert.True(employeesPage.IsHeadingPresent(), "Employees page heading not found.");
        
    }

    [Fact]
    public void EmployeesPage_Should_OpenBulkUploadModal_OnButtonClick()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var employeesPage = new EmployeesPage(driver);
        employeesPage.Navigate();

        var bulkUploadButton = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(d =>
            {
                var el = d.FindElement(EmployeesPage.BulkUploadEmployeeButton);
                return (el.Displayed && el.Enabled) ? el : null;
            });

        // Scroll and JS click to be sure
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", bulkUploadButton);
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", bulkUploadButton);

        // Wait for modal (adjust xpath as per your modal header)
        var modalHeader = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(d => d.FindElement(By.XPath("//h2[contains(text(), 'Bulk Upload')]")));

        Assert.True(modalHeader.Displayed, "Bulk Upload modal did not open on button click.");
    }

    [Fact]
    public void EmployeesPage_Should_OpenAddNewEmployeeModal_OnButtonClick()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var employeesPage = new EmployeesPage(driver);
        employeesPage.Navigate();

        // Wait for ADD NEW Employee button to be visible and enabled
        var addNewEmployeeButton = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(d =>
            {
                var el = d.FindElement(EmployeesPage.AddNewEmployeeButton);
                return (el.Displayed && el.Enabled) ? el : null;
            });

        // Scroll into view and click using JavaScript
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", addNewEmployeeButton);
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", addNewEmployeeButton);

        // Wait for the modal header "Add New Employee" to be visible
        var modalHeader = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(d => d.FindElement(By.XPath("//h2[contains(text(),'Add New Employee')]")));

        Assert.True(modalHeader.Displayed, "Add New Employee modal header is not visible after clicking the button.");
    }



    [Fact]
    public void EmployeesPage_Should_DisplayEmployeeList()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var employeesPage = new EmployeesPage(driver);
        employeesPage.Navigate();

        // Wait for actual employee rows using the updated locator
        var employeeItems = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(d => d.FindElements(EmployeesPage.EmployeeListItems));

        Console.WriteLine($"Employee rows found: {employeeItems.Count}");
        Assert.NotNull(employeeItems);
        Assert.True(employeeItems.Count > 0, "No employee entries found in the list.");
    }

    [Fact]
    public void EmployeesPage_Should_ShowValidationMessages_OnEmptySave()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var employeesPage = new EmployeesPage(driver);
        employeesPage.Navigate();

        
        var addNewEmployeeButton = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(d =>
            {
                var el = d.FindElement(EmployeesPage.AddNewEmployeeButton);
                return (el.Displayed && el.Enabled) ? el : null;
            });

        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", addNewEmployeeButton);

        var modalHeader = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(d => d.FindElement(By.XPath("//h2[contains(text(),'Add New Employee')]")));
        Assert.True(modalHeader.Displayed, "Add New Employee modal header is not visible.");

       
        var saveButton = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(d => d.FindElement(EmployeesPage.SaveButton));

        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", saveButton);

        
        var validationMessage = new WebDriverWait(driver, TimeSpan.FromSeconds(5))
            .Until(d => d.FindElements(EmployeesPage.ValidationMessages).FirstOrDefault(e => e.Displayed));

        Assert.NotNull(validationMessage);
        Assert.True(validationMessage.Displayed, "Validation message not displayed on empty form submission.");
    }
    //[Fact]
    //public void EmployeesPage_Should_OpenEditEmployeeModal_OnEditIconClick()
    //{
    //    var loginPage = new LoginPage(driver);
    //    loginPage.Navigate();
    //    loginPage.Login("admin@gmail.com", "admin");

    //    var employeesPage = new EmployeesPage(driver);
    //    employeesPage.Navigate();

    //    Thread.Sleep(1000); // Allow table to render

    //    // Check if at least one employee entry exists
    //    var employeeEntries = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
    //        .Until(d => d.FindElements(EmployeesPage.EmployeeListItems));

    //    Assert.True(employeeEntries.Count > 0, "No employee entries found in the table.");

    //    // Find the first visible edit button
    //    var editButton = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
    //        .Until(d =>
    //            d.FindElements(EmployeesPage.EditButton)
    //             .FirstOrDefault(e => e.Displayed && e.Enabled));

    //    Assert.NotNull(editButton);

    //    //Click the edit button using JavaScript to avoid click interception
    //    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", editButton);
    //    Thread.Sleep(1000);
    //    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", editButton);
    //    Thread.Sleep(1000);

    //    // Step 4: Wait for Edit Employee modal to appear
    //    var editModalHeading = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
    //        .Until(d => d.FindElements(By.XPath("//h2[contains(text(), 'Edit Employee')]"))
    //                     .FirstOrDefault(h => h.Displayed));

    //    Assert.NotNull(editModalHeading);
    //    Assert.True(editModalHeading.Displayed, "Edit Employee modal did not appear.");
    //}
    //[Fact]
    //public void EmployeesPage_Should_OpenBulkUploadSalaryModal_OnButtonClick()
    //{
    //    var loginPage = new LoginPage(driver);
    //    loginPage.Navigate();
    //    loginPage.Login("admin@gmail.com", "admin");

    //    var employeesPage = new EmployeesPage(driver);
    //    employeesPage.Navigate();

    //    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

    //    // Click Salary tab and wait for tab content to load
    //    var salaryTab = wait.Until(d => d.FindElement(EmployeesPage.SalaryTab));
    //    salaryTab.Click();

    //    // Wait for salary tab content
    //    wait.Until(d => d.FindElement(By.XPath("//div[contains(text(),'No salary data found for active employees')]")));

    //    // Find Bulk Upload Salary button
    //    var bulkUploadSalaryButton = wait.Until(d =>
    //    {
    //        var button = d.FindElement(EmployeesPage.BulkUploadSalaryButton);
    //        return (button.Displayed && button.Enabled) ? button : null;
    //    });

    //    // Scroll into view and try normal click
    //    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", bulkUploadSalaryButton);
    //    Thread.Sleep(1000);

    //    try
    //    {
    //        bulkUploadSalaryButton.Click();
    //    }
    //    catch
    //    {
    //        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", bulkUploadSalaryButton);
    //    }

    //    Thread.Sleep(2000); 

    //    // Wait for modal header
    //    var modalHeader = wait.Until(d => d.FindElement(EmployeesPage.BulkUploadSalaryModalHeader));

    //    Assert.True(modalHeader.Displayed, "Bulk Upload Employee Salary modal did not appear.");
    //}

}
