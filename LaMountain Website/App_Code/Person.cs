using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;  // Library to connect to SQL DB's
using System.Data;        // Library to bring in a dataset

/// <summary>
/// Summary description for Person
/// </summary>
public class Person
{
    private const string connstring = @"####";

    private string nameFirst;
    private string nameLast;
    private string characterNameFirst;
    private string characterNameLast;
    private string zipCode;
    private string eMail;
    private string phoneCell;
    private string state;
    private DateTime birthday;
    private string password;
    private string salt;

    public Int32 Account_ID = 0;
    public Int32 AdminLevel = 0;

    public string NameFirst
    {
        get { return nameFirst; }
        set { nameFirst = value; }
    }

    public string NameLast
    {
        get { return nameLast; }
        set { nameLast = value; }
    }

    public string CharacterNameFirst
    {
        get { return characterNameFirst; }
        set { characterNameFirst = value; }
    }
    public string CharacterNameLast
    {
        get { return characterNameLast; }
        set { characterNameLast = value; }
    }
    public string ZipCode
    {
        get { return zipCode; }
        set { zipCode = value; }
    }
    public string EMail
    {
        get { return eMail; }
        set { eMail = value; }
    }
    public string PhoneCell
    {
        get { return phoneCell; }
        set { phoneCell = value; }
    }

    public string State
    {
        get { return state; }
        set { state = value; }
    }

    public DateTime Birthday
    {
        get { return birthday; }
        set { birthday = value; }
    }

    public string Password
    {
        get { return password; }
        set { password = value; }
    }

    public string Salt
    {
        get { return salt; }
        set { salt = value; }
    }

    public Person()
    {
        
    }

    public string AddContact()
    {
        string strFeedback = "";

        if (IsAccountByEmail(eMail))
        {
            strFeedback = "Account exists with that E-Mail";

            return strFeedback;
        }

        //Create SQL command string
        string strSQL = "INSERT INTO Accounts (NameFirst, NameLast, Password, Salt, ZipCode, EMail, PhoneCell, Birthday, State, AdminLevel) " +
                                    "VALUES (@NameFirst, @NameLast, @Password, @Salt, @ZipCode, @EMail, @PhoneCell, @Birthday, @State, @AdminLevel)";

        // Create a connection to DB
        SqlConnection conn = new SqlConnection();
        //Create the who, what where of the DB
        string strConn = @GetConnectionString();
        conn.ConnectionString = strConn;

        // Bark out our command
        SqlCommand comm = new SqlCommand();
        comm.CommandText = strSQL;  //Commander knows what to say
        comm.Connection = conn;     //Where's the phone?  Here it is

        //Fill in the paramters (Has to be created in same sequence as they are used in SQL Statement)
        comm.Parameters.AddWithValue("@NameFirst", nameFirst);
        comm.Parameters.AddWithValue("@NameLast", nameLast);
        comm.Parameters.AddWithValue("@Password", password);
        comm.Parameters.AddWithValue("@Salt", salt);
        comm.Parameters.AddWithValue("@ZipCode", zipCode);
        comm.Parameters.AddWithValue("@EMail", eMail);
        comm.Parameters.AddWithValue("@PhoneCell", phoneCell);
        comm.Parameters.Add("@Birthday", SqlDbType.DateTime).Value = birthday;
        comm.Parameters.AddWithValue("@State", state);
        comm.Parameters.AddWithValue("@AdminLevel", AdminLevel);

        //Open the connection
        conn.Open();

        //Bark command and Hangup
        comm.ExecuteNonQuery();
        conn.Close();            //Disconnect
        
        return strFeedback;
    }

    private bool IsAccountByEmail(string _eMail)
    {
        DataSet data = SearchContactsByEmail(_eMail, true);

        try
        {
            Account_ID = Int32.Parse(data.Tables[0].Rows[0]["Account_ID"].ToString());
            nameFirst = data.Tables[0].Rows[0]["NameFirst"].ToString();
            nameLast = data.Tables[0].Rows[0]["NameLast"].ToString();
            zipCode = data.Tables[0].Rows[0]["ZipCode"].ToString();
            eMail = data.Tables[0].Rows[0]["EMail"].ToString();
            phoneCell = data.Tables[0].Rows[0]["PhoneCell"].ToString();
            state = data.Tables[0].Rows[0]["State"].ToString();
            birthday = DateTime.Parse(data.Tables[0].Rows[0]["Birthday"].ToString());
            password = data.Tables[0].Rows[0]["Password"].ToString();
            salt = data.Tables[0].Rows[0]["Salt"].ToString();
            AdminLevel = Int32.Parse(data.Tables[0].Rows[0]["AdminLevel"].ToString());

            return true;
        }
        catch (Exception err)
        {
            return false;
        }
    }

