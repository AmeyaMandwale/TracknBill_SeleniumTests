
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

public class LeavesDataPageTests : TestBase
{
    [Fact]
    public void LeavesDataPage_Should_Load_And_Display_Heading()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var leavesDataPage = new LeavesDataPage(driver);
        leavesDataPage.NavigateThroughMenu();

        Assert.True(leavesDataPage.IsHeadingPresent(), "Leave Management heading was not found.");
    }

    [Fact]
    public void LeavesDataPage_Should_DisplayLeaveList()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var leavesDataPage = new LeavesDataPage(driver);
        leavesDataPage.NavigateThroughMenu();

        var leaveItems = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(d => d.FindElements(LeavesDataPage.LeaveEntries));

        Assert.True(leaveItems.Count > 0, "No leave entries found.");
    }
    [Fact]
    public void LeavesDataPage_Should_ClickAddLeaveButton_And_DisplayModal()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var leavesDataPage = new LeavesDataPage(driver);
        leavesDataPage.NavigateThroughMenu();

        var addLeaveButton = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(d => d.FindElement(LeavesDataPage.AddLeaveButton));

        Assert.True(addLeaveButton.Displayed, "'Add Leave' button is not visible.");

        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", addLeaveButton);

        var modalHeadingLocator = By.XPath("//h2[contains(text(),'Add New Leave')]");

        var modalHeading = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(d => d.FindElement(modalHeadingLocator));

        Assert.True(modalHeading.Displayed, "Add Leave modal did not appear after clicking the button.");
    }

    [Fact]
    public void EditLeaveForm_Should_OpenOnEditButtonClick()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var leavesDataPage = new LeavesDataPage(driver);
        leavesDataPage.NavigateThroughMenu();

        //  wait for page to settle
        Thread.Sleep(1000);

        // Click the Edit button
        var editButton = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(d => d.FindElement(LeavesDataPage.FirstEditButton));

        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", editButton);
        Thread.Sleep(1000); // Pause for observation

        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", editButton);
        Thread.Sleep(1000); // Pause after click

        //  Wait for the Edit Leave modal
        var editModalHeading = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(d => d.FindElement(LeavesDataPage.EditModalHeading));

        Thread.Sleep(1000); // Pause to view the modal

        //  Assertion
        Assert.True(editModalHeading.Displayed, "Edit Leave modal did not appear.");
    }

    [Fact]
    public void ViewLeaveForm_Should_OpenOnViewButtonClick()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var leavesDataPage = new LeavesDataPage(driver);
        leavesDataPage.NavigateThroughMenu();

        // Optional: wait for table to load
        Thread.Sleep(1000);

        // Step 1: Find and click the View button 
        var viewButton = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(d => d.FindElement(By.CssSelector("svg[data-testid='VisibilityIcon']")));

        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", viewButton);
        Thread.Sleep(1000); 

        // Click the parent <button> of the SVG icon
        var viewButtonParent = viewButton.FindElement(By.XPath("./ancestor::button"));
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", viewButtonParent);
        Thread.Sleep(1000); 

        // Step 2: Assert modal is displayed (adjust heading if needed)
        var modalHeading = new WebDriverWait(driver, TimeSpan.FromSeconds(5))
            .Until(d => d.FindElement(By.XPath("//h2[contains(text(),'Leave Details')]")));

        Assert.True(modalHeading.Displayed, "Leave Details modal did not appear.");
    }

    [Fact]
    public void LeavesDataPage_Should_Fail_If_UploadLeaveDataModal_NotDisplayedAfterClick()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var leavesDataPage = new LeavesDataPage(driver);
        leavesDataPage.NavigateThroughMenu();

        var uploadButton = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(d => d.FindElement(LeavesDataPage.UploadLeaveDataButton));

        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", uploadButton);

        // Wait for modal header that should appear
        var modalHeaderLocator = By.XPath("//h2[contains(text(),'Upload Leave Data')]");

        bool modalVisible;
        try
        {
            modalVisible = new WebDriverWait(driver, TimeSpan.FromSeconds(5))
                .Until(d => d.FindElement(modalHeaderLocator).Displayed);
        }
        catch
        {
            modalVisible = false;
        }

        Assert.True(modalVisible, "'Upload Leave Data' modal did not appear after clicking the button.");
    }

    [Fact]
    public void LeavesDataPage_Should_Fail_If_UploadLeaveDataButton_IsNotClickable()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var leavesDataPage = new LeavesDataPage(driver);
        leavesDataPage.NavigateThroughMenu();

        var uploadButton = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(d => d.FindElement(LeavesDataPage.UploadLeaveDataButton)); // make sure this locator exists

        // Check if button is enabled and displayed
        bool isClickable = uploadButton.Displayed && uploadButton.Enabled;

        Assert.True(isClickable, "'Upload Leave Data' button is not clickable.");

        // Try clicking, expecting failure if it is not really clickable
        try
        {
            uploadButton.Click();
        }
        catch (Exception ex)
        {
            Assert.True(false, $"Failed to click 'Upload Leave Data' button: {ex.Message}");
        }
    }

    //[Fact]
    //public void DeleteLeave_Should_OpenConfirmationDialogOrRemoveRow()
    //{
    //    var leavesDataPage = new LeavesDataPage(driver);
    //    leavesDataPage.NavigateThroughMenu();

    //    // Optional: Wait for table to load
    //    Thread.Sleep(1000);

    //    // Step 1: Find the Delete button via DeleteIcon SVG
    //    var deleteIcon = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
    //        .Until(d => d.FindElement(By.CssSelector("svg[data-testid='DeleteIcon']")));

    //    // Step 2: Click the parent <button> of the Delete icon
    //    var deleteButton = deleteIcon.FindElement(By.XPath("./ancestor::button"));
    //    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", deleteButton);

    //    // Optional: Wait for confirmation dialog or alert
    //    Thread.Sleep(1000);

    //    // Step 3 (option 1): Check for confirmation dialog/modal
    //    // You can replace this with the actual heading/text of your confirmation modal
    //    var confirmationDialog = new WebDriverWait(driver, TimeSpan.FromSeconds(5))
    //        .Until(d => d.FindElement(By.XPath("//*[contains(text(),'Are you sure')]"))); // change if needed

    //    Assert.True(confirmationDialog.Displayed, "Confirmation dialog not displayed after clicking delete.");
    //}
    //[Fact]
    //public void BulkUploadButton_Should_OpenBulkUploadModal()
    //{
    //    var leavesDataPage = new LeavesDataPage(driver);
    //    leavesDataPage.NavigateThroughMenu();

    //    // Step 1: Wait for and click the Bulk Upload button
    //    var bulkUploadButton = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
    //        .Until(d => d.FindElement(LeavesDataPage.BulkUploadButton));

    //    Assert.True(bulkUploadButton.Displayed, "Bulk Upload button not visible.");
    //    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", bulkUploadButton);

    //    // Step 2: Wait for Bulk Upload modal to appear
    //    var modalHeader = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
    //        .Until(d => d.FindElement(LeavesDataPage.BulkUploadModalHeader));

    //    // Step 3: Assert modal is displayed
    //    Assert.True(modalHeader.Displayed, "Bulk Upload modal did not appear.");
    //}
}