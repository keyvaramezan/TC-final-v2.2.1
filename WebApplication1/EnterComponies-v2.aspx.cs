using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI.WebControls;


namespace TenderComp
{
    public partial class EnterComponiesv2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrdComponies();
            }
        }
        /// <summary>
        /// خواندن نام شرکتها و قیمت آنها از دیتا بیس
        /// </summary>
        private void BindGrdComponies()
        {
            // ReSharper disable once TooWideLocalVariableScope
            TenderComputDBEntities1 oTenderComputDbEntities1 = null;
            try
            {
                //List<tblCompony> componies = new List<tblCompony>();
                oTenderComputDbEntities1 = new TenderComputDBEntities1();
                var componies = oTenderComputDbEntities1.tblComponies.Select
                    (i =>
                    new
                    {
                        ID = i.ID,
                        ComponyName = i.ComponyName,
                        Price = i.Price,
                        Comment = i.Comment,
                        t = i.t,
                        L = i.L,
                        IeDiff = i.IeDiff
                    }).ToList();
                GrdComponies.DataSource = componies;
                GrdComponies.DataBind();
            }
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }
            finally
            {

            };
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

        protected void InsertCompony(object sender, EventArgs e)
        {
            TenderComputDBEntities1 oTenderComputDbEntities1 = null;
            try
            {
                oTenderComputDbEntities1 = new TenderComputDBEntities1();
                tblCompony compony = null;
                if (drpTenderType.SelectedValue == "0")
                {
                    compony = new tblCompony
                    {
                        ComponyName = txtComponyName.Text,
                        Price = double.Parse(txtPrice.Text),
                        IsAccept = false
                    };
                }
                else
                {
                    compony = new tblCompony
                    {
                        ComponyName = txtComponyName.Text,
                        Price = double.Parse(txtPrice.Text),
                        t = double.Parse(txtEmtiazFani.Text),
                        IsAccept = false
                    };
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
            var baravord = double.Parse(txtBaravord.Text);

            TenderComputDBEntities1 oTenderComputDbEntities1 = null;
            try
            {
                oTenderComputDbEntities1 = new TenderComputDBEntities1();
                var companies = oTenderComputDbEntities1.tblComponies.ToList();
                int n = companies.Count + 1;
                if (companies.Count >= 3)
                {
                    foreach (var item in companies)
                    {
                        item.X = item.Price / baravord * 100;
                        var cmp = oTenderComputDbEntities1.tblComponies.FirstOrDefault(i => i.ID == item.ID);
                        // ReSharper disable once PossibleNullReferenceException
                        cmp.X = item.X;
                        cmp.IeDiff = (1 - (item.Price / baravord)) * 100;
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
                                cmp.IsAccept = false;
                                cmp.Comment = "خارج از دامنه در مرحله اول";
                            }
                            oTenderComputDbEntities1.SaveChanges();
                        }
                    }
                    new List<double?>();
                    var x2 = oTenderComputDbEntities1.tblComponies.Where(i => i.IsAccept == true).Select(i => i.X).ToList();
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
                    double nsdd = Math.Pow((double) (100 - m2), 2); 
                    x2.Add(nsdd);

                    //float sumform = (float) x2.Sum(i => i.X);
                    // ReSharper disable once PossibleInvalidOperationException
                    double sumxm2 = (double)x2.Sum();
                    double s2 = (double)(sumxm2 / (n - 1));
                    s2 = (float)Math.Sqrt(s2);
                    var t = ComputT();
                    double c1 = m2 - t * s2;
                    double c2 = m2 + t * s2;
                    var x2Comps = oTenderComputDbEntities1.tblComponies.Where(i => i.IsAccept == true).ToList();
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
                    companies = oTenderComputDbEntities1.tblComponies.Where(i => i.IsAccept == true).ToList();
                    var tabsarecompanies = oTenderComputDbEntities1.tblComponies.Where(i => i.X < c1).ToList();
                    var c1C2Min = companies.Where(i => i.IsAccept == true).OrderBy(i => i.Price).FirstOrDefault();
                    double tazmin = 0;
                    if (txtTazmin.Text != string.Empty)
                    {
                        tazmin = double.Parse(txtTazmin.Text);
                    }
                    foreach (var item in tabsarecompanies)
                    {
                        Debug.Assert(c1C2Min != null, "c1C2Min != null");
                        var subt3 = c1C2Min.Price - item.Price;
                        if (tazmin > subt3)
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

                    double nesab = 0;
                    if (txtNesab.Text != string.Empty)
                    {
                        nesab = 1000 * double.Parse(txtNesab.Text);
                    }

                    int count = oTenderComputDbEntities1.tblComponies.Count();
                    tabsarecompanies = oTenderComputDbEntities1.tblComponies.Where(i => i.IsAccept == false).ToList();
                    if (nesab > 0 && txtTazmin.Text != string.Empty && count <= 5 || baravord > nesab)
                    {
                        foreach (var item in tabsarecompanies)
                        {
                            if (item.X > c1 * 0.97)
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
                    if (drpTenderType.SelectedValue == "1")
                    {
                        var step2Companies = oTenderComputDbEntities1.tblComponies.Where(i => i.IsAccept == true).ToList();
                        double tasir = double.Parse(txti.Text);
                        foreach (var item in step2Companies)
                        {
                            var makhraj = 100 - (tasir * (100 - item.t));
                            var l = 100 * item.Price / makhraj;
                            var step2Comp = oTenderComputDbEntities1.tblComponies.FirstOrDefault(i => i.ID == item.ID);
                            Debug.Assert(step2Comp != null, "step2Comp != null");
                            step2Comp.L = l;
                        }
                        oTenderComputDbEntities1.SaveChanges();
                        var winnercomp = oTenderComputDbEntities1.tblComponies.Where(i => i.IsAccept == true).OrderBy(i => i.L).FirstOrDefault();
                        lblt.Text = string.Format("برنده مناقصه شرکت {0} می باشد", winnercomp.ComponyName);
                    }
                    else
                    {
                        var winnercomps =
                            oTenderComputDbEntities1.tblComponies.Where(i => i.IsAccept == true)
                                .OrderBy(i => i.Price)
                                .ToList();
                        // ReSharper disable once PossibleInvalidOperationException
                        double cmpwinner = (double)(winnercomps[1].Price - winnercomps[0].Price);
                        if (tazmin > cmpwinner)
                        {
                            lblt.Text = string.Format("برنده اول شرکت {0} و برنده دوم شرکت {1} می باشند", winnercomps[0].ComponyName,
                                winnercomps[1].ComponyName);

                        }
                        else
                        {
                            lblt.Text = string.Format("برنده مناقصه شرکت {0} می باشد", winnercomps[0].ComponyName);
                            
                        }
                        
                        
                        // ReSharper disable once PossibleInvalidOperationException
                        if (Math.Abs((double) winnercomps[0].IeDiff) > 10)
                        {
                            lblt.Text += "اختلاف با برآورد بیش از 10% بوده و باید به کمیته فنی و بازرگانی ارجاع شود";
                        }
                    }

                    oTenderComputDbEntities1.SaveChanges();
                }
                else
                {
                    var componymin = companies.OrderBy(i => i.Price).FirstOrDefault();
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



        private float ComputT()
        {
            // ReSharper disable once TooWideLocalVariableScope
            TenderComputDBEntities1 oTenderComputDbEntities1 = null;
            float t = 0;
            try
            {
                oTenderComputDbEntities1 = new TenderComputDBEntities1();
                var n = oTenderComputDbEntities1.tblComponies.Count();
                if (n >= 3 && n <= 6 && drpimpTender.SelectedValue == "0")
                {
                    t = (float)1.1;
                }
                else if (n >= 3 && n <= 6 && drpimpTender.SelectedValue == "1")
                {
                    t = (float)1.0;
                }
                else if (n >= 3 && n <= 6 && drpimpTender.SelectedValue == "2")
                {
                    t = (float)0.9;
                }
                else if (n >= 7 && n <= 10 && drpimpTender.SelectedValue == "0")
                {
                    t = (float)1.3;
                }
                else if (n >= 7 && n <= 10 && drpimpTender.SelectedValue == "1")
                {
                    t = (float)1.2;
                }
                else if (n >= 7 && n <= 10 && drpimpTender.SelectedValue == "2")
                {
                    t = (float)1.1;
                }
                else if (n > 10 && drpimpTender.SelectedValue == "0")
                {
                    t = (float)1.5;
                }
                else if (n > 10 && drpimpTender.SelectedValue == "1")
                {
                    t = (float)1.4;
                }
                else if (n > 10 && drpimpTender.SelectedValue == "2")
                {
                    t = (float)1.2;
                }
                return t;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }
        }

        protected void drpTenderType_TextChanged(object sender, EventArgs e)
        {
            if (drpTenderType.SelectedValue == "1")
            {
                spni.Visible = true;
                spnEmtiazFani.Visible = true;
                txti.Visible = true;
                txtEmtiazFani.Visible = true;
                vldEmtiazFani.Visible = true;
                vldi.Visible = true;
            }
            else
            {
                spni.Visible = false;
                spnEmtiazFani.Visible = false;
                txti.Visible = false;
                txtEmtiazFani.Visible = false;
                vldEmtiazFani.Visible = false;
                vldi.Visible = false;
            }


        }

        protected void btnDeleteAll_OnClick(object sender, EventArgs e)
        {
            // ReSharper disable once TooWideLocalVariableScope
            TenderComputDBEntities1 oTenderComputDbEntities1 = null;
            try
            {
                using (oTenderComputDbEntities1 = new TenderComputDBEntities1())
                {
                    var itemsToDelete = oTenderComputDbEntities1.Set<tblCompony>();
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
    }
}