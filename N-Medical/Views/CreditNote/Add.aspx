<%@ Page Title="Credit Note" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="N_Medical.Views.CreditNote.Add" %>

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
                                <legend class="scheduler-border">Credit Note Request : Create</legend>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Invoice Number : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7 control-label" style="text-align: left;">
                                                    <asp:Label ID="lblAddInvoiceNumber" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                    <asp:Label ID="lblAddInvocieType" runat="server" Visible="false">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Sale Order Number : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7 control-label" style="text-align: left;">
                                                    <asp:Label ID="lblAddSaleOrderNumber" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Remark : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="DDLAddRemark" runat="server" CssClass="form-control">
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
                                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-info" OnClick="btnSave_Click" OnClientClick="return confirm('Do you want create credit note request?');"/>
                                                &nbsp;
                                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-info" OnClick="btnBack_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div>
                            <br />
                        </div>
                        <div style="text-align: center;">
                            <asp:UpdatePanel ID="upAddDetail" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GridView" runat="server" HorizontalAlign="Center" AllowSorting="true"
                                        AutoGenerateColumns="false" AllowPaging="true" DataKeyNames="Id" OnRowDataBound="GridView_RowDataBound"
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
                                            <asp:TemplateField HeaderText="Item Code" ItemStyle-Width="70px" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblItemCode" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="30%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Name" ItemStyle-Width="70px" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblItemName" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="30%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Qty" ItemStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblItemQty" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Price" ItemStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvlblUnitPrice" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UOM" ItemStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblUOM" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Batch No." ItemStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblBatchNo" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                                <ControlStyle Width="150px" />
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />--%>
                                            <asp:TemplateField HeaderText="Amount" ItemStyle-Width="30px" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblAmount" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
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
