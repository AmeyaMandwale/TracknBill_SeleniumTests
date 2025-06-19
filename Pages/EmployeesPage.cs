using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

public class EmployeesPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public EmployeesPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

   

    public static readonly By Heading = By.XPath("//h6[normalize-space(text())='Employees Basic Data']");
   // public static readonly By AddNewEmployeeButton = By.XPath("//button[normalize-space()='ADD NEW Employee']");
    public static readonly By BulkUploadEmployeeButton = By.XPath("//button[normalize-space()='BULK UPLOAD Employee']");
    public static readonly By AddNewEmployeeButton = By.XPath("//button[normalize-space()='ADD NEW Employee']");

    public static readonly By CreateEmployeeButton = By.XPath("//button[normalize-space()='Create Employee']");
    public static readonly By ValidationMessage = By.XPath("//*[contains(text(),'required') or contains(text(),'Required')]");
    public static readonly By EmployeeListItems = By.XPath("//div[contains(@class, 'MuiBox-root') and contains(@class, 'css-vl76e8')]");
    public static readonly By SaveButton = By.XPath("//button[normalize-space()='Save']");
    public static readonly By ValidationMessages = By.XPath("//*[contains(text(),'required') or contains(text(),'Required') or contains(@class,'Mui-error')]");

    public static readonly By EditButton = By.XPath("//button[.//svg[@data-testid='EditIcon']]");
    public static readonly By EditEmployeeDialogHeader = By.XPath("//h2[contains(text(),'Edit Employee')]");
    public static readonly By BasicDataTab = By.XPath("//button[@role='tab' and text()='Basic Data']");
    public static readonly By SalaryTab = By.XPath("//button[@role='tab' and text()='Salary']");

    public static readonly By BulkUploadSalaryButton = By.XPath("//button[contains(text(),'Bulk Upload Salary')]");
    public static readonly By BulkUploadSalaryModalHeader = By.XPath("//h2[contains(text(),'Bulk Upload Employee Salary')]");



    public void Navigate() => driver.Navigate().GoToUrl("http://localhost:3000/Employees");

    public bool IsHeadingPresent() => IsVisible(Heading);
    public bool IsAddEmployeeButtonPresent() => IsVisible(AddNewEmployeeButton);

    private bool IsVisible(By selector)
    {
        try
        {
            return wait.Until(d => d.FindElement(selector)).Displayed;
        }
        catch
        {
            return false;
        }
    }
}
