using Xunit;
using System;
using System.Threading;

public class SowPageTests : TestBase
{
    [Fact]
    public void SowPage_Should_Load_And_Display_Heading()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var projectsPage = new ProjectsPage(driver);
        projectsPage.Navigate();
        Thread.Sleep(1000);

        projectsPage.ClickScopeOfWorkButton(); // Navigate to SOW page
        Thread.Sleep(1500); // Wait for page load

        var sowPage = new SowPage(driver);
        Assert.True(sowPage.IsPageLoaded(), "SOW page did not load properly or heading is missing.");
    }

    [Fact]
    public void SowPage_Should_Open_NewSowModal_OnButtonClick()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var projectsPage = new ProjectsPage(driver);
        projectsPage.Navigate();
        Thread.Sleep(1000);

        projectsPage.ClickScopeOfWorkButton(); // Navigate to SOW page
        Thread.Sleep(1500);

        var sowPage = new SowPage(driver);

        // Ensure the page is loaded before clicking the button
        Assert.True(sowPage.IsPageLoaded(), "SOW page did not load properly.");

        sowPage.ClickNewSowButton();
        Assert.True(sowPage.IsCreateSowModalVisible(), "'Create New SOW' modal was not displayed.");
    }
    [Fact]
    public void SowPage_Should_Open_EditSowModal_OnEditButtonClick()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var projectsPage = new ProjectsPage(driver);
        projectsPage.Navigate();
        Thread.Sleep(1000);

        projectsPage.ClickScopeOfWorkButton(); // Navigate to SOW page
        Thread.Sleep(1500);

        var sowPage = new SowPage(driver);
        Assert.True(sowPage.IsPageLoaded(), "SOW page did not load properly.");

        // Click Edit button (assume this clicks the first or specific edit button)
        sowPage.ClickEditButton();

        // Wait for Edit SOW modal to appear
        bool modalVisible = sowPage.WaitForEditSowModal();
        Assert.True(modalVisible, "Edit SOW modal did not appear.");

        // Get the modal heading text and verify it starts with "Edit SOW:"
        string headingText = sowPage.GetEditSowModalHeadingText();
        Assert.StartsWith("Edit SOW:", headingText);
    }


}
