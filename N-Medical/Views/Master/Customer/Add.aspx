<%@ Page Title="Customer Master" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="N_Medical.Views.Master.Customer.Add" %>

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
                                <legend class="scheduler-border">Customer Master : Create</legend>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <div class="col-md-5 control-label" style="text-align: right;">
                                                    <asp:Label runat="server" Text="Customer Group : " CssClass="control-label"></asp:Label>
                                                    <asp:Label runat="server" Text="*" CssClass="control-label" ForeColor="Red"></asp:Label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="DDLAddCustomerGroup" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDLAddCustomerGroup_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <div class="col-md-5 control-label" style="text-align: right;">
                                                    <asp:Label runat="server" Text="Customer Code : " CssClass="control-label"></asp:Label>
                                                    <asp:Label runat="server" Text="*" CssClass="control-label" ForeColor="Red"></asp:Label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtAddCustomerCode" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <div class="col-md-5 control-label" style="text-align: right;">
                                                    <asp:Label runat="server" Text="Customer Name : " CssClass="control-label"></asp:Label>
                                                    <asp:Label runat="server" Text="*" CssClass="control-label" ForeColor="Red"></asp:Label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtAddCustomerName" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <div class="col-md-5 control-label" style="text-align: right;">
                                                    <asp:Label runat="server" Text="Customer Billing : " CssClass="control-label"></asp:Label>
                                                    <asp:Label runat="server" Text="*" CssClass="control-label" ForeColor="Red"></asp:Label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtAddCustomerBilling" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <div class="col-md-5 control-label" style="text-align: right;">
                                                    <asp:Label runat="server" Text="Customer Contact : " CssClass="control-label"></asp:Label>
                                                    <asp:Label runat="server" Text="*" CssClass="control-label" ForeColor="Red"></asp:Label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtAddCustomerContact" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Customer Tel : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtAddCustomerTel" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Customer Term : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="DDLAddCustomerTerm" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="Credit" Value="CR"></asp:ListItem>
                                                        <asp:ListItem Text="Cash" Value="CA"></asp:ListItem>
                                                        <asp:ListItem Text="Dues Immediately" Value="0000"></asp:ListItem>
                                                        <asp:ListItem Text="Dues in 30 days" Value="0030"></asp:ListItem>
                                                        <asp:ListItem Text="Dues in 45 days" Value="0045"></asp:ListItem>
                                                        <asp:ListItem Text="Dues in 60 days" Value="0060"></asp:ListItem>
                                                        <asp:ListItem Text="Dues in 90 days" Value="0090"></asp:ListItem>
                                                        <asp:ListItem Text="Net 120" Value="0120"></asp:ListItem>
                                                        <asp:ListItem Text="Net 180" Value="0180"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Customer Type : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="DDLAddCustomerType" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <div class="col-md-5 control-label" style="text-align: right;">
                                                    <asp:Label runat="server" Text="Customer Vat : " CssClass="control-label"></asp:Label>
                                                    <asp:Label runat="server" Text="*" CssClass="control-label" ForeColor="Red"></asp:Label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtAddCustomerVat" runat="server" CssClass="form-control" Text="7">
                                                    </asp:TextBox>
                                                    <asp:CompareValidator ControlToValidate="txtAddCustomerVat" runat="server" ErrorMessage="Numberic only please" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-md-12" style="text-align: center;">
                                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-info" OnClick="btnSave_Click" OnClientClick="return confirm('Do you want create request?');" />
                                                &nbsp;
                                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-info" OnClick="btnBack_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
