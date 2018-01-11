<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="N_Medical.Views.SaleOrder.Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="container">
            <div class="shadowBox">
                <div class="page-container">
                    <div class="container">
                        <div>
                            <br />
                        </div>
                        <div>
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border">Sale Order Request : Detail</legend>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Customer Group : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7 control-label" style="text-align: left;">
                                                    <asp:Label ID="lblDetailCustomerGroup" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                    <asp:Label ID="lblDetailId" runat="server" Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Customer Name : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7 control-label" style="text-align: left;">
                                                    <asp:Label ID="lblDetailCustomer" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Sale Order Type : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7 control-label" style="text-align: left;">
                                                    <asp:Label ID="lblDetailSaleOrderType" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Vender Name : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7 control-label" style="text-align: left;">
                                                    <asp:Label ID="lblDetailVenderName" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Sale Order Number : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7 control-label" style="text-align: left;">
                                                    <asp:Label ID="lblDetailSaleOrderNumber" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Purchase Order/ Quotation No.: " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7 control-label" style="text-align: left;">
                                                    <asp:Label ID="lblDetailPONumber" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Sale Order Shipping Date : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7 control-label" style="text-align: left;">
                                                    <asp:Label ID="lblDetailSaleOrderShippingDate" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Sale Order Amount (Exc. Vat) : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7 control-label" style="text-align: left;">
                                                    <asp:Label ID="lblDetailSaleOrderAmount" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Sale Order Outstanding (Exc. Vat) : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7 control-label" style="text-align: left;">
                                                    <asp:Label ID="lblDetailSaleOrderBalance" runat="server" Font-Bold="true" ForeColor="Red">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Sale Name : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7 control-label" style="text-align: left;">
                                                    <asp:Label ID="lblDetailSale" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Cost Center : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7 control-label" style="text-align: left;">
                                                    <asp:Label ID="lblDetailCostCenter" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Remark : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtEditRemark" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false" Width="280px" Height="200px">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Sale Status : " CssClass="col-md-5" Style="text-align: right;"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:RadioButtonList ID="RdSaleOrderStatus" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                                        <asp:ListItem Text="Complete" Value="C" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="On Process" Value="O"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Warehouse Status : " CssClass="col-md-5" Style="text-align: right;"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:RadioButtonList ID="RdWarehouseStatus" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                                        <asp:ListItem Text="Complete" Value="A"></asp:ListItem>
                                                        <asp:ListItem Text="On Process" Value="C"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="form-group">
                                            <%--<div class="col-md-12" style="text-align: center;">
                                                <asp:CheckBox ID="chkNoDate" runat="server" Text="Exc. Date" />
                                                &nbsp;
                                                <asp:CheckBox ID="chkNoVat" runat="server" Text="Exc. Vat" />
                                            </div>--%>
                                            <div class="col-md-12" style="text-align: center;">
                                                <asp:Button ID="btnNew" runat="server" Text="Create Invoice" CssClass="btn btn-info" OnClick="btnNew_Click" OnClientClick="return confirm('Do you want to create new invoice?');" />
                                                &nbsp;
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel Sale Order" CssClass="btn btn-info" OnClick="btnCancel_Click" OnClientClick="return confirm('Do you want to cancel sale order?');" />
                                                &nbsp;
                                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-info" OnClick="btnBack_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div>
                            <hr />
                        </div>
                        <div>
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border">Add New : Attach Files</legend>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="File Type : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="DDLAddType" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="Buy" Value="B"></asp:ListItem>
                                                        <asp:ListItem Text="Sell" Value="S"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Attach File: " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:FileUpload ID="fuAttach" runat="server" CssClass="btn btn-info"></asp:FileUpload>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-md-12" style="text-align: center;">
                                                <asp:Button ID="btnUploadFile" runat="server" Text="Add" CssClass="btn btn-info" OnClick="btnUploadFile_Click" OnClientClick="return confirm('Do you want upload new file?');" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div>
                            <hr />
                        </div>
                        <div style="text-align: center;">
                            <asp:UpdatePanel ID="upEditDetail" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GridView" runat="server" HorizontalAlign="Center" AllowSorting="true"
                                        AutoGenerateColumns="false" AllowPaging="true" DataKeyNames="SALE_ORDER_DETAIL_ID" OnRowDataBound="GridView_RowDataBound"
                                        CssClass="table table-hover table-striped">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No." ItemStyle-Width="10px" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                    <asp:Label ID="gvlblId" runat="server" Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type Attach File" ItemStyle-Width="30px" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblTypeAttach" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="File Name" ItemStyle-Width="30px" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblFileName" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Download" ItemStyle-Width="30px" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%--<asp:Label ID="gvlblAmount" runat="server"></asp:Label>--%>
                                                    <asp:Button ID="btnDownload" Text="Download" runat="server" OnClick="btnDownload_Click" CssClass="btn btn-info" OnClientClick="target = '_blank';" />
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Status" ItemStyle-Width="30px" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="DDLDetailStatus" runat="server" CssClass="btn btn-info" AutoPostBack="true" OnSelectedIndexChanged="DDLDetailStatus_SelectedIndexChanged">
                                                        <asp:ListItem Text="Waiting Order" Value="W"></asp:ListItem>
                                                        <asp:ListItem Text="Ordered" Value="O"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div>
                            <hr />
                        </div>
                        <div>
                            Invoice
                        </div>
                        <div>
                            <hr />
                        </div>
                        <div class="row">
                            <div class="col-lg-12 ">
                                <div class="table-responsive">
                                    <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="GridViewInvoice" runat="server" HorizontalAlign="Center" PageSize="10" Width="100%"
                                                OnRowCommand="GridViewInvoice_RowCommand" AutoGenerateColumns="false" AllowSorting="true"
                                                DataKeyNames="Id" CssClass="table table-striped table-bordered table-hover footable"
                                                PagerStyle-CssClass="pagination-ys" PagerSettings-Mode="NumericFirstLast" PagerStyle-HorizontalAlign="Center">
                                                <Columns>
                                                    <asp:ButtonField CommandName="detail" ControlStyle-CssClass="btn btn-info"
                                                        ButtonType="Button" Text="Detail" HeaderText="Detail">
                                                        <ControlStyle CssClass="btn btn-info"></ControlStyle>
                                                    </asp:ButtonField>
                                                    <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                                                    <asp:BoundField DataField="Invoice_Number" HeaderText="Invoice Number" SortExpression="Invoice_Number" />
                                                    <asp:BoundField DataField="Credit_Note" HeaderText="Credit Note Number" SortExpression="Credit_Note" />
                                                    <asp:BoundField DataField="Consumtion_Number" HeaderText="Consumtion Number" SortExpression="Consumtion_Number" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                    <asp:BoundField DataField="PO_Number" HeaderText="PO Number" SortExpression="PO_Number" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                    <asp:BoundField DataField="Cus_Name" HeaderText="Customer Name" SortExpression="Cus_Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                    <asp:BoundField DataField="INV_Date" HeaderText="Invoice Date" SortExpression="INV_Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                    <asp:BoundField DataField="Status_Name" HeaderText="Status" SortExpression="Status_Name" />
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="updProgressMain" AssociatedUpdatePanelID="UpdatePanel" runat="server">
                                        <ProgressTemplate>
                                            <div style="text-align: center;">
                                                <img style="position: relative; top: 50%;" src="~/Images/loader.gif" runat="server" alt="Loading" height="40" width="40" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                            </div>
                        </div>
                        <div>
                            <hr />
                        </div>
                        <div>
                            Credit Note
                        </div>
                        <div>
                            <hr />
                        </div>
                        <div class="row">
                            <div class="col-lg-12 ">
                                <div class="table-responsive">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="GridViewCreditNote" runat="server" HorizontalAlign="Center" PageSize="10" Width="100%"
                                                OnRowCommand="GridViewCreditNote_RowCommand" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true"
                                                DataKeyNames="Id" CssClass="table table-striped table-bordered table-hover footable"
                                                PagerStyle-CssClass="pagination-ys" PagerSettings-Mode="NumericFirstLast" PagerStyle-HorizontalAlign="Center">
                                                <Columns>
                                                    <asp:ButtonField CommandName="detail" ControlStyle-CssClass="btn btn-info"
                                                        ButtonType="Button" Text="Detail" HeaderText="Detail">
                                                        <ControlStyle CssClass="btn btn-info"></ControlStyle>
                                                    </asp:ButtonField>
                                                    <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="false" />
                                                    <asp:BoundField DataField="CreditNote_Number" HeaderText="Credit Note Number" SortExpression="CreditNote_Number" />
                                                    <asp:BoundField DataField="Invoice_Number" HeaderText="Invoice Number" SortExpression="Invoice_Number" />
                                                    <asp:BoundField DataField="Consumtion_Number" HeaderText="Consumtion Number" SortExpression="Consumtion_Number" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                    <asp:BoundField DataField="PO_Number" HeaderText="PO Number" SortExpression="PO_Number" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                    <asp:BoundField DataField="Cus_Name" HeaderText="Customer Name" SortExpression="Cus_Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                    <asp:BoundField DataField="CreditNote_Date" HeaderText="Credit Note Date" SortExpression="CreditNote_Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                    <asp:BoundField DataField="Status_Name" HeaderText="Status" SortExpression="Status_Name" />
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel" runat="server">
                                        <ProgressTemplate>
                                            <div style="text-align: center;">
                                                <img style="position: relative; top: 50%;" src="~/Images/loader.gif" runat="server" alt="Loading" height="40" width="40" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                            </div>
                        </div>
                        <div>
                            <hr />
                        </div>
                        <div>
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border">Add New : Comment</legend>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-sm-2 col-lg-2">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text=" " CssClass="col-md-2 control-label"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-8 col-lg-8">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Comment : " CssClass="col-md-2 control-label"></asp:Label>
                                                <div class="col-md-10">
                                                    <asp:TextBox ID="txtComment" runat="server" CssClass="form-control" TextMode="MultiLine" Width="480px" Height="200px">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-2 col-lg-2">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text=" " CssClass="col-md-2 control-label"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-md-12" style="text-align: center;">
                                                <asp:Button ID="btnAddComment" runat="server" Text="Add Comment" CssClass="btn btn-info" OnClick="btnAddComment_Click" OnClientClick="return confirm('Do you want add new comment?');" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div>
                            <hr />
                        </div>
                        <div style="text-align: center;">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GridViewComment" runat="server" HorizontalAlign="Center" AllowSorting="true"
                                        AutoGenerateColumns="false" AllowPaging="true" DataKeyNames="SALE_ORDER_Comment_Id" OnRowDataBound="GridViewComment_RowDataBound"
                                        CssClass="table table-hover table-striped">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No." ItemStyle-Width="10px" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                    <asp:Label ID="gvlblId" runat="server" Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Comment" ItemStyle-Width="30px" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblComment" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="75%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="By Whom" ItemStyle-Width="30px" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblByWhom" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date" ItemStyle-Width="30px" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblDate" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
