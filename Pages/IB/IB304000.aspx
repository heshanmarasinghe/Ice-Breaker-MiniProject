<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormView.master" AutoEventWireup="true" ValidateRequest="false" 
    CodeFile="IB304000.aspx.cs" Inherits="Page_IB304000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormView.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="PXDataSource1" runat="server" Visible="True" Width="100%"
        TypeName="PX.Objects.IB.HMLKProductionStockAllocationEntry"
        PrimaryView="StockItem">
        <CallbackCommands>
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="PXDataSource1" DataMember="StockItem" Width="100%" AllowAutoHide="false">
        <Template>
            <px:PXLayoutRule LabelsWidth="S" ControlSize="M" runat="server" ID="PXLayoutRule1" StartRow="True"></px:PXLayoutRule>
            <px:PXSelector runat="server" ID="CstPXSelector1" DataField="WarehouseNo" CommitChanges="true" AutoRefresh="true" Width="300px"></px:PXSelector>
            <px:PXSelector runat="server" ID="CstPXSelector2" DataField="LocationNo" CommitChanges="true"  AutoRefresh="true" Width="300px"></px:PXSelector>
            <px:PXSelector runat="server" ID="CstPXSelector3" DataField="PartNo" CommitChanges="true"  AutoRefresh="true" Width="300px"></px:PXSelector>
            <px:PXTextEdit runat="server" ID="CstPXTextEdit2" DataField="Qty" ></px:PXTextEdit>
        </Template>
        <AutoSize Container="Window" Enabled="True" MinHeight="200"></AutoSize>
    </px:PXFormView>
</asp:Content>




