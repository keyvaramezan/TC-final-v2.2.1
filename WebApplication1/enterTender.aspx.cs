using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TenderComp.InfraStructure;

namespace TenderComp
{
    public partial class enterTender : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void drpTenderType_TextChanged(object sender, EventArgs e)
        {
            if (drpTenderType.SelectedValue == "1")
            {
                spni.Visible = true;
                //spnEmtiazFani.Visible = true;
                txti.Visible = true;
                //txtEmtiazFani.Visible = true;
                //vldEmtiazFani.Visible = true;
                vldi.Visible = true;
            }
            else
            {
                spni.Visible = false;
                //spnEmtiazFani.Visible = false;
                txti.Visible = false;
                //txtEmtiazFani.Visible = false;
                //vldEmtiazFani.Visible = false;
                vldi.Visible = false;
            }
        }

        protected void btnEnter_OnClick(object sender, EventArgs e)
        {
            // ReSharper disable once NotAccessedVariable
            TenderComputDBEntities1 oTenderComputDbEntities1 = null;
            try
            {
                oTenderComputDbEntities1 = new TenderComputDBEntities1();
                tblTender oTblTender = new tblTender();
                bool type;
                if (drpTenderType.SelectedValue == "0")
                {
                    type = false;
                }
                else
                {
                    type = true;
                    oTblTender.i = Convert.ToDouble(txti.Text);
                }
                oTblTender.Fguaranty = Convert.ToDouble(txtTazmin.Text);
                oTblTender.TenderType = type;
                oTblTender.Estimate = Convert.ToDouble(txtBaravord.Text);
                oTblTender.t = Convert.ToDouble(txtt.Text);
                oTblTender.TenderNo = Convert.ToInt32(txtTenderno.Text);
                InfraStructure.DateConvertor oDateConvertor = new DateConvertor();
                oTblTender.TenderDate = oDateConvertor.Shamsitomiladi(txtTenderDate.Text);
                oTenderComputDbEntities1.tblTenders.Add(oTblTender);
                oTenderComputDbEntities1.SaveChanges();

            }
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }
        }
        
    }
}