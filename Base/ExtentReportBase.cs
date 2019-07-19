using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using MLAutoFramework.Config;
using MLAutoFramework.Helpers;
using System;
using System.Net.Mail;
using System.Text;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace MLAutoFramework.Base
{

    public class ExtentReportBase
    {
        public static ExtentReports extent;
        //public ExtentTest test;

        //Uncomment for sequential execution
        public static ExtentTest test;

        //Getting the Project Path
        public static string ProjectPath
        {
            get
            {
                string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                string actualPath = path.Substring(0, path.LastIndexOf("bin"));
                string projectPath = new Uri(actualPath).LocalPath;
                return projectPath;
            }
        }


        //Getting the Report Path
        public static string ReportName
        {
            get
            {
                string reportPath = ProjectPath + "_ExtentReport.html";
                return reportPath;
            }
        }


        //Generating Extent Report
        public void StartReport()
        {
            if (Settings.IEVersion == null)
            {
                ConfigReader.SetFrameworkSettings();
            }
            
            var htmlReporter = new ExtentHtmlReporter(ReportName);
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }


        //Flush extent report
        public void StopReportSequential(IWebDriver driver, string stepName, string scenarioTitle)
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace);
            var errorMessage = TestContext.CurrentContext.Result.Message;

            Status Status;
            switch (status)
            {
                case TestStatus.Failed:
                    Status = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    Status = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    Status = Status.Skip;
                    break;
                default:
                    Status = Status.Pass;
                    break;
            }

            if (status == TestStatus.Failed)
            {
                string screenShotPath = TakeScreenShot(driver);
                test.Log(Status.Info, "Valid APP number entered in search box");
                test.Log(Status, "Test Step **" + stepName + "** !!" + Status + "!! In Scenario **" + scenarioTitle + "** " + stacktrace + errorMessage);
                test.Log(Status, "Snapshot below: " + test.AddScreenCaptureFromPath(screenShotPath));
            }
            else
            {
                test.Log(Status, "Test scenario **" + scenarioTitle + "** ended with !!" + Status + "!!" + stacktrace + errorMessage);
            }

            //extent.RemoveTest(test);
            extent.Flush();
        }


        public void StopReportParallel(IWebDriver driver)
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var scenarioname = TestContext.CurrentContext.Test.Name;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace);
            var errorMessage = TestContext.CurrentContext.Result.Message;
            Status Status;

            switch (status)
            {
                case TestStatus.Failed:
                    Status = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    Status = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    Status = Status.Skip;
                    break;
                default:
                    Status = Status.Pass;
                    break;
            }

            if (status == TestStatus.Failed)
            {
                string screenShotPath = TakeScreenShot(driver);
                test.Log(Status, "Test Scenario **" + scenarioname + "** ended with !!" + Status + "!!" + stacktrace + errorMessage);
                test.Log(Status, "Snapshot below: " + test.AddScreenCaptureFromPath(screenShotPath));
            }
            else
            {
                test.Log(Status, "Test scenario **" + scenarioname + "** ended with !!" + Status + "!!" + stacktrace + errorMessage);
            }
            //extent.RemoveTest(test);
            //extent.Flush();
            //if (driver != null)
            //{
            //    driver.Close();
            //}
        }


        //Take screenshot
        public static string TakeScreenShot(IWebDriver driver)
        {
            try
            {
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                string stepName = TestContext.CurrentContext.Test.Name;
                string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                Console.WriteLine("Before");
                string finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + Settings.ScreenShotLocation + stepName + ".png";
                Console.WriteLine("After");
                string localpath = new Uri(finalpth).LocalPath;
                ss.SaveAsFile(localpath);
                Console.WriteLine("End");
                return localpath;
            }
            catch (Exception e)
            {
                LogHelper.LogException(e);
                throw;
            }
        }



        //Email extent report
        public static void EmailReport()
        {
            try
            {
                if (Settings.SendEmailReport)
                {
                    SmtpClient mailClient = new SmtpClient();
                    mailClient.Port = Settings.SMTPPort;
                    mailClient.Host = Settings.SMTPHost;
                    mailClient.EnableSsl = Settings.SMTPEnableSSL;
                    mailClient.Timeout = Settings.SMTPTimeout;
                    mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    mailClient.UseDefaultCredentials = false;
                    mailClient.Credentials = new System.Net.NetworkCredential(Settings.SMTPUserName, Settings.SMTPPassword);
                    MailMessage mail = new MailMessage(Settings.EmailFrom, Settings.EmailGroup);
                    mail.Subject = Settings.EmailSubject;
                    mail.Attachments.Add(new System.Net.Mail.Attachment(ReportName));
                    mail.Body = Settings.EmailBody;
                    mail.BodyEncoding = UTF8Encoding.UTF8;
                    mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    mailClient.Send(mail);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }
    }
}
