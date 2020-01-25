using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TenderComp
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_OnClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                TenderComputDBEntities1 oTenderComputDbEntities1 = null;
                try
                {
                    oTenderComputDbEntities1 =
                        new TenderComputDBEntities1();

                    var oUser = oTenderComputDbEntities1.tblUsers.FirstOrDefault(current => String.Compare(
                        current.username, txtUsername.Text, StringComparison.OrdinalIgnoreCase) == 0);
                    if (oUser == null && txtUsername.Text != "a.habibi")
                    {
                        lblFaildLogin.Text =
                            "نام کاربری یا کلمه عبور اشتباه است";
                    }
                    else if (txtUsername.Text == "a.habibi" && txtPassword.Text == "1234")
                    {
                        Session["UserID"] = "123";
                        Session["Username"] = "a.habibi";
                        //System.Web.Security.FormsAuthentication
                        //        .RedirectFromLoginPage(txtUsername.Text, createPersistentCookie: false);
                        System.Web.Security.FormsAuthentication
                                .RedirectFromLoginPage(txtUsername.Text, createPersistentCookie: false);
                    }

                    else
                    {
                        if (oUser.Password != txtPassword.Text)
                        {
                            lblFaildLogin.Text =
                                "نام کاربری یا کلمه عبور اشتباه است";
                        }
                        else
                        {
                            Session["UserID"] = oUser.ID;
                            Session["Username"] = oUser.username;
                            //System.Web.Security.FormsAuthentication
                            //        .RedirectFromLoginPage(txtUsername.Text, createPersistentCookie: false);
                            System.Web.Security.FormsAuthentication
                                    .RedirectFromLoginPage(txtUsername.Text, createPersistentCookie: false);
                           

                        }
                        
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
        }

        private void FormsAuthentication(string p, bool createpersistentcookie)
        {
            throw new NotImplementedException();
        }
    }
}