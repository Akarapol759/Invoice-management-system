<%@ Page Title="Customer Master" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="N_Medical.Views.Master.Customer.Edit" %>

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
                                <legend class="scheduler-border">Customer Master : Edit</legend>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Customer Group Code : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7 control-label" style="text-align: left;">
                                                    <asp:Label ID="lblEditCustomerGroupCode" runat="server" Font-Bold="true">
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
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtEditCustomerName" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Customer Billing : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtEditCustomerBilling" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Customer Contact : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtEditCustomerContact" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Customer Tel : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtEditCustomerTel" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Customer Term : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="DDLEditCustomerTerm" runat="server" CssClass="form-control">
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
                                                    <asp:DropDownList ID="DDLEditCustomerType" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Customer Vat : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtEditCustomerVat" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                    <asp:CompareValidator ControlToValidate="txtEditCustomerVat" runat="server" ErrorMessage="Numberic only please" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
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
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-md-12" style="text-align: center;">
                                                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-info" OnClick="btnUpdate_Click" OnClientClick="return confirm('Do you want create request?');"/>
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