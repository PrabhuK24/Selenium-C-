
using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Project_Amazon.Drivers;
using Project_Amazon.Pages;
using Project_Amazon.Reports;
using SeleniumExtras.WaitHelpers;
using System;
using System.IO;
using System.Linq;

namespace Project_Amazon.Tests
{
    public class AmazonTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private HomePage homePage;
        private SearchResultsPage searchResultsPage;
        private ProductPage productPage;
        private ExtentReports extent;
        private ExtentTest test;
        private string screenshotFolder;

        [OneTimeSetUp]
        public void InitReport()
        {
            extent = ReportManager.GetInstance();

            screenshotFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", DateTime.Now.ToString("yyyy-MM-dd"), "Screenshots");
            Directory.CreateDirectory(screenshotFolder);
        }

        [SetUp]
        public void Setup()
        {
            driver = DriverFactory.CreateDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            homePage = new HomePage(driver);
            searchResultsPage = new SearchResultsPage(driver);
            productPage = new ProductPage(driver);
        }

        [Test]
        public void BuyIphoneFlow()
        {
            test = extent.CreateTest("Buy Iphone Flow");

            try
            {
                homePage.GoToAmazon();
                test.Info("Navigated to Amazon");
                Thread.Sleep(2000);

                homePage.SearchProduct("Iphone");
                test.Info("Searched for Iphone");
                Thread.Sleep(2000);

                searchResultsPage.ClickFirstProduct();
                test.Info("Clicked first product");
                Thread.Sleep(2000);

                var handles = driver.WindowHandles;
                if (handles.Count > 1)
                {
                    driver.SwitchTo().Window(handles.Last());
                    test.Info("Switched to product tab");
                }

                productPage.ClickBuyNow();
                test.Info("Clicked Buy Now");
                

                Assert.That(driver.Url, Does.Contain("buy-now").IgnoreCase);

                productPage.Email("sample@1234");
                test.Info("Enter email address");
                Thread.Sleep(2000);

                productPage.Continuefield();
                test.Info("Clicked the continue Button");
                Thread.Sleep(2000);

                string successScreenshot = CaptureScreenshot("BuyLaptopFlow_Success");
                test.Pass("Buy Laptop Flow completed successfully")
                    .AddScreenCaptureFromPath(successScreenshot);
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("BuyLaptopFlow_Failure");
                test.Fail($"Test failed: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }

           
        }

        private string CaptureScreenshot(string testName)
        {
            string screenshotPath = Path.Combine(screenshotFolder, $"{testName}_{DateTime.Now:HHmmss}.png");
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(screenshotPath);
            return screenshotPath;
        }

        [TearDown]
        public void Cleanup()
        {
            driver?.Quit();
            driver?.Dispose();
        }


        [OneTimeTearDown]
        public void CloseReport()
        {
            extent.Flush();

            string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", DateTime.Now.ToString("yyyy-MM-dd"), "ExtentReport.html");
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = reportPath,
                UseShellExecute = true
            });
        }

    }

}
