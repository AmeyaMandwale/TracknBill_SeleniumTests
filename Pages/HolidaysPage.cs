using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

public class HolidaysPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public HolidaysPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
    }

    public static readonly By SidebarVisibleElement = By.XPath("//span[normalize-space()='Dashboard' or normalize-space()='Company Setting']");
    public static readonly By CompanySettingMenu = By.XPath("//span[normalize-space()='Company Setting']");
    public static readonly By HolidaysSubMenu = By.XPath("//span[normalize-space()='Holidays']");
    public static readonly By HolidaysHeading = By.XPath("//h5[normalize-space()='List of Holidays']");
    public static readonly By HolidayTableRows = By.XPath("//tr[contains(@class,'MuiTableRow-root')]");
    public static readonly By AddHolidayButton = By.XPath("//button[contains(., 'Add Holiday')]");

    //public static readonly By AddHolidayButton = By.XPath("//button[.//svg[@data-testid='AddIcon'] and contains(., 'Add Holiday')]");
    public static readonly By AddHolidayModalHeading = By.XPath("//h2[contains(text(),'Add New Holiday')]");


    public static readonly By Table = By.XPath("//table[contains(@class, 'MuiBox-root')]");
    public static readonly By HolidayRows = By.XPath("//table[contains(@class, 'MuiBox-root')]//tbody/tr");
    
    public static readonly By ModalHeading = By.XPath("//h2[contains(text(),'Add New Holiday')]");
    public static readonly By FirstEditButton = By.XPath("//tbody/tr[1]//button[@aria-label='Edit']");
    public static readonly By EditModalHeading = By.XPath("//h2[normalize-space()='Edit Holiday']");
    public static readonly By ViewButton = By.CssSelector("svg[data-testid='VisibilityIcon']");
    public static readonly By ViewModalHeading = By.XPath("//h2[contains(text(),'Holiday Details')]");
    public static readonly By SearchInput = By.XPath("//input[@placeholder='Search by holiday name']");
    public static readonly By FilterDropdown = By.XPath("//label[contains(text(),'Filter')]//following::div[@role='button'][1]");

    public void NavigateThroughMenu()
    {
        Console.WriteLine("Navigating to Holidays from sidebar...");

        wait.Until(d => d.FindElement(CompanySettingMenu));
        var companySetting = wait.Until(d => d.FindElement(CompanySettingMenu));
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", companySetting);

        var holidaysSubMenu = wait.Until(d => d.FindElement(HolidaysSubMenu));
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", holidaysSubMenu);

        wait.Until(d => d.FindElement(HolidaysHeading));
    }



    public bool IsHeadingPresent()
    {
        try { return wait.Until(d => d.FindElement(HolidaysHeading)).Displayed; }
        catch { return false; }
    }


    public bool IsTableDisplayed() => wait.Until(d => d.FindElement(Table)).Displayed;

    public int GetHolidayRowsCount() => wait.Until(d => d.FindElements(HolidayRows)).Count;

    public bool IsAddHolidayButtonVisible() => wait.Until(d => d.FindElement(AddHolidayButton)).Displayed;

    public bool IsSearchInputVisible() => wait.Until(d => d.FindElement(SearchInput)).Displayed;

    public bool IsFilterDropdownVisible() => wait.Until(d => d.FindElement(FilterDropdown)).Displayed;
}
