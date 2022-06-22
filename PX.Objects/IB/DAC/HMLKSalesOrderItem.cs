using System;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;

namespace PX.Objects.IB
{
	[PXCacheName("Sales Order Items")]
	public class HMLKSalesOrderItem : IBqlTable
	{
		#region CustomerOrderNo
		[PXDBString(15, IsKey = true)]
		[PXDBDefault(typeof(HMLKSalesOrder.customerOrderNo))]
		[PXUIField(DisplayName = "Customer Order No", Visibility = PXUIVisibility.SelectorVisible)]
		[PXParent(typeof(SelectFrom<HMLKSalesOrder>.
						 Where<HMLKSalesOrder.customerOrderNo.
						 IsEqual<HMLKSalesOrderItem.customerOrderNo.FromCurrent>>))]
		public virtual string CustomerOrderNo { get; set; }
		public abstract class customerOrderNo : PX.Data.BQL.BqlString.Field<customerOrderNo> { }
		#endregion

		#region PartNo
		[PXDBInt(IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Part No")]
		[PXSelector(typeof(Search<HMLKInventory.partNo, Where<HMLKInventory.inventoryType.IsEqual<stockInventoryType>>>),
			typeof(HMLKInventory.partCD),
			typeof(HMLKInventory.description),
			SubstituteKey = typeof(HMLKInventory.partCD),
			DescriptionField = typeof(HMLKInventory.description))]
		public virtual int? PartNo { get; set; }
		public abstract class partNo : PX.Data.BQL.BqlInt.Field<partNo> { }
		#endregion

		#region Description
		[PXDBString(50, IsUnicode = true, InputMask = "")]
		[PXDefault]
		[PXUIField(DisplayName = "Description")]
		public virtual string Description { get; set; }
		public abstract class description : PX.Data.BQL.BqlString.Field<description> { }
		#endregion

		#region Qty
		[PXDBDecimal()]
		[PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Quantity")]
		public virtual Decimal? Qty { get; set; }
		public abstract class qty : PX.Data.BQL.BqlDecimal.Field<qty> { }
		#endregion

		#region Price
		[PXDBDecimal()]
		[PXDefault]
		[PXUIField(DisplayName = "Price")]
		public virtual Decimal? Price { get; set; }
		public abstract class price : PX.Data.BQL.BqlDecimal.Field<price> { }
		#endregion

		#region TotalPrice
		[PXDBDecimal()]
		[PXUIField(DisplayName = "Total Price", Enabled = false)]
		public virtual Decimal? TotalPrice { get; set; }
		public abstract class totalPrice : PX.Data.BQL.BqlDecimal.Field<totalPrice> { }
		#endregion

		#region Status
		[PXDBString(3, IsFixed = true)]
		[PXDefault(SalesOrderItemStatusConstants.Required)]
		[PXUIField(DisplayName = "Status", Enabled = false)]
		[PXStringList(
				new string[]
				{
					SalesOrderItemStatusConstants.Required,
					SalesOrderItemStatusConstants.Delivered,
					SalesOrderItemStatusConstants.Cancelled,
				},
				new string[]
				{
					Messages.Required,
					Messages.Delivered,
					Messages.Cancelled
				})]
		public virtual string Status { get; set; }
		public abstract class status : PX.Data.BQL.BqlString.Field<status> { }
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

	[PXCacheName("Sales Order Stock Items")]
	public class HMLKSalesOrderStockItem : HMLKSalesOrderItem
	{
		#region Keys
		public class PK : PrimaryKeyOf<HMLKSalesOrderStockItem>.By<partNo, customerOrderNo>
		{
			public static HMLKSalesOrderStockItem Find(PXGraph graph, int? partNo, string customerOrderNo) =>
				FindBy(graph, partNo, customerOrderNo);
		}
		#endregion

		#region PartNo
		[PXDBInt(IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Part ID")]
		[PXSelector(typeof(Search<HMLKInventory.partNo, Where<HMLKInventory.inventoryType.IsEqual<stockInventoryType>>>),
			typeof(HMLKInventory.partCD),
			typeof(HMLKInventory.description),
			SubstituteKey = typeof(HMLKInventory.partCD),
			DescriptionField = typeof(HMLKInventory.description))]
		public new virtual int? PartNo { get; set; }
		public new abstract class partNo : PX.Data.BQL.BqlInt.Field<partNo> { }
		#endregion

		#region TotalPrice
		[PXDBDecimal()]
		[PXUIField(DisplayName = "Total Price", Enabled = false)]
		[PXFormula(
			 typeof(Mult<HMLKSalesOrderStockItem.qty, HMLKSalesOrderStockItem.price>),
			 typeof(SumCalc<HMLKSalesOrder.orderTotalStock>))]
		public new virtual Decimal? TotalPrice { get; set; }
		public new abstract class totalPrice : PX.Data.BQL.BqlDecimal.Field<totalPrice> { }
		#endregion

		#region Selected
		public abstract class selected : PX.Data.BQL.BqlBool.Field<selected> { }
		[PXBool]
		[PXUIField(DisplayName = "Selected")]
		public virtual bool? Selected { get; set; }
		#endregion
	}

	[PXCacheName("Sales Order Non Stock Items")]
	public class HMLKSalesOrderNonStockItem : HMLKSalesOrderItem
	{
		#region PartNo
		[PXDBInt(IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Part ID")]
		[PXSelector(typeof(Search<HMLKInventory.partNo, Where<HMLKInventory.inventoryType.IsEqual<nonStockInventoryType>>>),
			typeof(HMLKInventory.partCD),
			typeof(HMLKInventory.description),
			SubstituteKey = typeof(HMLKInventory.partCD),
			DescriptionField = typeof(HMLKInventory.description))]
		public new virtual int? PartNo { get; set; }
		public new abstract class partNo : PX.Data.BQL.BqlInt.Field<partNo> { }
		#endregion

		#region TotalPrice
		[PXDBDecimal()]
		[PXUIField(DisplayName = "Total Price", Enabled = false)]
		[PXFormula(
			 typeof(Mult<HMLKSalesOrderNonStockItem.qty, HMLKSalesOrderNonStockItem.price>),
			 typeof(SumCalc<HMLKSalesOrder.orderTotalNonStock>))]
		public new virtual Decimal? TotalPrice { get; set; }
		public new abstract class totalPrice : PX.Data.BQL.BqlDecimal.Field<totalPrice> { }
		#endregion

		#region Status
		[PXDBString(3, IsFixed = true)]
		[PXDefault(SalesOrderItemStatusConstants.Delivered)]
		[PXUIField(DisplayName = "Status", Enabled = false)]
		[PXStringList(
				new string[]
				{
					SalesOrderItemStatusConstants.Required,
					SalesOrderItemStatusConstants.Delivered,
					SalesOrderItemStatusConstants.Cancelled,
				},
				new string[]
				{
					Messages.Required,
					Messages.Delivered,
					Messages.Cancelled
				})]
		public new virtual string Status { get; set; }
		public new abstract class status : PX.Data.BQL.BqlString.Field<status> { }
		#endregion
	}
}
