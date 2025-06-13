// LoginPageTests.cs
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

public class LoginPageTests : TestBase
{
    [Fact]
    public void LoginPage_Should_Have_EmailAndPasswordFields()
    {
        driver.Navigate().GoToUrl("http://localhost:3000/login");

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        Console.WriteLine(driver.PageSource);
        // Use correct IDs or names
        var emailField = wait.Until(d => d.FindElement(By.XPath("//input[contains(@type, 'email')]")));
        var passwordField = wait.Until(d => d.FindElement(By.XPath("//input[@type='password']")));

        Assert.NotNull(emailField);
        Assert.NotNull(passwordField);
    }

    [Fact]
    public void Login_With_Invalid_Credentials_Should_Show_Error()
    {
        driver.Navigate().GoToUrl("http://localhost:3000/login");

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        // Enter email
        var emailField = wait.Until(d => d.FindElement(By.XPath("//input[@type='email']")));
        emailField.SendKeys("wrong@example.com");

        // Enter password
        var passwordField = wait.Until(d => d.FindElement(By.XPath("//input[@type='password']")));
        passwordField.SendKeys("wrongpassword");

        // Click the Submit button
        var submitButton = wait.Until(d => d.FindElement(By.XPath("//button[@type='submit']")));
        submitButton.Click();

        // Wait for the Material UI error message div to appear
        var error = wait.Until(d =>
            d.FindElement(By.XPath("//div[contains(@class, 'MuiAlert-message') and contains(text(), 'Invalid email or password')]"))
        );

        Assert.Contains("Invalid email or password", error.Text);
    }
}