<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/FormTab.master" ValidateRequest="false" 
    CodeFile="IB201000.aspx.cs" Inherits="Page_IB201000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="PX.Objects.IB.HMLKWarehouseMaint" PrimaryView="Warehouses">
        <CallbackCommands>
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="Warehouses" Width="100%" Height="100px" AllowAutoHide="false">
        <Template>
            <px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartRow="True" ControlSize="M" LabelsWidth="S" />
            <px:PXTextEdit runat="server" ID="PXTextEdit2" DataField="WarehouseCD"></px:PXTextEdit>
            <px:PXTextEdit runat="server" ID="PXTextEdit3" DataField="Description"></px:PXTextEdit>
            <px:PXLayoutRule ControlSize="M" LabelsWidth="S" runat="server" ID="CstPXLayoutRule4" StartColumn="True"></px:PXLayoutRule>
            <px:PXTextEdit runat="server" ID="PXTextEdit1" DataField="Address"></px:PXTextEdit>
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" Width="100%" Height="150px" DataSourceID="ds" AllowAutoHide="false">
        <Items>
            <px:PXTabItem Text="Locations">
                <Template>
                    <px:PXGrid SyncPosition="True" Width="100%" SkinID="Details" runat="server" ID="CstPXGrid5">
                        <Levels>
                            <px:PXGridLevel DataMember="WarehouseLocations">
                                <Columns>
                                    <px:PXGridColumn DataField="LocationCD" Width="70"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Address" Width="70"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Description" Width="280"></px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True"></AutoSize>
                        <Mode InitNewRow="False"></Mode>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
        </Items>
        <AutoSize Container="Window" Enabled="True" MinHeight="150" />
    </px:PXTab>
</asp:Content>
