// Pages/LoginPage.cs
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

public class LoginPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public LoginPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public void Navigate()
    {
        driver.Navigate().GoToUrl("http://localhost:3000/login");
    }

    public void Login(string email, string password)
    {
        wait.Until(d => d.FindElement(By.XPath("//input[@type='email']"))).SendKeys(email);
        driver.FindElement(By.XPath("//input[@type='password']")).SendKeys(password);
        driver.FindElement(By.XPath("//button[@type='submit']")).Click();

        // Wait for successful redirect (assume heading contains 'Dashboard' or 'Customer')
        wait.Until(d => d.Url != "http://localhost:3000/login");
    }
}
