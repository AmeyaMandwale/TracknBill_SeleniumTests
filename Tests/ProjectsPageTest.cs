using Xunit;
using System;
using System.Threading;
using OpenQA.Selenium.Support.UI;

public class ProjectsPageTests : TestBase
{
    [Fact]
    public void ProjectsPage_Should_Load_And_Display_Heading()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var projectsPage = new ProjectsPage(driver);
        projectsPage.Navigate();
        Thread.Sleep(2000);

        Assert.True(projectsPage.IsHeadingVisible(), "Projects page heading not found.");
    }

    [Fact]
    public void ProjectsPage_Should_Open_AddProjectDialog_OnButtonClick()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var projectsPage = new ProjectsPage(driver);
        projectsPage.Navigate();
        Thread.Sleep(1500);

        projectsPage.OpenAddProjectDialog();
        Thread.Sleep(1000);

        Assert.True(projectsPage.IsAddProjectDialogVisible(), "'Add Project' modal was not opened.");
    }


    [Fact]
    public void AddProjectForm_Should_ShowValidationErrors_WhenRequiredFieldsEmpty()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var projectsPage = new ProjectsPage(driver);
        projectsPage.Navigate();
        projectsPage.OpenAddProjectDialog();
        projectsPage.ClickSubmitButton();

        Assert.True(
            projectsPage.IsValidationMessageVisible("Project name is required"),
            "Validation message for 'Project Name' was not shown."
        );
    }



    [Fact]
    public void ProjectsPage_Should_Open_EditProjectDialog_OnEditIconClick()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var projectsPage = new ProjectsPage(driver);
        projectsPage.Navigate();
        Thread.Sleep(1000);

        projectsPage.OpenEditProjectDialog();
        Assert.True(projectsPage.IsEditProjectDialogVisible(), "'Edit Project' modal was not opened.");
    }

    //[Fact]
    //public void ProjectsPage_Should_ShowValidationMessages_OnInvalidEditFormSubmission()
    //{
    //    var loginPage = new LoginPage(driver);
    //    loginPage.Navigate();
    //    loginPage.Login("admin@gmail.com", "admin");

    //    var projectsPage = new ProjectsPage(driver);
    //    projectsPage.Navigate();
    //    Thread.Sleep(1000);

    //    projectsPage.OpenEditProjectDialog();
    //    projectsPage.FillInvalidEditData();
    //    projectsPage.ClickSaveChanges();

    //    Assert.True(projectsPage.AreValidationMessagesVisible(), "Validation messages were not shown on invalid input.");
    //}

    [Fact]
    public void ProjectsPage_Should_Display_ProjectEntriesInTable()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var projectsPage = new ProjectsPage(driver);
        projectsPage.Navigate();
        Thread.Sleep(1500); // wait for rows to load

        Assert.True(projectsPage.IsTableDisplayed(), "Project entries table is not displayed.");
        //Assert.True(projectsPage.GetProjectRowsCount() > 0, "No project rows found in the table.");
    }
    [Fact]
    public void ProjectsPage_Should_RedirectToSOWPage_When_ScopeOfWorkButtonClicked()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var projectsPage = new ProjectsPage(driver);
        projectsPage.Navigate();
        Thread.Sleep(1000); // Wait for projects to load

        string originalUrl = driver.Url;

        projectsPage.ClickScopeOfWorkButton();

        // Wait until URL changes
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(d => d.Url != originalUrl && d.Url.Contains("/sow/"));

        // Extract dynamic SOW ID from new URL
        string sowUrl = driver.Url;
        string idPart = sowUrl.Split("/sow/")[1];
        int dynamicSowId = int.Parse(idPart);

        Console.WriteLine($"Navigated to SOW ID: {dynamicSowId}");

        //  Optionally, test SOW page directly here or call SowPage
        var sowPage = new SowPage(driver);
        Assert.True(sowPage.IsPageLoaded(), "SOW page did not load properly.");
    }



}
