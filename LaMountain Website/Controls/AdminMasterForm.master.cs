using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_AdminMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string pageName = this.Page.ToString().Substring(13, this.Page.ToString().Substring(13).Length - 5);

        if (pageName != "default") // makes sure your not on the Default page
        {
            if (Session["AdminLogIn"] != null && (bool)Session["AdminLogIn"] == true && Session["AdminLevel"] != null)
            {
                // Good to go
            }
            else
            {
                Response.Redirect("default.aspx");
            }
        }
    }

    protected void BtnLogout_Click(object sender, EventArgs e)
    {
        Session["AdminLogIn"] = false;
        Response.Redirect("default.aspx");
    }
}
