<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="TenderComp.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>سامانه تعیین دامنه قیمتهای مناسب پیشنهادی در مناقصات </title>
    <link rel="stylesheet" href="css/st.css" />
    <link href="css/all.min.css" rel="stylesheet"/>
    <link href="css/all.min.css" rel="stylesheet"/>
</head>
<body>
    <form id="frmLogin" runat="server" class="form-group">
        <div class="content">
            <div id="login">
                <div class="effect">
                    <div class="contLogin">
                        <div class="contBackground">
                            <div class="logo">
                                <img src="images/bg.png" />
                                <div class="logotxt">
                                    <p>شرکت مهندسی و توسعه گاز ایران</p>
                                    <br />
                                    <p>
                                        سامانه تعیین دامنه<br />
                                        مناسب پیشنهادی در مناقصات
                                    </p>
                                </div>
                            </div>
                            <div class="login">
                                <div class="logintext mid">
                                    <div class="arang">
                                        <p>ورود به سامانه</p>
                                    </div>
                                    <div class="loginUP arang">
                                        <div style="float: left"><i class="fas fa-user fa-3x"></i></div>
                                        <div style="width: 90%; float: right">
                                            <asp:TextBox ID="txtUsername" runat="server" CssClass="inputlogin dirl" />
                                            <asp:RequiredFieldValidator runat="server" ID="vldUsername" ControlToValidate="txtUsername" Text="*" ForeColor="#ff8c00" Font-Bold="true" Font-Size="10" />
                                        </div>
                                    </div>
                                    <div class="loginUP arang">
                                        <div style="float: left"><i class="fas fa-lock fa-3x"></i></div>
                                        <div style="width: 90%; float: right">
                                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="inputlogin dirl" />
                                            <asp:RequiredFieldValidator ID="vldPassword" runat="server" ControlToValidate="txtPassword" Text="*" ForeColor="#ff8c00" Font-Bold="true" Font-Size="10" />
                                        </div>
                                    </div>
                                    <div class="box btn arang">
                                        <asp:Button ID="btnLogin" runat="server" Text="ورود" OnClick="btnLogin_OnClick" />
                                    </div>
                                    <div>
                                        <asp:Label ID="lblFaildLogin" runat="server" ForeColor="#13dfe0" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
