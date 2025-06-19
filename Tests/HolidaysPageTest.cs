using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;
using System;
using System.Threading;
using SeleniumExtras.WaitHelpers;


public class HolidaysDataPageTests : TestBase
{
    [Fact]
    public void HolidaysPage_Should_Load_And_Display_Heading()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var holidaysPage = new HolidaysPage(driver); 
        holidaysPage.NavigateThroughMenu();
        Thread.Sleep(2000);

        Assert.True(holidaysPage.IsHeadingPresent(), "'List of Holidays' heading was not found.");
    }

    [Fact]
    public void HolidaysPage_Should_Display_HolidayList()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var holidaysPage = new HolidaysPage(driver);
        holidaysPage.NavigateThroughMenu();

        // Pause to observe
        Thread.Sleep(2000);

        var rows = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(d => d.FindElements(HolidaysPage.HolidayTableRows));

        Assert.True(rows.Count > 0, "No holiday entries found in the table.");
    }

    [Fact]
    public void HolidaysPage_AddHolidayButton_Should_DisplayModal()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var holidaysPage = new HolidaysPage(driver);
        holidaysPage.NavigateThroughMenu();

        Thread.Sleep(2000); // Let UI load fully

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

        var addButton = wait.Until(ExpectedConditions.ElementToBeClickable(HolidaysPage.AddHolidayButton));
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", addButton);
        Thread.Sleep(500);

        try
        {
            addButton.Click();
        }
        catch
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", addButton);
        }

        var modal = wait.Until(ExpectedConditions.ElementIsVisible(HolidaysPage.AddHolidayModalHeading));
        Assert.True(modal.Displayed, "Add New Holiday modal did not appear.");
    }




    //    [Fact]
    //public void EditHolidayForm_Should_Open_On_Edit_Click()
    //{
    //    var loginPage = new LoginPage(driver);
    //    loginPage.Navigate();
    //    loginPage.Login("admin@gmail.com", "admin");

    //    var holidaysPage = new HolidaysPage(driver);
    //    holidaysPage.NavigateThroughMenu();

    //    Thread.Sleep(1000);
    //    var editButton = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
    //        .Until(d => d.FindElement(HolidaysPage.FirstEditButton));

    //    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", editButton);
    //    Thread.Sleep(500);
    //    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", editButton);

    //    var editModalHeading = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
    //        .Until(d => d.FindElement(HolidaysPage.EditModalHeading));

    //    Assert.True(editModalHeading.Displayed, "Edit Holiday modal did not appear.");
    //}

    [Fact]
    public void ViewHolidayForm_Should_Open_On_View_Click()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var holidaysPage = new HolidaysPage(driver);
        holidaysPage.NavigateThroughMenu();

        Thread.Sleep(1000);
        var viewButton = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(d => d.FindElement(HolidaysPage.ViewButton));

        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", viewButton);
        var viewButtonParent = viewButton.FindElement(By.XPath("./ancestor::button"));
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", viewButtonParent);

        var modalHeading = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(d => d.FindElement(HolidaysPage.ViewModalHeading));

        Assert.True(modalHeading.Displayed, "Holiday Details modal did not appear.");
    }
}