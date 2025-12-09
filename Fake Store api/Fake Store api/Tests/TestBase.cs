
using NUnit.Framework;
using AventStack.ExtentReports;
using AmazonAutomation.Reports;

namespace AmazonAutomation.Tests
{
    public abstract class TestBase
    {
        protected static ExtentReports Extent;
        protected AventStack.ExtentReports.ExtentTest Test;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            Extent = ReportManager.GetInstance();
        }

        [SetUp]
        public void SetUp()
        {
            var testName = TestContext.CurrentContext.Test.Name;
            var className = GetType().Name;
            Test = Extent.CreateTest($"{className} :: {testName}").AssignCategory(className);
        }

        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var message = TestContext.CurrentContext.Result.Message;

            if (status == NUnit.Framework.Interfaces.TestStatus.Passed) Test.Pass("Passed");
            else if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                if (string.IsNullOrWhiteSpace(message)) Test.Fail("Failed");
                else Test.Fail(message);
            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Skipped) Test.Skip("Skipped");
            else Test.Warning(status.ToString());
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            ReportManager.FlushAndOpen();
        }
    }
}
