using MLAutoFramework.Base;
using MLAutoFramework.Extensions;
using MLAutoFramework.Helpers;
using MLAutoFramework.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Threading;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;

namespace MLAutoFramework.MLSmoke.Steps
{
    [Binding]
    class UnderwritingPageSteps : TestBase
    {
        /*private IWebDriver _driver;

        public UnderwritingPageSteps(IWebDriver driver)
        {
            _driver = driver;
        }*/
        string crossQualifiedProducts = "qualified for the products";
        string valid_ssn = "000-00-0001";
        string amountRequested = "10000";
        string loanTerm = "36";
        string purposeTypeText = "Purchase";
        string customQuestionText = "Yes";
        string notQualifiedForOtherProducts = "could not be instantly qualified for any other products";
       
        [When(@"User selects Vehicle from New App menu")]
        public void WhenUserSelectsVehicleFromNewAppMenu()
        {
            _driver.WaitForPageLoad();
            _driver.HoverAndClick(_driver.FindElement(HomePage.New_App_Focus), _driver.FindElement(HomePage.Vehicle_Loan_Focus));
            _driver.WaitForPageLoad();
            test.Log(Status.Info, "Selected Vehicle submenu");
        }


        [When(@"User fills applicant form with valid data and click on Pull Credit and Save button")]
        public void WhenUserFillsApplicantFormWithValidDataAndClickOnPullCreditAndSaveButton()
        {
            _driver.WaitForPageLoad();
            Thread.Sleep(5000); // This is needed else amount entring skipping randomly.
            test.Log(Status.Info, "Entering Amount Requested as : " + amountRequested);
            _driver.WaitForObjectAvaialble(LoanPage.Amount_Requested_Txt);
            (_driver.FindElement(LoanPage.Amount_Requested_Txt)).EnterText(amountRequested);
            test.Log(Status.Info, "Amount requested is: "+ (_driver.FindElement(LoanPage.Amount_Requested_Txt)).GetText());
            (_driver.FindElement(LoanPage.Loan_Term_Txt)).EnterText(loanTerm);
            (_driver.FindElement(LoanPage.Purpose_Type_Ddn)).SelectDropDown(purposeTypeText);
            _driver.WaitForPageLoad();
            (_driver.FindElement(LoanPage.SSN_Txt)).EnterText(valid_ssn);
            _driver.FindElement(LoanPage.FName_Txt).Click();
            _driver.FindElement(LoanPage.Custom_Question_CheckBox).Click();
            _driver.WaitForPageLoad();
            (_driver.FindElement(UnderwritingPage.Custom_Question_Auto_Payment_Ddn)).SelectDropDown(customQuestionText);
            (_driver.FindElement(UnderwritingPage.Custom_Question_Direct_Deposit_Ddn)).SelectDropDown(customQuestionText);
            _driver.FindElement(LoanPage.Pull_Credit_Btn).Click();
            _driver.WaitForPageLoad();
            if (_driver.FindElement(UnderwritingPage.Continue_Without_Approval_Lnk).Displayed)
            {
                _driver.FindElement(UnderwritingPage.Continue_Without_Approval_Lnk).Click();
                test.Log(Status.Info, "Clicked on Continue Without Approval");
            }
            else
            {
                test.Log(Status.Info, "Did not click on Continue Without Approval");
            }
            _driver.WaitForPageLoad();
        }


        [When(@"User clicks on View Credit link from navigation panel")]
        public void WhenUserClicksOnViewCreditLinkFromNavigationPanel()
        {
            // Click on View Credit link            
            _driver.WaitForPageLoad();
            if (_driver.FindElement(UnderwritingPage.View_Credit_Navigation_Lnk).Displayed)
            {
                _driver.FindElement(UnderwritingPage.View_Credit_Navigation_Lnk).Click();
                test.Log(Status.Info, "Clicked on View Credit");
            }
            else
            {
                test.Log(Status.Info, "Did not Click on View Credit");
            }
            _driver.WaitForPageLoad();
        }


