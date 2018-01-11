<%@ Page Title="JNJ Report" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="JNJ_Report.aspx.cs" Inherits="N_Medical.Views.Reports.JNJ_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <div class="jumbotron">
        <div class="container">
            <div class="form-group">
                <div class="col-sm-1">
                </div>
                <div class="col-sm-2" style="text-align: right;">
                    <asp:Label ID="Label1" runat="server" Text="Start Date : " CssClass="control-label"></asp:Label>
                </div>
                <div class="col-sm-3">
                    <asp:TextBox ID="txtSearchStdDate" runat="server" CssClass="form-control"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSearchStdDate" Format="dd/MM/yyyy"/>
                </div>
                <div class="col-sm-2" style="text-align: right;">
                    <asp:Label ID="Label2" runat="server" Text="End Date : " CssClass="control-label"></asp:Label>
                </div>
                <div class="col-sm-3" style="text-align: left;">
                    <asp:TextBox ID="txtSearchEndDate" runat="server" CssClass="form-control"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSearchEndDate" Format="dd/MM/yyyy"/>
                </div>
            </div>
        </div>
        <div class="form-group">
        </div>
        <div class="form-group">
            <div style="text-align: center;">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info" OnClick="btnSearch_Click" />
                &nbsp;
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-info" OnClick="btnClear_Click" />
            </div>
        </div>
    </div>
    <!--Show Report-->
    <div class="container" style="overflow-y: scroll;">
        <div class="form-group">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" BackColor="#CCCCFF"
                EnableTheming="True" AsyncRendering="False" SizeToReportContent="True"
                InteractiveDeviceInfos="(Collection)">
            </rsweb:ReportViewer>
        </div>
    </div>
</asp:Content>