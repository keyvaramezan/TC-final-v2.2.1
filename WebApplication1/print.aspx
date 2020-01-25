<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="print.aspx.cs" Inherits="TenderComp.print" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <link href="css/st.css" rel="stylesheet" />

    <title>پرینت گزارش مناقصات</title>

    <script language="javascript" type="text/javascript">
        function printDiv(divID) {

            //Get the HTML of div
            var divElements = document.getElementById(divID).innerHTML;

            //Get the HTML of whole page
            var oldPage = document.body.innerHTML;

            //Reset the page's HTML with div's HTML only
            document.body.innerHTML =
                "<html><head></head><body>" +
                divElements + "</body>";

            //Print Page
            window.print();

            //Restore orignal HTML
            document.body.innerHTML = oldPage;
        }
    </script>
</head>

<body onload="replaceDigits()" style="background:none;color:#4c5851">
    <form id="form1" runat="server">
        <div class="report-container">
            <div class="report">
                <div class="report-list" id="printsection">
                    <p>گزارش مناقصه</p>

                    <div class="report-desc" id="body">
                        <span>شماره مناقصه:</span>
                        <asp:Label runat="server" Style="font-weight: bold; color: #000" ID="lblTenderno" />
                        <span>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;</span>
                        <span>عنوان مناقصه:</span>
                        <asp:Label runat="server" Style="font-weight: bold; color: #000" ID="lblTenderTitle" />
                        <%--<span style="font-weight: bold; color: #000">مخابرات</span>--%>
                    </div>

                    <div class="report-grid" id="reportGrid">
                        <asp:GridView ID="GrdComponies" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" EmptyDataText="هیچ آیتمی وجود ندارد" BorderWidth="0" EmptyDataRowStyle-CssClass="gridtxt">
                            <Columns>
                                <asp:TemplateField HeaderText="نام شرکت" ItemStyle-CssClass="gridName" HeaderStyle-CssClass="gridheader">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("ComponyName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="قیمت" ItemStyle-CssClass="gridItem" HeaderStyle-CssClass="gridheader">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrice" runat="server" Text='<%#  Eval("Price", "{0:#,0}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="امتیاز فنی" ItemStyle-CssClass="gridItem" HeaderStyle-CssClass="gridheader">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmtiazFani" runat="server" Text='<%# Eval("t") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="قیمت تراز شده" ItemStyle-CssClass="gridItem" HeaderStyle-CssClass="gridheader">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTarazShode" runat="server" Text='<%# Eval("L", "{0:#,0}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="درصد اختلاف از برآورد" ItemStyle-CssClass="gridItem" HeaderStyle-CssClass="gridheader">
                                    <ItemTemplate>
                                        <asp:Label ID="IeDiff" runat="server" Text='<%# Eval("IeDiff", "{0:#.##}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="شاخص" ItemStyle-CssClass="gridItem" HeaderStyle-CssClass="gridheader">
                                    <ItemTemplate>
                                        <asp:Label ID="lblX" runat="server" Text='<%# Eval("X", "{0:#.###}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="برنده" ItemStyle-CssClass="gridItem" HeaderStyle-CssClass="gridheader" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIswin" runat="server" Text='<%# Eval("Iswin") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="شرح" ItemStyle-CssClass="gridComment" HeaderStyle-CssClass="gridheader">
                                    <ItemTemplate>
                                        <asp:Label ID="lblComment" runat="server" Text='<%# Eval("Comment") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                    <div class="report-exp">
                        <span>انحراف از معیار: </span>
                        <asp:Label runat="server" ID="lblenheraf" Style="font-weight: bold; color: #000" />
                        <span>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;</span>
                        <span>میانگین مرحله اول : </span>
                        <asp:Label runat="server" ID="lblave1" Style="font-weight: bold; color: #000" />
                        <span>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;</span>
                        <span>میانگین مرحله دوم : </span>
                        <asp:Label runat="server" ID="lblave2" Style="font-weight: bold; color: #000" />
                        <span>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;</span>
                        <span>حد بالا : </span>
                        <asp:Label runat="server" ID="lblup" Style="font-weight: bold; color: #000" />
                        <span>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;</span>
                        <span>حد پایین : </span>
                        <asp:Label runat="server" ID="lbldown" Style="font-weight: bold; color: #000" />
                    </div>

                </div>

                <div class="printBtn">
                    <img src="images/printer.png" id="btnPrint" onclick="printDiv('printsection')"/>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
