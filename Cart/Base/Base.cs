using AventStack.ExtentReports;
using NUnit.Framework;
using CartAPI_Testing.Utils.Reporter;

namespace CartAPI_Testing.Tests
{
    public class BaseTest
    {
        protected ExtentReports extent;
        protected ExtentTest test;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            extent = ReportManager.GetInstance();
        }

        [SetUp]
        public void Setup()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var message = TestContext.CurrentContext.Result.Message;

            if (status.ToString() == "Failed")
            {
                test.Fail("Test Failed: " + message);
            }
            else
            {
                test.Pass("Test Passed");
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            extent.Flush();   
        }
    }
}
