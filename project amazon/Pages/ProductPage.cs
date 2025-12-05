using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;


namespace Project_Amazon.Pages
{
    public class ProductPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private readonly By buyNowButton = By.Id("buy-now-button");
        private readonly By EmailField = By.Id("ap_email_login");
        private readonly By Continuebutton = By.ClassName("a-button-input");
        public ProductPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        public void ClickBuyNow()
        {
            var element = wait.Until(ExpectedConditions.ElementToBeClickable(buyNowButton));
            element.Click();
        }

        public void Email(string email)
        {
            var element = wait.Until(ExpectedConditions.ElementToBeClickable(EmailField));
            element.Click();
            element.SendKeys("sample@1234");

        }

        public void Continuefield()
        {
            var element = wait.Until(ExpectedConditions.ElementToBeClickable(Continuebutton));
            element.Click();

        }
    }
}
