<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnterComponies-v2.aspx.cs" Inherits="TenderComp.EnterComponiesv2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>سامانه تعیین دامنه قیمتهای مناسب پیشنهادی در مناقصات </title>
    <link rel="stylesheet" href="css/StyleSheet.css" />
    <script type="text/javascript" src="libs/jquery.min.js"></script>



    <script type="text/javascript">
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
    </script>

    <link rel="stylesheet" href="css/bootstrap.min1.css" />
    <script src="src/jquery-1.9.1.min.js"></script>
    <script src="src/bootstrap.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {
                localStorage.setItem('activeTab', $(e.target).attr('href'));
            });
            var activeTab = localStorage.getItem('activeTab');
            if (activeTab) {
                $('#myTab a[href="' + activeTab + '"]').tab('show');
            }
        });
    </script>

    <link rel="stylesheet" href="css/multi1.css" />
    <script type="text/javascript" src="src/multi.js"></script>
    <script type="text/javascript" src="src/jquery.easing.min.js"></script>




</head>
<body>
    <form id="msform" runat="server">
        <div class="content">
            <div class="logo">
                <img src="images/bg.png" />
                <div class="logotxt">
                    <p>شرکت مهندسی و توسعه گاز ایران</p>
                    <p style="font-weight: bold;font-size:11px;margin:0;padding:3px 0">سامانه تعیین دامنه قیمتهای مناسب پیشنهادی در مناقصات</p>
                </div>
            </div>
            <div class="bs-example">
                <ul style="display: none" class="nav nav-tabs" id="myTab">
                    <li class="active"><a data-toggle="tab" href="#sectionA">Section A</a></li>
                    <li><a data-toggle="tab" href="#sectionB">Section B</a></li>
                    <li><a data-toggle="tab" href="#sectionC">Section C</a></li>
                    <li><a data-toggle="tab" href="#sectionD">Section D</a></li>
                </ul>
                <div class="tab-content">
                    <fieldset id="sectionA" class="tab-pane fade in active">
                        <div class="box box1 tentype">
                            <p>لطفا نوع مناقصه را انتخاب نمایید</p>
                            <asp:DropDownList ID="drpTenderType" runat="server" AutoPostBack="true" OnTextChanged="drpTenderType_TextChanged">
                                <asp:ListItem Text="یک مرحله ای" Value="0"></asp:ListItem>
                                <asp:ListItem Text="دو مرحله ای" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="nav nav-tabs" id="myTab1">
                            <a data-toggle="tab" href="#sectionB">
                                <div class="action-button inp bot right">مرحله بعد</div>
                            </a>
                        </div>
                    </fieldset>
                    <fieldset id="sectionB" class="tab-pane fade">
                        <div class="box box2">
                            <p>لطفا مشخصات شرکت کنندگان در مناقصه را وارد نمایید</p>
                            <div class="tenpartiname">
                                <div style="width: 100%; height: 21px">
                                    <div style="width: 38.5%; float: right; text-align: left"><span>نام شرکت :</span></div>
                                    <div style="width: 3%; float: right">
                                        <asp:RequiredFieldValidator ID="vldCompanyName" ForeColor="Red" Text="*" ControlToValidate="txtComponyName" runat="server" ValidationGroup="1" />
                                    </div>
                                    <div style="width: 30%; float: right; text-align: right">
                                        <asp:TextBox runat="server" ID="txtComponyName" CssClass="txtbox"></asp:TextBox>
                                    </div>
                                    <div style="width: 26%; float: right; text-align: right"></div>
                                </div>
                                <div style="width: 100%; height: 21px">
                                    <div style="width: 38.5%; float: right; text-align: left"><span>قیمت پشنهادی :</span></div>
                                    <div style="width: 3%; float: right">
                                        <asp:RequiredFieldValidator ID="vldPrice" ForeColor="Red" Text="*" ControlToValidate="txtPrice" runat="server" ValidationGroup="1" />
                                    </div>
                                    <div style="width: 30%; float: right; text-align: right">
                                        <asp:TextBox runat="server" ID="txtPrice" onkeypress="return functionx(event)" onblur="ontextchange0();" onkeyup="ontextchange0();" CssClass="txtbox"></asp:TextBox>
                                    </div>
                                    <div style="width: 26%; float: right; text-align: right">
                                        <asp:Label runat="server" ID="lblPrice"></asp:Label><sapn>ریال</sapn>
                                    </div>
                                </div>
                                <div style="width: 100%; height: 21px">
                                    <div style="width: 38.5%; float: right; text-align: left"><span id="spnEmtiazFani" runat="server" visible="false">امتیاز فنی :</span></div>
                                    <div style="width: 3%; float: right">
                                        <asp:RequiredFieldValidator ID="vldEmtiazFani" ForeColor="Red" Text="*" ControlToValidate="txtEmtiazFani" runat="server" ValidationGroup="1" Visible="false" />
                                    </div>
                                    <div style="width: 30%; float: right; text-align: right">
                                        <asp:TextBox runat="server" ID="txtEmtiazFani" Visible="false" CssClass="txtbox" />
                                    </div>
                                    <div style="width: 26%; float: right; text-align: right">
                                        <asp:RegularExpressionValidator ForeColor="red" runat="server" ID="RvldEmtiazFani" Text="باید بین 50 تا 100 باشد" ValidationExpression="\b([5-9][0-9]{1,2}\.[0-9]{1,2})|100|([5-9][0-9]{1,2})\b" ControlToValidate="txtEmtiazFani" ValidationGroup="1" />
                                    </div>
                                </div>
                            </div>
                            <div class="btn">
                                <asp:Button runat="server" ID="btnAddcompony" Text="اضافه" OnClick="InsertCompony" ValidationGroup="1" />
                            </div>
                            <hr />
                            <p>لیست شرکت کنندگان</p>
                            <div class="gridv" id="scroll">
                                <asp:GridView ID="GrdComponies" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" EmptyDataText="هیچ آیتمی وجود ندارد" BorderWidth="0" EmptyDataRowStyle-CssClass="gridtxt" Width="100%">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-CssClass="gridcheck">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkRow" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="نام شرکت" ItemStyle-CssClass="gridname">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("ComponyName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="قیمت" ItemStyle-CssClass="gridprice">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPrice" runat="server" Text='<%#  Eval("Price", "{0:#,0}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="امتیاز فنی" ItemStyle-CssClass="gridtechnical">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmtiazFani" runat="server" Text='<%# Eval("t") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="قیمت تراز شده" ItemStyle-CssClass="gridtaraz">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTarazShode" runat="server" Text='<%# Eval("L", "{0:#,0}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="درصد اختلاف از برآورد" ItemStyle-CssClass="gridtaraz">
                                            <ItemTemplate>
                                                <asp:Label ID="IeDiff" runat="server" Text='<%# Eval("IeDiff") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="شرح" ItemStyle-CssClass="griddes">
                                            <ItemTemplate>
                                                <asp:Label ID="lblComment" runat="server" Text='<%# Eval("Comment") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="btn" style="margin-bottom:9px">
                                <asp:Button runat="server" ID="btnDelete" Text="حذف" OnClick="DeleteItem" />
                                <asp:Button runat="server" ID="btnDeleteAll" Text="حذف همه" OnClick="btnDeleteAll_OnClick"/>
                            </div>
                        </div>
                        <div class="nav nav-tabs" id="myTab2">
                            <a data-toggle="tab" href="#sectionA">
                                <div class="action-button inp bot left">مرحله قبل</div>
                            </a>
                            <a data-toggle="tab" href="#sectionC">
                                <div class="action-button inp bot right">مرحله بعد</div>
                            </a>
                        </div>
                    </fieldset>
                    <fieldset id="sectionC" class="tab-pane fade">
                        <div class="box box3 tencondi">
                            <p>لطفا شرایط مناقصه وارد نمایید</p>
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
                                <div style="width: 38.5%; float: right; text-align: left"><span id="spni" runat="server" visible="false">ضریب تاثیر</span></div>
                                <div style="width: 3%; float: right">
                                    <asp:RequiredFieldValidator ID="vldi" ForeColor="Red" Text="*" ControlToValidate="txti" runat="server" ValidationGroup="2" Visible="false" />
                                </div>
                                <div style="width: 30%; float: right; text-align: right">
                                    <asp:TextBox runat="server" ID="txti" Visible="false" CssClass="txtbox" />
                                </div>
                                <div style="width: 26%; float: right; text-align: right">
                                    <asp:RegularExpressionValidator ForeColor="red" runat="server" ID="rvldi" Text="باید بین 0.1 تا 0.4 باشد" ValidationExpression="\b([0]\.[1-3][0-9]{1,2})|([0]\.[1-4]{1})\b" ControlToValidate="txti" ValidationGroup="2" />
                                </div>
                            </div>
                            <div style="width: 100%; height: 32px">
                                <div style="width: 38.5%; float: right; text-align: left"><span>اهمیت مناقصه</span></div>
                                <div style="width: 3%; float: right">&nbsp;</div>
                                <div style="width: 30%; float: right; text-align: right">
                                    <asp:DropDownList ID="drpimpTender" runat="server">
                                        <asp:ListItem Text="متوسط" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="زیاد" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="بسیار زیاد" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div style="width: 26%; float: right; text-align: right"></div>
                            </div>
                            <div class="btn">
                                <asp:Button runat="server" ID="btnComput" Text="محاسبه برنده مناقصه" OnClick="btnComput_OnClick" ValidationGroup="2" Width="157" />
                            </div>
                        </div>
                        <div class="nav nav-tabs" id="myTab4">
                            <a data-toggle="tab" href="#sectionB">
                                <div class="action-button inp bot left">مرحله قبل</div>
                            </a>
                            <a data-toggle="tab" href="#sectionD">
                                <div class="action-button inp bot right">مرحله بعد</div>
                            </a>
                        </div>
                    </fieldset>
                    <fieldset id="sectionD" class="tab-pane fade">
                        <div class="box box4" data-jquery-page-name="04">
                            <p>شرکتهای مجاز</p>
                            <asp:Label runat="server" ID="lblt" />
                        </div>
                        <div class="nav nav-tabs" id="myTab5">
                            <a data-toggle="tab" href="#sectionC">
                                <div class="action-button inp bot left">مرحله قبل</div>
                            </a>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
