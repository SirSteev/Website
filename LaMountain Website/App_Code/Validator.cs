using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;

/// <summary>
/// Summary description for Validator
/// </summary>
public class Validator
{
    public const int minPasswordLength = 8;
    public const int zipcodeLength = 5;
    public const int phoneNumberLength = 10;

    public static bool ContainsBadWords(string temp)
    {
        temp = temp.ToLower();

        bool result = false;

        if (temp.Contains("poop"))
        {
            result = true;
        }

        return result;
    }

    public static bool ValidEMail(string temp)
    {
        bool result = true;

        if (temp == "")
            return false;

        try
        {
            var eMailValidator = new System.Net.Mail.MailAddress(temp);
        }
        catch (FormatException ex)
        {
            return false;
        }

        return result;
    }

    public static string GetNewSalt()
    {
        byte[] salt = new byte[128 / 8];
        RNGCryptoServiceProvider.Create().GetBytes(salt);
        return Convert.ToBase64String(salt);
    }

    public static string HashPassword(string password, string salt)
    {
        byte[] bytePassword = new byte[password.Length + salt.Length];
        bytePassword = Encoding.ASCII.GetBytes(password + salt);

        return Convert.ToBase64String(HashAlgorithm.Create().ComputeHash(bytePassword));
    }
}