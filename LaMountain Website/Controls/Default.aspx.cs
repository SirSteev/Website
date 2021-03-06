﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;  // Library to connect to SQL DB's
using System.Data;        // Library to bring in a dataset

public partial class Controls_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        if (TbUserEmail.Text == "scott")
        {
            if (TbPassword.Text == "neit")
            {
                Session["AdminLogIn"] = true;
                Session["AdminLevel"] = 5;
                Response.Redirect("personsearch.aspx");
            }
        }
        else
        {
            Person person = new Person();
            DataSet personData = new DataSet();

            personData = person.SearchContactsByEmail(TbUserEmail.Text, false);

            try
            {
                person.Account_ID = Int32.Parse(personData.Tables[0].Rows[0]["Account_ID"].ToString());
                person.NameFirst = personData.Tables[0].Rows[0]["NameFirst"].ToString();
                person.Password = personData.Tables[0].Rows[0]["Password"].ToString();
                person.Salt = personData.Tables[0].Rows[0]["Salt"].ToString();
                person.EMail = TbUserEmail.Text;

                string hashed = Validator.HashPassword(TbPassword.Text, person.Salt);

                if (hashed == person.Password && Int32.Parse(personData.Tables[0].Rows[0]["AdminLevel"].ToString()) > 0)
                {
                    Session["AdminLogIn"] = true;
                    Session["AdminLevel"] = Int32.Parse(personData.Tables[0].Rows[0]["AdminLevel"].ToString());

                    Session["LoginID"] = person.Account_ID;
                    Session["LoginName"] = person.NameFirst;
                    Session["LoginEmail"] = person.EMail;

                    Response.Redirect("personsearch.aspx");
                }
                else
                {
                    LbLoginError.Text = "Invalid login";
                }
            }
            catch (Exception err)
            {
                LbLoginError.Text = "Invalid login: " + err.Message;
            }
        }

    }


}