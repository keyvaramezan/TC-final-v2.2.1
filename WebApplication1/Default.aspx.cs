using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Web.UI.WebControls;
using TenderComp.InfraStructure;
using System.Web;
using System.Web.Services;
using System.Text;
using System.Web.UI;

namespace TenderComp
{
    public partial class Default : System.Web.UI.Page
    {

        #region Mehtods
        private string getjQueryCode(string jsCodetoRun)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("$(document).ready(function() {");
            sb.AppendLine(jsCodetoRun);
            sb.AppendLine(" });");
            return sb.ToString();
        }
        private void RunjQueryCode(string jsCodetoRun)
        {
            ScriptManager requestSM = ScriptManager.GetCurrent(this);
            if (requestSM != null && requestSM.IsInAsyncPostBack)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), getjQueryCode(jsCodetoRun), true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(typeof(Page), Guid.NewGuid().ToString(), getjQueryCode(jsCodetoRun), true);
            }
        }
        private void Initialize()
        {
            int userId = Convert.ToInt32(Session["UserID"]);
            if (Session["UserID"] != null)
            {
                TenderComputDBEntities1 oTenderComputDbEntities1 = null;
                try
                {
                    oTenderComputDbEntities1 = new TenderComputDBEntities1();
                    var oUser = oTenderComputDbEntities1.tblUsers.FirstOrDefault(current => current.ID == userId);
                    if (oUser == null && userId != 123)
                    {
                        Response.Redirect("login.aspx", endResponse: true);
                    }

                }
                catch (Exception ex)
                {

                    Response.Write(ex.ToString());
                }
                finally
                {
                    if (oTenderComputDbEntities1 != null)
                    {
                        oTenderComputDbEntities1.Dispose();
                        oTenderComputDbEntities1 = null;
                    }
                }
            }
            //else
            //{
            //    Response.Redirect("login.aspx", endResponse: true);
            //}
        }
        /// <summary>
        /// خواندن نام شرکتها و قیمت آنها از دیتا بیس
        /// </summary>
        private void BindGrdComponies()
        {
            int tenderid = Convert.ToInt32(ViewState["TenderID"]);

            // ReSharper disable once TooWideLocalVariableScope
            TenderComputDBEntities1 oTenderComputDbEntities1 = null;
            try
            {
                //List<tblCompony> componies = new List<tblCompony>();
                oTenderComputDbEntities1 = new TenderComputDBEntities1();
                var componies = oTenderComputDbEntities1.tblComponies.Where(i => i.TenderID == tenderid).Select
                    (i =>
                    new
                    {
                        ID = i.ID,
                        ComponyName = i.ComponyName,
                        Price = i.Price,
                        Comment = i.Comment,
                        t = i.t,
                        L = i.L,
                        IeDiff = i.IeDiff,
                        X = i.X,
                        CurrencyPrice = i.CurrencyPrice,
                        RialiPrice = i.RialiPrice,
                        Iswin = i.IsWin
                    }).OrderBy(i => i.Price).ToList();
                GrdComponies.DataSource = componies;
                GrdComponies.DataBind();
                foreach (GridViewRow row in GrdComponies.Rows)
                {
                    var rowvalue = (Label)row.FindControl("lblIswin");
                    if (rowvalue.Text == "True")
                    {
                        row.BackColor = Color.Gray;
                        row.ForeColor = Color.Orange;
                    }
                }
            }
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }

        }
        private void BindGrdTender()
        {
            int tenderid = Convert.ToInt32(ViewState["TenderID"]);

            // ReSharper disable once TooWideLocalVariableScope
            TenderComputDBEntities1 oTenderComputDbEntities1 = null;
            try
            {
                //List<tblCompony>c componies = new List<tblCompony>();
                oTenderComputDbEntities1 = new TenderComputDBEntities1();
                var oTender = oTenderComputDbEntities1.tblTenders.Where(i => i.ID == tenderid).Select
                    (i =>
                    new
                    {
                        Tenderno = i.TenderNo,
                        TenderName = i.TenderName,
                        Variance = i.Variance,
                        Average = i.Average,
                        UpperLimit = i.UpperLimit,
                        BottomLimit = i.BottomLimit
                    }).ToList();
                grdTenderProperties.DataSource = oTender;
                grdTenderProperties.DataBind();

            }
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }

        }
        private void BindTenderno(string TenderName)
        {
            // ReSharper disable once NotAccessedVariable
            TenderComputDBEntities1 oTenderComputDbEntities1 = null;
            try
            {
                if (string.IsNullOrEmpty(TenderName))
                {
                    ddlTenderno.Items.Clear();
                    int userId = Convert.ToInt32(Session["UserID"]);
                    oTenderComputDbEntities1 = new TenderComputDBEntities1();
                    var tenderNumbers = oTenderComputDbEntities1.tblTenders.Where(i => i.UserID == userId).Select(i => i.TenderNo).ToList();
                    ddlTenderno.Items.Add("");
                    foreach (var item in tenderNumbers)
                    {
                        ddlTenderno.Items.Add(item.ToString());
                    }
                }
                else
                {
                    ddlTenderno.Items.Clear();
                    int userId = Convert.ToInt32(Session["UserID"]);
                    oTenderComputDbEntities1 = new TenderComputDBEntities1();
                    var tenderNumbers = oTenderComputDbEntities1.tblTenders.Where(i => i.UserID == userId &&
                        i.TenderName.Contains(TenderName)).Select(i => i.TenderNo).ToList();
                    ddlTenderno.Items.Add("");
                    foreach (var item in tenderNumbers)
                    {
                        ddlTenderno.Items.Add(item.ToString());
                    }
                }
                
            }
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }


        }
        #endregion

        #region events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Initialize();
                BindGrdComponies();
                BindGrdTender();
                BindTenderno("");
            }

        }
        /// <summary>
        /// حذف کردن ردیف شرکتها
        /// </summary>
        protected void DeleteItem(object sender, EventArgs e)
        {
            foreach (GridViewRow grow in GrdComponies.Rows)
            {
                var chbox = (CheckBox)grow.FindControl("chkrow");
                if (!chbox.Checked) continue;
                var dataKey1 = GrdComponies.DataKeys[grow.RowIndex];
                if (dataKey1 == null) continue;
                var dataKey = dataKey1.Value;
                var key = dataKey.ToString();
                var componyId = int.Parse(key);
                {
                    TenderComputDBEntities1 oTenderComputDbEntities1 = null;
                    try
                    {
                        oTenderComputDbEntities1 = new TenderComputDBEntities1();
                        var otblCompony = oTenderComputDbEntities1.tblComponies.FirstOrDefault(i => i.ID == componyId);
                        oTenderComputDbEntities1.tblComponies.Remove(otblCompony);
                        oTenderComputDbEntities1.SaveChanges();
                        Page.ClientScript.RegisterStartupScript(GetType(), "MyScript",
                            "javascript:$(\"#sectionC\").get(0).scrollIntoView()", true);
                    }

                    catch (Exception exception)
                    {

                        Response.Write(exception.ToString());
                    }
                    finally
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        if (oTenderComputDbEntities1 != null)
                        {
                            oTenderComputDbEntities1.Dispose();
                            // ReSharper disable once RedundantAssignment
                            oTenderComputDbEntities1 = null;
                        }
                    }
                }
            }
            BindGrdComponies();
        }
        /// <summary>
        /// اضافه کردن مشخصات شرکتها
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertCompony(object sender, EventArgs e)
        {
            var tenderid = Convert.ToInt32(ViewState["TenderID"]);
            TenderComputDBEntities1 oTenderComputDbEntities1 = null;
            try
            {
                oTenderComputDbEntities1 = new TenderComputDBEntities1();
                var oTender = oTenderComputDbEntities1.tblTenders.FirstOrDefault(i => i.ID == tenderid);
                tblCompony compony = null;
                if (txtPrice.Text == string.Empty)
                    txtPrice.Text = "0";
                if (txtCurrencyPrice2.Text == string.Empty)
                    txtCurrencyPrice2.Text = "0";
                if (txtEmtiazFani.Text == string.Empty)
                    txtEmtiazFani.Text = "0";
                if (oTender == null)
                {
                    divReqTender.Visible = true;
                }
                else
                {
                    divReqTender.Visible = false;
                    if (oTender.TenderType == false)
                    {
                        compony = new tblCompony
                        {
                            ComponyName = txtComponyName.Text,
                            IsAccept = false,
                            TenderID = tenderid
                        };
                        if (oTender.Currency == 1)
                        {
                            compony.RialiPrice = double.Parse(txtPrice.Text);
                            compony.Price = oTender.CurrencyPrice * double.Parse(txtPrice.Text);
                        }
                        else if(oTender.Currency == 2)
                        {
                            compony.RialiPrice = double.Parse(txtPrice.Text);
                            compony.Price = (oTender.CurrencyPrice * double.Parse(txtCurrencyPrice2.Text)) 
                                + double.Parse(txtPrice.Text);
                        }
                        else
                        {
                            compony.RialiPrice = double.Parse(txtPrice.Text);
                            compony.Price = double.Parse(txtPrice.Text);
                        }
                    }
                    else
                    {
                        compony = new tblCompony
                        {
                            ComponyName = txtComponyName.Text,
                            t = double.Parse(txtEmtiazFani.Text),
                            IsAccept = false,
                            TenderID = tenderid
                        };
                        if (oTender.Currency == 1)
                        {
                            compony.Price = oTender.CurrencyPrice * double.Parse(txtPrice.Text);
                        }
                        else if (oTender.Currency == 2)
                        {
                            compony.RialiPrice = double.Parse(txtPrice.Text);
                            compony.CurrencyPrice = double.Parse(txtCurrencyPrice2.Text);
                            compony.Price = (oTender.CurrencyPrice * double.Parse(txtCurrencyPrice2.Text))
                                + double.Parse(txtPrice.Text);
                        }
                        else
                        {
                            compony.RialiPrice = double.Parse(txtPrice.Text);
                            compony.Price = double.Parse(txtPrice.Text);
                        }
                    }
                    oTenderComputDbEntities1.tblComponies.Add(compony);
                    oTenderComputDbEntities1.SaveChanges();
                }
                BindGrdComponies();
                txtComponyName.Text = string.Empty;
                txtPrice.Text = string.Empty;
                txtEmtiazFani.Text = string.Empty;
                txtCurrencyPrice2.Text = string.Empty;
                Page.ClientScript.RegisterStartupScript(GetType(), "MyScript", "javascript:$(\"#sectionC\").get(0).scrollIntoView()", true);
            }
            catch (Exception exception)
            {

                Response.Write(exception);
            }
            finally
            {
                // ReSharper disable once PossibleNullReferenceException
                if (oTenderComputDbEntities1 != null)
                {
                    oTenderComputDbEntities1.Dispose();
                    // ReSharper disable once RedundantAssignment
                    oTenderComputDbEntities1 = null;
                }

            }
        }
        protected void btnComput_OnClick(object sender, EventArgs e)
        {
            int tenderID = Convert.ToInt32(ViewState["TenderID"]);
            TenderComputDBEntities1 oTenderComputDbEntities1 = null;
            try
            {
                oTenderComputDbEntities1 = new TenderComputDBEntities1();
                var otender = oTenderComputDbEntities1.tblTenders.First(i => i.ID == tenderID);
                var companies = oTenderComputDbEntities1.tblComponies.Where(i => i.TenderID == tenderID).ToList();
                if (companies.Count > 0)
                {
                    lblnullData.Visible = false;
                    int n = companies.Count + 1;
                    if (companies.Count >= 3)
                    {
                        foreach (var item in companies)
                        {
                            if (item.Price != 0)
                            {
                                item.X = item.Price / otender.Estimate * 100;
                                var cmp = oTenderComputDbEntities1.tblComponies.FirstOrDefault(i => i.ID == item.ID);
                                // ReSharper disable once PossibleNullReferenceException
                                cmp.X = item.X;
                                cmp.IeDiff = ((item.Price / otender.Estimate) - 1) * 100;
                            }
                            else
                            {
                                throw new Exception("\r\n آیتم قیمت در یک یا چند شرکت وارد نشده است \r\n");
                            }
                            
                        }
                        oTenderComputDbEntities1.SaveChanges();
                        var d = companies.Sum(i => i.X) + 100;
                        if (d == null) return;
                        double sumx = (double)d;
                        double m = sumx / n;
                        List<double> xm =
                            (from i in companies select Math.Pow((double)(i.X - m), 2) into d1 where true select (double)d1)
                                .ToList();
                        double sum = xm.Sum();
                        var tenderproperties = oTenderComputDbEntities1.tblTenders.FirstOrDefault(i => i.ID == tenderID);
                        // ReSharper disable once PossibleNullReferenceException

                        var s = Math.Sqrt(sum / (n - 1));
                        //foreach (var item in x)
                        //{
                        //    lblt.Text += item.ToString() + "<br />";
                        //}
                        double b = 0;
                        if (m <= 115)
                        {
                            b = (double)(m * 1.25);
                            foreach (var item in companies)
                            {
                                var cmp = oTenderComputDbEntities1.tblComponies.FirstOrDefault(i => i.ID == item.ID);
                                if (item.X < b)
                                {
                                    // ReSharper disable once PossibleNullReferenceException
                                    cmp.IsAccept = true;
                                    cmp.Comment = "داخل دامنه";
                                }
                                else
                                {
                                    cmp.IsAccept = false;
                                    cmp.Comment = "خارج از دامنه در مرحله اول";
                                }
                            }
                            oTenderComputDbEntities1.SaveChanges();
                        }
                        else if (m > 115)
                        {
                            b = (double)(m * 1.10);

                            foreach (var item in companies)
                            {
                                var cmp = oTenderComputDbEntities1.tblComponies.FirstOrDefault(i => i.ID == item.ID);
                                if (item.X < b)
                                {
                                    // ReSharper disable once PossibleNullReferenceException
                                    cmp.IsAccept = true;
                                    cmp.Comment = "داخل دامنه";

                                }
                                else
                                {
                                    Debug.Assert(cmp != null, "cmp != null");
                                    cmp.IsAccept = false;
                                    cmp.Comment = "خارج از دامنه در مرحله اول";
                                }
                                oTenderComputDbEntities1.SaveChanges();
                            }
                        }
                        // ReSharper disable once ObjectCreationAsStatement
                        new List<double?>();
                        var x2 = oTenderComputDbEntities1.tblComponies.Where(i => i.IsAccept == true && i.TenderID == tenderID).Select(i => i.X).ToList();
                        n = x2.Count + 1;
                        var d2 = x2.Sum() + 100;
                        if (d2 == null) return;
                        double sumx2 = (double)d2;
                        double m2 = sumx2 / n;
                        for (int i = 0; i < x2.Count; i++)
                        {
                            x2[i] = x2[i] - m2;
                            // ReSharper disable once PossibleInvalidOperationException
                            x2[i] = Math.Pow((double)x2[i], 2);
                        }
                        double nsdd = Math.Pow((double)(100 - m2), 2);
                        x2.Add(nsdd);

                        //double sumform = (double) x2.Sum(i => i.X);
                        // ReSharper disable once PossibleInvalidOperationException
                        double sumxm2 = (double)x2.Sum();
                        double s2 = (double)(sumxm2 / (n - 1));
                        s2 = (double)Math.Sqrt(s2);
                        double c1 = (double)(m2 - otender.t * s2);
                        double c2 = (double)(m2 + otender.t * s2);
                        tenderproperties.Average = m;
                        tenderproperties.Average2 = m2;
                        tenderproperties.Variance = s2;
                        tenderproperties.UpperLimit = c2;
                        tenderproperties.BottomLimit = c1;
                        oTenderComputDbEntities1.SaveChanges();
                        var x2Comps = oTenderComputDbEntities1.tblComponies.Where(i => i.IsAccept == true && i.TenderID == tenderID).ToList();
                        foreach (var item in x2Comps)
                        {
                            if (item.X > c2 || item.X < c1)
                            {
                                var x2Company = oTenderComputDbEntities1.tblComponies.FirstOrDefault(i => i.ID == item.ID);
                                //if (x2Company != null) x2Company.IsAccept = null;
                                // ReSharper disable once PossibleNullReferenceException
                                x2Company.IsAccept = false;
                                x2Company.Comment = "خارج از دامنه در مرحله دوم";
                            }
                        }
                        oTenderComputDbEntities1.SaveChanges();
                        companies = oTenderComputDbEntities1.tblComponies.Where(i => i.IsAccept == true && i.TenderID == tenderID).ToList();
                        var tabsarecompanies = oTenderComputDbEntities1.tblComponies.Where(i => i.X < c1 && i.TenderID == tenderID).ToList();
                        var c1C2Min = companies.Where(i => i.IsAccept == true && i.TenderID == tenderID).OrderBy(i => i.Price).FirstOrDefault();

                        foreach (var item in tabsarecompanies)
                        {
                            Debug.Assert(c1C2Min != null, "c1C2Min != null");
                            var subt3 = c1C2Min.Price - item.Price;
                            if (otender.Fguaranty > subt3)
                            {
                                tblCompony companiestabsare3 =
                                    oTenderComputDbEntities1.tblComponies.FirstOrDefault(i => i.ID == item.ID);
                                // ReSharper disable once PossibleNullReferenceException
                                companiestabsare3.IsAccept = true;
                                companiestabsare3.Comment = "داخل دامنه بر اساس تبصره 3";
                            }
                            // ReSharper disable once PossibleNullReferenceException
                        }
                        oTenderComputDbEntities1.SaveChanges();
                        var nesab = 1000 * otender.Quorum;
                        var count = oTenderComputDbEntities1.tblComponies.Count(i => i.TenderID == tenderID);
                        tabsarecompanies = oTenderComputDbEntities1.tblComponies.Where(i => i.IsAccept == false && i.TenderID == tenderID).ToList();
                        if (count <= 5 || otender.Estimate > nesab)
                        {
                            foreach (var item in tabsarecompanies)
                            {
                                if (item.X < c1 && item.X > c1 * 0.97)
                                {
                                    var componiestabsare4 =
                                        oTenderComputDbEntities1.tblComponies.FirstOrDefault(i => i.ID == item.ID);
                                    // ReSharper disable once PossibleNullReferenceException
                                    componiestabsare4.IsAccept = true;
                                    componiestabsare4.Comment = "داخل دامنه اساس تبصره 4";
                                }
                            }
                        }
                        oTenderComputDbEntities1.SaveChanges();
                        //علامت برنده شدن همه شرکت ها را خالی می کند
                        var dofalseIswinComponies =
                            oTenderComputDbEntities1.tblComponies.Where(i => i.TenderID == tenderID).ToList();
                        foreach (var item in dofalseIswinComponies)
                        {
                            item.IsWin = null;
                        }
                        oTenderComputDbEntities1.SaveChanges();
                        if (otender.TenderType == true)
                        {
                            var step2Companies = oTenderComputDbEntities1.tblComponies.Where(i => i.IsAccept == true && i.TenderID == tenderID).ToList();

                            foreach (var item in step2Companies)
                            {
                                if (item.t != 0)
                                {
                                    var makhraj = 100 - (otender.i * (100 - item.t));
                                    var l = 100 * item.Price / makhraj;
                                    var step2Comp = oTenderComputDbEntities1.tblComponies.FirstOrDefault(i => i.ID == item.ID);
                                    Debug.Assert(step2Comp != null, "step2Comp != null");
                                    step2Comp.L = l;
                                }
                                else
                                {
                                    throw new Exception("\r\nامتیاز فنی یک یا چند شرکت وارد نشده است\r\n");
                                }
                            }
                            oTenderComputDbEntities1.SaveChanges();
                            var winnercomp = oTenderComputDbEntities1.tblComponies.Where(i => i.IsAccept == true && i.TenderID == tenderID).OrderBy(i => i.L).FirstOrDefault();
                            // ReSharper disable once PossibleNullReferenceException
                            winnercomp.IsWin = true;
                            oTenderComputDbEntities1.SaveChanges();
                            lblt.Text = string.Format("برنده مناقصه شرکت {0} می باشد", winnercomp.ComponyName);
                            if (Math.Abs((double)winnercomp.IeDiff) > 10)
                            {
                                winnercomp.Comment +=
                              "<p>ارجاع به کمیته فنی و بازرگانی</p>";
                                lblt.Text += "<div style='color: #1c4e6b'>اختلاف با برآورد بیش از 10% بوده و باید به کمیته فنی و بازرگانی ارجاع شود</div>";
                            }
                        }
                        else
                        {
                            var winnercomps =
                                oTenderComputDbEntities1.tblComponies.Where(i => i.IsAccept == true && i.TenderID == tenderID)
                                    .OrderBy(i => i.Price)
                                    .ToList();
                            // ReSharper disable once PossibleInvalidOperationException
                            double cmpwinner = (double)(winnercomps[1].Price - winnercomps[0].Price);
                            if (otender.Fguaranty > cmpwinner)
                            {
                                lblt.Text = string.Format("برنده اول شرکت {0} و برنده دوم شرکت {1} می باشند", winnercomps[0].ComponyName,
                                    winnercomps[1].ComponyName);
                                winnercomps[0].IsWin = true;
                                winnercomps[1].IsWin = true;
                                oTenderComputDbEntities1.SaveChanges();


                            }
                            else
                            {
                                winnercomps[0].IsWin = true;
                                lblt.Text = string.Format("برنده مناقصه شرکت {0} می باشد", winnercomps[0].ComponyName);

                            }


                            // ReSharper disable once PossibleInvalidOperationException
                            if (Math.Abs((double)winnercomps[0].IeDiff) > 10)
                            {
                                winnercomps[0].Comment +=
                                 "<p>ارجاع به کمیته فنی و بازرگانی</p>";
                                lblt.Text += "اختلاف با برآورد بیش از 10% بوده و باید به کمیته فنی و بازرگانی ارجاع شود";
                            }
                        }

                        oTenderComputDbEntities1.SaveChanges();
                    }
                    else
                    {
                        var componymin = companies.Where(i => i.TenderID == tenderID).OrderBy(i => i.Price).FirstOrDefault();
                        // ReSharper disable once PossibleNullReferenceException
                        componymin.IsWin = true;
                        Debug.Assert(componymin != null, "componymin != null");
                        lblt.Text = string.Format("برنده مناقصه شرکت {0} می باشد", componymin.ComponyName);
                        oTenderComputDbEntities1.SaveChanges();
                    }

                    BindGrdComponies();
                    BindGrdTender();
                    Page.ClientScript.RegisterStartupScript(GetType(), "MyScript", "javascript:$(\"#sectionD\").get(0).scrollIntoView()", true);
                }
                else
                {
                    lblnullData.Visible = true;
                    Page.ClientScript.RegisterStartupScript(GetType(), "MyScript", "javascript:$(\"#sectionD\").get(0).scrollIntoView()", true);
                }

            }
            catch (Exception ex)
            {
                string[] ExceptionMessage = ex.ToString().Split(new string[] { "\r\n" }, StringSplitOptions.None);
                lblt.Text = ExceptionMessage[1];
                Page.ClientScript.RegisterStartupScript(GetType(), "MyScript", "javascript:$(\"#sectionD\").get(0).scrollIntoView()", true);
                Response.Write(ex.ToString());
            }


        }
        protected void drpTenderType_TextChanged(object sender, EventArgs e)
        {
            //اگر دومرحله ای انتخاب شد فیلد ضریب تاثیر نمایان می شود
            divTenderType.Visible = drpTenderType.SelectedValue == "true";
            Page.ClientScript.RegisterStartupScript(GetType(), "MyScript", "javascript:$(\"#sectionB\").get(0).scrollIntoView()", true);
        }
        protected void btnDeleteAll_OnClick(object sender, EventArgs e)
        {
            // ReSharper disable once TooWideLocalVariableScope
            int tenderid = Convert.ToInt32(ViewState["TenderID"]);
            TenderComputDBEntities1 oTenderComputDbEntities1 = null;
            try
            {
                using (oTenderComputDbEntities1 = new TenderComputDBEntities1())
                {
                    var itemsToDelete = oTenderComputDbEntities1.tblComponies.Where(i => i.TenderID == tenderid);
                    oTenderComputDbEntities1.tblComponies.RemoveRange(itemsToDelete);
                    oTenderComputDbEntities1.SaveChanges();
                }
                BindGrdComponies();
                Page.ClientScript.RegisterStartupScript(GetType(), "MyScript", "javascript:$(\"#sectionC\").get(0).scrollIntoView()", true);
            }
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }
            finally
            {
                if (oTenderComputDbEntities1 != null)
                {
                    oTenderComputDbEntities1.Dispose();
                    oTenderComputDbEntities1 = null;


                }
            }
        }
        /// <summary>
        /// وارد کردن مشخصات مناقصه و ذخیره در دیتابیس
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEnter_OnClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                TenderComputDBEntities1 oTenderComputDbEntities1 = null;
                try
                {
                    if (ViewState["TenderID"] == null)
                    {
                        oTenderComputDbEntities1 = new TenderComputDBEntities1();
                        tblTender oTblTender = new tblTender();
                        oTblTender.Fguaranty = Convert.ToDouble(txtTazmin.Text);
                        oTblTender.TenderType = Convert.ToBoolean(drpTenderType.SelectedValue);
                        var tenderno = Convert.ToInt32(txtTenderno.Text);
                        if (drpTenderType.SelectedValue == "true")
                        {
                            oTblTender.i = Convert.ToDouble(txti.Text);
                        }
                        oTblTender.Estimate = Convert.ToDouble(txtBaravord.Text);
                        oTblTender.Quorum = Convert.ToDouble(txtNesab.Text);
                        oTblTender.t = Convert.ToDouble(txtt.Text);
                        oTblTender.TenderNo = tenderno;
                        oTblTender.TenderName = txtTenderName.Text;
                        InfraStructure.DateConvertor oDateConvertor = new InfraStructure.DateConvertor();
                        oTblTender.TenderDate = oDateConvertor.Shamsitomiladi(txtTenderDate.Text);
                        oTblTender.UserID = Convert.ToInt32(Session["UserID"]);
                        oTblTender.Currency = Convert.ToInt16(drpCurrency.SelectedValue);
                        if (int.Parse(drpCurrency.SelectedValue) == 1)
                        {
                            oTblTender.CurrencyPrice = Convert.ToDouble(txtCurrencyPrice.Text);
                            oTblTender.Estimate = Convert.ToDouble(txtBaravord.Text) * Convert.ToDouble(txtCurrencyPrice.Text);
                            oTblTender.CurrencyEstimate = Convert.ToDouble(txtBaravord.Text);

                        }
                        else if (int.Parse(drpCurrency.SelectedValue) == 2)
                        {
                            oTblTender.CurrencyPrice = Convert.ToDouble(txtCurrencyPrice.Text);
                            oTblTender.Estimate = (Convert.ToDouble(txtBaravordCurrency.Text) * Convert.ToDouble(txtCurrencyPrice.Text))
                                                    + Convert.ToDouble(txtBaravord.Text);
                            oTblTender.RialiEstimate = Convert.ToDouble(txtBaravord.Text);
                            oTblTender.CurrencyEstimate = Convert.ToDouble(txtBaravordCurrency.Text);
                        }
                        else
                        {
                            oTblTender.RialiEstimate = Convert.ToDouble(txtBaravord.Text);
                            oTblTender.Estimate = Convert.ToDouble(txtBaravord.Text);
                        }
                        oTenderComputDbEntities1.tblTenders.Add(oTblTender);
                        oTenderComputDbEntities1.SaveChanges();
                        if (oTblTender.TenderType == true)
                        {
                            divEmtiazFani.Visible = true;
                        }
                        BindTenderno("");
                        ViewState["TenderID"] = oTblTender.ID.ToString();
                    }
                    else
                    {
                        var tenerid = Convert.ToInt32(ViewState["TenderID"].ToString());
                        oTenderComputDbEntities1 = new TenderComputDBEntities1();
                        var oTblTender = oTenderComputDbEntities1.tblTenders.FirstOrDefault(i => i.ID == tenerid);
                        var tenderno = Convert.ToInt32(txtTenderno.Text);
                        // ReSharper disable once PossibleNullReferenceException
                        oTblTender.TenderType = Convert.ToBoolean(drpTenderType.SelectedValue);
                        if (drpTenderType.SelectedValue == "true")
                        {
                            Debug.Assert(oTblTender != null, "oTblTender != null");
                            oTblTender.i = Convert.ToDouble(txti.Text);
                        }
                        Debug.Assert(oTblTender != null, "oTblTender != null");
                        oTblTender.Estimate = Convert.ToDouble(txtBaravord.Text);
                        oTblTender.t = Convert.ToDouble(txtt.Text);
                        oTblTender.Quorum = Convert.ToDouble(txtNesab.Text);
                        oTblTender.TenderNo = tenderno;
                        oTblTender.TenderName = txtTenderName.Text;
                        InfraStructure.DateConvertor oDateConvertor = new InfraStructure.DateConvertor();
                        oTblTender.TenderDate = oDateConvertor.Shamsitomiladi(txtTenderDate.Text);
                        oTblTender.UserID = Convert.ToInt32(Session["UserID"]);
                        oTblTender.Currency = Convert.ToInt32(drpCurrency.SelectedValue);
                        oTblTender.Fguaranty = Convert.ToDouble(txtTazmin.Text);
                        if (int.Parse(drpCurrency.SelectedValue) == 1)
                        {
                            oTblTender.CurrencyPrice = Convert.ToDouble(txtCurrencyPrice.Text);
                            oTblTender.Estimate = Convert.ToDouble(txtBaravord.Text) * Convert.ToDouble(txtCurrencyPrice.Text);
                            oTblTender.CurrencyEstimate = Convert.ToDouble(txtBaravord.Text);

                        }
                        else if (int.Parse(drpCurrency.SelectedValue) == 2)
                        {
                            oTblTender.CurrencyPrice = Convert.ToDouble(txtCurrencyPrice.Text);
                            oTblTender.Estimate = (Convert.ToDouble(txtBaravordCurrency.Text) * Convert.ToDouble(txtCurrencyPrice.Text)) 
                                                    + Convert.ToDouble(txtBaravord.Text);
                            oTblTender.RialiEstimate = Convert.ToDouble(txtBaravord.Text);
                            oTblTender.CurrencyEstimate = Convert.ToDouble(txtBaravordCurrency.Text);
                        }
                        else 
                        {
                            oTblTender.RialiEstimate = Convert.ToDouble(txtBaravord.Text);
                            oTblTender.Estimate = Convert.ToDouble(txtBaravord.Text);
                        }
                        oTenderComputDbEntities1.SaveChanges();
                        if (oTblTender.TenderType == true)
                        {
                            divEmtiazFani.Visible = true;
                        }
                        BindTenderno("");
                    }
                    BindGrdTender();
                    Page.ClientScript.RegisterStartupScript(GetType(), "MyScript", "javascript:$(\"#sectionB\").get(0).scrollIntoView()", true);
                    divUnSuccess.Visible = false;
                    divSuccess.Visible = true;
                }
                catch (Exception ex)
                {

                    divSuccess.Visible = false;
                    divUnSuccess.Visible = true;
                    Page.ClientScript.RegisterStartupScript(GetType(), "MyScript", "javascript:$(\"#sectionB\").get(0).scrollIntoView()", true);

                }
                finally
                {
                    if (oTenderComputDbEntities1 != null)
                    {
                        oTenderComputDbEntities1.Dispose();
                        oTenderComputDbEntities1 = null;
                        // ReSharper disable once PossibleNullReferenceException

                    }
                }
            }

        }
        /// <summary>
        /// اعتبار سنجی در خصوص تکراری نبودن شماره مناقصه
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void cvlTenderno_ServerValidate(object source, ServerValidateEventArgs e)
        {
            if (e.IsValid == true)
            {
                var tenderid = 0;
                if (ViewState["TenderID"] != null)
                {
                    tenderid = Convert.ToInt32(ViewState["TenderID"].ToString());
                }

                TenderComputDBEntities1 oTenderComputDbEntities1 = null;
                try
                {
                    oTenderComputDbEntities1 = new TenderComputDBEntities1();
                    int tenderno = Convert.ToInt32(txtTenderno.Text);
                    var oTender =
                        oTenderComputDbEntities1.tblTenders
                            .FirstOrDefault(i => i.TenderNo == tenderno);
                    if (oTender != null && oTender.ID != tenderid)
                    {
                        e.IsValid = false;
                    }
                }
                catch (Exception ex)
                {

                    Response.Write(ex.ToString());
                }
                finally
                {
                    if (oTenderComputDbEntities1 != null)
                    {
                        oTenderComputDbEntities1.Dispose();
                        // ReSharper disable once PossibleNullReferenceException
                        oTenderComputDbEntities1 = null;

                    }
                }
            }


        }
        protected void drpCurrency_OnTextChanged(object sender, EventArgs e)
        {
            
                switch (drpCurrency.SelectedValue)
                {
                    case "2":
                        divCurrencyPrice.Visible = true;
                        divCurrencyPrice2.Visible = true;
                        divBaravordCurrency.Visible = true;
                        lblCurrencyRiali.Text = "ریالی";
                        break;
                    case "1":
                        divCurrencyPrice.Visible = true;
                        divCurrencyPrice2.Visible = true;
                        divBaravordCurrency.Visible = false;
                        lblCurrencyRiali.Text = "ارزی";
                        break;
                   default:
                        divBaravordCurrency.Visible = false;
                        divCurrencyPrice.Visible = false;
                        divCurrencyPrice2.Visible = false;
                        break;
                }
            Page.ClientScript.RegisterStartupScript(GetType(), "MyScript", "javascript:$(\"#sectionB\").get(0).scrollIntoView()", true);
        }
        protected void btnEditeTender_OnClick(object sender, EventArgs e)
        {
            RunjQueryCode("$('html, body').animate({scrollTop: $('#sectionB').offset().top}, 2000);");
            TenderComputDBEntities1 oTenderComputDbEntities1 = null;
            int tenderselectedItem = Convert.ToInt32(ddlTenderno.SelectedItem.Text);
            try
            {
                oTenderComputDbEntities1 = new TenderComputDBEntities1();
                var tender =
                    oTenderComputDbEntities1.tblTenders.FirstOrDefault(
                        i => i.TenderNo == tenderselectedItem);
                // ReSharper disable once PossibleNullReferenceException
                ViewState["TenderID"] = tender.ID.ToString();
                if (tender.TenderType == true)
                {
                    divEmtiazFani.Visible = true;
                    divTenderType.Visible = true;
                    drpTenderType.SelectedValue = "true";
                }
                else
                {
                    divEmtiazFani.Visible = false;
                    divTenderType.Visible = false;
                    drpTenderType.SelectedValue = "false";
                }
                if (tender.Currency == 1)
                {
                    divCurrencyPrice.Visible = true;
                    drpCurrency.SelectedValue = "1";
                    txtCurrencyPrice.Text = tender.CurrencyPrice.ToString();
                }
                else if (tender.Currency == 2)
                {
                    divCurrencyPrice.Visible = true;
                    divCurrencyPrice2.Visible = true;
                    divBaravordCurrency.Visible = true;
                    drpCurrency.SelectedValue = "2";
                    txtCurrencyPrice.Text = tender.CurrencyPrice.ToString();
                    txtBaravordCurrency.Text = tender.CurrencyEstimate.ToString();
                }
                else
                {
                    divCurrencyPrice.Visible = false;
                    drpCurrency.SelectedValue = "0";
                }
                txtTenderno.Text = tender.TenderNo.ToString();
                txtTenderName.Text = tender.TenderName;
                txtBaravord.Text = tender.Estimate.ToString();
                txtTazmin.Text = tender.Fguaranty.ToString();
                txtNesab.Text = tender.Quorum.ToString();
                txtt.Text = tender.t.ToString();
                txti.Text = tender.i.ToString();

                InfraStructure.DateConvertor dateConvertor = new DateConvertor();
                // ReSharper disable once PossibleInvalidOperationException
                txtTenderDate.Text = dateConvertor.MiladiToShamsi((DateTime)tender.TenderDate);
                BindGrdComponies();
                BindGrdTender();
                divSuccess.Visible = false;
            }
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }
        }
        protected void GrdComponies_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GrdComponies.EditIndex = e.NewEditIndex;
            BindGrdComponies();
            Page.ClientScript.RegisterStartupScript(GetType(), "MyScript", "javascript:$(\"#sectionC\").get(0).scrollIntoView()", true);
        }
        protected void GrdComponies_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var dataKey1 = GrdComponies.DataKeys[e.RowIndex];
            var txtEditRialiPrice = (TextBox)GrdComponies.Rows[e.RowIndex].FindControl("txtEditRialiPrice");
            var txtEditCurrencyPriece = (TextBox)GrdComponies.Rows[e.RowIndex].FindControl("txtEditCurrencyPriece");
            var txtEditPrice = (TextBox)GrdComponies.Rows[e.RowIndex].FindControl("txtEditPrice");
            var txtEditName = (TextBox)GrdComponies.Rows[e.RowIndex].FindControl("txtEditName");
            var txtEditEmtiazFani = (TextBox)GrdComponies.Rows[e.RowIndex].FindControl("txtEditEmtiazFani");
            var dataKey = dataKey1.Value;
            var key = dataKey.ToString();
            var componyId = int.Parse(key);
            {
                TenderComputDBEntities1 oTenderComputDbEntities1 = null;
                try
                {
                    if (txtEditRialiPrice.Text == string.Empty)
                        txtEditRialiPrice.Text = "0";
                    if (txtEditCurrencyPriece.Text == string.Empty)
                        txtEditCurrencyPriece.Text = "0";
                    if (txtEditEmtiazFani.Text == string.Empty)
                        txtEditEmtiazFani.Text = "0";
                    oTenderComputDbEntities1 = new TenderComputDBEntities1();
                    var otblCompony = oTenderComputDbEntities1.tblComponies.FirstOrDefault(i => i.ID == componyId);
                    var otblTender = oTenderComputDbEntities1.tblTenders.FirstOrDefault(i => i.ID == otblCompony.TenderID);
                    if (otblTender.Currency == 1)
                    {
                        otblCompony.Price = otblTender.CurrencyPrice * double.Parse(txtEditRialiPrice.Text);
                    }
                    else if (otblTender.Currency == 2)
                    {
                        otblCompony.RialiPrice = double.Parse(txtEditRialiPrice.Text);
                        otblCompony.CurrencyPrice = double.Parse(txtEditCurrencyPriece.Text);
                        otblCompony.Price = (otblTender.CurrencyPrice * double.Parse(txtEditCurrencyPriece.Text))
                            + double.Parse(txtEditRialiPrice.Text);
                    }
                    else
                    {
                        otblCompony.RialiPrice = double.Parse(txtEditRialiPrice.Text);
                        otblCompony.Price = double.Parse(txtEditRialiPrice.Text);
                    }
                    if(!string.IsNullOrEmpty(txtEditEmtiazFani.Text))
                        otblCompony.t = double.Parse(txtEditEmtiazFani.Text);
                    otblCompony.ComponyName = txtEditName.Text;
                    oTenderComputDbEntities1.SaveChanges();
                    GrdComponies.EditIndex = -1;
                    BindGrdComponies();
                    Page.ClientScript.RegisterStartupScript(GetType(), "MyScript",
                        "javascript:$(\"#sectionC\").get(0).scrollIntoView()", true);
                }

                catch (Exception exception)
                {

                    Response.Write(exception.ToString());
                }
                finally
                {
                    // ReSharper disable once PossibleNullReferenceException
                    if (oTenderComputDbEntities1 != null)
                    {
                        oTenderComputDbEntities1.Dispose();
                        // ReSharper disable once RedundantAssignment
                        oTenderComputDbEntities1 = null;
                    }
                }

                Page.ClientScript.RegisterStartupScript(GetType(), "MyScript", "javascript:$(\"#sectionC\").get(0).scrollIntoView()", true);
            }
        }

        protected void GrdComponies_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GrdComponies.EditIndex = -1;
            BindGrdComponies();
            Page.ClientScript.RegisterStartupScript(GetType(), "MyScript", "javascript:$(\"#sectionC\").get(0).scrollIntoView()", true);
        }
        protected void btnNewTen_Click(object sender, EventArgs e)
        {
            RunjQueryCode("$('html, body').animate({scrollTop: $('#sectionB').offset().top}, 2000);");
            ViewState["TenderID"] = null;
            txtTenderno.Text = string.Empty;
            drpTenderType.SelectedValue = "false";
            txtTazmin.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtNesab.Text = string.Empty;
            txtTenderDate.Text = string.Empty;
            txtBaravord.Text = string.Empty;
            txtt.Text = string.Empty;
            txtTenderName.Text = string.Empty;
            drpCurrency.SelectedIndex = 0;
            txtNesab.Text = string.Empty;
            divBaravordCurrency.Visible = false;
            divCurrencyPrice.Visible = false;
            divCurrencyPrice2.Visible = false;
            divTenderType.Visible = false;
            divSuccess.Visible = false;
            divEmtiazFani.Visible = false;
            lblt.Text = string.Empty;
            GrdComponies.DataSource = null;
            GrdComponies.DataBind();
            grdTenderProperties.DataSource = null;
            grdTenderProperties.DataBind();

        }
        protected void btnSendPrint_OnClick(object sender, EventArgs e)
        {
            if (ViewState["TenderID"] != null)
            {
                Session["TenderID"] = ViewState["TenderID"];
                Response.Redirect("print.aspx", false);
            }
            else
            {
                lblnullData.Visible = true;
            }

        }
        protected void btnFindTenderNo_OnClick(object sender, EventArgs e)
        {
            BindTenderno(txtfindTenderName.Text);
        }


        #endregion

        
    }
}