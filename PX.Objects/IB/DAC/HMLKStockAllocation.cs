using System;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;

namespace PX.Objects.IB
{
	[Serializable]
	[PXCacheName("Stock Allocation")]
	public class HMLKStockAllocation : IBqlTable
	{
		#region Keys
		public class PK : PrimaryKeyOf<HMLKStockAllocation>.By<partNo, locationNo>
		{
			public static HMLKStockAllocation Find(PXGraph graph, int? partNo, int? locationNo) => FindBy(graph, partNo, locationNo);
		}
		#endregion

		#region WarehouseNo
		[PXDBInt(IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Warehouse")]
		[PXSelector(typeof(Search<HMLKWarehouse.warehouseNo>),
			typeof(HMLKWarehouse.warehouseCD),
			typeof(HMLKWarehouse.description),
			SubstituteKey = typeof(HMLKWarehouse.warehouseCD),
			DescriptionField = typeof(HMLKWarehouse.description))]
		public virtual int? WarehouseNo { get; set; }
		public abstract class warehouseNo : PX.Data.BQL.BqlInt.Field<warehouseNo> { }
		#endregion

		#region LocationNo
		[PXDBInt(IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Location")]
		[PXSelector(typeof(Search<HMLKInventoryLocation.locationNo>),
			typeof(HMLKInventoryLocation.locationCD),
			typeof(HMLKInventoryLocation.description),
			SubstituteKey = typeof(HMLKInventoryLocation.locationCD),
			DescriptionField = typeof(HMLKInventoryLocation.description))]
		public virtual int? LocationNo { get; set; }
		public abstract class locationNo : PX.Data.BQL.BqlInt.Field<locationNo> { }
		#endregion

		#region PartNo
		[PXDBInt(IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Part")]
		[PXSelector(typeof(Search<HMLKInventory.partNo, Where<HMLKInventory.inventoryType.IsEqual<stockInventoryType>>>),
			typeof(HMLKInventory.partCD),
			typeof(HMLKInventory.description),
			typeof(HMLKInventory.price),
			SubstituteKey = typeof(HMLKInventory.partCD),
			DescriptionField = typeof(HMLKInventory.description))]
		public virtual int? PartNo { get; set; }
		public abstract class partNo : PX.Data.BQL.BqlInt.Field<partNo> { }
		#endregion

		#region Qty
		[PXDBInt()]
		[PXDefault]
		[PXUIField(DisplayName = "Quantity")]
		public virtual int? Qty { get; set; }
		public abstract class qty : PX.Data.BQL.BqlInt.Field<qty> { }
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

		#region CreatedDateTime
		[PXDBCreatedDateTime()]
		public virtual DateTime? CreatedDateTime { get; set; }
		public abstract class createdDateTime : PX.Data.BQL.BqlDateTime.Field<createdDateTime> { }
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

		#region LastModifiedDateTime
		[PXDBLastModifiedDateTime()]
		public virtual DateTime? LastModifiedDateTime { get; set; }
		public abstract class lastModifiedDateTime : PX.Data.BQL.BqlDateTime.Field<lastModifiedDateTime> { }
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
