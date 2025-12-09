
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Diagnostics;
using System.IO;

namespace AmazonAutomation.Reports
{
    public static class ReportManager
    {
        private static ExtentReports extent;
        private static ExtentSparkReporter sparkReporter;
        private static string reportPath;

        public static ExtentReports GetInstance()
        {
            if (extent == null)
            {
                var reportFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", DateTime.Now.ToString("yyyy-MM-dd"));
                Directory.CreateDirectory(reportFolder);

                reportPath = Path.Combine(reportFolder, "ExtentReport.html");
                sparkReporter = new ExtentSparkReporter(reportPath);
                sparkReporter.Config.DocumentTitle = "Amazon Automation Report";
                sparkReporter.Config.ReportName = "Buy Laptop Flow";

                extent = new ExtentReports();
                extent.AttachReporter(sparkReporter);
                extent.AddSystemInfo("Tester", "Tinu Tony");
                extent.AddSystemInfo("Framework", "NUnit");
            }
            return extent;
        }

        public static void FlushAndOpen()
        {
            extent?.Flush();
            if (!string.IsNullOrWhiteSpace(reportPath) && File.Exists(reportPath))
            {
                var psi = new ProcessStartInfo { FileName = reportPath, UseShellExecute = true };
                Process.Start(psi);
            }
        }
    }
}
