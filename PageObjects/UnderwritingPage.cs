using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAutoFramework.PageObjects
{
    class UnderwritingPage
    {
        //public static By NewAppMenu = By.XPath("//img[@class='rollover' and @alt='New App']");

        //public static By VehicleSubmenu = By.XPath("//img[@class='rollover' and @alt='New App']//following-sibling::ul/li[6]/a");

        //public static By AmountRequestedTextBox = By.Id("AmountRequested");

        //public static By LoanTerm_Txt = By.Id("LoanTerm");

        //public static By Purpose_Type_Ddn = By.Id("RequestType_RequestType");

        //public static By SSNTextBox = By.Id("sa_SSN");

        //public static By FirstNameTextBox = By.Id("sa_FName");

        //public static By CustomQuestionCheckBox = By.Id("CQuest_rpt_ctl01_SingleCustomQuestion_chkAnswer_0");

        public static By Custom_Question_Auto_Payment_Ddn = By.Id("CQuest_rpt_ctl03_SingleCustomQuestion_drpAnswer");

        public static By Custom_Question_Direct_Deposit_Ddn = By.Id("CQuest_rpt_ctl04_SingleCustomQuestion_drpAnswer");

        //public static By PullCreditButton = By.Id("runcredit_btnSavePullCredit");

        public static By Continue_Without_Approval_Lnk = By.Id("PreQualifications_lbtnContinueWithoutPreapproval");

        public static By View_Credit_Navigation_Lnk = By.Id("pre_lab_credit_linkViewBorrowerCredit");

        public static By Applicant_Info_Lnk = By.Id("pre_lab_lbtnShortApp");

        //public static By Credit_Score_ApplicantInfo = By.Id("pre_sb_ctl01_PrimaryCreditScore");

        public static By Qualified_Product_Accept_Btn = By.Id("products_rptPass_ctl00_dgRates_ctl02_btnAccept");

        public static By UnderWwriting_Tbl = By.Id("ufo_calc_tblBorrower");

        public static By View_Credit_SSN_txt = By.XPath("//*[text()='000-00-0001']");

        public static By Cross_Qualification_Summary_Lnk = By.Id("pre_lab_CrossQualifyButtonSummary1_lbtnCrossQALL");

        public static By PreQualified_Product_dialog_Frm = By.Name("dialog-frame");

        public static By BtnClosePreQualifiedProduct_dialog = By.Id("ctl00_Buttons_closeButton");

        public static By Revolving_Account_PreQualified_Product_Lbl = By.XPath("//table[@id='tblRevolving']/tbody/tr/td[1]");

        public static By Cross_Qualified_Products_Txt = By.Id("ctl00_bc_tdResult");

        public static By Not_Qualified_For_Other_Products_Txt = By.Id("ctl00_bc_lblError");

    }
}
