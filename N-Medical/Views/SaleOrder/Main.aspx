<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="N_Medical.Views.SaleOrder.Main" %>

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
                                <legend class="scheduler-border">Sale Order Criteria : Search</legend>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Sale Order Type : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="DDLSaleOrderType" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="DDLSaleOrderType_TextChanged">
                                                        <asp:ListItem Text="All" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="ES" Value="ES"></asp:ListItem>
                                                        <asp:ListItem Text="SP" Value="SP"></asp:ListItem>
                                                        <asp:ListItem Text="LP" Value="LP"></asp:ListItem>
                                                        <asp:ListItem Text="MS" Value="MS"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Sale Order Number : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtSaleOrderNumber" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Purchase Order/ Quotation No. : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtPONumber" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div><div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Customer Code : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCustomerCode_TextChanged">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Customer Group : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="DDLCustomerGroup" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDLCustomerGroup_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Customer Name : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="DDLCustomer" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Vender : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="DDLVender" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Cost Center : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="DDLCostCenter" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Sale Status : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="DDLSaleStatus" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="All" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Complete" Value="C"></asp:ListItem>
                                                        <asp:ListItem Text="On Process" Value="O"></asp:ListItem>
                                                        <asp:ListItem Text="Cancel" Value="N"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Warehouse Status : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="DDLWarehouseStatus" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="All" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Complete" Value="A"></asp:ListItem>
                                                        <asp:ListItem Text="On Process" Value="C"></asp:ListItem>
                                                        <asp:ListItem Text="Cancel" Value="N"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-md-12" style="text-align: center;">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info" OnClick="btnSearch_Click" />
                                                &nbsp;
                                                <asp:Button ID="btnRequest" runat="server" Text="New" CssClass="btn btn-info" OnClick="btnRequest_Click" />
                                                &nbsp;
                                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-info" OnClick="btnClear_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div>
                        <hr />
                    </div>
                    <div class="row">
                        <div class="col-lg-12 ">
                            <div class="table-responsive">
                                <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView" runat="server" HorizontalAlign="Center" PageSize="10" Width="100%"
                                            OnRowCommand="GridView_RowCommand" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true"
                                            DataKeyNames="SALE_ORDER_ID" CssClass="table table-striped table-bordered table-hover footable"
                                            OnPageIndexChanging="GridView_PageIndexChanging"
                                            PagerStyle-CssClass="pagination-ys" PagerSettings-Mode="NumericFirstLast" PagerStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <asp:ButtonField CommandName="detail" ControlStyle-CssClass="btn btn-info"
                                                    ButtonType="Button" Text="Detail" HeaderText="Detail">
                                                    <ControlStyle CssClass="btn btn-info"></ControlStyle>
                                                </asp:ButtonField>
                                                <asp:BoundField DataField="SALE_ORDER_ID" HeaderText="Id" SortExpression="Id" />
                                                <asp:BoundField DataField="SALE_ORDER_Number" HeaderText="Sale Order Number" SortExpression="Invoice_Number" />
                                                <asp:BoundField DataField="PO_Number" HeaderText="PO Number" SortExpression="PO_Number"/>
                                                <asp:BoundField DataField="Cus_Name" HeaderText="Customer Name" SortExpression="Cus_Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="VENDER_NAME" HeaderText="VENDER NAME" SortExpression="VENDER_NAME" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="Create_Date" HeaderText="Sale Order Date" SortExpression="Create_Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="Sale_Status_Name" HeaderText="Sale Status" SortExpression="Sale_Status_Name" />
                                                <asp:BoundField DataField="Warehouse_Status_Name" HeaderText="Warehouse Status" SortExpression="Warehouse_Status_Name" />
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
                </div>
            </div>
        </div>
    </div>
</asp:Content>