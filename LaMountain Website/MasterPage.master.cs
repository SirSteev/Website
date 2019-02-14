using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;  // Library to connect to SQL DB's
using System.Data;        // Library to bring in a dataset

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginID"] != null && Session["LoginName"] != null)
        {
            LbLoginWelcome.Text = "Hi, " + Session["LoginName"];
            BtnLogin.Text = "Logout";
        }
    }

    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        if (Session["LoginID"] == null || Session["LoginName"] == null)
        {
            Response.Redirect("/Auth/Logon.aspx");
        }
        else
        {
            LbLoginWelcome.Text = "Welcome";
            BtnLogin.Text = "Login";
            Session["LoginID"] = null;
            Session["LoginName"] = null;
        }
    }
}
