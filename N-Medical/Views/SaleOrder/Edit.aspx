<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="N_Medical.Views.SaleOrder.Edit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
                                <legend class="scheduler-border">Sale Order Request : Edit</legend>
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Sale Order Type : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="DDLEditSaleOrderType" runat="server" Enabled="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="DDLEditSaleOrderType_TextChanged">
                                                        <asp:ListItem Text="Please select" Value="-1"></asp:ListItem>
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
                                                <asp:Label runat="server" Text="Customer Code : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtEditCustomerCode" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtEditCustomerCode_TextChanged">
                                                    </asp:TextBox>
                                                    <asp:Label ID="lblEditId" runat="server" Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Customer Group : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="DDLEditCustomerGroup" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDLEditCustomerGroup_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Customer Name : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="DDLEditCustomer" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Vender : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="DDLEditVender" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Sale Order Number : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7 control-label" style="text-align: left;">
                                                    <asp:Label ID="lblEditSaleOrderNumber" runat="server" Font-Bold="true">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Purchase Order/ Quotation No. : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtEditPONumber" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Sale Order Shipping Date : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7 control-label" style="text-align: left;">
                                                    <asp:TextBox ID="txtEditSaleOrderShippingDate" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEditSaleOrderShippingDate" Format="dd/MM/yyyy"/>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Sale Order Amount (Exc. Vat) : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtEditSaleOrderAmount" runat="server" CssClass="form-control">
                                                    </asp:TextBox>
                                                    <asp:CompareValidator ControlToValidate="txtEditSaleOrderAmount" runat="server" ErrorMessage="Numberic only please" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Sale Name : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="DDLEditSale" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Cost Center : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="DDLEditCostCenter" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Remark : " CssClass="col-md-5 control-label"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtEditRemark" runat="server" CssClass="form-control" TextMode="MultiLine" Width="280px" Height="200px">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Sale Status : " CssClass="col-md-5" Style="text-align: right;"></asp:Label>
                                                <div class="col-md-7">
                                                    <asp:RadioButtonList ID="RdSaleOrderStatus" runat="server" RepeatDirection="Horizontal">
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
                                            <%--<div class="col-md-12" style="text-align: center;">
                                                <asp:CheckBox ID="chkNoDate" runat="server" Text="Exc. Date" />
                                                &nbsp;
                                                <asp:CheckBox ID="chkNoVat" runat="server" Text="Exc. Vat" />
                                            </div>--%>
                                            <div class="col-md-12" style="text-align: center;">
                                                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-info" OnClick="btnUpdate_Click" OnClientClick="return confirm('Do you want update sale order?');" />
                                                &nbsp;
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-info" OnClick="btnCancel_Click" OnClientClick="return confirm('Do you want cancel sale order?');" />
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
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-Width="30px" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <%--<ItemTemplate>
                                                    <asp:ImageButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" ImageUrl="~/Images/file-delete.png"/>
                                                </ItemTemplate>--%>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnDelete" Text="Delete" runat="server" CssClass="btn btn-info"  OnClick="btnDelete_Click"/>
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
                                            <%--<asp:TemplateField HeaderText="" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:Image runat="server" ID="imgDoc" Width="40px" ImageUrl="~/img/doc.png" Visible="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Download" ItemStyle-Width="30px" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnLinkDownload" Text="Download" runat="server" CssClass="btn btn-info"  OnClick="btnDownload_Click"/>
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
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