    public DataSet SearchContactNames(String FName, String LName)
    {
        //Create a dataset to return filled
        DataSet ds = new DataSet();

        //Create a command for our SQL statement
        SqlCommand comm = new SqlCommand();

        //Write a Select Statement to perform Search
        String strSQL = "SELECT Account_ID, NameFirst, NameLast FROM Accounts WHERE 0=0";

        //If the First/Last Name is filled in include it as search criteria
        if (FName.Length > 0)
        {
            strSQL += " AND NameFirst LIKE @NameFirst";
            comm.Parameters.AddWithValue("@NameFirst", "%" + FName + "%");
        }
        if (LName.Length > 0)
        {
            strSQL += " AND NameLast LIKE @NameLast";
            comm.Parameters.AddWithValue("@NameLast", "%" + LName + "%");
        }

        //Create DB tools and Configure
        //*********************************************************************************************
        SqlConnection conn = new SqlConnection();
        //Create the who, what where of the DB
        string strConn = @GetConnectionString();
        conn.ConnectionString = strConn;

        //Fill in basic info to command object
        comm.Connection = conn;     //tell the commander what connection to use
        comm.CommandText = strSQL;  //tell the command what to say

        //Create Data Adapter
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = comm;    //commander needs a translator(dataAdapter) to speak with datasets

        //*********************************************************************************************

        //Get Data
        conn.Open();                //Open the connection (pick up the phone)
        da.Fill(ds, "Accounts");     //Fill the dataset with results from database and call it "Persons"
        conn.Close();               //Close the connection (hangs up phone)

        //Return the data
        return ds;
    }

    public DataSet SearchContacts(String FName, String LName)
    {
        //Create a dataset to return filled
        DataSet ds = new DataSet();
        
        //Create a command for our SQL statement
        SqlCommand comm = new SqlCommand();
        
        //Write a Select Statement to perform Search
        String strSQL = "SELECT Account_ID, NameFirst, NameLast, ZipCode, EMail, PhoneCell, State, Birthday FROM Accounts WHERE 0=0";

        //If the First/Last Name is filled in include it as search criteria
        if (FName.Length > 0)
        {
            strSQL += " AND NameFirst LIKE @NameFirst";
            comm.Parameters.AddWithValue("@NameFirst", "%" + FName + "%");
        }
        if (LName.Length > 0)
        {
            strSQL += " AND NameLast LIKE @NameLast";
            comm.Parameters.AddWithValue("@NameLast", "%" + LName + "%");
        }
        
        //Create DB tools and Configure
        //*********************************************************************************************
        SqlConnection conn = new SqlConnection();
        //Create the who, what where of the DB
        string strConn = @GetConnectionString();
        conn.ConnectionString = strConn;

        //Fill in basic info to command object
        comm.Connection = conn;     //tell the commander what connection to use
        comm.CommandText = strSQL;  //tell the command what to say

        //Create Data Adapter
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = comm;    //commander needs a translator(dataAdapter) to speak with datasets

        //*********************************************************************************************

        //Get Data
        conn.Open();                //Open the connection (pick up the phone)
        da.Fill(ds, "Accounts");     //Fill the dataset with results from database and call it "Persons"
        conn.Close();               //Close the connection (hangs up phone)
        
        //Return the data
        return ds;
    }

    public DataSet SearchContactsByEmail(string _eMail, bool isFullTable)
    {
        //Create a dataset to return filled
        DataSet ds = new DataSet();

        //Create a command for our SQL statement
        SqlCommand comm = new SqlCommand();

        //Write a Select Statement to perform Search
        String strSQL = "SELECT Account_ID, NameFirst, Password, Salt, AdminLevel FROM Accounts WHERE 0=0";
        if (isFullTable) strSQL = "SELECT Account_ID, NameFirst, NameLast, ZipCode, EMail, PhoneCell, State, Birthday, Password, Salt, AdminLevel FROM Accounts WHERE 0=0";

        //If the First/Last Name is filled in include it as search criteria
        if (_eMail.Length > 0)
        {
            strSQL += " AND EMail = @EMail";
            comm.Parameters.AddWithValue("@EMail", _eMail);
        }

        //Create DB tools and Configure
        //*********************************************************************************************
        SqlConnection conn = new SqlConnection();
        //Create the who, what where of the DB
        string strConn = @GetConnectionString();
        conn.ConnectionString = strConn;

        //Fill in basic info to command object
        comm.Connection = conn;     //tell the commander what connection to use
        comm.CommandText = strSQL;  //tell the command what to say

        //Create Data Adapter
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = comm;    //commander needs a translator(dataAdapter) to speak with datasets

        //*********************************************************************************************

        //Get Data
        conn.Open();                //Open the connection (pick up the phone)
        da.Fill(ds, "Accounts");     //Fill the dataset with results from database and call it "Persons"
        conn.Close();               //Close the connection (hangs up phone)

        //Return the data
        return ds;
    }

