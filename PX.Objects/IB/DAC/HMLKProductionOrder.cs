using System;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;

namespace PX.Objects.IB
{
	[Serializable]
	[PXCacheName("Production Order")]
	public class HMLKProductionOrder : IBqlTable
	{
		#region Keys
		public class PK : PrimaryKeyOf<HMLKProductionOrder>.By<salesOrderNo>
		{
			public static HMLKProductionOrder FindSalesOrder(PXGraph graph, string salesOrderNo) => FindBy(graph, salesOrderNo);
		}
		public class UK : PrimaryKeyOf<HMLKProductionOrder>.By<salesOrderNo, partNo>
		{
			public static HMLKProductionOrder Find(PXGraph graph, string salesOrderNo, int? partNo) => FindBy(graph, salesOrderNo, partNo);
		}
		#endregion

		#region OrderNo
		[PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
		[PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
		[PXUIField(DisplayName = "Order No", Visibility = PXUIVisibility.SelectorVisible)]
		[AutoNumber(typeof(HMLKSetup.productionNumberingID), typeof(HMLKProductionOrder.orderDate))]
		public virtual string OrderNo { get; set; }
		public abstract class orderNo : PX.Data.BQL.BqlString.Field<orderNo> { }
		#endregion

		#region PartNo
		[PXDBInt(IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Part No")]
		[PXSelector(typeof(Search<HMLKInventory.partNo, Where<HMLKInventory.partTypeNo.IsEqual<manufacturedInventoryPartType>>>),
			typeof(HMLKInventory.partCD),
			typeof(HMLKInventory.description),
			SubstituteKey = typeof(HMLKInventory.partCD),
			DescriptionField = typeof(HMLKInventory.description))]
		public virtual int? PartNo { get; set; }
		public abstract class partNo : PX.Data.BQL.BqlInt.Field<partNo> { }
		#endregion

		#region Qty
		[PXDBInt()]
		[PXUIField(DisplayName = "Lot Size")]
		public virtual int? Qty { get; set; }
		public abstract class qty : PX.Data.BQL.BqlInt.Field<qty> { }
		#endregion

		#region OrderDate
		[PXDBDate()]
		[PXDefault(typeof(AccessInfo.businessDate))]
		[PXUIField(DisplayName = "Order Date")]
		public virtual DateTime? OrderDate { get; set; }
		public abstract class orderDate : PX.Data.BQL.BqlDateTime.Field<orderDate> { }
		#endregion

		#region RequiredDate
		[PXDBDate()]
		[PXUIField(DisplayName = "Required Date")]
		public virtual DateTime? RequiredDate { get; set; }
		public abstract class requiredDate : PX.Data.BQL.BqlDateTime.Field<requiredDate> { }
		#endregion

		#region Status
		[PXDBString(3, IsFixed = true)]
		[PXDefault(ProductionOrderStatusConstants.Released)]
		[PXUIField(DisplayName = "Status", Enabled = false)]
		[PXStringList(
				new string[]
				{
					ProductionOrderStatusConstants.Released,
					ProductionOrderStatusConstants.Reserved,
					ProductionOrderStatusConstants.Closed,
					ProductionOrderStatusConstants.Cancelled,
				},
				new string[]
				{
					Messages.Released,
					Messages.Reserved,
					Messages.Closed,
					Messages.Cancelled
				})]
		public virtual string Status { get; set; }
		public abstract class status : PX.Data.BQL.BqlString.Field<status> { }
		#endregion

		#region SalesOrderNo
		[PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
		[PXUIField(DisplayName = "Sales Order No")]
		public virtual string SalesOrderNo { get; set; }
		public abstract class salesOrderNo : PX.Data.BQL.BqlString.Field<salesOrderNo> { }
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
