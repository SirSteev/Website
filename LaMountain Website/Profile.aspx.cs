using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DdState.Items.Add("ALABAMA");
            DdState.Items.Add("ALASKA");
            DdState.Items.Add("ARIZONA");
            DdState.Items.Add("ARKANSAS");
            DdState.Items.Add("CALIFORNIA");
            DdState.Items.Add("COLORADO");
            DdState.Items.Add("CONNECTICUT");
            DdState.Items.Add("DELAWARE");
            DdState.Items.Add("FLORIDA");
            DdState.Items.Add("GEORGIA");
            DdState.Items.Add("HAWAII");
            DdState.Items.Add("IDAHO");
            DdState.Items.Add("ILLINOIS");
            DdState.Items.Add("INDIANA");
            DdState.Items.Add("IOWA");
            DdState.Items.Add("KANSAS");
            DdState.Items.Add("KENTUCKY");
            DdState.Items.Add("LOUISIANA");
            DdState.Items.Add("MAINE");
            DdState.Items.Add("MARYLAND");
            DdState.Items.Add("MASSACHUSETTS");
            DdState.Items.Add("MICHIGAN");
            DdState.Items.Add("MINNESOTA");
            DdState.Items.Add("MISSISSIPPI");
            DdState.Items.Add("MISSOURI");
            DdState.Items.Add("MONTANA");
            DdState.Items.Add("NEBRASKA");
            DdState.Items.Add("NEVADA");
            DdState.Items.Add("NEW HAMPSHIRE");
            DdState.Items.Add("NEW JERSEY");
            DdState.Items.Add("NEW MEXICO");
            DdState.Items.Add("NEW YORK");
            DdState.Items.Add("NORTH CAROLINA");
            DdState.Items.Add("NORTH DAKOTA");
            DdState.Items.Add("OHIO");
            DdState.Items.Add("OKLAHOMA");
            DdState.Items.Add("OREGON");
            DdState.Items.Add("PENNSYLVANIA");
            DdState.Items.Add("RHODE ISLAND");
            DdState.Items.Add("SOUTH CAROLINA");
            DdState.Items.Add("SOUTH DAKOTA");
            DdState.Items.Add("TENNESSEE");
            DdState.Items.Add("TEXAS");
            DdState.Items.Add("UTAH");
            DdState.Items.Add("VERMONT");
            DdState.Items.Add("VIRGINIA");
            DdState.Items.Add("WASHINGTON");
            DdState.Items.Add("WEST VIRGINIA");
            DdState.Items.Add("WISCONSIN");
            DdState.Items.Add("WYOMING");

            for (int i = 0; i < 200; i++)
            {
                int currentYear = DateTime.Now.Year - i;
                DdYear.Items.Add(currentYear.ToString());
            }

            DdMonth.Items.Add("January");
            DdMonth.Items.Add("February");
            DdMonth.Items.Add("March");
            DdMonth.Items.Add("April");
            DdMonth.Items.Add("May");
            DdMonth.Items.Add("June");
            DdMonth.Items.Add("July");
            DdMonth.Items.Add("August");
            DdMonth.Items.Add("September");
            DdMonth.Items.Add("October");
            DdMonth.Items.Add("November");
            DdMonth.Items.Add("December");

            if (Session["LoginID"] != null && Session["LoginName"] != null && Session["LoginEmail"] != null)
            {
                try
                {
                    FillProfile();
                }
                catch (Exception err)
                {
                    LbHeaderError.Text = err.Message;
                }
            }
            else
            {
                Response.Redirect("Auth/Logon.aspx");
            }
        }
    }

    private void FillProfile()
    {
        Person person = new Person();

        person.FillThisOnePerson(Session["LoginEmail"].ToString());

        TbNameFirst.Text = person.NameFirst;
        TbNameLast.Text = person.NameLast;
        TbZipCode.Text = person.ZipCode;
        TbEmail.Text = person.EMail;
        TbPhoneCell.Text = person.PhoneCell;
        DdState.SelectedValue = person.State;
        CalBirthday.TodaysDate = CalBirthday.SelectedDate = person.Birthday.Date;

        DdYear.SelectedValue = person.Birthday.Year.ToString();
        DdMonth.SelectedIndex = person.Birthday.Month - 1;

        Session["ProfileSalt"] = person.Salt;
        Session["ProfilePassword"] = person.Password;
    }

    protected void StateChange(object sender, EventArgs e)
    {
        //DdState.Items.Remove("");
    }

    protected void BirthYearChange(object sender, EventArgs e)
    {
        //DdYear.Items.Remove("");
        int selectedYear = Int32.Parse(DdYear.SelectedValue);
        //int currentYear = DateTime.Now.Year;
        DateTime newYear = new DateTime(selectedYear, CalBirthday.TodaysDate.Month, 1);
        CalBirthday.TodaysDate = CalBirthday.SelectedDate = newYear;
    }

    protected void BirthMonthChange(object sender, EventArgs e)
    {
        //DdMonth.Items.Remove("");
        int selectedMonth = DdMonth.SelectedIndex + 1;
        int currentMonth = DateTime.Now.Month;
        DateTime newMonth = new DateTime(CalBirthday.SelectedDate.Year, selectedMonth, 1);
        CalBirthday.TodaysDate = CalBirthday.SelectedDate = newMonth;
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        string feedBack = string.Empty;

        bool errorFlagInvalid = false;
        bool errorFlagMissing = false;
        bool errorFlagContainsBadWords = false;
        bool errorPasswordNoMatch = false;
        bool errorPasswordTooShort = false;
        bool errorInvalidPassword = false;

        if (TbNameFirst.Text.Length < 1 ||
            TbNameLast.Text.Length < 1 ||
            TbZipCode.Text.Length < 1 ||
            TbPhoneCell.Text.Length < 1 ||
            TbEmail.Text.Length < 1 ||
            TbPassword.Text.Length < 1)
        {
            errorFlagMissing = true;
        }

        if (!Validator.ValidEMail(TbEmail.Text) || TbZipCode.Text.Length != Validator.zipcodeLength)
        {
            errorFlagInvalid = true;
        }
        if (TbNewPassword.Text != TbConfirmNewPassword.Text)
        {
            errorPasswordNoMatch = true;
        }
        if (TbPassword.Text.Length < Validator.minPasswordLength || 
           (TbConfirmNewPassword.Text.Length > 0 && TbConfirmNewPassword.Text.Length < Validator.minPasswordLength) ||
           (TbNewPassword.Text.Length > 0 && TbNewPassword.Text.Length < Validator.minPasswordLength))
        {
            errorPasswordTooShort = true;
        }


        if (!(Validator.HashPassword(TbPassword.Text, Session["ProfileSalt"].ToString()) == Session["ProfilePassword"].ToString()))
        {
            errorInvalidPassword = true;
        }

        InformationCheck();

        if (errorFlagMissing)
        {
            feedBack += " Please fill in the required missing information.";
        }
        if (errorFlagContainsBadWords)
        {
            feedBack += " Please dont use dirty words in character name.";
        }
        if (errorFlagInvalid)
        {
            feedBack += " Please use a valid information.";
        }
        if (errorPasswordNoMatch)
        {
            feedBack += " Passwords do not Match.";
        }
        if (errorPasswordTooShort)
        {
            feedBack += " Password must be 8 or more characters.";
        }
        if (errorInvalidPassword)
        {
            feedBack += " The Current Password you entered was invalid.";
        }

        LbHeaderError.Text = feedBack;

        if (!errorFlagInvalid && !errorFlagMissing && !errorFlagContainsBadWords && !errorPasswordNoMatch && !errorPasswordTooShort && !errorInvalidPassword)
        {
            SubmitInformation();
            //Response.Redirect("accountlogin.aspx");
            //BtnSubmit.Enabled = false;
        }
    }

    private void InformationCheck()
    {
        if (TbNameFirst.Text.Length < 1)
        {
            LbNameFirst.Text = "First Name *";
        }
        else
        {
            LbNameFirst.Text = "First Name";
        }

        if (TbNameLast.Text.Length < 1)
        {
            LbNameLast.Text = "Last Name *";
        }
        else
        {
            LbNameLast.Text = "Last Name";
        }

        if (TbZipCode.Text.Length != Validator.zipcodeLength)
        {
            LbZip.Text = "Zip Code *";
        }
        else
        {
            LbZip.Text = "Zip Code";
        }

        if (TbPhoneCell.Text.Length != Validator.phoneNumberLength)
        {
            LbPhoneCell.Text = "Cell Phone *";
        }
        else
        {
            LbPhoneCell.Text = "Cell Phone";
        }

        if (!Validator.ValidEMail(TbEmail.Text))
        {
            LbEmail.Text = "E-Mail *";
        }
        else
        {
            LbEmail.Text = "E-Mail";
        }

        if (((TbNewPassword.Text.Length > 0 || TbConfirmNewPassword.Text.Length > 0) && (TbNewPassword.Text.Length < Validator.minPasswordLength || TbConfirmNewPassword.Text.Length < Validator.minPasswordLength)) ||
            TbNewPassword.Text != TbConfirmNewPassword.Text)
        {
            LbNewPassword.Text = "New Password *";
            LbConfirmNewPassword.Text = "Confirm New Password *";
        }
        else
        {
            LbNewPassword.Text = "New Password";
            LbConfirmNewPassword.Text = "Confirm New Password";
        }
    }

    private void SubmitInformation()
    {
        LbHeaderError.Text = "Information submition successful";

        //BtnSubmit.Enabled = false;

        Person newPerson = new Person();

        newPerson.Account_ID = Int32.Parse(Session["LoginID"].ToString());
        newPerson.NameFirst = TbNameFirst.Text;
        newPerson.NameLast = TbNameLast.Text;
        newPerson.ZipCode = TbZipCode.Text;
        newPerson.PhoneCell = TbPhoneCell.Text;
        newPerson.EMail = TbEmail.Text;

        if (TbNewPassword.Text.Length > 0)
        {
            newPerson.Salt = Validator.GetNewSalt();
            newPerson.Password = Validator.HashPassword(TbNewPassword.Text, newPerson.Salt);
            newPerson.UpdatePassword();
        }

        newPerson.State = DdState.SelectedValue;
        newPerson.Birthday = CalBirthday.SelectedDate;

        newPerson.UpdateAContact();

        Session["LoginName"] = null;
        Session["LoginEmail"] = null;
        Session["ProfileSalt"] = null;
        Session["ProfilePassword"] = null;

        Response.Redirect("default.aspx");
    }
}