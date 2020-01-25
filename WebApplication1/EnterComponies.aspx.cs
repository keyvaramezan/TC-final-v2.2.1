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


namespace TenderComp
{
    public partial class EnterComponies : System.Web.UI.Page
    {
        #region Mehtods
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
                    if (oUser == null)
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
                        Iswin = i.IsWin
                    }).OrderBy(i => i.Price).ToList();
                GrdComponies.DataSource = componies; 
                GrdComponies.DataBind();
            }
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }

        }

        private void BindTenderno()
        {
            // ReSharper disable once NotAccessedVariable
            TenderComputDBEntities1 oTenderComputDbEntities1 = null;
            try
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
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }


        }
        //private float ComputT()
        //{
        //    // ReSharper disable once TooWideLocalVariableScope
        //    TenderComputDBEntities1 oTenderComputDbEntities1 = null;
        //    float t = 0;
        //    try
        //    {
        //        oTenderComputDbEntities1 = new TenderComputDBEntities1();
        //        var n = oTenderComputDbEntities1.tblComponies.Count();
        //        if (n >= 3 && n <= 6 && drpimpTender.SelectedValue == "0")
        //        {
        //            t = (float)1.1;
        //        }
        //        else if (n >= 3 && n <= 6 && drpimpTender.SelectedValue == "1")
        //        {
        //            t = (float)1.0;
        //        }
        //        else if (n >= 3 && n <= 6 && drpimpTender.SelectedValue == "2")
        //        {
        //            t = (float)0.9;
        //        }
        //        else if (n >= 7 && n <= 10 && drpimpTender.SelectedValue == "0")
        //        {
        //            t = (float)1.3;
        //        }
        //        else if (n >= 7 && n <= 10 && drpimpTender.SelectedValue == "1")
        //        {
        //            t = (float)1.2;
        //        }
        //        else if (n >= 7 && n <= 10 && drpimpTender.SelectedValue == "2")
        //        {
        //            t = (float)1.1;
        //        }
        //        else if (n > 10 && drpimpTender.SelectedValue == "0")
        //        {
        //            t = (float)1.5;
        //        }
        //        else if (n > 10 && drpimpTender.SelectedValue == "1")
        //        {
        //            t = (float)1.4;
        //        }
        //        else if (n > 10 && drpimpTender.SelectedValue == "2")
        //        {
        //            t = (float)1.2;
        //        }
        //        return t;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    finally
        //    {

        //    }
        //}
        #endregion
        #region events
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                Initialize();
                BindGrdComponies();
                BindTenderno();
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
                Debug.Assert(oTender != null, "oTender != null");
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
                        compony.Price = oTender.CurrencyPrice*double.Parse(txtPrice.Text);
                    }
                    else
                    {
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
                        compony.Price = oTender.CurrencyPrice*double.Parse(txtPrice.Text);
                    }
                    else
                    {
                        compony.Price = double.Parse(txtPrice.Text);
                    }
                }
                oTenderComputDbEntities1.tblComponies.Add(compony);
                oTenderComputDbEntities1.SaveChanges();
                BindGrdComponies();
                txtComponyName.Text = string.Empty;
                txtPrice.Text = string.Empty;
                txtEmtiazFani.Text = string.Empty;
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
                int n = companies.Count + 1;
                if (companies.Count >= 3)
                {
                    foreach (var item in companies)
                    {
                        item.X = item.Price / otender.Estimate * 100;
                        var cmp = oTenderComputDbEntities1.tblComponies.FirstOrDefault(i => i.ID == item.ID);
                        // ReSharper disable once PossibleNullReferenceException
                        cmp.X = item.X;
                        cmp.IeDiff = ((item.Price / otender.Estimate) - 1) * 100;
                    }
                    oTenderComputDbEntities1.SaveChanges();
                    var d = companies.Sum(i => i.X) + 100;
                    if (d == null) return;
                    float sumx = (float)d;
                    float m = sumx / n;
                    List<float> xm =
                        (from i in companies select Math.Pow((double)(i.X - m), 2) into d1 where true select (float)d1)
                            .ToList();
                    float sum = xm.Sum();
                    var s = Math.Sqrt(sum / (n - 1));
                    //foreach (var item in x)
                    //{
                    //    lblt.Text += item.ToString() + "<br />";
                    //}
                    float b = 0;
                    if (m <= 115)
                    {
                        b = (float)(m * 1.25);
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
                        BindGrdComponies();
                    }
                    else if (m > 115)
                    {
                        b = (float)(m * 1.10);

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
                    float sumx2 = (float)d2;
                    float m2 = sumx2 / n;
                    for (int i = 0; i < x2.Count; i++)
                    {
                        x2[i] = x2[i] - m2;
                        // ReSharper disable once PossibleInvalidOperationException
                        x2[i] = Math.Pow((double)x2[i], 2);
                    }
                    double nsdd = Math.Pow((double)(100 - m2), 2);
                    x2.Add(nsdd);

                    //float sumform = (float) x2.Sum(i => i.X);
                    // ReSharper disable once PossibleInvalidOperationException
                    double sumxm2 = (double)x2.Sum();
                    double s2 = (double)(sumxm2 / (n - 1));
                    s2 = (float)Math.Sqrt(s2);
                    double c1 = (double) (m2 - otender.t * s2);
                    double c2 = (double) (m2 + otender.t * s2);
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
                    if (otender.TenderType == true)
                    {
                        var step2Companies = oTenderComputDbEntities1.tblComponies.Where(i => i.IsAccept == true && i.TenderID == tenderID).ToList();
                       
                        foreach (var item in step2Companies)
                        {
                            var makhraj = 100 - (otender.t * (100 - item.t));
                            var l = 100 * item.Price / makhraj;
                            var step2Comp = oTenderComputDbEntities1.tblComponies.FirstOrDefault(i => i.ID == item.ID);
                            Debug.Assert(step2Comp != null, "step2Comp != null");
                            step2Comp.L = l;
                        }
                        oTenderComputDbEntities1.SaveChanges();
                        var winnercomp = oTenderComputDbEntities1.tblComponies.Where(i => i.IsAccept == true && i.TenderID == tenderID).OrderBy(i => i.L).FirstOrDefault();
                        // ReSharper disable once PossibleNullReferenceException
                        winnercomp.IsWin = true;
                        oTenderComputDbEntities1.SaveChanges();
                        lblt.Text = string.Format("برنده مناقصه شرکت {0} می باشد", winnercomp.ComponyName);
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
                }

                BindGrdComponies();
            }
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }


        }

        protected void drpTenderType_TextChanged(object sender, EventArgs e)
        {
            //اگر دومرحله ای انتخاب شد فیلد ضریب تاثیر نمایان می شود
            divTenderType.Visible = drpTenderType.SelectedValue == "true";
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
                        InfraStructure.DateConvertor oDateConvertor = new InfraStructure.DateConvertor();
                        oTblTender.TenderDate = oDateConvertor.Shamsitomiladi(txtTenderDate.Text);
                        oTblTender.UserID = Convert.ToInt32(Session["UserID"]);
                        oTblTender.Currency = Convert.ToInt16(drpCurrency.SelectedValue);
                        if (int.Parse(drpCurrency.SelectedValue) == 1 || int.Parse(drpCurrency.SelectedValue) == 2)
                        {
                            oTblTender.CurrencyPrice = Convert.ToDouble(txtCurrencyPrice.Text);
                        }
                        oTenderComputDbEntities1.tblTenders.Add(oTblTender);
                        oTenderComputDbEntities1.SaveChanges();
                        if (oTblTender.TenderType == true)
                        {
                            divEmtiazFani.Visible = true;
                        }
                        BindTenderno();
                        ViewState["TenderID"] = oTblTender.ID.ToString();
                    }
                    else
                    {
                        var tenerid = Convert.ToInt32(ViewState["TenderID"].ToString());
                        oTenderComputDbEntities1 = new TenderComputDBEntities1();
                        var oTblTender = oTenderComputDbEntities1.tblTenders.FirstOrDefault(i => i.ID == tenerid);
                        var tenderno = Convert.ToInt32(txtTenderno.Text);
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
                        InfraStructure.DateConvertor oDateConvertor = new InfraStructure.DateConvertor();
                        oTblTender.TenderDate = oDateConvertor.Shamsitomiladi(txtTenderDate.Text);
                        oTblTender.UserID = Convert.ToInt32(Session["UserID"]);
                        oTblTender.Currency = Convert.ToInt16(drpCurrency.SelectedValue);
                        oTblTender.Fguaranty = Convert.ToDouble(txtTazmin.Text);
                        if (int.Parse(drpCurrency.SelectedValue) == 1)
                        {
                            oTblTender.CurrencyPrice = Convert.ToDouble(txtCurrencyPrice.Text);
                        }
                        oTenderComputDbEntities1.SaveChanges();
                        if (oTblTender.TenderType == true)
                        {
                            divEmtiazFani.Visible = true;
                        }
                        BindTenderno();
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
            if (drpCurrency.SelectedValue == "true")
            {
                divCurrencyPrice.Visible = true;
            }
            else
            {
                divCurrencyPrice.Visible = false;
            }
        }
        protected void btnEditeTender_OnClick(object sender, EventArgs e)
        {
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
                if (tender.Currency == 1 || tender.Currency == 2)
                {
                    divCurrencyPrice.Visible = true;
                    drpCurrency.SelectedValue = "true";
                    txtCurrencyPrice.Text = tender.CurrencyPrice.ToString();
                }
                else
                {
                    divCurrencyPrice.Visible = false;
                    drpTenderType.SelectedValue = "false";
                }
                txtTenderno.Text = tender.TenderNo.ToString();
                txtBaravord.Text = tender.Estimate.ToString();
                txtTazmin.Text = tender.Fguaranty.ToString();
                txtNesab.Text = tender.Quorum.ToString();
                txtt.Text = tender.t.ToString();
                txti.Text = tender.i.ToString();

                InfraStructure.DateConvertor dateConvertor = new DateConvertor();
                // ReSharper disable once PossibleInvalidOperationException
                txtTenderDate.Text = dateConvertor.MiladiToShamsi((DateTime)tender.TenderDate);
                BindGrdComponies();

            }
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }
        }
        protected void btnNewTender_OnClick(object sender, EventArgs e)
        {
            ViewState["TenderID"] = null;
            txtTenderno.Text = string.Empty;
            drpTenderType.SelectedValue = "false";
            txtTazmin.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtNesab.Text = string.Empty;
            txtTenderDate.Text = string.Empty;
            txtBaravord.Text = string.Empty;
            txtt.Text = string.Empty;
            drpCurrency.SelectedValue = "false";
            txtNesab.Text = string.Empty;
        }

        #endregion


    }
}