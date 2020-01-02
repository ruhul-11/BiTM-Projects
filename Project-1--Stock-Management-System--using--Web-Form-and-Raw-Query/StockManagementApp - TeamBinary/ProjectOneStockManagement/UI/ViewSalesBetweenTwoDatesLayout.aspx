<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewSalesBetweenTwoDatesLayout.aspx.cs" Inherits="ProjectOneStockManagement.UI.ViewSalesBetweenTwoDatesLayout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="text-center bg-success custom-h2">View Sales Between Two Dates</h2>
    <div class="container">
        <br />
        <br />
        <div>
            <div class="row form-group justify-content-center">
                <asp:Label ID="Label3" CssClass="col-sm-3 col-form-label text-right" runat="server" Text="From Date :"></asp:Label>
                <div class="col-sm-6 d-flex">
                    <asp:TextBox ID="textBoxFromDate" Type="Date" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorFromDate" ForeColor="Red" ControlToValidate="textBoxFromDate" runat="server" ErrorMessage="From Date is required"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row form-group justify-content-center">
                <asp:Label ID="Label4" CssClass="col-sm-3 col-form-label text-right" runat="server" Text="To Date :"></asp:Label>
                <div class="col-sm-6 d-flex">
                    <asp:TextBox ID="textBoxToDate" Type="Date" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorToDate" ForeColor="Red" ControlToValidate="textBoxToDate" runat="server" ErrorMessage="To Date is required"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row form-group justify-content-center">
                <div class="col-sm-6 offset-sm-3">
                    <asp:Button ID="btnSearchSellStatus" CssClass="btn btn-success" runat="server" Text="Search" OnClick="btnSearchSellStatus_Click" />
                    <br />
                    <asp:Label ID="lblSearchStatus" CssClass="text-success" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <div class="row justify-content-center">
            <div class="col-md-6 d-flex justify-content-center">
                <asp:GridView CssClass="table" ID="gridViewSellStatus" runat="server" Width="600px" CellPadding="15" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" AutoGenerateColumns="False">
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
                        <asp:TemplateField HeaderText="Sell Quantity">
                            <ItemTemplate>
                                <%#Eval("Quantity") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
