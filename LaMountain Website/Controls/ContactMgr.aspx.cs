using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;  // Library to connect to SQL DB's
using System.Data;        // Library to bring in a dataset

public partial class Controls_Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DdState.Items.Add("");
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

            DdYear.Items.Add("");
            for (int i = 0; i < 200; i++)
            {
                int currentYear = DateTime.Now.Year - i;
                DdYear.Items.Add(currentYear.ToString());
            }

            DdMonth.Items.Add("");
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

            CalBirthday.TodaysDate = CalBirthday.SelectedDate = DateTime.Now.Date;

            int currentAdminLevel = Int32.Parse(Session["AdminLevel"].ToString());

            for (int i = 0; i < currentAdminLevel; i++)
            {
                DdAdminLevel.Items.Add(i.ToString());
            }
            
        }

        string strAcc_ID = "";
        int intAcc_ID = 0;

        //Does Per_ID Exist?
        if ((!IsPostBack) && Request.QueryString["Acc_ID"] != null)
        {
            //If so...Gather Account ID and Fill Form
            strAcc_ID = Request.QueryString["Acc_ID"].ToString();
            //lblPerson_ID.Text = strPer_ID;

            intAcc_ID = Convert.ToInt32(strAcc_ID);

            //Fill the "temp" person's info based on ID
            Person temp = new Person();
            SqlDataReader dr = temp.FindOnePerson(intAcc_ID);

            Session["Account_ID"] = intAcc_ID;

            
            while (dr.Read())
            {
                TbNameFirst.Text = dr["NameFirst"].ToString();
                TbNameLast.Text = dr["NameLast"].ToString();
                TbZipCode.Text = dr["Zipcode"].ToString();
                TbEmail.Text = dr["EMail"].ToString();
                TbPhoneCell.Text = dr["PhoneCell"].ToString();
                DdState.SelectedValue = dr["State"].ToString();
                CalBirthday.TodaysDate = CalBirthday.SelectedDate = DateTime.Parse(dr["Birthday"].ToString());
                
                DdYear.SelectedValue = DateTime.Parse(dr["Birthday"].ToString()).Year.ToString();
                DdMonth.SelectedIndex = DateTime.Parse(dr["Birthday"].ToString()).Month;
                DdAdminLevel.SelectedValue = dr["AdminLevel"].ToString();

                if (Int32.Parse(dr["AdminLevel"].ToString()) >= Int32.Parse(Session["AdminLevel"].ToString()))
                {
                    BtnUpdateRecord.Enabled = false;
                    BtnDeleteRecord.Enabled = false;
                    DdAdminLevel.Enabled = false;
                }
                
            }
            TbPassword.Enabled = false;
            TbConfirmPassword.Enabled = false;
            BtnAddRecord.Enabled = false;
        }

        if (TbNameFirst.Text == "" || TbNameLast.Text == "")
        {
            BtnDeleteRecord.Enabled = false;
            BtnUpdateRecord.Enabled = false;

            CalBirthday.TodaysDate = CalBirthday.SelectedDate = DateTime.Now.Date;
        }

    }

    protected void StateChange(object sender, EventArgs e)
    {
        DdState.Items.Remove("");
    }

    protected void BirthYearChange(object sender, EventArgs e)
    {
        DdYear.Items.Remove("");
        int selectedYear = Int32.Parse(DdYear.SelectedValue);
        int currentYear = DateTime.Now.Year;
        DateTime newYear = new DateTime(selectedYear, CalBirthday.TodaysDate.Month, 1);
        CalBirthday.TodaysDate = CalBirthday.SelectedDate = newYear;
    }

    protected void BirthMonthChange(object sender, EventArgs e)
    {
        DdMonth.Items.Remove("");
        int selectedMonth = DdMonth.SelectedIndex + 1;
        int currentMonth = DateTime.Now.Month;
        DateTime newMonth = new DateTime(CalBirthday.TodaysDate.Year, selectedMonth, 1);
        CalBirthday.TodaysDate = CalBirthday.SelectedDate = newMonth;
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        string feedBack = string.Empty;

        bool errorFlagInvalid = false;
        bool errorFlagMissing = false;
        bool errorFlagContainsBadWords = false;

        if (TbNameFirst.Text.Length < 1 ||
            TbNameLast.Text.Length < 1 ||
            TbZipCode.Text.Length < 1 ||
            TbPhoneCell.Text.Length < 1 ||
            TbEmail.Text.Length < 1)
        {
            errorFlagMissing = true;
        }

        if (!Validator.ValidEMail(TbEmail.Text) || TbZipCode.Text.Length != Validator.zipcodeLength)
        {
            errorFlagInvalid = true;
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

        LbHeaderError.Text = feedBack;

        if (!errorFlagInvalid && !errorFlagMissing && !errorFlagContainsBadWords)
        {
            SubmitInformation();
        }
    }

    protected void BtnClearFields_Click(object sender, EventArgs e)
    {
        ClearFields();

        LbHeaderError.Text = "Fields Clear.";
    }

    private void ClearFields()
    {
        BtnUpdateRecord.Enabled = false;
        BtnDeleteRecord.Enabled = false;
        BtnAddRecord.Enabled = true;

        TbNameFirst.Text = "";
        TbNameLast.Text = "";
        TbZipCode.Text = "";
        TbEmail.Text = "";
        TbPhoneCell.Text = "";

        DdState.Items.Remove("");
        DdMonth.Items.Remove("");
        DdYear.Items.Remove("");

        DdState.Items.Insert(0, "");
        DdMonth.Items.Insert(0, "");
        DdYear.Items.Insert(0, "");

        DdState.SelectedValue = "";
        DdYear.SelectedValue = "";
        DdMonth.SelectedValue = "";
        CalBirthday.SelectedDate = CalBirthday.TodaysDate = DateTime.Now.Date;

        LbNameFirst.Text = "First Name";
        LbNameLast.Text = "Last Name";
        LbZip.Text = "Zip Code";
        LbEmail.Text = "E-Mail";

        Session["Account_ID"] = null;

        TbPassword.Enabled = true;
        TbConfirmPassword.Enabled = true;

        DdAdminLevel.Enabled = true;
        DdAdminLevel.SelectedIndex = 0;

        //BtnUpdateRecord.Enabled = true;
    }

    protected void BtnSearchRecord_Click(object sender, EventArgs e)
    {
        Response.Redirect("personsearch.aspx");

        //LbHeaderError.Text = "";
        //
        //Person person = new Person();
        //DataSet personData = new DataSet();
        //
        //if (TbNameFirst.Text == "" || TbNameLast.Text == "")
        //{
        //    LbHeaderError.Text = "Please search using First Name AND Last Name fields";
        //
        //    if (TbNameFirst.Text == "")
        //    {
        //        LbNameFirst.Text = "First Name *";
        //    }
        //    else
        //    {
        //        LbNameFirst.Text = "First Name";
        //    }
        //
        //    if (TbNameLast.Text == "")
        //    {
        //        LbNameLast.Text = "Last Name *";
        //    }
        //    else
        //    {
        //        LbNameLast.Text = "Last Name";
        //    }
        //}
        //else
        //{
        //    LbNameFirst.Text = "First Name";
        //    LbNameLast.Text = "Last Name";
        //
        //    BtnUpdateRecord.Enabled = true;
        //    BtnDeleteRecord.Enabled = true;
        //    BtnAddRecord.Enabled = false;
        //    
        //    personData = person.SearchContacts(TbNameFirst.Text, TbNameLast.Text);
        //
        //    try
        //    {
        //        person.NameFirst = personData.Tables[0].Rows[0]["NameFirst"].ToString();
        //        person.NameLast = personData.Tables[0].Rows[0]["NameLast"].ToString();
        //        person.Account_ID = Int32.Parse(personData.Tables[0].Rows[0]["Account_ID"].ToString());
        //        person.ZipCode = personData.Tables[0].Rows[0]["ZipCode"].ToString();
        //        person.EMail = personData.Tables[0].Rows[0]["EMail"].ToString();
        //        person.PhoneCell = personData.Tables[0].Rows[0]["PhoneCell"].ToString();
        //        person.State = personData.Tables[0].Rows[0]["State"].ToString();
        //        person.Birthday = DateTime.Parse(personData.Tables[0].Rows[0]["Birthday"].ToString());
        //
        //        Session["Account_ID"] = person.Account_ID;
        //
        //        TbNameFirst.Text = person.NameFirst;
        //        TbNameLast.Text = person.NameLast;
        //        TbZipCode.Text = person.ZipCode;
        //        TbEmail.Text = person.EMail;
        //        TbPhoneCell.Text = person.PhoneCell;
        //        DdState.SelectedValue = person.State;
        //        CalBirthday.TodaysDate = CalBirthday.SelectedDate = person.Birthday.Date;
        //
        //        DdYear.SelectedValue = person.Birthday.Year.ToString();
        //        DdMonth.SelectedIndex = person.Birthday.Month;
        //
        //        TbPassword.Enabled = false;
        //        TbConfirmPassword.Enabled = false;
        //    }
        //    catch
        //    {
        //        LbHeaderError.Text = "No Record Found.";
        //        ClearFields();
        //        BtnUpdateRecord.Enabled = false;
        //        BtnDeleteRecord.Enabled = false;
        //    }
        //    
        //}
    }

    protected void BtnUpdateRecord_Click(object sender, EventArgs e)
    {
        Person personToUpdate = new Person();

        personToUpdate.FillThisOnePerson((Int32)Session["Account_ID"]);

        if (personToUpdate.AdminLevel >= (Int32)Session["AdminLevel"])
        {
            LbHeaderError.Text = "Wow.... Really?";
            return;
        }

        string feedBack = string.Empty;

        bool errorFlagInvalid = false;
        bool errorFlagMissing = false;

        if (TbNameFirst.Text.Length < 1 ||
            TbNameLast.Text.Length < 1 ||
            TbZipCode.Text.Length < 1 ||
            TbEmail.Text.Length < 1)
        {
            errorFlagMissing = true;
        }

        if (!Validator.ValidEMail(TbEmail.Text) || TbZipCode.Text.Length != Validator.zipcodeLength)
        {
            errorFlagInvalid = true;
        }

        InformationCheck();

        if (errorFlagMissing)
        {
            feedBack += " Please fill in the required missing information.";
        }

        if (errorFlagInvalid)
        {
            feedBack += " Please use a valid information.";
        }

        LbHeaderError.Text = feedBack;

        if (!errorFlagInvalid && !errorFlagMissing)
        {
            personToUpdate.NameFirst = TbNameFirst.Text;
            personToUpdate.NameLast = TbNameLast.Text;
            personToUpdate.ZipCode = TbZipCode.Text;
            personToUpdate.EMail = TbEmail.Text;
            personToUpdate.PhoneCell = TbPhoneCell.Text;
            personToUpdate.Birthday = CalBirthday.SelectedDate;
            personToUpdate.State = DdState.SelectedValue;
            personToUpdate.Account_ID = (Int32)Session["Account_ID"];
            personToUpdate.AdminLevel = Int32.Parse(DdAdminLevel.SelectedValue);

            personToUpdate.UpdateAContact();

            LbHeaderError.Text = "Record has been updated.";
        }
        
    }

    protected void BtnDeleteRecord_Click(object sender, EventArgs e)
    {
        Person playerToDelete = new Person();

        playerToDelete.FillThisOnePerson((Int32)Session["Account_ID"]);

        if (playerToDelete.AdminLevel >= (Int32)Session["AdminLevel"])
        {
            LbHeaderError.Text = "Wow.... Really?";
            return;
        }

        BtnUpdateRecord.Enabled = false;
        BtnDeleteRecord.Enabled = false;

        TbNameFirst.Text = "";
        TbNameLast.Text = "";
        TbZipCode.Text = "";
        TbEmail.Text = "";
        TbPhoneCell.Text = "";

        DdState.Items.Remove("");
        DdMonth.Items.Remove("");
        DdYear.Items.Remove("");

        DdState.Items.Insert(0, "");
        DdMonth.Items.Insert(0, "");
        DdYear.Items.Insert(0, "");

        DdState.SelectedValue = "";
        DdYear.SelectedValue = "";
        DdMonth.SelectedValue = "";
        CalBirthday.TodaysDate = DateTime.Now;

        LbHeaderError.Text = "Record Deleted.";
        
        playerToDelete.DeleteOneAccount((Int32)Session["Account_ID"]);

        Session["Account_ID"] = null;

        TbPassword.Enabled = true;
        TbConfirmPassword.Enabled = true;
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
    }

    private void SubmitInformation()
    {
        BtnUpdateRecord.Enabled = false;

        Person newPerson = new Person();

        newPerson.NameFirst = TbNameFirst.Text;
        newPerson.NameLast = TbNameLast.Text;
        newPerson.ZipCode = TbZipCode.Text;
        newPerson.PhoneCell = TbPhoneCell.Text;
        newPerson.EMail = TbEmail.Text;

        newPerson.Salt = Validator.GetNewSalt();
        newPerson.Password = Validator.HashPassword(TbPassword.Text, newPerson.Salt);

        newPerson.State = DdState.SelectedValue;
        newPerson.Birthday = CalBirthday.SelectedDate;

        newPerson.AdminLevel = Int32.Parse(DdAdminLevel.SelectedValue);

        string feedback = newPerson.AddContact();

        if (feedback == "")
        {
            LbHeaderError.Text = "Information submition successful.";
        }
        else
        {
            LbHeaderError.Text = feedback;
        }
        
    }
}
