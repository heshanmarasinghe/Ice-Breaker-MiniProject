using System;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CS;

namespace PX.Objects.IB
{
	[Serializable]
	[PXCacheName("Sales Order")]
	public class HMLKSalesOrder : IBqlTable
	{
		#region Keys
		public class PK : PrimaryKeyOf<HMLKSalesOrder>.By<customerOrderNo>
		{
			public static HMLKSalesOrder Find(PXGraph graph, string customerOrderNo) => FindBy(graph, customerOrderNo);
		}
		#endregion

		#region CustomerOrderNo
		[PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
		[PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
		[PXUIField(DisplayName = "Customer Order No", Visibility = PXUIVisibility.SelectorVisible)]
		[AutoNumber(typeof(HMLKSetup.salesNumberingID), typeof(HMLKSalesOrder.orderDate))]
		[PXSelector(typeof(Search<HMLKSalesOrder.customerOrderNo>),
			typeof(HMLKSalesOrder.customerName),
			typeof(HMLKSalesOrder.orderDate))]
		public virtual string CustomerOrderNo { get; set; }
		public abstract class customerOrderNo : PX.Data.BQL.BqlString.Field<customerOrderNo> { }
		#endregion

		#region CustomerID
		[PXDefault]
		[CustomerActive(DisplayName = "Customer ID", DescriptionField = typeof(Customer.acctName))]
		public virtual int? CustomerID { get; set; }
		public abstract class customerID : PX.Data.BQL.BqlInt.Field<customerID> { }
		#endregion

		#region CustomerName
		[PXDBString(50, IsUnicode = true, InputMask = "")]
		[PXDefault]
		[PXUIField(DisplayName = "Customer Name")]
		public virtual string CustomerName { get; set; }
		public abstract class customerName : PX.Data.BQL.BqlString.Field<customerName> { }
		#endregion

		#region CustomerDeliveryAddress
		[PXDBString(50, IsUnicode = true, InputMask = "")]
		[PXDefault]
		[PXUIField(DisplayName = "Customer Delivery Address")]
		public virtual string CustomerDeliveryAddress { get; set; }
		public abstract class customerDeliveryAddress : PX.Data.BQL.BqlString.Field<customerDeliveryAddress> { }
		#endregion

		#region OrderDate
		[PXDBDate()]
		[PXDefault(typeof(AccessInfo.businessDate))]
		[PXUIField(DisplayName = "Order Date")]
		public virtual DateTime? OrderDate { get; set; }
		public abstract class orderDate : PX.Data.BQL.BqlDateTime.Field<orderDate> { }
		#endregion

		#region OrderTotalStock
		[PXDecimal()]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Decimal? OrderTotalStock { get; set; }
		public abstract class orderTotalStock : PX.Data.BQL.BqlDecimal.Field<orderTotalStock> { }
		#endregion

		#region OrderTotalNonStock
		[PXDecimal()]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual Decimal? OrderTotalNonStock { get; set; }
		public abstract class orderTotalNonStock : PX.Data.BQL.BqlDecimal.Field<orderTotalNonStock> { }
		#endregion

		#region OrderTotal
		[PXDBDecimal()]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Order Total", Enabled = false)]
		[PXFormula(typeof(Add<HMLKSalesOrder.orderTotalNonStock, HMLKSalesOrder.orderTotalStock>))]
		public virtual Decimal? OrderTotal { get; set; }
		public abstract class orderTotal : PX.Data.BQL.BqlDecimal.Field<orderTotal> { }
		#endregion

		#region Status
		[PXDBString(3, IsFixed = true)]
		[PXDefault(SalesOrderStatusConstants.Planned)]
		[PXUIField(DisplayName = "Status")]
		[PXStringList(
				new string[]
				{
					SalesOrderStatusConstants.Planned,
					SalesOrderStatusConstants.Released,
					SalesOrderStatusConstants.Closed,
					SalesOrderStatusConstants.Cancelled,
				},
				new string[]
				{
					Messages.Planned,
					Messages.Released,
					Messages.Closed,
					Messages.Cancelled
				})]
		public virtual string Status { get; set; }
		public abstract class status : PX.Data.BQL.BqlString.Field<status> { }
		#endregion

		#region CreatePOOrder
		[PXBool()]
		[PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Create Production Order")]
		public virtual bool? IsCreatePOOrder { get; set; }
		public abstract class isCreatePOOrder : PX.Data.BQL.BqlBool.Field<isCreatePOOrder> { }
		#endregion

		#region CreatedDateTime
		[PXDBCreatedDateTime()]
		public virtual DateTime? CreatedDateTime { get; set; }
		public abstract class createdDateTime : PX.Data.BQL.BqlDateTime.Field<createdDateTime> { }
		#endregion

		#region CreatedByID
		[PXDBCreatedByID()]
		public virtual Guid? CreatedByID { get; set; }
		public abstract class createdByID : PX.Data.BQL.BqlGuid.Field<createdByID> { }
		#endregion

		#region CreatedByScreenID
		[PXDBCreatedByScreenID()]
		public virtual string CreatedByScreenID { get; set; }
		public abstract class createdByScreenID : PX.Data.BQL.BqlString.Field<createdByScreenID> { }
		#endregion

		#region LastModifiedDateTime
		[PXDBLastModifiedDateTime()]
		public virtual DateTime? LastModifiedDateTime { get; set; }
		public abstract class lastModifiedDateTime : PX.Data.BQL.BqlDateTime.Field<lastModifiedDateTime> { }
		#endregion

		#region LastModifiedByID
		[PXDBLastModifiedByID()]
		public virtual Guid? LastModifiedByID { get; set; }
		public abstract class lastModifiedByID : PX.Data.BQL.BqlGuid.Field<lastModifiedByID> { }
		#endregion

		#region LastModifiedByScreenID
		[PXDBLastModifiedByScreenID()]
		public virtual string LastModifiedByScreenID { get; set; }
		public abstract class lastModifiedByScreenID : PX.Data.BQL.BqlString.Field<lastModifiedByScreenID> { }
		#endregion

		#region Tstamp
		[PXDBTimestamp()]
		[PXUIField(DisplayName = "Tstamp")]
		public virtual byte[] Tstamp { get; set; }
		public abstract class tstamp : PX.Data.BQL.BqlByteArray.Field<tstamp> { }
		#endregion

		#region Noteid
		[PXNote()]
		public virtual Guid? Noteid { get; set; }
		public abstract class noteid : PX.Data.BQL.BqlGuid.Field<noteid> { }
		#endregion
	}
}
