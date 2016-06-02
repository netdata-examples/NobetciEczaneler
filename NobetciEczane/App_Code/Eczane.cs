using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Eczane
/// </summary>
public class Eczane
{
    public static string MetaIl { get; set; }
    public static string MetaIlce { get; set; }

    public static string UrlSEO(string Text)
    {
        System.Globalization.CultureInfo cui = new System.Globalization.CultureInfo("en-US");

        string strReturn = System.Net.WebUtility.HtmlDecode(Text.Trim());
        strReturn = strReturn.Replace("ğ", "g");
        strReturn = strReturn.Replace("Ğ", "g");
        strReturn = strReturn.Replace("ü", "u");
        strReturn = strReturn.Replace("Ü", "u");
        strReturn = strReturn.Replace("ş", "s");
        strReturn = strReturn.Replace("Ş", "s");
        strReturn = strReturn.Replace("ı", "i");
        strReturn = strReturn.Replace("İ", "i");
        strReturn = strReturn.Replace("ö", "o");
        strReturn = strReturn.Replace("Ö", "o");
        strReturn = strReturn.Replace("ç", "c");
        strReturn = strReturn.Replace("Ç", "c");
        strReturn = strReturn.Replace(" - ", "+");
        strReturn = strReturn.Replace("-", "+");
        strReturn = strReturn.Replace(" ", "+");
        strReturn = strReturn.Trim();
        strReturn = new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9+]").Replace(strReturn, "");
        strReturn = strReturn.Trim();
        strReturn = strReturn.Replace("+", "-");
        return strReturn.ToLower(cui);
    }
}