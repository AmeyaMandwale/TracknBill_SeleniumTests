using Xunit;
using OpenQA.Selenium.Support.UI;
using System;

public class DepartmentPageTests : TestBase
{
    [Fact]
    public void DepartmentPage_Should_Load_And_Display_Heading()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var departmentPage = new DepartmentPage(driver);
        departmentPage.Navigate();
        Thread.Sleep(2000);

        Assert.True(departmentPage.IsHeadingPresent(), "Department page heading not found.");
    }

    [Fact]
    public void DepartmentPage_Should_Open_AddDepartmentDialog_OnButtonClick()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var departmentPage = new DepartmentPage(driver);
        departmentPage.Navigate();
        Thread.Sleep(1500); // Optional: Wait to observe UI

        departmentPage.OpenAddDepartmentDialog();
        Thread.Sleep(1500); // Optional: Wait to observe modal open

        Assert.True(departmentPage.IsAddDepartmentDialogVisible(), "'Add Department' modal was not opened.");
    }

    [Fact]
    public void DepartmentPage_Should_ShowValidation_OnEmptySubmit()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var departmentPage = new DepartmentPage(driver);
        departmentPage.Navigate();
        Thread.Sleep(1000); // Optional visual pause

        departmentPage.OpenAddDepartmentDialog();
        Thread.Sleep(1000); // Wait for modal

        departmentPage.ClickSubmitButton();

        // Assert any known validation message like "Department Name is required"
        Assert.True(
            departmentPage.IsValidationMessageVisible("required") ||
            departmentPage.IsValidationMessageVisible("Department Name"),
            "Validation message not displayed for required fields."
        );
    }

    [Fact]
    public void DepartmentPage_Should_Open_EditDepartmentDialog_OnEditIconClick()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var departmentPage = new DepartmentPage(driver);
        departmentPage.Navigate();
        Thread.Sleep(1000); // Optional visual delay

        departmentPage.OpenEditDepartmentDialog();

        Assert.True(departmentPage.IsEditDepartmentDialogVisible(), "'Edit Department' modal was not opened.");
    }
    [Fact]
    public void DepartmentPage_Should_ShowValidationMessages_OnInvalidEditFormSubmission()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var departmentPage = new DepartmentPage(driver);
        departmentPage.Navigate();
        Thread.Sleep(1000); // Optional delay

        departmentPage.OpenEditDepartmentDialog();
        departmentPage.FillInvalidEditData();
        departmentPage.ClickSaveChanges();

        Assert.True(departmentPage.AreAnyValidationMessagesVisible(), "Validation messages were not shown on invalid input.");
    }


    [Fact]
    public void DepartmentPage_Should_Display_DepartmentList()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var departmentPage = new DepartmentPage(driver);
        departmentPage.Navigate();

        int rowCount = departmentPage.GetDepartmentRowCount();

        Assert.True(rowCount > 0, "No departments found in the department list table.");
    }

    [Fact]
    public void DepartmentPage_Should_Fail_When_ValidationMessage_Is_NotPresent()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var departmentPage = new DepartmentPage(driver);
        departmentPage.Navigate();

        departmentPage.OpenAddDepartmentDialog();
        departmentPage.ClickSubmitButton();

        // Looking for a fake validation message that won’t appear
        Assert.True(departmentPage.IsValidationMessageVisible("This field is super required"),
            "Expected validation message 'This field is super required' was not found.");
    }

}