    public DataSet SearchContactsByID(Int32 _accountID, bool isFullTable)
    {
        //Create a dataset to return filled
        DataSet ds = new DataSet();

        //Create a command for our SQL statement
        SqlCommand comm = new SqlCommand();

        //Write a Select Statement to perform Search
        String strSQL = "SELECT Account_ID, NameFirst, Password, Salt, AdminLevel FROM Accounts WHERE 0=0";
        if (isFullTable) strSQL = "SELECT Account_ID, NameFirst, NameLast, ZipCode, EMail, PhoneCell, State, Birthday, Password, Salt, AdminLevel FROM Accounts WHERE 0=0";
        
        strSQL += " AND Account_ID = @Account_ID";
        comm.Parameters.AddWithValue("@Account_ID", _accountID);

        //Create DB tools and Configure
        //*********************************************************************************************
        SqlConnection conn = new SqlConnection();
        //Create the who, what where of the DB
        string strConn = @GetConnectionString();
        conn.ConnectionString = strConn;

        //Fill in basic info to command object
        comm.Connection = conn;     //tell the commander what connection to use
        comm.CommandText = strSQL;  //tell the command what to say

        //Create Data Adapter
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = comm;    //commander needs a translator(dataAdapter) to speak with datasets

        //*********************************************************************************************

        //Get Data
        conn.Open();                //Open the connection (pick up the phone)
        da.Fill(ds, "Accounts");     //Fill the dataset with results from database and call it "Persons"
        conn.Close();               //Close the connection (hangs up phone)

        //Return the data
        return ds;
    }

    //Method that returns a Data Reader filled with all the data
    // of one person.  This one person is specified by the ID passed
    // to this function
    public SqlDataReader FindOnePerson(int intAccount_ID)
    {
        //Create and Initialize the DB Tools we need
        SqlConnection conn = new SqlConnection();
        SqlCommand comm = new SqlCommand();

        //My Connection String
        string strConn = @GetConnectionString();

        //My SQL command string to pull up one person's data
        string sqlString =
       "SELECT Account_ID, NameFirst, NameLast, ZipCode, EMail, PhoneCell, State, Birthday, Password, Salt, AdminLevel FROM Accounts WHERE Account_ID = @Account_ID;";

        //Tell the connection object the who, what, where, how
        conn.ConnectionString = strConn;

        //Give the command object info it needs
        comm.Connection = conn;
        comm.CommandText = sqlString;
        comm.Parameters.AddWithValue("@Account_ID", intAccount_ID);

        //Open the DataBase Connection and Yell our SQL Command
        conn.Open();

        //Return some form of feedback
        return comm.ExecuteReader();   //Return the dataset to be used by others (the calling form)

    }

    public void FillThisOnePerson(string _Email)
    {
        DataSet data = SearchContactsByEmail(_Email, true);

        Account_ID = Int32.Parse(data.Tables[0].Rows[0]["Account_ID"].ToString());
        nameFirst = data.Tables[0].Rows[0]["NameFirst"].ToString();
        nameLast = data.Tables[0].Rows[0]["NameLast"].ToString();
        zipCode = data.Tables[0].Rows[0]["ZipCode"].ToString();
        eMail = data.Tables[0].Rows[0]["EMail"].ToString();
        phoneCell = data.Tables[0].Rows[0]["PhoneCell"].ToString();
        state = data.Tables[0].Rows[0]["State"].ToString();
        birthday = DateTime.Parse(data.Tables[0].Rows[0]["Birthday"].ToString());
        password = data.Tables[0].Rows[0]["Password"].ToString();
        salt = data.Tables[0].Rows[0]["Salt"].ToString();
        AdminLevel = Int32.Parse(data.Tables[0].Rows[0]["AdminLevel"].ToString());
    }

    public void FillThisOnePerson(Int32 _accountID)
    {
        DataSet data = SearchContactsByID(_accountID, true);

        Account_ID = Int32.Parse(data.Tables[0].Rows[0]["Account_ID"].ToString());
        nameFirst = data.Tables[0].Rows[0]["NameFirst"].ToString();
        nameLast = data.Tables[0].Rows[0]["NameLast"].ToString();
        zipCode = data.Tables[0].Rows[0]["ZipCode"].ToString();
        eMail = data.Tables[0].Rows[0]["EMail"].ToString();
        phoneCell = data.Tables[0].Rows[0]["PhoneCell"].ToString();
        state = data.Tables[0].Rows[0]["State"].ToString();
        birthday = DateTime.Parse(data.Tables[0].Rows[0]["Birthday"].ToString());
        password = data.Tables[0].Rows[0]["Password"].ToString();
        salt = data.Tables[0].Rows[0]["Salt"].ToString();
        AdminLevel = Int32.Parse(data.Tables[0].Rows[0]["AdminLevel"].ToString());
    }

