<%@ Page Title="Shipping Master" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="N_Medical.Views.Master.Shipping.Edit" %>

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
                                <legend class="scheduler-border">Shipping Master : Edit</legend>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Customer Group : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7 control-label" style="text-align: left;">
                                                    <asp:Label ID="lblEditCustomerGroup" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Customer Code : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7 control-label" style="text-align: left;">
                                                    <asp:Label ID="lblEditCustomerCode" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Cus Name : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7 control-label" style="text-align: left;">
                                                    <asp:Label ID="lblEditCustomerName" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-md-12" style="text-align: center;">
                                                <%--<asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-info" OnClick="btnNew_Click" OnClientClick="return confirm('Do you want create request?');" />
                                                &nbsp;--%>
                                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-info" OnClick="btnBack_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div>
                            <fieldset class="scheduler-border">
                                <legend class="scheduler-border">Add New : Shipping Address</legend>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Shipping Address : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtAddShippingAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Height="100px">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-md-12" style="text-align: center;">
                                                <asp:Button ID="btnAddShipping" runat="server" Text="Add" CssClass="btn btn-info" OnClick="btnAddDepartment_Click" OnClientClick="return confirm('Do you want add new department?');" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div>
                            <br />
                        </div>
                        <div class="row">
                            <div class="col-lg-12 ">
                                <div class="table-responsive">
                                    <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="GridView" runat="server" HorizontalAlign="Center" PageSize="10" Width="100%"
                                                OnRowCommand="GridView_RowCommand" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true"
                                                DataKeyNames="Cus_Shipping_Code" CssClass="table table-striped table-bordered table-hover footable"
                                                OnPageIndexChanging="GridView_PageIndexChanging"
                                                PagerStyle-CssClass="pagination-ys" PagerSettings-Mode="NumericFirstLast" PagerStyle-HorizontalAlign="Center">
                                                <Columns>
                                                    <asp:ButtonField CommandName="editRow" ControlStyle-CssClass="btn btn-info"
                                                        ButtonType="Button" Text="Edit" HeaderText="Edit">
                                                        <ControlStyle CssClass="btn btn-info"></ControlStyle>
                                                    </asp:ButtonField>
                                                    <asp:BoundField DataField="Cus_Shipping_Code" HeaderText="Shipping Code" SortExpression="Cus_Shipping_Code" />
                                                    <asp:BoundField DataField="Cus_Shipping_Detail" HeaderText="Shipping Address" SortExpression="Cus_Shipping_Detail" />
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
                        <div id="editModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="editModalLabel" aria-hidden="true" style="width: 700px; top: 5%; left: 30%;">
                            <%--<div class="modal-dialog">--%>
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close"
                                        data-dismiss="modal" aria-hidden="true">
                                        ×</button>
                                    <h3 id="editModalLabel">Edit Shipping Address</h3>
                                </div>
                                <div class="modal-body">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div>
                                                <fieldset class="scheduler-border">
                                                    <legend class="scheduler-border">Edit Shipping Address</legend>
                                                    <div class="form-horizontal">
                                                        <div class="row">
                                                            <div class="col-sm-6 col-lg-6">
                                                                <div class="form-group">
                                                                    <asp:Label runat="server" Text="Shipping Address : " CssClass="col-md-5 control-label"></asp:Label>
                                                                    <div class="col-md-7">
                                                                        <asp:TextBox ID="txtEditShippingAddress" runat="server" CssClass="form-control">
                                                                        </asp:TextBox>
                                                                        <asp:Label ID="lblEditShippingAddressCode" runat="server" Visible="false"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6 col-lg-6">
                                                                <div class="form-group">
                                                                    <asp:Label runat="server" Text="Status : " CssClass="col-md-5" Style="text-align: right;"></asp:Label>
                                                                    <div class="col-md-7">
                                                                        <asp:RadioButtonList ID="RdStatus" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Text="Active" Value="A"></asp:ListItem>
                                                                            <asp:ListItem Text="Inactive" Value="I"></asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <%--<div class="form-horizontal">
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <div class="col-md-12" style="text-align: center;">
                                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-info" OnClick="btnUpdate_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                </fieldset>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-info" OnClick="btnUpdate_Click" OnClientClick="return confirm('Do you want update price?');" />
                                    <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                                </div>
                            </div>
                            <%--</div>--%>
                        </div>
                        <!-- Edit Modal Ends here -->
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
