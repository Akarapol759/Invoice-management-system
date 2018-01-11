<%@ Page Title="Credit Note" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="N_Medical.Views.CreditNote.Main" %>

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
                                <legend class="scheduler-border">Credit Note Criteria : Search</legend>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Invoice Number : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtInvoiceNumber" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Consumtion Number : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtConsumtionNumber" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="PO Number : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtPONumber" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Credit Note Number : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtCNNumber" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Status : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="DDLCREStatus" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="All" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Complete" Value="C"></asp:ListItem>
                                                        <asp:ListItem Text="On Process" Value="O"></asp:ListItem>
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
                                            DataKeyNames="Id" CssClass="table table-striped table-bordered table-hover footable"
                                            OnPageIndexChanging="GridView_PageIndexChanging"
                                            PagerStyle-CssClass="pagination-ys" PagerSettings-Mode="NumericFirstLast" PagerStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <asp:ButtonField CommandName="detail" ControlStyle-CssClass="btn btn-info"
                                                    ButtonType="Button" Text="Detail" HeaderText="Detail">
                                                    <ControlStyle CssClass="btn btn-info"></ControlStyle>
                                                </asp:ButtonField>
                                                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="false"/>
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