<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemSetupLayout.aspx.cs" Inherits="ProjectOneStockManagement.UI.ItemSetupLayout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <h2 class="text-center bg-success custom-h2">Item Setup</h2>
    <div class="container">
        <br />
        <br />
        <div>
            <div class="row form-group justify-content-center">
                <asp:Label ID="Label5" CssClass="col-sm-3 col-form-label text-right" runat="server" Text="Category :"></asp:Label>
                <div class="col-sm-6 d-flex">
                    <asp:DropDownList CssClass="form-control" ID="ddlItemCategory" DataTextField="Name" DataValueField="ID" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorItemCategory" ForeColor="Red" InitialValue="-1" ControlToValidate="ddlItemCategory" runat="server" ErrorMessage="Category is Required"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row form-group justify-content-center">
                <asp:Label ID="Label6" CssClass="col-sm-3 col-form-label text-right" runat="server" Text="Company :"></asp:Label>
                <div class="col-sm-6 d-flex">
                    <asp:DropDownList CssClass="form-control" ID="ddlItemCompany" DataTextField="Name" DataValueField="ID" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorItemCompany" ForeColor="Red" InitialValue="-1" ControlToValidate="ddlItemCompany" runat="server" ErrorMessage="Company is Required"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row form-group justify-content-center">
                <asp:Label ID="Label1" CssClass="col-sm-3 col-form-label text-right" runat="server" Text="Item Name :"></asp:Label>
                <div class="col-sm-6 d-flex">
                    <asp:TextBox ID="textBoxItemName" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorItemName" ForeColor="Red" ControlToValidate="textBoxItemName" runat="server" ErrorMessage="Item Name is required"></asp:RequiredFieldValidator>
                </div>
            </div>
             <div class="row form-group justify-content-center">
                <asp:Label ID="Label2" CssClass="col-sm-3 col-form-label text-right" runat="server" Text="Reorder Level :"></asp:Label>
                <div class="col-sm-6 d-flex">
                    <asp:TextBox ID="textBoxReorderLevel" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorReorderLevel" Type="Integer" ForeColor="Red" ControlToValidate="textBoxReorderLevel" runat="server" ErrorMessage="Reorder Level is required"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="row form-group justify-content-center">
                <div class="col-sm-6 offset-sm-3">
                    <asp:Button ID="btnSaveItem" CssClass="btn btn-success" runat="server" Text="Save" OnClick="btnSaveItem_Click" />
                    <br />
                    <asp:Label ID="lblItemSaveStatus" CssClass="text-success" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
