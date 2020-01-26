<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TenderComp.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>سامانه تعیین دامنه قیمتهای مناسب در مناقصات</title>
    <script src="assets/js/jquery.min.js"></script>
    <link rel="stylesheet" href="css/st.css" />
    <link href="css/chosen.css" rel="stylesheet" />
    <link href="css/kendo.common.min.css" rel="stylesheet" />
    <link href="css/kendo.default.min.css" rel="stylesheet" />
    <link href="css/all.min.css" rel="stylesheet" />
    <script src="src/chosen.jquery.js" type="text/javascript"></script>
    <script src="src/JalaliDate.js"></script>
    <script src="src/kendo.web.js"></script>
    <script src="src/fa-IR.js"></script>
    <script src="src/kendo.datepickershamsi.js"></script>
    <script src="src/kendo.calendarshamsi.js"></script>
    <%--NUMBER VALIDATION--%>
    <script type="text/javascript">
        function golocation(section) {
            window.location.hash = section;
        }
        function functionx(evt) {
            if (evt.charCode > 31 && (evt.charCode < 48 || evt.charCode > 57)) {
                alert("فقط عدد وارد کنید");
                return false;
            }
        }
        function convertDigitIn(enDigit) { // PERSIAN, ARABIC, URDO
            var newValue = "";
            for (var i = 0; i < enDigit.length; i++) {
                var ch = enDigit.charCodeAt(i);
                if (ch >= 48 && ch <= 57) {
                    // european digit range
                    var newChar = ch + 1584;
                    newValue = newValue + String.fromCharCode(newChar);
                }
                else
                    newValue = newValue + String.fromCharCode(ch);
            }
            return newValue;
        }
        function Comma(Num) { //function to add commas to textboxes
            Num += '';
            Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
            Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
            x = Num.split('.');
            x1 = x[0];
            x2 = x.length > 1 ? '.' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1))
                x1 = x1.replace(rgx, '$1' + ',' + '$2');
            return x1 + x2;
        }
        function ontextchange0() {
            var text = document.getElementById("txtPrice").value;
            document.getElementById("lblPrice").innerText = convertDigitIn(Comma(text));
        }
        function ontextchange1() {
            var text = document.getElementById("txtTazmin").value;
            document.getElementById("lblTazmin").innerText = convertDigitIn(Comma(text));
        }
        function ontextchange2() {
            var text = document.getElementById("txtBaravord").value;
            document.getElementById("lblBaravord").innerText = convertDigitIn(Comma(text));
        }
        function ontextchange3() {
            var text = document.getElementById("txtNesab").value;
            document.getElementById("lblNesab").innerText = convertDigitIn(Comma(text));
        }
        function ontextchange4() {
            var text = document.getElementById("txtBaravordCurrency").value;
            document.getElementById("lblBaravordCurrency").innerText = convertDigitIn(Comma(text));
        }
    </script>
    <%--DATE PICKER--%>
    <script>
        $(document).ready(function () {

            // create DatePicker from input HTML element
            $("#txtTenderDate").kendoDatePickerShamsi({ culture: "fa-IR" });
        });
    </script>
    <%--PRINT--%>
    <script>
        function printDiv(divId, divId2) {
            //Get the HTML of div
            var divElements = document.getElementById(divId).innerHTML;
            divElements += document.getElementById(divId2).innerHTML;
            //Get the HTML of whole page
            var oldPage = document.body.innerHTML;

            //Reset the page's HTML with div's HTML only
            document.body.innerHTML = "" + divElements + "";

            //Print Page
            window.print();

            //Restore orignal HTML
            document.body.innerHTML = oldPage;

            //go to current section
            $("#sectionD").get(0).scrollIntoView();

        }
    </script>
    <%--SCROLL--%>
    <script type="text/javascript">
        function scroll_to_div(div_id) {
            $('html,body').animate(
                {
                    scrollTop: $("#" + div_id).offset().top
                },
                2000);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="content">
            <a href="logout.aspx">
                <div class="out">خروج</div>
            </a>
            <div id="sectionA">
                <div class="effect">
                    <div class="contA">
                        <div class="contBackground">
                            <div class="logo">
                                <img src="images/bg.png" />
                                <div class="logotxt">
                                    <p>شرکت مهندسی و توسعه گاز ایران</p>
                                    <p>سامانه تعیین دامنه قیمتهای مناسب پیشنهادی در مناقصات</p>
                                </div>
                            </div>
                            <div class="btn">
                                <asp:Button runat="server" ID="btnNewTen" OnClick="btnNewTen_Click" Text="مناقصه جدید" />
                            </div>
                            <div class="formfield">
                                <asp:TextBox ID="txtfindTenderName" runat="server" autocomplete="off" CssClass="inputlogin dirl" />
                                <asp:Button runat="server" ID="btnFindTenderNo" OnClick="btnFindTenderNo_OnClick" Text="" CssClass="sebtn fa" />
                            </div>
                            <%--<p>برای ویرایش لطفا شماره مناقصه را انتخاب نمایید.</p>--%>
                            <div class="formedit">
                                <asp:DropDownList placeholder="Choose a Country..." runat="server" ID="ddlTenderno" class="chzn-select" Style="width: 100%">
                                    <asp:ListItem Text=""></asp:ListItem>
                                </asp:DropDownList>
                                <script type="text/javascript">
                                    $(".chzn-select").chosen();
                                    $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
                                </script>
                                <p>
                                    <asp:RequiredFieldValidator runat="server" ID="rvldTenderNo" ControlToValidate="ddlTenderno" ValidationGroup="3" Text="*" ForeColor="#ff8c00" />
                                </p>
                            </div>

                            <div class="btn">
                                <asp:Button runat="server" ID="btnEditeTender" Text="ویرایش مناقصه" OnClick="btnEditeTender_OnClick" ValidationGroup="3" />
                            </div>
                        </div>
                    </div>
                </div>
                <!--<div class="sectionContent Content_A effect">

                </div>-->
            </div>
            <br />
            <hr />
            <br />
            <div id="sectionB">
                <div class="effect">
                    <div class="contB">
                        <div class="contBackground">
                            <div class="side">
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
                            </div>
                            <div class="allbox allboxB">
                                <p>لطفا مشخصات مناقصه را وارد نمایید</p>
                                <div class="box">
                                    <div class="lbl1"><span>شماره مناقصه</span></div>
                                    <div class="val1">
                                        <asp:RequiredFieldValidator ID="vldTenderno" Text="█" ControlToValidate="txtTenderno" runat="server" ValidationGroup="2" />
                                    </div>
                                    <div class="control">
                                        <asp:TextBox runat="server" ID="txtTenderno" CssClass="input" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="val2">
                                        <asp:CustomValidator runat="server" ID="cvlTenderno" ControlToValidate="txtTenderno" ValidationGroup="2" OnServerValidate="cvlTenderno_ServerValidate" Text="شماره مناقصه تکراری است" ForeColor="red" />
                                    </div>
                                </div>
                                <div class="box">
                                    <div class="lbl1"><span>نام مناقصه</span></div>
                                    <div class="val1">
                                        <asp:RequiredFieldValidator ID="vldTenderName" Text="█" ControlToValidate="txtTendername" runat="server" ValidationGroup="2" />
                                    </div>
                                    <div class="control">
                                        <asp:TextBox runat="server" ID="txtTenderName" CssClass="input" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="val2"></div>
                                </div>
                                <div class="box">
                                    <div class="lbl1"><span>نوع مناقصه</span></div>
                                    <div class="val1"><span></span></div>
                                    <div class="control">
                                        <asp:DropDownList ID="drpTenderType" runat="server" AutoPostBack="true" class="select" OnTextChanged="drpTenderType_TextChanged">
                                            <asp:ListItem Text="یک مرحله ای" Value="false"></asp:ListItem>
                                            <asp:ListItem Text="دو مرحله ای" Value="true"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="val2"><span></span></div>
                                </div>
                                <div class="box">
                                    <div class="lbl1"><span>مبلغ تضمین</span></div>
                                    <div class="val1">
                                        <asp:RequiredFieldValidator ID="vldTazmin" Text="█" ControlToValidate="txtTazmin" runat="server" ValidationGroup="2" />
                                    </div>
                                    <div class="control">
                                        <asp:TextBox autocomplete="off" runat="server" ID="txtTazmin" onkeypress="return functionx(event)" onblur="ontextchange1();" onkeyup="ontextchange1();" class="input"></asp:TextBox>
                                    </div>
                                    <div class="val2">
                                        <asp:Label runat="server" ID="lblTazmin"></asp:Label><span>ریال</span>
                                    </div>
                                </div>
                                <div class="box">
                                    <div class="lbl1">
                                        <span>برآورد
                                <asp:Label runat="server" ID="lblCurrencyRiali" Text="ریالی" />
                                            بهنگام کارفرما</span>
                                    </div>
                                    <div class="val1">
                                        <asp:RequiredFieldValidator ID="vldBaravord" Text="█" ControlToValidate="txtBaravord" runat="server" ValidationGroup="2" />
                                    </div>
                                    <div class="control">
                                        <asp:TextBox autocomplete="off" runat="server" ID="txtBaravord" onkeypress="return functionx(event)" onblur="ontextchange2();" onkeyup="ontextchange2();" CssClass="input" />
                                    </div>
                                    <div class="val2">
                                        <asp:Label runat="server" ID="lblBaravord"></asp:Label><span>ریال</span>
                                    </div>
                                </div>

                                <div class="box">
                                    <div class="lbl1"><span>نصاب معاملات متوسط</span></div>
                                    <div class="val1">
                                        <asp:RequiredFieldValidator runat="server" ID="vldnesab" ControlToValidate="txtNesab" ValidationGroup="2" Text="█"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="control">
                                        <asp:TextBox autocomplete="off" runat="server" ID="txtNesab" onkeypress="return functionx(event)" onblur="ontextchange3();" onkeyup="ontextchange3();" CssClass="input" />
                                    </div>
                                    <div class="val2">
                                        <asp:Label runat="server" ID="lblNesab"></asp:Label><span>ریال</span>
                                    </div>
                                </div>
                                <div class="box" runat="server" visible="False" id="divTenderType">
                                    <div class="lbl1"><span>ضریب تاثیر</span></div>
                                    <div class="val1">
                                        <asp:RequiredFieldValidator ID="vldi" Text="█" ControlToValidate="txti" runat="server" ValidationGroup="2" />
                                    </div>
                                    <div class="control">
                                        <asp:TextBox autocomplete="off" runat="server" ID="txti" CssClass="input" />
                                    </div>
                                    <div class="val2">
                                        <%--                                <asp:RegularExpressionValidator ForeColor="#ff8c00" runat="server" ID="rvldi" Text="باید بین 0.1 تا 0.4 باشد" ValidationExpression="\b([0]\.[1-3][0-9]{1,2})|([0]\.[1-4]{1})\b" ControlToValidate="txti" ValidationGroup="2" />--%>
                                    </div>
                                </div>
                                <div class="box">
                                    <div class="lbl1"><span>تاریخ مناقصه </span></div>
                                    <div class="val1">
                                        <asp:RequiredFieldValidator ID="vldTenderDate" Text="█" ControlToValidate="txtTenderDate" runat="server" ValidationGroup="2" />
                                    </div>
                                    <div class="control">
                                        <asp:TextBox autocomplete="off" runat="server" ID="txtTenderDate" CssClass="input test" />
                                    </div>
                                    <div class="val2"><span></span></div>
                                </div>
                                <div class="box">
                                    <div class="lbl1"><span>اهمیت مناقصه</span></div>
                                    <div class="val1">
                                        <asp:RequiredFieldValidator ID="vldt" ForeColor="#ff8c00" Text="*" ControlToValidate="txtt" runat="server" ValidationGroup="2" />
                                    </div>
                                    <div class="control">
                                        <asp:TextBox autocomplete="off" runat="server" ID="txtt" CssClass="input" />
                                    </div>
                                    <div class="val2">
                                        <%--                                <asp:RegularExpressionValidator ForeColor="#ff8c00" runat="server" ID="rvldt" Text="باید بین 0.9 تا 1.5 باشد" ValidationExpression="\b([0]\.[9]{1})|([1]\.[0-5]{1})|([1])\b" ControlToValidate="txtt" ValidationGroup="2" />--%>
                                    </div>
                                </div>
                                <div class="box">
                                    <div class="lbl1"><span>ارزی/ ریالی</span></div>
                                    <div class="val1"><span></span></div>
                                    <div class="control">
                                        <asp:DropDownList runat="server" ID="drpCurrency" AutoPostBack="True" class="select" OnTextChanged="drpCurrency_OnTextChanged">
                                            <asp:ListItem Text="ریالی" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="ارزی" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="ارزی/ریالی" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="val2"><span></span></div>
                                </div>
                                <div class="box" id="divCurrencyPrice" runat="server" visible="False">
                                    <div class="lbl1"><span>قیمت ارز</span></div>
                                    <div class="val1">
                                        <asp:RequiredFieldValidator runat="server" ID="rvldCurrencyprice" ControlToValidate="txtCurrencyPrice" Text="█" />
                                    </div>
                                    <div class="control">
                                        <asp:TextBox autocomplete="off" runat="server" ID="txtCurrencyPrice" CssClass="input"></asp:TextBox>
                                    </div>
                                    <div class="val2"><span></span></div>
                                </div>
                                <div class="box" runat="server" id="divBaravordCurrency" visible="false">
                                    <div class="lbl1"><span>برآورد ارزی بهنگام کارفرما</span></div>
                                    <div class="val1">
                                        <asp:RequiredFieldValidator ID="vldBaravordCurrency" Text="█" ControlToValidate="txtBaravordCurrency" runat="server" ValidationGroup="2" />
                                    </div>
                                    <div class="control">
                                        <asp:TextBox autocomplete="off" runat="server" ID="txtBaravordCurrency" onkeypress="return functionx(event)" onblur="ontextchange4();" onkeyup="ontextchange4();" CssClass="input" />
                                    </div>
                                    <div class="val2">
                                        <asp:Label runat="server" ID="lblBaravordCurrency" />
                                        <span></span>
                                    </div>
                                </div>
                                <div class="box btn">
                                    <input type="button" class="scroll_button" value="مرحله قبل" onclick="scroll_to_div('sectionA')" />
                                    <div class="blnk"></div>
                                    <asp:Button runat="server" ID="btnEnter" OnClick="btnEnter_OnClick" Text="ذخیره" ValidationGroup="2" />
                                    <div class="blnk"></div>
                                    <input type="button" class="scroll_button" value="مرحله بعد" onclick="scroll_to_div('sectionC')" />
                                </div>
                                <div runat="server" class="lbl" id="divSuccess" visible="False">
                                    <asp:Label runat="server" ID="lblSuccess"></asp:Label>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <hr />
            <br />
            <div id="sectionC">
                <div class="effect">
                    <div class="contC">
                        <div class="contBackground">
                            <div class="allbox allboxC">
                                <p>لطفا مشخصات شرکت کنندگان در مناقصه را وارد نمایید</p>
                                <div class="box">
                                    <div class="lbl1"><span>نام شرکت :</span></div>
                                    <div class="val1">
                                        <asp:RequiredFieldValidator ID="vldCompanyName" Text="█" ControlToValidate="txtComponyName" runat="server" ValidationGroup="1" />
                                    </div>
                                    <div class="control">
                                        <asp:TextBox autocomplete="off" runat="server" ID="txtComponyName" CssClass="input"></asp:TextBox>
                                    </div>
                                    <div class="val2"><span></span></div>
                                </div>
                                <div class="box">
                                    <div class="lbl1"><span>قیمت پشنهادی :</span></div>
                                    <div class="val1">
                                        <%-- <asp:RequiredFieldValidator ID="vldPrice" ForeColor="#ff8c00" Text="*" ControlToValidate="txtPrice" runat="server" ValidationGroup="1" />--%>
                                    </div>
                                    <div class="control">
                                        <asp:TextBox autocomplete="off" runat="server" ID="txtPrice" onkeypress="return functionx(event)" onblur="ontextchange0();" onkeyup="ontextchange0();" CssClass="input"></asp:TextBox>
                                    </div>
                                    <div class="val2">
                                        <asp:Label runat="server" ID="lblPrice"></asp:Label><span>ریال</span>
                                    </div>
                                </div>
                                <div class="box" id="divCurrencyPrice2" runat="server" visible="False">
                                    <div class="lbl1"><span>قیمت ارزی :</span></div>
                                    <div class="val1">
                                        <%--<asp:RequiredFieldValidator ID="valCurrencyPrice2" ForeColor="#ff8c00" Text="*" ControlToValidate="txtCurrencyPrice2" runat="server" ValidationGroup="1" />--%>
                                    </div>
                                    <div class="control">
                                        <asp:TextBox autocomplete="off" runat="server" ID="txtCurrencyPrice2" onkeypress="return functionx(event)" onblur="ontextchange0();" onkeyup="ontextchange0();" CssClass="input"></asp:TextBox>
                                    </div>
                                    <div class="val2">
                                        <%-- <asp:Label runat="server" ID="Label1"></asp:Label><span>ریال</span>--%>
                                    </div>
                                </div>
                                <div class="box" id="divEmtiazFani" runat="server" visible="False">
                                    <div class="lbl1"><span>امتیاز فنی :</span></div>
                                    <div class="val1">
                                        <%--<asp:RequiredFieldValidator ID="vldEmtiazFani" ForeColor="#ff8c00" Text="*" ControlToValidate="txtEmtiazFani" runat="server" ValidationGroup="1" />--%>
                                    </div>
                                    <div class="control">
                                        <asp:TextBox autocomplete="off" runat="server" ID="txtEmtiazFani" CssClass="input" />
                                    </div>
                                    <div class="val2">
                                        <asp:RegularExpressionValidator ForeColor="" runat="server" ID="RvldEmtiazFani" Text="باید بین 50 تا 100 باشد" ValidationExpression="\b([5-9]{1}[0-9]{1}\.[0-9]{1,2})|100|([5-9]{1}[0-9]{1})\b" ControlToValidate="txtEmtiazFani" ValidationGroup="1" />
                                    </div>
                                </div>
                                <div class="val2" runat="server" visible="False" id="divReqTender">
                                    <span style="color: #ff8c00">مشخصات مناقصه وارد  یا ذخیره نشده است</span>
                                </div>
                                <div class="box btn">
                                    <asp:Button runat="server" ID="btnAddcompony" Text="اضافه" OnClick="InsertCompony" ValidationGroup="1" />

                                </div>
                                <br />
                                <p>لیست شرکت کنندگان</p>
                                <div class="gridv" id="printablediv">
                                    <asp:GridView ID="GrdComponies" OnRowEditing="GrdComponies_RowEditing" OnRowUpdating="GrdComponies_RowUpdating" OnRowCancelingEdit="GrdComponies_RowCancelingEdit" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" EmptyDataText="هیچ آیتمی وجود ندارد" BorderWidth="0" EmptyDataRowStyle-CssClass="gridtxt">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-CssClass="gridcheck" HeaderStyle-CssClass="gridheader">
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="chkRow" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="نام شرکت" ItemStyle-CssClass="gridname" HeaderStyle-CssClass="gridheader">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("ComponyName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtEditName" runat="server" Text='<%# Eval("ComponyName") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="قیمت ارزی" ItemStyle-CssClass="gridprice" HeaderStyle-CssClass="gridheader">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCurrencyPrice" runat="server" Text='<%#  Eval("CurrencyPrice", "{0:#,0}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtEditCurrencyPriece" runat="server" Text='<%#  Eval("CurrencyPrice", "{0:#,0}") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="قیمت ریالی" ItemStyle-CssClass="gridprice" HeaderStyle-CssClass="gridheader">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRialiPrice" runat="server" Text='<%#  Eval("RialiPrice", "{0:#,0}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtEditRialiPrice" runat="server" Text='<%#  Eval("RialiPrice", "{0:#,0}") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="جمع ارزی و ریالی" ItemStyle-CssClass="gridprice" HeaderStyle-CssClass="gridheader">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrice" runat="server" Text='<%#  Eval("Price", "{0:#,0}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="امتیاز فنی" ItemStyle-CssClass="gridtechnical" HeaderStyle-CssClass="gridheader">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmtiazFani" runat="server" Text='<%# Eval("t") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtEditEmtiazFani" runat="server" Text='<%# Eval("t") %>'></asp:TextBox>
                                                    <span>
                                                        <asp:RequiredFieldValidator ID="vldEditEmtiazFani" ForeColor="#ff8c00" Text="*" ControlToValidate="txtEditEmtiazFani" runat="server" ValidationGroup="3" /></span>
                                                    <span>
                                                        <asp:RegularExpressionValidator ForeColor="#c58d21" runat="server" ID="RvldEditEmtiazFani" Text="باید بین 50 تا 100 باشد" ValidationExpression="\b([5-9]{1}[0-9]{1}\.[0-9]{1,2})|100|([5-9]{1}[0-9]{1})\b" ControlToValidate="txtEditEmtiazFani" ValidationGroup="3" /></span>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="قیمت تراز شده" ItemStyle-CssClass="gridtaraz" HeaderStyle-CssClass="gridheader">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTarazShode" runat="server" Text='<%# Eval("L", "{0:#,0}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="درصد اختلاف از برآورد" ItemStyle-CssClass="gridtaraz" HeaderStyle-CssClass="gridheader">
                                                <ItemTemplate>
                                                    <asp:Label ID="IeDiff" runat="server" Text='<%# Eval("IeDiff", "{0:#.##}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="شاخص" ItemStyle-CssClass="gridtaraz" HeaderStyle-CssClass="gridheader">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblX" runat="server" Text='<%# Eval("X", "{0:#.###}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="برنده" ItemStyle-CssClass="gridtaraz" HeaderStyle-CssClass="gridheader" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIswin" runat="server" Text='<%# Eval("Iswin") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="شرح" ItemStyle-CssClass="griddes" HeaderStyle-CssClass="gridheader">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblComment" runat="server" Text='<%# Eval("Comment") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ButtonType="Link" ShowEditButton="true" ControlStyle-ForeColor="White" EditText="ویرایش" CancelText="لغو" UpdateText="ذخیره" ValidationGroup="3" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="gridv" id="printablediv2">
                                    <asp:GridView runat="server" ID="grdTenderProperties" AutoGenerateColumns="False" EmptyDataText="هیچ آیتمی وجود ندارد" BorderWidth="0" EmptyDataRowStyle-CssClass="gridtxt">
                                        <Columns>
                                            <asp:TemplateField HeaderText="شماره مناقصه" ItemStyle-CssClass="griddes" HeaderStyle-CssClass="gridheader">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTenderNo" runat="server" Text='<%# Eval("Tenderno") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="نام مناقصه" ItemStyle-CssClass="griddes" HeaderStyle-CssClass="gridheader">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTenderName" runat="server" Text='<%# Eval("TenderName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="انحراف از معیار" ItemStyle-CssClass="griddes" HeaderStyle-CssClass="gridheader">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVariance" runat="server" Text='<%# Eval("Variance" ,"{0:#.##}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="میانگین" ItemStyle-CssClass="griddes" HeaderStyle-CssClass="gridheader">
                                                <ItemTemplate>
                                                    <asp:Label ID="Average" runat="server" Text='<%# Eval("Average", "{0:#.##}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="حدبالا" ItemStyle-CssClass="griddes" HeaderStyle-CssClass="gridheader">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUpperLimit" runat="server" Text='<%# Eval("UpperLimit", "{0:#.##}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="حدپایین" ItemStyle-CssClass="griddes" HeaderStyle-CssClass="gridheader">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBottomLimit" runat="server" Text='<%# Eval("BottomLimit" ,"{0:#.##}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="box btn">
                                    <div class="blnk"></div>
                                    <input type="button" class="scroll_button" value="مرحله قبل" onclick="scroll_to_div('sectionB')" />
                                    <div class="blnk"></div>
                                    <asp:Button runat="server" ID="btnDelete" Text="حذف" OnClick="DeleteItem" />
                                    <div class="blnk"></div>
                                    <asp:Button runat="server" ID="btnDeleteAll" Text="حذف همه" OnClick="btnDeleteAll_OnClick" />
                                    <div class="blnk"></div>
                                    <input type="button" class="scroll_button" value="مرحله بعد" onclick="scroll_to_div('sectionD')" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <hr />
            <br />
            <div id="sectionD">
                <div class="effect">
                    <div class="contD">
                        <div class="contBackground">
                            <div class="allbox allboxD">
                                <p>شرکتهای مجاز</p>
                                <div class="box">
                                    <asp:Label runat="server" ID="lblt" />
                                </div>
                                <div class="box">
                                    <asp:Label runat="server" ID="lblnullData" Visible="False" ForeColor="#1c4e6b" Text="اطلاعات ناقص است" />
                                </div>
                                <div class="box btn">
                                    <div class="blnk"></div>
                                    <input type="button" class="scroll_button" value="مرحله قبل" onclick="scroll_to_div('sectionC')" />
                                    <div class="blnk"></div>
                                    <asp:Button runat="server" ID="btnComput" OnClick="btnComput_OnClick" Text="محاسبه برنده" />
                                    <div class="blnk"></div>
                                    <%--                        <div class="btnlink" onclick="printDiv('printablediv', 'printablediv2')">چاپ نتیجه</div>--%>
                                    <asp:Button runat="server" CssClass="btnlink" ID="btnSendPrint" Text="چاپ نتیجه" OnClick="btnSendPrint_OnClick" OnClientClick="document.forms[0].target='_blank';" />
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
