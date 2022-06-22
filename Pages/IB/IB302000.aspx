<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true" ValidateRequest="false"
    CodeFile="IB302000.aspx.cs" Inherits="Page_IB302000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="PX.Objects.IB.HMLKProductionOrderEntry"
        PrimaryView="ProductionOrder">
        <CallbackCommands>
            <px:PXDSCallbackCommand Name="Save" />
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="ProductionOrder" Width="100%" AllowAutoHide="false">
        <Template>
            <px:PXLayoutRule runat="server" ID="PXLayoutRule2"
                StartColumn="True" ControlSize="SM" LabelsWidth="S">
            </px:PXLayoutRule>
            <px:PXTextEdit runat="server" ID="PXSelector1" DataField="OrderNo"></px:PXTextEdit>
            <px:PXDateTimeEdit runat="server" ID="CstPXDateTimeEdit6" DataField="OrderDate" />
            <px:PXDateTimeEdit runat="server" ID="CstPXDateTimeEdit7" DataField="RequiredDate" />
            <px:PXDropDown runat="server" ID="CstPXDropDown14" DataField="Status" />

            <px:PXLayoutRule runat="server" ID="CstPXLayoutRule16"
                StartColumn="True" ControlSize="XM" LabelsWidth="S">
            </px:PXLayoutRule>
            <px:PXSelector CommitChanges="True" runat="server" ID="CstPXSelector13" DataField="PartNo"></px:PXSelector>
            <px:PXNumberEdit CommitChanges="True" runat="server" ID="PXNumberEdit1" DataField="Qty"></px:PXNumberEdit>
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" Width="100%" Height="150px" DataSourceID="ds" AllowAutoHide="false">
        <Items>
            <px:PXTabItem Text="Bill of Materials">
                <Template>
                    <px:PXGrid SyncPosition="True" Width="100%" SkinID="Details" runat="server" ID="CstPXGrid5">
                        <Levels>
                            <px:PXGridLevel DataMember="BOM">
                                <Columns>
                                    <px:PXGridColumn DataField="PartNo" Width="70"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Description" Width="200"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Qty" Width="100"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="TotalQty" Width="150"></px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True"></AutoSize>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
        </Items>
        <AutoSize Container="Window" Enabled="True" MinHeight="150" />
    </px:PXTab>
</asp:Content>