    //Method that will delete one person record specified by the ID
    //It will return an Interger meant for feedback on how many 
    // records were deleted
    public Int32 DeleteOneAccount(int intAccount_ID)
    {
        Int32 intRecords = 0;

        //Create and Initialize the DB Tools we need
        SqlConnection conn = new SqlConnection();
        SqlCommand comm = new SqlCommand();

        //My Connection String
        string strConn = @GetConnectionString();

        //My SQL command string to pull up one person's data
        string sqlString =
       "DELETE FROM Accounts WHERE Account_ID = @Account_ID;";

        //Tell the connection object the who, what, where, how
        conn.ConnectionString = strConn;

        //Give the command object info it needs
        comm.Connection = conn;
        comm.CommandText = sqlString;
        comm.Parameters.AddWithValue("@Account_ID", intAccount_ID);

        //Open the DataBase Connection and Yell our SQL Command
        conn.Open();

        //Run the deletion and store the number of records effected
        intRecords = comm.ExecuteNonQuery();

        //close the connection
        conn.Close();

        return intRecords;   //Return # of records deleted

    }
    
    public Int32 UpdateAContact()
    {
        Int32 intRecords = 0;
        string feedback = "";

        //Create SQL command string
        string strSQL = "UPDATE Accounts " +
                        "SET NameFirst = @NameFirst, NameLast = @NameLast, ZipCode = @ZipCode, EMail = @EMail, PhoneCell = @PhoneCell, Birthday = @Birthday, State = @State, AdminLevel = @AdminLevel " +
                        "WHERE Account_ID = @Account_ID;";

        // Create a connection to DB
        SqlConnection conn = new SqlConnection();
        //Create the who, what where of the DB
        string strConn = @GetConnectionString();
        conn.ConnectionString = strConn;

        // Bark out our command
        SqlCommand comm = new SqlCommand();
        comm.CommandText = strSQL;  //Commander knows what to say
        comm.Connection = conn;     //Where's the phone?  Here it is

        //Fill in the paramters (Has to be created in same sequence as they are used in SQL Statement)
        comm.Parameters.AddWithValue("@NameFirst", nameFirst);
        comm.Parameters.AddWithValue("@NameLast", nameLast);
        comm.Parameters.AddWithValue("@ZipCode", zipCode);
        comm.Parameters.AddWithValue("@EMail", eMail);
        comm.Parameters.AddWithValue("@PhoneCell", phoneCell);
        comm.Parameters.AddWithValue("@Birthday", birthday);
        comm.Parameters.AddWithValue("@State", state);
        comm.Parameters.AddWithValue("@Account_ID", Account_ID);
        comm.Parameters.AddWithValue("@AdminLevel", AdminLevel);

        try
        {
            //Open the connection
            conn.Open();

            //Run the Update and store the number of records effected
            intRecords = comm.ExecuteNonQuery();
        }
        catch (Exception err)
        {
            feedback = err.Message;
        }
        finally
        {
            //close the connection
            conn.Close();
        }

        return intRecords;
    }

    public Int32 UpdatePassword()
    {
        Int32 intRecords = 0;

        //Create SQL command string
        string strSQL = "UPDATE Accounts " +
                        "SET Password = @Password, Salt = @Salt " +
                        "WHERE Account_ID = @Account_ID;";

        // Create a connection to DB
        SqlConnection conn = new SqlConnection();
        //Create the who, what where of the DB
        string strConn = @GetConnectionString();
        conn.ConnectionString = strConn;

        // Bark out our command
        SqlCommand comm = new SqlCommand();
        comm.CommandText = strSQL;  //Commander knows what to say
        comm.Connection = conn;     //Where's the phone?  Here it is

        //Fill in the paramters (Has to be created in same sequence as they are used in SQL Statement)
        comm.Parameters.AddWithValue("@Password", password);
        comm.Parameters.AddWithValue("@Salt", salt);
        comm.Parameters.AddWithValue("@Account_ID", Account_ID);

        try
        {
            //Open the connection
            conn.Open();

            //Run the Update and store the number of records effected
            intRecords = comm.ExecuteNonQuery();
        }
        catch (Exception err)
        {

        }
        finally
        {
            //close the connection
            conn.Close();
        }

        return intRecords;
    }

    public string GetConnectionString()
    {
        return connstring;
    }
}