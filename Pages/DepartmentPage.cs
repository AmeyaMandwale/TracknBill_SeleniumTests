using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

public class DepartmentPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public DepartmentPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public static readonly By Heading = By.XPath("//h5[contains(translate(text(), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), 'list of departments')]");

    public static readonly By AddDepartmentButton = By.XPath("//button[normalize-space()='Add Department']");
    public static readonly By DialogHeader = By.XPath("//h2[normalize-space()='Add Department']");
    public static readonly By DepartmentEntries = By.XPath("//div[contains(@class, 'MuiBox-root') and .//span]");
   
    public static readonly By ValidationMessage = By.XPath("//p[contains(@class,'Mui-error') or contains(@class,'MuiFormHelperText-root')]");
    public static readonly By SubmitButton = By.XPath("//button[normalize-space()='Submit']");

    public static readonly By EditButton = By.CssSelector("svg[data-testid='EditIcon']");
    public static readonly By EditDialogHeader = By.XPath("//h2[normalize-space()='Edit Department']");


    public static readonly By DepartmentNameField = By.Name("departmentName"); 
    public static readonly By TotalEmployeesField = By.Name("totalEmployees"); 
    public static readonly By SaveChangesButton = By.XPath("//button[normalize-space()='Save Changes']");
    public static readonly By GenericValidationMessages = By.XPath("//p[contains(@class,'Mui-error') or contains(@class,'MuiFormHelperText-root')]");

    public static readonly By DepartmentRows = By.XPath("//tr[contains(@class, 'MuiTableRow-root')]");


    public void Navigate()
    {
        driver.Navigate().GoToUrl("http://localhost:3000/company/departments");
    }

    public bool IsHeadingPresent()
    {
        try
        {
            return wait.Until(driver => driver.FindElement(Heading)).Displayed;
        }
        catch
        {
            return false;
        }
    }


    public bool IsTablePresent()
    {
        try
        {
            return wait.Until(driver => driver.FindElement(By.XPath("//div[@role='table']"))).Displayed;
        }
        catch
        {
            return false;
        }
    }

    public void OpenAddDepartmentDialog()
    {
        var addButton = wait.Until(driver => driver.FindElement(AddDepartmentButton));
        addButton.Click();
        Thread.Sleep(1000); 
        wait.Until(driver => driver.FindElement(DialogHeader));
    }

    public bool IsAddDepartmentDialogVisible()
    {
        try
        {
            return wait.Until(driver => driver.FindElement(DialogHeader)).Displayed;
        }
        catch
        {
            return false;
        }
    }

    public void ClickSubmitButton()
    {
        var submit = wait.Until(driver => driver.FindElement(SubmitButton));
        submit.Click();
        Thread.Sleep(500); // Optional: Wait to see validation animation
    }

    public bool IsValidationMessageVisible(string messageText)
    {
        try
        {
            return wait.Until(driver =>
                driver.FindElement(By.XPath($"//p[contains(text(),'{messageText}')]"))
            ).Displayed;
        }
        catch
        {
            return false;
        }
    }
    public void OpenEditDepartmentDialog()
    {
        
        var editIcon = wait.Until(driver => driver.FindElement(EditButton));

        
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", editIcon);
        Thread.Sleep(500); 
        editIcon.Click();
        Thread.Sleep(1000); 

        wait.Until(driver => driver.FindElement(EditDialogHeader));
    }

    public bool IsEditDepartmentDialogVisible()
    {
        try
        {
            return wait.Until(driver => driver.FindElement(EditDialogHeader)).Displayed;
        }
        catch
        {
            return false;
        }
    }

    public void FillInvalidEditData()
    {
        wait.Until(driver => driver.FindElement(DepartmentNameField)).Clear();
        driver.FindElement(DepartmentNameField).SendKeys("12345"); // Invalid: numeric in name

        wait.Until(driver => driver.FindElement(TotalEmployeesField)).Clear();
        driver.FindElement(TotalEmployeesField).SendKeys("abc"); // Invalid: characters in numeric field
    }

    public void ClickSaveChanges()
    {
        var saveBtn = wait.Until(driver => driver.FindElement(SaveChangesButton));
        saveBtn.Click();
        Thread.Sleep(500); // Wait for validation UI
    }

    public bool AreAnyValidationMessagesVisible()
    {
        try
        {
            var messages = wait.Until(driver => driver.FindElements(GenericValidationMessages));
            return messages.Any(msg => msg.Displayed);
        }
        catch
        {
            return false;
        }
    }


    public int GetDepartmentRowCount()
    {
        var rows = wait.Until(driver => driver.FindElements(DepartmentRows));
        return rows.Count;
    }

}
