using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TenderComp
{
    public partial class print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrdComponies();
                SetTenderProperties();
            }

        }

        private void SetTenderProperties()
        {
            if (Session["TenderID"] != null)
            {
                int tenderid = Convert.ToInt32(Session["TenderID"]);
                // ReSharper disable once NotAccessedVariable
                TenderComputDBEntities1 oTenderComputDbEntities1 = null;
                try
                {
                    //List<tblCompony> componies = new List<tblCompony>();
                    oTenderComputDbEntities1 = new TenderComputDBEntities1();
                    var oTender = oTenderComputDbEntities1.tblTenders.FirstOrDefault(i => i.ID == tenderid);
                    lblTenderno.Text = oTender.TenderNo.ToString();
                    lblTenderTitle.Text = oTender.TenderName;
                    lblave1.Text = string.Format("{0:#.###}",oTender.Average);
                    lblave2.Text = string.Format("{0:#.###}", oTender.Average2);
                    lblenheraf.Text = string.Format("{0:#.###}",oTender.Variance);
                    lblup.Text = string.Format("{0:#.###}",oTender.UpperLimit);
                    lbldown.Text = string.Format("{0:#.###}", oTender.BottomLimit);

                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }

            }

        }
        private void BindGrdComponies()
        {
            if (Session["TenderID"] != null)
            {
                int tenderid = Convert.ToInt32(Session["TenderID"]);

                // ReSharper disable once TooWideLocalVariableScope
                TenderComputDBEntities1 oTenderComputDbEntities1 = null;
                try
                {
                    //List<tblCompony> componies = new List<tblCompony>();
                    oTenderComputDbEntities1 = new TenderComputDBEntities1();
                    var oTender = oTenderComputDbEntities1.tblTenders.Where(i => i.ID == tenderid).FirstOrDefault();
                    

                    List<tblCompony> companies = oTenderComputDbEntities1.tblComponies.Where(i => i.TenderID == tenderid)
                        .OrderBy(i => i.Price).ToList();
                    var otblCompony = new tblCompony();
                    otblCompony.ID = oTender.ID;
                    otblCompony.ComponyName = "برآورد بهنگام کارفرما";
                    otblCompony.CurrencyPrice = oTender.CurrencyEstimate;
                    otblCompony.RialiPrice = oTender.RialiEstimate;
                    otblCompony.Price = oTender.Estimate;

                    companies.Add(otblCompony);


                    GrdComponies.DataSource = companies;

                     GrdComponies.DataBind();
                   foreach (GridViewRow row in GrdComponies.Rows)
                    {
                        var rowvalue = (Label)row.FindControl("lblIswin");
                        if (rowvalue.Text == "True")
                        {
                            //row.BackColor = Color.Silver;
                            //row.ForeColor = Color.Sienna;
                            row.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
                            row.Style.Add(HtmlTextWriterStyle.Color, "Sienna");
                        }
                    }
                }
                catch (Exception ex)
                {

                    Response.Write(ex.ToString());
                }
            }
            else
            {
                Response.Redirect("Default.aspx", true);
            }
           

        }
    }
}