<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchAndViewItemSummaryLayout.aspx.cs" Inherits="ProjectOneStockManagement.UI.SearchAndViewItemSummaryLayout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="text-center bg-success custom-h2">Search & View Item Summary</h2>
    <div class="container">
        <br />
        <br />
        <div>
            <div class="row form-group justify-content-center">
                <asp:Label ID="Label3" CssClass="col-sm-3 col-form-label text-right" runat="server" Text="Company :"></asp:Label>
                <div class="col-sm-6 d-flex">
                    <asp:DropDownList CssClass="form-control" ID="ddlSearchCompany" DataValueField="ID" DataTextField="Name" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorCompany" InitialValue="-1" runat="server" ForeColor="Red" ControlToValidate="ddlSearchCompany" ErrorMessage="Company is required"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row form-group justify-content-center">
                <asp:Label ID="Label4" CssClass="col-sm-3 col-form-label text-right" runat="server" Text="Category :"></asp:Label>
                <div class="col-sm-6">
                    <asp:DropDownList CssClass="form-control" DataValueField="ID" DataTextField="Name" ID="ddlSearchCategory" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorCategory" InitialValue="-1" runat="server" ForeColor="Red" ControlToValidate="ddlSearchCategory" ErrorMessage="Category is required"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row form-group justify-content-center">
                <div class="col-sm-6 offset-sm-3">
                    <asp:Button ID="btnSearchItemSummary" CssClass="btn btn-success" runat="server" Text="Search" OnClick="btnSearchItemSummary_Click" />
                    <br />
                    <asp:Label ID="lblItemSummaryStatus" CssClass="text-success" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <div class="row justify-content-center">
            <div class="col-md-6 d-flex justify-content-center">
                <asp:GridView CssClass="table" ID="gridViewItemSummary" runat="server" CellPadding="15" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" AutoGenerateColumns="False">
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
                                <%#Eval("SL") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item">
                            <ItemTemplate>
                                <%#Eval("Item") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Company">
                            <ItemTemplate>
                                <%#Eval("Company") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category">
                            <ItemTemplate>
                                <%#Eval("Category") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Available Quantity">
                            <ItemTemplate>
                                <%#Eval("AvailableQuantity") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reorder Level">
                            <ItemTemplate>
                                <%#Eval("ReorderLevel") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
