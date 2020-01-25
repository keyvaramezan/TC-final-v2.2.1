using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TenderComp
{
    public partial class TenderMng : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrdTender();
            }

        }

        private void BindGrdTender()
        {
            TenderComputDBEntities1 oTenderComputDbEntities1 = null;
            try
            {
                oTenderComputDbEntities1 = new TenderComputDBEntities1();
                var oTenders = oTenderComputDbEntities1.tblTenders.Select
                    (i =>
                    new
                    {
                        ID = i.ID,
                        Tenderno = i.TenderNo,
                        TenderName = i.TenderName,
                        Variance = i.Variance,
                        Average = i.Average,
                        UpperLimit = i.UpperLimit,
                        BottomLimit = i.BottomLimit
                    }).ToList();
                grdTender.DataSource = oTenders;
                grdTender.DataBind();
            }
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }
        }

        protected void btnSelect_OnClick(object sender, EventArgs e)
        {

            var row = ((GridViewRow)((Control)sender).NamingContainer);
            var rowindex = row.RowIndex;
            var dataKey = grdTender.DataKeys[rowindex];
            Debug.Assert(dataKey != null, "dataKey != null");
            var key = int.Parse(dataKey.Value.ToString());

            TenderComputDBEntities1 oTenderComputDbEntities1 = null;
            try
            {
                oTenderComputDbEntities1 = new TenderComputDBEntities1();
                var companies = oTenderComputDbEntities1.tblComponies.Where(i => i.TenderID == key).ToList();
                grdCompanies.DataSource = companies;
                grdCompanies.DataBind();
                foreach (GridViewRow tenderRow in grdTender.Rows)
                {
                    tenderRow.CssClass = "";
                }
                row.CssClass = "highlight";
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}