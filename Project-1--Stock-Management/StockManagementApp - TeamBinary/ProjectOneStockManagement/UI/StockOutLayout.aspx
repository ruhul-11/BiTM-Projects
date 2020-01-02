<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StockOutLayout.aspx.cs" Inherits="ProjectOneStockManagement.UI.StockOutLayout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="text-center bg-success custom-h2">Stock Out</h2>
    <div class="container">
        <br />
        <br />
        <div>
            <div class="row form-group justify-content-center">
                <asp:Label ID="Label6" CssClass="col-sm-3 col-form-label text-right" runat="server" Text="Company :"></asp:Label>
                <div class="col-sm-6 d-flex">
                    <asp:DropDownList CssClass="form-control" ID="ddlStockOutCompany" DataValueField="ID" DataTextField="Name" runat="server" OnSelectedIndexChanged="ddlStockOutCompany_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorCompany" InitialValue="-1" runat="server" ForeColor="Red" ControlToValidate="ddlStockOutCompany" ErrorMessage="Company is required"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row form-group justify-content-center">
                <asp:Label ID="Label7" CssClass="col-sm-3 col-form-label text-right" runat="server" Text="Item :"></asp:Label>
                <div class="col-sm-6 d-flex">
                    <asp:DropDownList CssClass="form-control" ID="ddlStockOutItem" DataValueField="ID" DataTextField="Name" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStockOutItem_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorStockOutItem" InitialValue="-1" runat="server" ForeColor="Red" ControlToValidate="ddlStockOutItem" ErrorMessage="Item is required"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="row form-group justify-content-center">
                <asp:Label ID="Label8" CssClass="col-sm-3 col-form-label text-right" runat="server" Text="Reorder Level :"></asp:Label>
                <div class="col-sm-6">
                    <asp:TextBox ID="textBoxReorderLevel" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="row form-group justify-content-center">
                <asp:Label ID="Label9" CssClass="col-sm-3 col-form-label text-right" runat="server" Text="Available Quantity :"></asp:Label>
                <div class="col-sm-6">
                    <asp:TextBox ID="textBoxAvailableQty" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row form-group justify-content-center">
                <asp:Label ID="Label10" CssClass="col-sm-3 col-form-label text-right" runat="server" Text="Stock Out Quantity :"></asp:Label>
                <div class="col-sm-6 d-flex">
                    <asp:TextBox ID="textBoxStockOutQty" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorStockOutQuantity" runat="server" ForeColor="Red" ControlToValidate="textBoxStockOutQty" ValidationExpression="\d+" ErrorMessage="Only Insert Numbers"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ControlToValidate="textBoxStockOutQty" ErrorMessage="Please enter quantity"></asp:RequiredFieldValidator>
                </div>
                
            </div>

            <div class="row form-group justify-content-center">
                <div class="col-sm-6 offset-sm-3">
                    <asp:Button ID="btnAddStockOut" CssClass="btn btn-success" runat="server" Text="Add" OnClick="btnAddStockOut_Click" />
                    <br />
                    <asp:Label ID="lblStockOutStatus" CssClass="text-success" runat="server"></asp:Label>
                </div>
            </div>
        </div>

        <div class="row justify-content-center">
            <div class="col-md-6 d-flex justify-content-center">
                <asp:GridView CssClass="table" ID="gridViewStockOut" runat="server" CellPadding="15" Width="600px" AutoGenerateColumns="false" ForeColor="#333333" GridLines="None" HorizontalAlign="Center">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                    <Columns>
                        <asp:TemplateField HeaderText="SL">
                            <ItemTemplate>
                                <%#Eval("ID") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item">
                            <ItemTemplate>
                                <%#Eval("Name") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Company">
                            <ItemTemplate>
                                <%#Eval("CompanyName") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <%#Eval("Quantity") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="row form-group justify-content-center">
            <div class="col-sm-6 text-center">
                <asp:Button ID="btnSell" CssClass="btn btn-success" runat="server" Text="Sell" OnClick="btnSell_Click" />
                <asp:Button ID="btnDamage" CssClass="btn btn-danger" runat="server" Text="Damage" OnClick="btnDamage_Click" />
                <asp:Button ID="btnLost" CssClass="btn btn-warning" runat="server" Text="Lost" OnClick="btnLost_Click" />
                <br />
                <asp:Label ID="Label1" CssClass="text-success" runat="server"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
