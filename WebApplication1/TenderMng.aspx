<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TenderMng.aspx.cs" Inherits="TenderComp.TenderMng" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>مدیریت مناقصات</title>
    <link rel="stylesheet" href="css/st.css" />
    <link href="css/chosen.css" rel="stylesheet" />
    <link href="css/kendo.common.min.css" rel="stylesheet" />
    <link href="css/kendo.default.min.css" rel="stylesheet" />
    <script src="assets/js/jquery.min.js"></script>
    <style>
        .gridv
        {
            max-width: 40vw;
        }

        .highlight
        {
            background-color: #2e348f;
        }

        .highlighthover
        {
            background-color: #ffa200;
        }
    </style>
    <%--<script type="text/javascript">
       function checkRadioBtn(id) {
           var gv = document.getElementById('<%=grdTender.ClientID %>');

        for (var i = 1; i < gv.rows.length; i++) {
            var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");
            // Check if the id not same
            if (radioBtn[0].id !== id.id) {
                radioBtn[0].checked = false;
            }
        }
    }
</script>--%>
    <script>
        $(document).ready(function () {
        //    $("#grdTender tr").click(function () {
        //        var selected = $(this).hasClass("highlight");
        //        $("#grdTender tr").removeClass("highlight");
        //        if (!selected)
        //            $(this).addClass("highlight");
        //    });
            $("#grdTender tr").hover(function () {
                $(this).toggleClass("highlighthover");
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="content">
            <div id="sectionA">
                <div class="sectionContent">
                    <asp:TextBox ID="txtSearch" runat="server" autocomplete="off" />
                    <span class="btn">
                        <asp:Button runat="server" Style="width: 55px" ID="btnSearch" Text="جستجو" /></span>
                    <div class="gridv">
                        <asp:GridView runat="server" ID="grdTender" DataKeyNames="ID" AutoGenerateColumns="False" EmptyDataText="هیچ آیتمی وجود ندارد" BorderWidth="0">
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
                                <asp:TemplateField ItemStyle-CssClass="griddes" HeaderStyle-CssClass="gridheader" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button Text="..." ID="btnSelect" CausesValidation="false" runat="server" OnClick="btnSelect_OnClick"></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <%--<SelectedRowStyle BackColor="blue"/>--%>
                        </asp:GridView>
                    </div>
                    <div id="CompanyPopup" class="k-popup-edit-form">
                        <h3>شرکتها</h3>
                        <asp:GridView ID="grdCompanies" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" EmptyDataText="هیچ آیتمی وجود ندارد" BorderWidth="0" EmptyDataRowStyle-CssClass="gridtxt">
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
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="قیمت" ItemStyle-CssClass="gridprice" HeaderStyle-CssClass="gridheader">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrice" runat="server" Text='<%#  Eval("Price", "{0:#,0}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="امتیاز فنی" ItemStyle-CssClass="gridtechnical" HeaderStyle-CssClass="gridheader">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmtiazFani" runat="server" Text='<%# Eval("t") %>'></asp:Label>
                                    </ItemTemplate>
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
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
