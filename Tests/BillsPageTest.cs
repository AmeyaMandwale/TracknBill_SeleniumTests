using OpenQA.Selenium.Support.UI;
using Xunit;

public class BillsPageTests : TestBase
{
    [Fact]
    public void BillsPage_Should_Load_And_Display_Heading()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var billsPage = new BillsPage(driver);
        billsPage.Navigate();

        Assert.True(billsPage.IsHeadingPresent(), "Bills page heading 'List of Bills' not found.");
    }


    [Fact]
    public void BillsPage_Should_Display_BillEntries()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var billsPage = new BillsPage(driver);
        billsPage.Navigate();

        int billCount = billsPage.GetBillEntriesCount();

        Assert.True(billCount > 0, $"Expected at least 1 bill entry, but found {billCount}.");
    }
    [Fact]
    public void BillsPage_Should_Open_EditModal_On_EditButtonClick()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Navigate();
        loginPage.Login("admin@gmail.com", "admin");

        var billsPage = new BillsPage(driver);
        billsPage.Navigate();

        billsPage.ClickFirstEditButton();

        Assert.True(billsPage.IsEditModalVisible(), "Edit modal did not appear.");
    }

}
