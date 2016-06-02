using Microsoft.AspNet.FriendlyUrls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class eczaneler : System.Web.UI.Page
{
    public static string XmlAccessObject = "Enter Xml Access Object Here";

    protected void Page_Load(object sender, EventArgs e)
    {
        tarihBaslik.InnerText = DateTime.Now.ToLongDateString();
        IList<string> segmentler = Request.GetFriendlyUrlSegments();

        if (segmentler.Count > 0)
        {
            string tmp = segmentler[0].ToString().Replace("-nobetci-eczaneleri", "");
            string il = "";
            string ilce = "";

            if (tmp.Split('-').Count() == 2)
            {
                il = tmp.Split('-')[0];
                ilce = tmp.Split('-')[1];
            }
            else if (tmp.Split('-').Count() == 1)
            {
                il = tmp.Split('-')[0];
            }

            DescriptionTanimla(il, ilce);
            EczaneAraVeYaz(il, ilce);
        }
        else
        {
            string il = "istanbul";
            string ilce = "";

            DescriptionTanimla(il, ilce);
            EczaneAraVeYaz(il, ilce);
        }
    }

    [WebMethod]
    public static string IlleriYukle()
    {
        dynamic sonuc = new ExpandoObject();
        try
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            DataSet ds = new DataSet();
            ds.ReadXml(new XmlTextReader(XmlAccessObject));
            DataView dv = new DataView(ds.Tables[0]);
            DataTable dt = dv.ToTable(true, new string[] { "dc_Il_Plaka_Kodu", "dc_Il" });

            sb.Append("<option value=0>İl Seçiniz...</option>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string il = dt.Rows[i]["dc_Il"].ToString();
                sb.Append("<option>" + il + "</option>");

                if (i % 2 == 0)
                    sb2.Append("<div class='row form-group'><div class='col-xs-6'><a href='/eczaneler/" + Eczane.UrlSEO(il + " Nöbetçi Eczaneleri") + "'>" + il + " Nöbetçi Eczaneleri</a></div>");
                else
                    sb2.Append("<div class='col-xs-6'><a href='/eczaneler/" + Eczane.UrlSEO(il + " Nöbetçi Eczaneleri") + "'>" + il + " Nöbetçi Eczaneleri</a></div></div>");

            }
            sonuc.Hata = "";
            sonuc.SagTarafIller = sb2.ToString();
            sonuc.Iller = sb.ToString();
        }
        catch (Exception ex)
        {
            sonuc.Hata = ex.Message;
        }
        return Newtonsoft.Json.JsonConvert.SerializeObject(sonuc);
    }

    [WebMethod]
    public static string AramaUrlDon(string il, string ilce)
    {
        if (il != "0" && ilce != "0")
        {
            return "/eczaneler/" + Eczane.UrlSEO(il + "-" + ilce + " Nöbetçi Eczaneleri");
        }
        else if (il != "0")
        {
            return "/eczaneler/" + Eczane.UrlSEO(il + " Nöbetçi Eczaneleri"); ;
        }
        else
        {
            return "0";
        }
    }

    [WebMethod]
    public static string IlceleriYukle(string il)
    {
        dynamic sonuc = new ExpandoObject();
        try
        {
            StringBuilder sb = new StringBuilder();
            DataSet ds = new DataSet();
            ds.ReadXml(new XmlTextReader(XmlAccessObject + "?$where=dc_Il=" + il));
            DataView dv = new DataView(ds.Tables[0]);
            dv.Sort = "dc_Ilce ASC";
            DataTable dt = dv.ToTable(true, "dc_Ilce");
            sb.Append("<option value=0>İlçe Seçiniz...</option>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("<option>" + dt.Rows[i]["dc_Ilce"].ToString() + "</option>");
            }
            sonuc.Hata = "";
            sonuc.Ilceler = sb.ToString();
        }
        catch (Exception ex)
        {
            sonuc.Hata = ex.Message;
        }
        return Newtonsoft.Json.JsonConvert.SerializeObject(sonuc);
    }

    public void EczaneAraVeYaz(string il, string ilce)
    {
        StringBuilder sb = new StringBuilder();
        DataSet ds = new DataSet();
        try
        {
            if ((string.IsNullOrWhiteSpace(il) && string.IsNullOrWhiteSpace(ilce))
                || (string.IsNullOrWhiteSpace(il) && !string.IsNullOrWhiteSpace(ilce)))
            {
                ds.ReadXml(new XmlTextReader(XmlAccessObject + "?$where=dc_Il=İstanbul"));
            }
            else if (!string.IsNullOrWhiteSpace(il) && !string.IsNullOrWhiteSpace(ilce))
            {
                ds.ReadXml(new XmlTextReader(XmlAccessObject + "?$where=dc_Il_Kucuk_Harf=" + il + "[and]dc_Ilce_Kucuk_Harf=" + ilce));
            }
            else if (!string.IsNullOrWhiteSpace(il) && string.IsNullOrWhiteSpace(ilce))
            {
                ds.ReadXml(new XmlTextReader(XmlAccessObject + "?$where=dc_Il_Kucuk_Harf=" + il));
            }

            try
            {
                if (ds.Tables[0].Rows[0]["Exception"].ToString() != "")
                {
                    HataMesajGoster("Kayıt bulunamadı!");
                    return;
                }
            }
            catch (Exception)
            { }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string dsIl = ds.Tables[0].Rows[i]["dc_Il"].ToString();
                string dsIlce = ds.Tables[0].Rows[i]["dc_Ilce"].ToString();
                string eczaneAdi = ds.Tables[0].Rows[i]["dc_Eczane_Adi"].ToString();
                string telefon = ds.Tables[0].Rows[i]["dc_Telefon"].ToString();
                string adres = ds.Tables[0].Rows[i]["dc_Adres"].ToString();
                string adresTarifi = ds.Tables[0].Rows[i]["dc_Adres_Aciklama"].ToString();
                string aciklama = ds.Tables[0].Rows[i]["dc_Ek_Bilgi"].ToString();
                string enlem = ds.Tables[0].Rows[i]["dc_Enlem"].ToString();
                string boylam = ds.Tables[0].Rows[i]["dc_Boylam"].ToString();
                string plaka = ds.Tables[0].Rows[i]["dc_Il_Plaka_Kodu"].ToString();

                sb.Append(@"<div class='col-xs-12 col-sm-8 col-sm-offset-2'>
                                <div class='panel panel-danger'>
                                    <div class='panel-heading'>
                                        <label>" + eczaneAdi + @"</label>
                                    </div>
                                    <div class='panel-body'>
                                        <div class='row'>");
                sb.Append("<div class='col-xs-12 form-group'><b>İl : </b>" + dsIl + "</div>");
                sb.Append("<div class='col-xs-12 form-group'><b>İlçe : </b>" + dsIlce + "</div>");
                if (adres != "")
                {
                    sb.Append("<div class='col-xs-12 form-group'><b>Adres : </b>" + adres + "</div>");
                }
                if (adresTarifi != "")
                {
                    sb.Append("<div class='col-xs-12 form-group'><b>Adres Tarifi : </b>" + adresTarifi + "</div>");
                }
                if (telefon != "")
                {
                    sb.Append("<div class='col-xs-12 form-group'><b>Telefon : </b>" + telefon + "</div>");
                }
                if (aciklama != "")
                {
                    sb.Append("<div class='col-xs-12 form-group'><b>Açıklama : </b>" + aciklama + "</div>");
                }
                if (enlem != "" && boylam != "")
                {
                    string url = "https://www.google.com.tr/maps/@" + enlem + "," + boylam + ",20z";
                    sb.Append("<div class='col-xs-12 form-group'><a href='" + url + "' class='btn btn-danger btn-sm' target='_blank' rel='nofollow'>Haritadan Görünüm</a></div>");
                }
                else
                {
                    string url = "https://www.google.com.tr/maps?q=" + adres;
                    sb.Append("<div class='col-xs-12 form-group'><a href='" + url + "' class='btn btn-danger btn-sm' target='_blank' rel='nofollow'>Haritadan Görünüm</a></div>");
                }
                sb.Append("</div></div></div></div>");
            }
            eczanelerList.InnerHtml = sb.ToString();
        }
        catch (Exception ex)
        {
            HataMesajGoster(ex.Message);
        }
    }

    public void HataMesajGoster(string hata)
    {
        Page.ClientScript.RegisterStartupScript(GetType(), "Javascript",
              "bootbox.alert(\"" + hata + "\");", true);
    }

    public void DescriptionTanimla(string il, string ilce)
    {
        string desc = "";
        string keyw = "";
        string url = XmlAccessObject + "?$where=dc_Il_Kucuk_Harf=" + il;
        DataSet ds = new DataSet();
        DataSet ds2 = new DataSet();
        ds.ReadXml(new XmlTextReader(url));
        try
        {
            if (ds.Tables[0].Rows[0]["Exception"].ToString() != "")
            {
                HataMesajGoster("Kayıt bulunamadı!");
                return;
            }
        }
        catch (Exception)
        { }
        il = ds.Tables[0].Rows[0]["dc_Il"].ToString();

        if (ilce != "")
        {
            ds2.ReadXml(new XmlTextReader(XmlAccessObject + "?$where=dc_Ilce_Kucuk_Harf=" + ilce));
            try
            {
                if (ds2.Tables[0].Rows[0]["Exception"].ToString() != "")
                {
                    HataMesajGoster("Kayıt bulunamadı!");
                    return;
                }
            }
            catch (Exception)
            { }
            ilce = ds2.Tables[0].Rows[0]["dc_Ilce"].ToString();
            hfIlce.Value = ilce;
        }


        if (il != "" && ilce != "")
        {
            desc = "Günlük olarak " + il + " ili " + ilce + " ilçesi nöbetçi eczanelerini gösteren bir web sitesi.";
            keyw = il + " ili nöbetçi eczaneleri, " + il + " nöbetçi eczaneleri, " + il + " ili " + ilce + " ilçesi nöbetçi eczaneleri, " + il + " " + ilce + " nöbetçi eczaneleri";
            sehirBaslik.InnerText = il.ToUpper() + " İli " + ilce.ToUpper() + " İlçesi Nöbetçi Eczaneleri";
        }
        else if (il != "" & ilce == "")
        {
            desc = "Günlük olarak " + il + " ili tüm nöbetçi eczanelerini gösteren bir web sitesi.";
            keyw = il + " ili nöbetçi eczaneleri, " + il + " nöbetçi eczaneleri," + il + " ili tüm ilçe nöbetçi eczaneleri";
            sehirBaslik.InnerText = il.ToUpper() + " İli Tüm Nöbetçi Eczaneleri";
        }

        hfSehir.Value = il;

        description.Attributes.Add("content", desc);
        keywords.Attributes.Add("content", keyw);
    }
}