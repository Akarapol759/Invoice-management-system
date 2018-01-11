<%@ Page Title="Credit Note" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="N_Medical.Views.CreditNote.Edit" %>

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
                                <legend class="scheduler-border">Credit Note Request : Edit</legend>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Invoice Number : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7 control-label" style="text-align: left;">
                                                    <asp:Label ID="lblEditInvoiceNumber" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                    <asp:label id="lblEditId" runat="server" visible="false"></asp:label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Remark : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7 control-label" style="text-align: left;">
                                                    <asp:Label ID="lblEditRemark" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Status : " CssClass="col-md-5" style="text-align: right;"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:RadioButtonList ID="RdCreditNoteStatus" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="Complete" Value="C"></asp:ListItem>
                                                        <asp:ListItem Text="On Process" Value="O" Selected="True"></asp:ListItem>
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
                                                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-info" OnClick="btnUpdate_Click" OnClientClick="return confirm('Do you want update request?');"/>
                                                &nbsp;
                                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-info" OnClick="btnDelete_Click" OnClientClick="return confirm('Do you want delete request?');"/>
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
                        <div style="text-align: center;">
                            <asp:UpdatePanel ID="upEditDetail" runat="server">
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
