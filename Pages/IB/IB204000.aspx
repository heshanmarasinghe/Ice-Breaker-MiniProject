<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true" ValidateRequest="false" 
    CodeFile="IB204000.aspx.cs" Inherits="Page_IB204000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="PX.Objects.IB.HMLKInventoryLocationMaint"
        PrimaryView="Locations">
        <CallbackCommands>
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="Locations" Width="100%" Height="100px" AllowAutoHide="false">
        <Template>
            <px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartRow="True" ControlSize="M" LabelsWidth="S" />
            <px:PXSelector runat="server" ID="PXSelector1" DataField="WarehouseNo" CommitChanges="True"></px:PXSelector>
            <px:PXSelector runat="server" ID="CstPXSelector2" DataField="LocationCD" CommitChanges="True"></px:PXSelector>
            <px:PXTextEdit runat="server" ID="PXTextEdit1" DataField="Address"></px:PXTextEdit>
            <px:PXLayoutRule ControlSize="M" LabelsWidth="S" runat="server" ID="CstPXLayoutRule4" StartColumn="True"></px:PXLayoutRule>
            <px:PXTextEdit runat="server" ID="PXTextEdit3" DataField="Description" Width="300"></px:PXTextEdit>
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" Width="100%" Height="150px" DataSourceID="ds" AllowAutoHide="false">
        <Items>
            <px:PXTabItem Text="Part Quantities">
                <Template>
                    <px:PXGrid SyncPosition="True" Width="100%" SkinID="Details" runat="server" ID="CstPXGrid5">
                        <Levels>
                            <px:PXGridLevel DataMember="PartStockItems">
                                <Columns>
                                    <px:PXGridColumn DataField="HMLKInventory__PartCD" Width="70"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="HMLKInventory__Description" Width="200"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Qty" Width="100"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="HMLKInventory__Price" Width="70"></px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                            <ActionBar>
                            <Actions>
                                <AddNew MenuVisible="False" ToolBarVisible="False" />
                                <Delete MenuVisible="False" ToolBarVisible="False" />
                            </Actions>
                        </ActionBar> 
                        <AutoSize Enabled="True"></AutoSize>
                        <Mode InitNewRow="True"></Mode>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
        </Items>
        <AutoSize Container="Window" Enabled="True" MinHeight="150" />
    </px:PXTab>
</asp:Content>
