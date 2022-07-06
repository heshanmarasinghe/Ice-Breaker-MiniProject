using System;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;

namespace PX.Objects.IB
{
	[PXCacheName("Inventory Part")]
	public class HMLKInventory : IBqlTable
	{
		#region Keys
		public class PK : PrimaryKeyOf<HMLKInventory>.By<partNo>
		{
			public static HMLKInventory Find(PXGraph graph, int? partNo) => FindBy(graph, partNo);
		}
		#endregion

		#region PartNo
		[PXDBIdentity]
		[PXSelector(typeof(Search<HMLKInventory.partNo>),
			typeof(HMLKInventory.partCD),
			typeof(HMLKInventory.description),
			SubstituteKey = typeof(HMLKInventory.partCD),
			DescriptionField = typeof(HMLKInventory.description))]
		public virtual int? PartNo { get; set; }
		public abstract class partNo : PX.Data.BQL.BqlInt.Field<partNo> { }
		#endregion

		#region PartCD
		[PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa")]
		[PXDefault]
		[PXUIField(DisplayName = "Part ID")]
		public virtual string PartCD { get; set; }
		public abstract class partCD : PX.Data.BQL.BqlString.Field<partCD> { }
		#endregion

		#region Description
		[PXDBString(50, IsUnicode = true, InputMask = "")]
		[PXDefault]
		[PXUIField(DisplayName = "Description")]
		public virtual string Description { get; set; }
		public abstract class description : PX.Data.BQL.BqlString.Field<description> { }
		#endregion

		#region Price
		[PXDBDecimal()]
		[PXDefault]
		[PXUIField(DisplayName = "Price Per Item")]
		public virtual Decimal? Price { get; set; }
		public abstract class price : PX.Data.BQL.BqlDecimal.Field<price> { }
		#endregion

		#region InventoryType
		[PXDBString(2, IsFixed = true)]
		[PXDefault(InventoryTypes.Stock)]
		[PXUIField(DisplayName = "Inventory Type")]
		[PXStringList(
				new string[]
				{
					InventoryTypes.Stock,
					InventoryTypes.NonStock,
				},
				new string[]
				{
					Messages.Stock,
					Messages.NonStock
				})]
		public virtual string InventoryType { get; set; }
		public abstract class inventoryType : PX.Data.BQL.BqlString.Field<inventoryType> { }
		#endregion

		#region PartTypeNo
		[PXDBInt(IsKey = true)]
		[PXUIField(DisplayName = "Part Type")]
		[PXSelector(typeof(Search<HMLKInventoryPartType.partTypeNo>),
			typeof(HMLKInventoryPartType.partTypeCD),
			typeof(HMLKInventoryPartType.description),
			SubstituteKey = typeof(HMLKInventoryPartType.partTypeCD),
			DescriptionField = typeof(HMLKInventoryPartType.description))]
		public virtual int? PartTypeNo { get; set; }
		public abstract class partTypeNo : PX.Data.BQL.BqlInt.Field<partTypeNo> { }
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
		public virtual Guid? NoteID { get; set; }
		public abstract class noteID : PX.Data.BQL.BqlGuid.Field<noteID> { }
		#endregion
	}

	[PXCacheName("Inventory")]
	[PXPrimaryGraph(typeof(HMLKBOMMaint))]
	public class HMLKBOMInventory : HMLKInventory
	{
		#region PartNo
		[PXDBInt(IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Manufactured Product")]
		[PXSelector(typeof(Search<HMLKInventory.partNo, Where<HMLKInventory.partTypeNo.IsEqual<manufacturedInventoryPartType>>>),
			typeof(HMLKInventory.partCD),
			typeof(HMLKInventory.description),
			SubstituteKey = typeof(HMLKInventory.partCD),
			DescriptionField = typeof(HMLKInventory.description))]
		public new virtual int? PartNo { get; set; }
		public new abstract class partNo : PX.Data.BQL.BqlInt.Field<partNo> { }
		#endregion
	}
}
