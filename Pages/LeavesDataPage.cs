
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

public class LeavesDataPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public LeavesDataPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
    }

    public static readonly By SidebarVisibleElement = By.XPath("//span[normalize-space()='Dashboard' or normalize-space()='Leaves']");
    public static readonly By LeavesMenu = By.XPath("//span[normalize-space()='Leaves']");
    public static readonly By LeaveDataSubMenu = By.XPath("//span[normalize-space()='Leave Data']");
    public static readonly By Heading = By.XPath("//h6[contains(translate(text(), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), 'leave management')]");

    public static readonly By Table = By.XPath("//table[contains(@class, 'MuiBox-root')]");
    public static readonly By LeaveEntries = By.XPath("//table[contains(@class, 'MuiBox-root')]//tbody/tr");
    public static readonly By AddLeaveButton = By.XPath("//button[normalize-space()='Add Leave']");
    public static readonly By ModalHeading = By.XPath("//h2[contains(text(),'Add New Leave')]");
    public static readonly By FirstEditButton = By.XPath("//tbody/tr[1]//button[@aria-label='Edit']");

    public static readonly By EditModalHeading = By.XPath("//h2[normalize-space()='Edit Leave']");
    public static readonly By ViewButton = By.CssSelector("svg[data-testid='VisibilityIcon']");
    public static readonly By LeaveDetailsModalHeading = By.XPath("//h2[contains(text(),'Leave Details')]");
    public static readonly By DeleteButtonIcon = By.CssSelector("svg[data-testid='DeleteIcon']");
    public static readonly By BulkUploadButton = By.XPath("//label[.//svg[@data-testid='UploadIcon'] and contains(., 'Bulk Upload')]");
    public static readonly By BulkUploadModalHeader = By.XPath("//h2[normalize-space()='Bulk Upload Files']");

    public static readonly By TableRows = By.XPath("//div[@role='row' and not(@aria-hidden='true')]");
    public static readonly By SearchInput = By.XPath("//input[@placeholder='Search by employee name']");
    public static readonly By FilterDropdown = By.XPath("//label[contains(text(),'Filter')]//following::div[@role='button'][1]");
    public static readonly By UploadLeaveDataButton = By.XPath("//button[contains(normalize-space(),'Upload Leave Data')]");

    public void NavigateThroughMenu()
    {
        // Assume already logged in
        wait.Until(d => d.FindElement(SidebarVisibleElement));

        var leavesMenu = wait.Until(d => d.FindElement(LeavesMenu));
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", leavesMenu);

        var leaveDataSubMenu = wait.Until(d => d.FindElement(LeaveDataSubMenu));
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", leaveDataSubMenu);

        wait.Until(d => d.FindElement(Heading));
    }


    public bool IsHeadingPresent()
    {
        try { return wait.Until(d => d.FindElement(Heading)).Displayed; }
        catch { return false; }
    }



    public bool IsTableDisplayed()
    {
        try { return wait.Until(d => d.FindElement(Table)).Displayed; }
        catch { return false; }
    }
    public bool IsAddLeaveButtonVisible()
    {
        try { return wait.Until(d => d.FindElement(AddLeaveButton)).Displayed; }
        catch { return false; }
    }

    public int GetLeaveRowsCount()
    {
        try { return wait.Until(d => d.FindElements(TableRows)).Count; }
        catch { return 0; }
    }

    public bool IsSearchInputVisible()
    {
        try { return wait.Until(d => d.FindElement(SearchInput)).Displayed; }
        catch { return false; }
    }

    public bool IsFilterDropdownVisible()
    {
        try { return wait.Until(d => d.FindElement(FilterDropdown)).Displayed; }
        catch { return false; }
    }
}
