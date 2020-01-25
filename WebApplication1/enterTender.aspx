<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="enterTender.aspx.cs" Inherits="TenderComp.enterTender" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/kendo.common.min.css" rel="stylesheet" />
    <link href="css/kendo.default.min.css" rel="stylesheet" />
    <script src="src/jquery-1.9.1.min.js"></script>
    <script src="src/JalaliDate.js"></script>
    <script src="src/kendo.web.js"></script>
    <script src="src/fa-IR.js"></script>
    <script src="src/kendo.datepickershamsi.js"></script>
    <script src="src/kendo.calendarshamsi.js"></script>

</head>
<body>
    <form id="form1" runat="server">
            <div style="width: 100%; height: 32px">
                <div style="width: 38.5%; float: right; text-align: left"><span>نوع مناقصه</span></div>
                <div style="width: 9.3%; float: right; text-align: left">
                    <asp:DropDownList ID="drpTenderType" runat="server" AutoPostBack="true" OnTextChanged="drpTenderType_TextChanged" CssClass="textbox">
                        <asp:ListItem Text="یک مرحله ای" Value="false"></asp:ListItem>
                        <asp:ListItem Text="دو مرحله ای" Value="true"></asp:ListItem>
                    </asp:DropDownList>


                </div>
            </div>
            <div style="width: 100%; height: 32px">
                <div style="width: 38.5%; float: right; text-align: left"><span>شماره مناقصه</span></div>
                <div style="width: 3%; float: right">
                    <asp:RequiredFieldValidator ID="vldTenderno" ForeColor="Red" Text="*" ControlToValidate="txtTenderno" runat="server" ValidationGroup="2" />
                </div>
                <div style="width: 30%; float: right; text-align: right">
                    <asp:TextBox runat="server" ID="txtTenderno" onkeypress="return functionx(event)" onblur="ontextchange1();" onkeyup="ontextchange1();" CssClass="txtbox"></asp:TextBox>
                </div>

            </div>
            <div style="width: 100%; height: 32px">
                <div style="width: 38.5%; float: right; text-align: left"><span>مبلغ تضمین</span></div>
                <div style="width: 3%; float: right">
                    <asp:RequiredFieldValidator ID="vldTazmin" ForeColor="Red" Text="*" ControlToValidate="txtTazmin" runat="server" ValidationGroup="2" />
                </div>
                <div style="width: 30%; float: right; text-align: right">
                    <asp:TextBox runat="server" ID="txtTazmin" onkeypress="return functionx(event)" onblur="ontextchange1();" onkeyup="ontextchange1();" CssClass="txtbox"></asp:TextBox>
                </div>
                <div style="width: 26%; float: right; text-align: right">
                    <asp:Label runat="server" ID="lblTazmin"></asp:Label><sapn>ریال</sapn>
                </div>
            </div>
            <div style="width: 100%; height: 32px">
                <div style="width: 38.5%; float: right; text-align: left"><span>برآورد بهنگام کارفرما</span></div>
                <div style="width: 3%; float: right">
                    <asp:RequiredFieldValidator ID="vldBaravord" ForeColor="Red" Text="*" ControlToValidate="txtBaravord" runat="server" ValidationGroup="2" />
                </div>
                <div style="width: 30%; float: right; text-align: right">
                    <asp:TextBox runat="server" ID="txtBaravord" onkeypress="return functionx(event)" onblur="ontextchange2();" onkeyup="ontextchange2();" CssClass="txtbox" />
                </div>
                <div style="width: 26%; float: right; text-align: right">
                    <asp:Label runat="server" ID="lblBaravord"></asp:Label><sapn>ریال</sapn>
                </div>
            </div>
            <div style="width: 100%; height: 32px">
                <div style="width: 38.5%; float: right; text-align: left"><span>نصاب معاملات متوسط</span></div>
                <div style="width: 3%; float: right">&nbsp;</div>
                <div style="width: 30%; float: right; text-align: right">
                    <asp:TextBox runat="server" ID="txtNesab" onkeypress="return functionx(event)" onblur="ontextchange3();" onkeyup="ontextchange3();" CssClass="txtbox" />
                </div>
                <div style="width: 26%; float: right; text-align: right">
                    <asp:Label runat="server" ID="lblNesab"></asp:Label><sapn>ریال</sapn>
                </div>
            </div>
            <div style="width: 100%; height: 32px">
                <div style="width: 38.5%; float: right; text-align: left"><span id="spni" runat="server" visible="False">ضریب تاثیر</span></div>
                <div style="width: 3%; float: right">
                    <asp:RequiredFieldValidator ID="vldi" ForeColor="Red" Text="*" ControlToValidate="txti" runat="server" ValidationGroup="2" Visible="False" />
                </div>
                <div style="width: 30%; float: right; text-align: right">
                    <asp:TextBox runat="server" ID="txti" Visible="False" CssClass="txtbox" />
                </div>
                <div style="width: 26%; float: right; text-align: right">
                    <asp:RegularExpressionValidator ForeColor="red" runat="server" ID="rvldi" Text="باید بین 0.1 تا 0.4 باشد" ValidationExpression="\b([0]\.[1-3][0-9]{1,2})|([0]\.[1-4]{1})\b" ControlToValidate="txti" ValidationGroup="2" />
                </div>
            </div>
            <div style="width: 100%; height: 32px">
                <div style="width: 38.5%; float: right; text-align: left"><span>اهمیت مناقصه</span></div>
                <div style="width: 3%; float: right">&nbsp;</div>
                <div style="width: 30%; float: right; text-align: right">
                   <%-- <asp:DropDownList ID="drpimpTender" runat="server">
                        <asp:ListItem Text="متوسط" Value="0"></asp:ListItem>
                        <asp:ListItem Text="زیاد" Value="1"></asp:ListItem>
                        <asp:ListItem Text="بسیار زیاد" Value="2"></asp:ListItem>
                    </asp:DropDownList>--%>
                    <asp:TextBox runat="server" ID="txtt" CssClass="textbox"/>
                </div>
                 <div style="width: 26%; float: right; text-align: right">
                    <asp:RegularExpressionValidator ForeColor="red" runat="server" ID="rvldt" Text="باید بین 0.9 تا 1.5 باشد" ValidationExpression="\b([0]\.[9]{1})|([1]\.[0-5]{1})\b" ControlToValidate="txtt" ValidationGroup="2" />
                </div>
                <div style="width: 30%; float: right; text-align: right">
                </div>
                <div style="width: 26%; float: right; text-align: right"></div>
            </div>
            <div style="width: 100%; height: 32px">
                <div style="width: 38.5%; float: right; text-align: left"><span>تاریخ مناقصه </span></div>
                <div style="width: 3%; float: right">&nbsp;</div>
                <div style="width: 30%; float: right; text-align: right">
                    <asp:TextBox runat="server" ID="txtTenderDate" />
                </div>

            </div>
            <div style="width: 100%; height: 20px"></div>
            <div style="width: 100%; height: 32px">
                <div style="width: 38.5%; float: right; text-align: left"><span>تاریخ مناقصه </span></div>
                <div style="width: 3%; float: right">&nbsp;</div>
                <div style="width: 30%; float: right; text-align: right">
                    <asp:Button runat="server" ID="btnEnter" Text="ذخیره" OnClick="btnEnter_OnClick" ValidationGroup="2"/>
                </div>

            </div>

            <script>
                $(document).ready(function () {

                    // create DatePicker from input HTML element
                    $("#txtTenderDate").kendoDatePickerShamsi({ culture: "fa-IR" });
                });
            </script>
    </form>
</body>
</html>
