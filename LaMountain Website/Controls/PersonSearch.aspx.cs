using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;        // Library to bring in a dataset


public partial class Controls_Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void BtnSearchRecords_Click(object sender, EventArgs e)
    {
        Person temp = new Person();

        DataSet ds = temp.SearchContactNames(TbNameFirst.Text, TbNameLast.Text);

        GvPersons.DataSource = ds;
        GvPersons.DataMember = ds.Tables[0].TableName;
        GvPersons.DataBind();
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("contactmgr.aspx");
    }
}