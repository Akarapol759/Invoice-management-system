<%@ Page Title="Sale Master" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="N_Medical.Views.Master.Sale.Main" %>

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
                                <legend class="scheduler-border">Sale Criteria : Search</legend>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Sale Code : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtSaleCode" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Sale Name : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtSaleName" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Sale Area : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="DDLSaleArea" runat="server" CssClass="form-control"></asp:DropDownList>
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
                                            DataKeyNames="Sale_Code" CssClass="table table-striped table-bordered table-hover footable"
                                            OnPageIndexChanging="GridView_PageIndexChanging"
                                            PagerStyle-CssClass="pagination-ys" PagerSettings-Mode="NumericFirstLast" PagerStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <asp:ButtonField CommandName="edit" ControlStyle-CssClass="btn btn-info"
                                                    ButtonType="Button" Text="Edit" HeaderText="Edit">
                                                    <ControlStyle CssClass="btn btn-info"></ControlStyle>
                                                </asp:ButtonField>
                                                <asp:BoundField DataField="Sale_Code" HeaderText="Sale Code" SortExpression="Sale_Code" />
                                                <asp:BoundField DataField="Sale_Name" HeaderText="Sale Name" SortExpression="Sale_Name" />
                                                <asp:BoundField DataField="Sale_Area" HeaderText="Sale Area" SortExpression="Sale_Area" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
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