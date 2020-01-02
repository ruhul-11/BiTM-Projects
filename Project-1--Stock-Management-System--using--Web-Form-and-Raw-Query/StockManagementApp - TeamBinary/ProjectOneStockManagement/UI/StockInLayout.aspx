<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StockInLayout.aspx.cs" Inherits="ProjectOneStockManagement.UI.StockInLayout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="text-center bg-success custom-h2">Stock In</h2>
    <div class="container">
        <br />
        <br />
        <form id="stockInForm">
            <div class="row form-group justify-content-center">
                <asp:Label ID="Label1" CssClass="col-sm-3 col-form-label text-right" runat="server" Text="Company :"></asp:Label>
                <div class="col-sm-6 d-flex">
                    <asp:DropDownList CssClass="form-control" ID="ddlStockInCompany" DataValueField="ID" DataTextField="Name" runat="server" OnSelectedIndexChanged="ddlStockInCompany_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorCompany" InitialValue="-1" runat="server" ForeColor="Red" ControlToValidate="ddlStockInCompany" ErrorMessage="Company is required"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row form-group justify-content-center">
                <asp:Label ID="Label2" CssClass="col-sm-3 col-form-label text-right" runat="server" Text="Item :"></asp:Label>
                <div class="col-sm-6 d-flex">
                    <asp:DropDownList CssClass="form-control" ID="ddlStockInItem" DataValueField="ID" DataTextField="Name" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStockInItem_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorStockInItem" InitialValue="-1" runat="server" ForeColor="Red" ControlToValidate="ddlStockInItem" ErrorMessage="Item is required"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="row form-group justify-content-center">
                <asp:Label ID="Label3" CssClass="col-sm-3 col-form-label text-right" runat="server" Text="Reorder Level :"></asp:Label>
                <div class="col-sm-6">
                    <asp:TextBox ID="textBoxReorderLevel" Names="reorderLevel" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
             
            <div class="row form-group justify-content-center">
                <asp:Label ID="Label4" CssClass="col-sm-3 col-form-label text-right" runat="server" Text="Available Quantity :"></asp:Label>
                <div class="col-sm-6">
                    <asp:TextBox ID="textBoxAvailableQty" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row form-group justify-content-center">
                <asp:Label ID="Label6" CssClass="col-sm-3 col-form-label text-right" runat="server" Text="Stock In Quantity :"></asp:Label>
                <div class="col-sm-6 d-flex">
                    <asp:TextBox ID="textBoxStockInQty" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorStockQuantity" runat="server" ForeColor="Red" ControlToValidate="textBoxStockInQty"  ValidationExpression="\d+" ErrorMessage="Only Insert Numbers"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorStockQuantity" CssClass="col-sm-3" runat="server" ForeColor="Red" ControlToValidate="textBoxStockInQty" ErrorMessage="Insert Stock In Quantity"></asp:RequiredFieldValidator>
                </div>
                
            </div>

            <div class="row form-group justify-content-center">
                <div class="col-sm-6 offset-sm-3">
                    <asp:Button ID="btnSaveStockIn" CssClass="btn btn-success" type="submit" runat="server" Text="Save" OnClick="btnSaveStockIn_Click" />
                    <br />
                    <asp:Label ID="lblStockInStatus" CssClass="text-success" runat="server"></asp:Label>
                </div>
            </div>
        </form>
    </div>
</asp:Content>
