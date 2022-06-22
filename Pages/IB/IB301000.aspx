<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormView.master" AutoEventWireup="true" ValidateRequest="false"
    CodeFile="IB301000.aspx.cs" Inherits="Page_IB301000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="PX.Objects.IB.HMLKStockAllocationEntry"
        PrimaryView="StockItem">
        <CallbackCommands>
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="StockItem" Width="100%" AllowAutoHide="false" SyncPosition="true">
        <Template>
            <px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartRow="True" ControlSize="M" LabelsWidth="S" />
            <px:PXSelector runat="server" CommitChanges="true" ID="CstPXSelector3" DataField="WarehouseNo" Width="300px"></px:PXSelector>
            <px:PXSelector AutoRefresh="true" runat="server" CommitChanges="true" ID="CstPXSelector1" DataField="LocationNo" Width="300px"></px:PXSelector>
            <px:PXSelector runat="server" CommitChanges="true" ID="CstPXSelector2" DataField="PartNo" Width="300px"></px:PXSelector>
            <px:PXNumberEdit runat="server" CommitChanges="true" ID="PXNumberEdit1" DataField="Qty"></px:PXNumberEdit>
        </Template>
        <AutoSize Container="Window" Enabled="True" MinHeight="100" />
    </px:PXFormView>
</asp:Content>

