using System;
using PX.Data;
using PX.Data.BQL.Fluent;

namespace PX.Objects.IB
{
	[Serializable]
	[PXCacheName("BOM")]
	[PXPrimaryGraph(typeof(HMLKBOMMaint))]
	public class HMLKBOM : IBqlTable
	{
		#region ProductNo
		[PXDBInt(IsKey = true)]
		[PXDBDefault(typeof(HMLKInventory.partNo))]
		[PXUIField(DisplayName = "Manufactured Product")]
		[PXSelector(typeof(Search<HMLKInventory.partNo, Where<HMLKInventory.partTypeNo.IsEqual<manufacturedInventoryPartType>>>),
			typeof(HMLKInventory.partCD),
			typeof(HMLKInventory.description),
			typeof(HMLKInventory.price),
			SubstituteKey = typeof(HMLKInventory.partCD),
			DescriptionField = typeof(HMLKInventory.description))]
		public virtual int? ProductNo { get; set; }
		public abstract class productNo : PX.Data.BQL.BqlInt.Field<productNo> { }
		#endregion

		#region PartNo
		[PXDBInt(IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Component Part")]
		[PXSelector(typeof(Search<HMLKInventory.partNo, Where<HMLKInventory.partTypeNo.IsEqual<purchasedInventoryPartType>
															.And<HMLKInventory.inventoryType.IsEqual<stockInventoryType>>>>),
			typeof(HMLKInventory.partCD),
			typeof(HMLKInventory.description),
			typeof(HMLKInventory.price),
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
		[PXDBInt()]
		[PXUIField(DisplayName = "Quantity")]
		public virtual int? Qty { get; set; }
		public abstract class qty : PX.Data.BQL.BqlInt.Field<qty> { }
		#endregion

		#region TotalQty
		[PXInt]
		[PXUIField(DisplayName = "Total Quantity")]
		public virtual int? TotalQty { get; set; }
		public abstract class totalQty : PX.Data.BQL.BqlInt.Field<totalQty> { }
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
