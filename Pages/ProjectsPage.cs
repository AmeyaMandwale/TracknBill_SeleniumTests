using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

public class ProjectsPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;


    public ProjectsPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public static readonly string PageUrl = "http://localhost:3000/company/projects";
    public static readonly By Heading = By.XPath("//h5[contains(text(),'List of Projects')]");
    public static readonly By AddProjectDialogHeader = By.XPath("//h2[contains(text(),'Add New Project')]");
    public static readonly By SubmitButton = By.XPath("//button[normalize-space()='Submit']");
    public static readonly By ProjectNameField = By.Name("projectName");
    public static readonly By ProjectManagerField = By.Name("projectManager");
    public static readonly By SaveChangesButton = By.XPath("//button[normalize-space()='Save Changes']");
    public static readonly By ValidationMessages = By.XPath("//p[contains(@class,'Mui-error') or contains(@class,'MuiFormHelperText-root')]");
    public static readonly By AddProjectButton = By.XPath("//button[contains(., 'Add Project')]");
    public static readonly By ModalHeading = By.XPath("//h2[contains(text(),'Add New Project')]");
    public static readonly By ProjectRows = By.XPath("//tr[contains(@class, 'MuiTableRow-root')]");
    public static readonly By EditButton = By.CssSelector("svg[data-testid='EditIcon']");
    public static readonly By EditDialogHeader = By.XPath("//h2[normalize-space()='Edit Project']");
    public static readonly By ScopeOfWorkButton = By.CssSelector("button[title='Scope of Work']");


    public void Navigate()
    {
        driver.Navigate().GoToUrl(PageUrl);
        Thread.Sleep(1000);
    }

    public bool IsHeadingVisible()
    {
        try { return driver.FindElement(Heading).Displayed; }
        catch { return false; }
    }

    public void OpenAddProjectDialog()
    {
        var addButton = driver.FindElement(AddProjectButton);
        addButton.Click();
        Thread.Sleep(1000); // Wait for modal
    }

    public bool IsAddProjectDialogVisible()
    {
        try
        {
            return driver.FindElement(ModalHeading).Displayed;
        }
        catch
        {
            return false;
        }
    }
    public bool IsTableDisplayed()
    {
        try
        {
            return driver.FindElements(ProjectRows).Count > 0;
        }
        catch
        {
            return false;
        }
    }


    public void ClickSubmitButton()
    {
        driver.FindElement(SubmitButton).Click();
        Thread.Sleep(500);
    }

    public bool IsValidationMessageVisible(string message)
    {
        try
        {
            return wait.Until(d =>
            {
                var elements = d.FindElements(By.XPath($"//p[normalize-space()='{message}']"));
                foreach (var el in elements)
                {
                    if (el.Displayed) return true;
                }
                return false;
            });
        }
        catch
        {
            return false;
        }
    }
    public void OpenEditProjectDialog()
    {
        var editIcon = driver.FindElement(EditButton);
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", editIcon);
        Thread.Sleep(500);
        editIcon.Click();
        Thread.Sleep(1000);
    }

    public bool IsEditProjectDialogVisible()
    {
        try { return driver.FindElement(EditDialogHeader).Displayed; }
        catch { return false; }
    }

    public void ClickScopeOfWorkButton()
    {
        var sowButton = driver.FindElement(ScopeOfWorkButton);
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", sowButton);
        sowButton.Click();
    }



    public void FillInvalidEditData()
    {
        driver.FindElement(ProjectNameField).Clear();
        driver.FindElement(ProjectNameField).SendKeys("123!@#");

        driver.FindElement(ProjectManagerField).Clear();
        driver.FindElement(ProjectManagerField).SendKeys("%%%");
    }

    public void ClickSaveChanges()
    {
        driver.FindElement(SaveChangesButton).Click();
        Thread.Sleep(500);
    }

    public bool AreValidationMessagesVisible()
    {
        try
        {
            var messages = driver.FindElements(ValidationMessages);
            foreach (var msg in messages)
            {
                if (msg.Displayed) return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    
}
