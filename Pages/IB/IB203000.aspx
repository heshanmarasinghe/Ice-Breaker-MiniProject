<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" 
    CodeFile="IB203000.aspx.cs" Inherits="Page_IB203000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="PX.Objects.IB.HMLKInventoryMaint"
        PrimaryView="Inventory">
        <CallbackCommands>
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
    <px:PXGrid runat="server" Height="150px" SkinID="Primary" Width="100%" ID="grid" AllowAutoHide="false" DataSourceID="ds">
        <Levels>
            <px:PXGridLevel DataMember="Inventory">
                <Columns>
                    <px:PXGridColumn DataField="PartCD" Width="140"></px:PXGridColumn>
                    <px:PXGridColumn DataField="Description" Width="180"></px:PXGridColumn>
                    <px:PXGridColumn DataField="Price" Width="100"></px:PXGridColumn>
                    <px:PXGridColumn DataField="InventoryType" Width="70"></px:PXGridColumn>
                    <px:PXGridColumn DataField="PartTypeNo" Width="70"></px:PXGridColumn>
                </Columns>
            </px:PXGridLevel>
        </Levels>
        <AutoSize Enabled="True" Container="Window" MinHeight="150"></AutoSize>
    </px:PXGrid>
</asp:Content>
