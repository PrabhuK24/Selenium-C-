using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.IO;

namespace CartAPI_Testing.Utils.Reporter
{
    public static class ReportManager
    {
        private static ExtentReports? extent;
        private static ExtentSparkReporter? sparkReporter;

        public static ExtentReports GetInstance()
        {
            if (extent == null)
            {
          
                string projectRoot = @"C:\\Users\\prabhu.km\\source\\repos\\CartAPI_Testing\\CartAPI_Testing\\Reports\\";
                string reportFolder = Path.Combine(projectRoot, "Reports", DateTime.Now.ToString("yyyy-MM-dd"));
                Directory.CreateDirectory(reportFolder);

                string reportPath = Path.Combine(reportFolder, "ExtentReport.html");

               
                sparkReporter = new ExtentSparkReporter(reportPath);

                sparkReporter.Config.DocumentTitle = "HTTP Report";
                sparkReporter.Config.ReportName = "HTTP Report";
                
                extent = new ExtentReports();
                extent.AttachReporter(sparkReporter);
            }
            return extent;
        }
    }
}