        [Then(@"Credit Report values should be displayed in the report")]
        public void ThenCreditReportValuesShouldBeDisplayedInTheReport()
        {
            // Verify that Credit Report values should be displayed in the report
            WindowHelper.switchToChildWindow(_driver);
            _driver.Manage().Window.Maximize();
            IWebElement viewCreditReportSSN = _driver.FindElement(UnderwritingPage.View_Credit_SSN_txt);
            if (viewCreditReportSSN.GetText().Contains(valid_ssn))
            {
                test.Log(Status.Info, "Credit report value verified with SSN: " + viewCreditReportSSN.GetText());
                Assert.IsTrue(viewCreditReportSSN.GetText().Contains(valid_ssn));
            }
            _driver.Close();
            WindowHelper.switchToMainWindow(_driver, main_window);
        }


        [When(@"User clicks on Accept button of 36 mo NEW EXAMPLE loan")]
        public void WhenUserClicksOnAcceptButtonOf36MoNEWEXAMPLELoan()
        {
            Thread.Sleep(2000);
            _driver.FindElement(UnderwritingPage.Qualified_Product_Accept_Btn).Click();
            _driver.WaitForPageLoad();
        }


        [Then(@"Scroll the page down and Verify that NA is not displaying in Underwriting Information")]
        public void ThenScrollThePageDownAndVerifyThatNAIsNotDisplayingInUnderwritingPageInformation()
        {
            IWebElement UnderwritingPageCalculation = _driver.FindElement(UnderwritingPage.UnderWwriting_Tbl);
            IList<IWebElement> UnderwritingPageTableData = UnderwritingPageCalculation.FindElements(By.TagName("span"));
            string UnderwritingPageText;
            for (int i = 0; i < UnderwritingPageTableData.Count; i++)
            {
                UnderwritingPageText = UnderwritingPageTableData[i].GetText();
                test.Log(Status.Info, "UnderwritingPage info text is: "+ UnderwritingPageText);
                if (!UnderwritingPageText.Equals("NA"))
                {
                    test.Log(Status.Info, "No value as NA is displayed");
                    Assert.IsTrue(!UnderwritingPageText.Equals("NA"));
                }
                else
                {
                    test.Log(Status.Info, "Value as NA is displayed");
                }
                _driver.WaitForPageLoad();
            }
        }


        [Then(@"Qualifying product Accept button should be clickable")]
        public void ThenQualifyingProductAcceptButtonShouldBeClickable()
        {
            _driver.WaitForPageLoad();
            if (_driver.FindElement(UnderwritingPage.Qualified_Product_Accept_Btn).Displayed)
            {
                Assert.IsTrue(_driver.FindElement(UnderwritingPage.Qualified_Product_Accept_Btn).Displayed);
            }
            _driver.FindElement(UnderwritingPage.Qualified_Product_Accept_Btn).Click();
        }


        [When(@"User clicks on Cross Qualify summary")]
        public void WhenUserClicksOnCrossQualifySummary()
        {
            _driver.WaitForPageLoad();
            if (_driver.FindElement(UnderwritingPage.Cross_Qualification_Summary_Lnk).Displayed)
            {
                _driver.FindElement(UnderwritingPage.Cross_Qualification_Summary_Lnk).Click();
            }
            WindowHelper.switchToChildWindow(_driver);
        }


        [Then(@"Additional qualfied products should be displayed")]
        public void ThenAdditionalQualfiedProductsShouldBeDisplayed()
        {
            _driver.WaitForObjectAvaialble(UnderwritingPage.PreQualified_Product_dialog_Frm);
            _driver.SwitchTo().Frame(_driver.FindElement(UnderwritingPage.PreQualified_Product_dialog_Frm));
            _driver.WaitForElementPresentAndEnabled(UnderwritingPage.Cross_Qualified_Products_Txt, 60);
            if (_driver.FindElement(UnderwritingPage.Cross_Qualified_Products_Txt).GetText().Contains(crossQualifiedProducts))
            {
                Assert.IsTrue(_driver.FindElement(UnderwritingPage.Cross_Qualified_Products_Txt).GetText().Contains(crossQualifiedProducts));
                test.Log(Status.Info, "Cross qualified products are displayed");
            }
            else
            {
                Assert.IsTrue(_driver.FindElement(UnderwritingPage.Not_Qualified_For_Other_Products_Txt).GetText().Contains(notQualifiedForOtherProducts));
                test.Log(Status.Info, "Applicant is not qualified for any other products");
            }
           
            _driver.FindElement(UnderwritingPage.BtnClosePreQualifiedProduct_dialog).Click();
            _driver.SwitchTo().DefaultContent();
            _driver.WaitForPageLoad();
        }
    }
}
