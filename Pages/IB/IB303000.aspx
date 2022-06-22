<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true" ValidateRequest="false"
    CodeFile="IB303000.aspx.cs" Inherits="Page_IB303000" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" runat="Server">
    <px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="PX.Objects.IB.HMLKSalesOrderEntry"
        PrimaryView="SalesOrder">
        <CallbackCommands>
            <px:PXDSCallbackCommand Name="Save" />
        </CallbackCommands>
    </px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" runat="Server">
    <px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="SalesOrder" Width="100%" AllowAutoHide="false">
        <Template>
            <px:PXLayoutRule runat="server" ID="PXLayoutRule2"
                StartColumn="True" ControlSize="M" LabelsWidth="M">
            </px:PXLayoutRule>
            <px:PXSelector runat="server" ID="PXSelector1" DataField="CustomerOrderNo"></px:PXSelector>
            <px:PXSegmentMask runat="server" CommitChanges="true" ID="CstPXSegmentMask4" DataField="CustomerID"></px:PXSegmentMask>
            <px:PXTextEdit runat="server" ID="PXTextEdit1" DataField="CustomerName"></px:PXTextEdit>

            <px:PXLayoutRule runat="server" ID="PXLayoutRule3" ColumnSpan="2" />
            <px:PXTextEdit runat="server" ID="CstPXTextEdit8" DataField="CustomerDeliveryAddress" Width="500" />

            <px:PXLayoutRule runat="server" ID="CstPXLayoutRule16"
                StartColumn="True" ControlSize="S" LabelsWidth="S">
            </px:PXLayoutRule>
            <px:PXDateTimeEdit runat="server" ID="CstPXDateTimeEdit6" DataField="OrderDate" />
            <px:PXDropDown runat="server" ID="CstPXDropDown14" DataField="Status" />
            <px:PXCheckBox ID="chkChkbox1" runat="server" DataField="IsCreatePOOrder" />

            <px:PXLayoutRule runat="server" ID="PXLayoutRule1"
                StartColumn="True" ControlSize="XM" LabelsWidth="XS">
            </px:PXLayoutRule>
            <px:PXNumberEdit CommitChanges="True" runat="server" ID="PXNumberEdit1" DataField="OrderTotal" />
        </Template>
    </px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
    <px:PXTab ID="tab" runat="server" Width="100%" Height="150px" DataSourceID="ds" AllowAutoHide="false">
        <Items>
            <px:PXTabItem Text="Part Details">
                <Template>
                    <px:PXGrid SyncPosition="True" Width="100%" SkinID="Details" runat="server" ID="CstPXGrid6">
                        <Levels>
                            <px:PXGridLevel DataMember="SalesOrderPartItems">
                                <Columns>
                                    <px:PXGridColumn Type="CheckBox" AllowCheckAll="True" TextAlign="Center" DataField="Selected" Width="60"></px:PXGridColumn>
                                    <px:PXGridColumn CommitChanges="True" DataField="PartNo" Width="200"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Description" Width="200"></px:PXGridColumn>
                                    <px:PXGridColumn CommitChanges="True" DataField="Qty" Width="100"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Price" Width="200"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="TotalPrice" Width="200"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Status" Width="100"></px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <ActionBar>
                            <CustomItems>
                                <px:PXToolBarButton Text="DeliverOrder">
                                    <AutoCallBack Command="DeliverOrder" Target="ds" />
                                </px:PXToolBarButton>
                                <px:PXToolBarButton Text="CancelOrderItem">
                                    <AutoCallBack Command="CancelOrderItem" Target="ds" />
                                </px:PXToolBarButton>
                            </CustomItems>
                        </ActionBar>
                        <AutoSize Enabled="True"></AutoSize>
                        <Mode InitNewRow="True"></Mode>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="No Part Details">
                <Template>
                    <px:PXGrid SyncPosition="True" Width="100%" SkinID="Details" runat="server" ID="CstPXGrid5">
                        <Levels>
                            <px:PXGridLevel DataMember="SalesOrderNoPartItems">
                                <Columns>
                                    <px:PXGridColumn CommitChanges="True" DataField="PartNo" Width="200"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Description" Width="200"></px:PXGridColumn>
                                    <px:PXGridColumn CommitChanges="True" DataField="Qty" Width="100"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Price" Width="200"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="TotalPrice" Width="200"></px:PXGridColumn>
                                    <px:PXGridColumn DataField="Status" Width="100"></px:PXGridColumn>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Enabled="True"></AutoSize>
                        <Mode InitNewRow="True"></Mode>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
        </Items>
        <AutoSize Container="Window" Enabled="True" MinHeight="150" />
    </px:PXTab>
</asp:Content>
